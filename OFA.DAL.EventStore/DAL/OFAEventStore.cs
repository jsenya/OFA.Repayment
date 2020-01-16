using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using EventStore.ClientAPI.Projections;
using OFA.Common;
using OFA.Common.Messages.Events;
using OFA.DAL.EventStore.DAL.IDAL;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static OFA.Common.Helpers;
using System.Net;
using EventStore.ClientAPI.Common.Log;
using System.Linq;

namespace OFA.DAL.EventStore.DAL
{
    public class OFAEventStore : IOFAEventStore
    {
        private readonly IEventStoreConnection _connection;
        private readonly ProjectionsManager _projectionsManager;
        public bool IsOpen { get; private set; }
        private readonly Serilog.ILogger _logger;
        public string ConnectionName { get; set; }
        private readonly string _username;
        private readonly string _password;
        public OFAEventStore(string userName, string password, string uri, string port, string connectionName)
        {
            _username = userName;
            _password = password;
            ConnectionName = connectionName;
            _logger = Log.ForContext<OFAEventStore>();
            _projectionsManager = new ProjectionsManager(new ConsoleLogger(), new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2113), TimeSpan.FromMilliseconds(5000));
            _connection = EventStoreConnection.Create(new Uri($"tcp://{userName}:{password}@{uri}:{port}"), ConnectionName);
            _connection.Connected += (s, e) =>
            {
                _logger.Information("connection to event store established.");
                IsOpen = true;
            };
            _connection.Closed += (s, e) =>
            {
                _logger.Information("connection to event store is closed.");
                IsOpen = false;
            };
        }

        public async Task<bool> AppendEventAsync(string stream, IEvent @event)
        {
            try
            {
                if (!IsOpen) await OpenConnectionAsync();

                await _connection.AppendToStreamAsync(stream, ExpectedVersion.Any, @event.Payload());

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ExceptionTemplate);
            }

            return false;
        }

        public async Task<IEnumerable<TEvent>> ReadAllEventsASync<TEvent>(string streamName, long page = 0, 
            int count = 50, string username = null, string password = null) where TEvent : IEvent
        {
            if (!IsOpen) await OpenConnectionAsync();

            List<TEvent> _events = new List<TEvent>();
            try
            {
                UserCredentials _creds = new UserCredentials(username ?? _username, password ?? _password);
                StreamEventsSlice streamSlice = await _connection.ReadStreamEventsForwardAsync(streamName, page, count, false, _creds);

                if(streamSlice.Events.Length > 0)
                {
                    foreach(var @evt in streamSlice.Events)
                    {
                       _events.Add(evt.Event.Data.FromBytes<TEvent>());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ExceptionTemplate);
            }

            return _events;
        }

        public async Task<bool> SubscribeToStreamAsync(string streamName, string groupName, string username = null, string password = null)
        {
            try
            {
                if (!IsOpen) await OpenConnectionAsync();
                PersistentSubscriptionSettings subSettings = PersistentSubscriptionSettings
                    .Create().DoNotResolveLinkTos().StartFromBeginning();
                UserCredentials _creds = new UserCredentials(username ?? _username, password ?? _password);

                await _connection.CreatePersistentSubscriptionAsync(streamName, groupName, subSettings, _creds);

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ExceptionTemplate);
            }

            return false;
        }

        public async Task OpenConnectionAsync()
        {
            try
            {
                if(!IsOpen)
                    await _connection.ConnectAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ExceptionTemplate);
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (IsOpen)
                    _connection.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ExceptionTemplate);
            }
        }

        public async Task<bool> SetListenerAsync(string streamName, string groupName, Action<EventStorePersistentSubscriptionBase, ResolvedEvent> @handler, string username = null, string password = null)
        {
            try
            {
                UserCredentials _creds = new UserCredentials(username ?? _username, password ?? _password);
                await _connection.ConnectToPersistentSubscriptionAsync(streamName, groupName, @handler, (sub, reason, ex) => { }, _creds);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ExceptionTemplate);
            }

            return false;
        }

        public async Task<string> GetProjectionResultAsync(string projectionName)
        {
            try
            {
                UserCredentials _creds = new UserCredentials(_username, _password);
                var result = await _projectionsManager.GetResultAsync(projectionName, _creds);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task CreateProjectionAsync(string name, string query)
        {
            try
            {
                UserCredentials _creds = new UserCredentials(_username, _password);
                var projections = await _projectionsManager.ListContinuousAsync(_creds);
                if(!projections.Where(q=> q.Name.Equals(name)).Any())
                    _projectionsManager.CreateContinuousAsync(name, query, _creds);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
