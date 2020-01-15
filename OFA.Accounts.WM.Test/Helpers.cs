using OFA.Accounts.WM.CommandHandlers;
using OFA.Accounts.WM.EventHandlers;
using OFA.Accounts.WM.Repositories;
using OFA.DAL.EventStore.DAL;

namespace OFA.Accounts.WM.Test
{
    public static class Helpers
    {
        public static OFAEventStore GetOFAEventStore()
            => new OFAEventStore("admin", "changeit", "localhost", "1113", "test_client");
        public static LedgerRepository GetLedgerRepository()
            => new LedgerRepository(GetOFAEventStore());
        public static CustomerSummaryCreatedEventHandler GetCustomerSummaryCreatedEventHandler()
            => new CustomerSummaryCreatedEventHandler(GetCreateLedgerDebitEntryCommandHandler());
        public static CreateLedgerDebitEntryCommandHandler GetCreateLedgerDebitEntryCommandHandler()
            => new CreateLedgerDebitEntryCommandHandler(GetLedgerRepository());
        public static CreateInitialLedgerEntryCommandHandler GetCreateInitialLedgerEntryCommandHandler()
            => new CreateInitialLedgerEntryCommandHandler(GetLedgerRepository());
    }
}
