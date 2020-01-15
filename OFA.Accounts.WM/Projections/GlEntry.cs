using System;
using System.Collections.Generic;
using System.Text;

namespace OFA.Accounts.WM.Projections
{
    public class GlEntry
    {
        public Entry[] items { get; set; }
    }
    public class Entry
    {
        public string AccountStatus { get; set; }
        public int CustomerId { get; set; }
        public int SeasonId { get; set; }
        public int Balance { get; set; }
    }

}
