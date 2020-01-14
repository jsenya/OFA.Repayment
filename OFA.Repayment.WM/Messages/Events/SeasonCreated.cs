using EventStore.ClientAPI;
using OFA.Common.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace OFA.Repayment.WM.Messages.Events
{
    public class SeasonCreated : IEvent
    {
        public Guid EventId { get; private set; }
        public int SeasonId { get; set; }
        public string SeasonName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public SeasonCreated(int id, string name, DateTime seasonStartDate, DateTime? seasonEndDate = null)
        {
            EventId = Guid.NewGuid();
            SeasonId = id;
            SeasonName = name;
            StartDate = seasonStartDate;
            EndDate = seasonEndDate;
        }
    }
}
