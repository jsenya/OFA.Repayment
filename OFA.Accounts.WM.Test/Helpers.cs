using OFA.Accounts.WM.CommandHandlers;
using OFA.Accounts.WM.Repositories;
using OFA.DAL.EventStore.DAL;

namespace OFA.Accounts.WM.Test
{
    public static class Helpers
    {
        public static OFAEventStore GetOFAEventStore()
            => new OFAEventStore("admin", "changeit", "localhost", "1113", "test_client");
        public static AccountRepository GetAccountRepository()
            => new AccountRepository(GetOFAEventStore());
        public static AccountCreatedCommandHandler GetAccountCreatedCommandHandler()
            => new AccountCreatedCommandHandler(GetAccountRepository());
    }
}
