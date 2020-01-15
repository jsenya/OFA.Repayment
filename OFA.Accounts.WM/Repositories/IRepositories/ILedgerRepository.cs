using OFA.Common.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Accounts.WM.Repositories.IRepositories
{
    public interface ILedgerRepository : IRepository<string>
    {
        Task GetActiveAccountAsync(string accountName);
    }
}
