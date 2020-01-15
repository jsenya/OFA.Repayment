using Newtonsoft.Json;
using OFA.DAL.EventStore.DAL;
using OFA.DAL.EventStore.DAL.IDAL;
using OFA.Repayment.WM.CommandHandlers;
using OFA.Repayment.WM.Repositories;
using System.IO;

namespace OFA.Repayment.WM.Test
{
    public static class Helpers
    {
        public static SampleData GetSampleData(this string filePath)
            => File.Exists(filePath) ? JsonConvert.DeserializeObject<SampleData>(File.ReadAllText(filePath))
            : null;
        public static OFAEventStore GetOFAEventStore()
            => new OFAEventStore("admin", "changeit", "localhost", "1113", "test_client");
        public static CustomerRepository GetCustomerRepository()
            => new CustomerRepository(GetOFAEventStore());
        public static SeasonRepository GetSeasonRepository()
            => new SeasonRepository(GetOFAEventStore());
        public static CustomerSummaryRepository GetCustomerSummaryRepository()
            => new CustomerSummaryRepository(GetOFAEventStore());
        public static CustomerCommandHandler GetCustomerCommandHandler()
            => new CustomerCommandHandler(GetCustomerRepository());
        public static SeasonCommandHandler GetSeasonCommandHandler()
            => new SeasonCommandHandler(GetSeasonRepository());
        public static CustomerSummaryCommandHandler GetCustomerSummaryCommandHandler()
            => new CustomerSummaryCommandHandler(GetCustomerSummaryRepository());
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
