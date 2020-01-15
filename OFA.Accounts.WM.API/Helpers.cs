using Newtonsoft.Json;
using OFA.DAL.EventStore.DAL;
using System.IO;

namespace OFA.Accounts.WM.API
{
    public static class Helpers
    {
        public static SampleData GetSampleData(this string filePath)
            => File.Exists(filePath) ? JsonConvert.DeserializeObject<SampleData>(File.ReadAllText(filePath))
            : null;
    }

    public class SampleData
    {
        public Season[] Seasons { get; set; }
        public Customer[] Customers { get; set; }
        public Customersummary[] CustomerSummaries { get; set; }
        public Repaymentupload[] RepaymentUploads { get; set; }
    }

    public class Season
    {
        public int SeasonID { get; set; }
        public string SeasonName { get; set; }
        public string StartDate { get; set; }
        public int EndDate { get; set; }
    }

    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
    }

    public class Customersummary
    {
        public int CustomerID { get; set; }
        public int SeasonID { get; set; }
        public int Credit { get; set; }
        public int TotalRepaid { get; set; }
    }

    public class Repaymentupload
    {
        public int CustomerID { get; set; }
        public int SeasonID { get; set; }
        public string Date { get; set; }
        public int Amount { get; set; }
    }
}
