using OFA.Repayment.WM.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace OFA.Repayment.WM.Messages.Commands
{
    public class CreateSeason : ICommand
    {
        public int SeasonId { get; set; }
        public string SeasonName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEvent @event => Apply();

        private IEvent Apply()
        {
            try
            {
                return new SeasonCreated(SeasonId, SeasonName, StartDate, EndDate);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CreateSeason(int id, string name, DateTime seasonStartDate, DateTime? seasonEndDate = null)
        {
            if (id == 0) throw new Exception("Season id is invalid.");
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("Season name is missing.");
            if (seasonStartDate.Equals(default)) throw new Exception("Season start date is invalide");

            SeasonId = id;
            SeasonName = name;
            StartDate = seasonStartDate;
            EndDate = seasonEndDate;
        }
    }
}
