using OFA.Accounts.WM.Projections;
using OFA.Common.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Accounts.WM.Repositories.IRepositories
{
    public interface ILedgerRepository : IRepository<string>
    {
        Task<GlEntry> GetPendingEntriesAsync(string projectionName);
        Task CreateProjectionAsync(string projectionName, string query);
    }
}
