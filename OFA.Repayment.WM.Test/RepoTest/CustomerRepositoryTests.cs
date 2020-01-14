using OFA.DAL.EventStore.DAL;
using OFA.DAL.EventStore.DAL.IDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace OFA.Repayment.WM.Test.RepoTest
{
    public class CustomerRepositoryTests
    {
        private readonly IOFAEventStore _evStore;
        public CustomerRepositoryTests()
        {
            _evStore = new OFAEventStore("admin", "changeit", "localhost", "1113", "test_client");
        }
    }
}
