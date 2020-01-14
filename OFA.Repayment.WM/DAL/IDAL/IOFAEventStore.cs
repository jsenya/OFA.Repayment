﻿using OFA.Repayment.WM.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Repayment.WM.DAL.IDAL
{
    public interface IOFAEventStore
    {
        bool IsOpen { get; }
        Task OpenConnectionAsync();
        void CloseConnection();
        Task<bool> AppendEventAsync(string stream, IEvent @event);

        Task<IEnumerable<TEvent>> ReadAllEventsASync<TEvent>(string streamName, long page = 0, 
            int count = 50, string username = null, string password = null) where TEvent : IEvent;

        Task SubscribeToStreamAsync(string streamName);
    }
}
