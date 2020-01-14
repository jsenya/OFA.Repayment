using OFA.Repayment.WM.DAL.IDAL;
using OFA.Repayment.WM.Messages.Events;
using System;
using System.Collections.Generic;
using Serilog;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using static OFA.Repayment.WM.Helpers;

namespace OFA.Repayment.WM.DAL
{
    public class OFAEventStore : IOFAEventStore
    {
        private readonly IEventStoreConnection _connection;
        public bool IsOpen { get; private set; }
        private readonly Serilog.ILogger _logger;
        public string ConnectionName { get; set; }

        public OFAEventStore(string userName, string password, string uri, string port, string connectionName)
        {
            ConnectionName = connectionName;
            _logger = Log.ForContext<OFAEventStore>();
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

        public async Task AppendEventAsync(IEvent @event)
        {
            throw new NotImplementedException();
        }

        public async Task ReadAllEventsASync(string streamName)
        {
            throw new NotImplementedException();
        }

        public async Task SubscribeToStreamAsync(string streamName)
        {
            throw new NotImplementedException();
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
    }
}
