﻿using OFA.Common;
using OFA.DAL.EventStore.DAL;
using OFA.DAL.EventStore.DAL.IDAL;
using OFA.Repayment.WM.Messages.Events;
using System;
using System.Threading.Tasks;
using Xunit;

namespace OFA.Repayment.WM.Test.DALTest
{
    public class EventStoreTest
    {
        private readonly IOFAEventStore _evStore;
        public EventStoreTest()
        {
            _evStore = new OFAEventStore("admin", "changeit", "localhost", "1113", "test_client");
        }

        [Fact]
        public async Task OpenConnectionShouldReturnTrue()
        {
            //arrange

            //act
            await _evStore.OpenConnectionAsync();
            await Task.Delay(1000);

            //assert
            Assert.True(_evStore.IsOpen);
        }

        [Fact]
        public async Task CloseConnectionShouldSetIsOpenToFalse()
        {
            //arrange
            await _evStore.OpenConnectionAsync();
            await Task.Delay(1000);

            //act
            _evStore.CloseConnection();
            await Task.Delay(1000);

            //assert
            Assert.False(_evStore.IsOpen);
        }

        [Fact]
        public async Task AppendEventShouldWriteEventToEventStore()
        {
            //arrange 
            await _evStore.OpenConnectionAsync();
            await Task.Delay(1000);
            var @event = new CustomerCreated(1, $"Demo @ {DateTime.UtcNow}");

            //act
            var saved = await _evStore.AppendEventAsync("test-customer-stream", @event);
            _evStore.CloseConnection();
            await Task.Delay(1000);

            //assert
            Assert.True(saved);
        }

        [Fact]
        public async Task ReadAllEventsReturnAllEventsOfStreamFromEventStore()
        {
            //arrange 
            await _evStore.OpenConnectionAsync();
            await Task.Delay(1000);

            //act
            var items = await _evStore.ReadAllEventsASync<CustomerCreated>("test-customer-stream");

            //assert
            Assert.NotEmpty(items);
        }

        [Fact]
        public async Task SubscribeToStreamAsync()
        {
            //arrange 
            await _evStore.OpenConnectionAsync();
            await Task.Delay(1000);

            //act
            var result = await _evStore.SubscribeToStreamAsync("test-customer-stream", $"testers-{DateTime.UtcNow}");
            _evStore.CloseConnection();
            await Task.Delay(1000);

            //assert
            Assert.True(result);
        }

        //[Fact]
        //public async Task ListenAsync()
        //{
        //    //arrange 
        //    CustomerCreated evt = null;
        //    await _evStore.OpenConnectionAsync();
        //    await Task.Delay(1000);

        //    //act
        //    var result = await _evStore.SetListenerAsync("customer", "testers", (_, x)=> 
        //    {
        //        evt = x.Event.Data.FromBytes<CustomerCreated>();
        //    });

        //    //assert
        //    Assert.True(result);
        //}

        [Fact]
        public async Task CreateProjection()
        {
            //arrange 
            await _evStore.OpenConnectionAsync();
            await Task.Delay(1000);

            //act
            var _task = _evStore.CreateProjectionAsync($"somequery{DateTime.UtcNow}", "fromStream('customer-summary').when({$init: function(){return {items: []}},CustomerSummaryCreated: function(s,e){if(e.body.CustomerId === 53)s.items.push(e.body);}});");
            await _task;

            //assert
            Assert.True(_task.IsCompleted);
        }

        [Fact]
        public async Task GetProjectionResult()
        {
            //arrange 
            await _evStore.OpenConnectionAsync();
            await Task.Delay(1000);

            //act
            var result = await _evStore.GetProjectionResultAsync("somequery55");

            //assert
            Assert.NotNull(result);
        }
    }
}
