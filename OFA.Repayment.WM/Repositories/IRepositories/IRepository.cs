using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Repayment.WM.Repositories.IRepositories
{
    public interface IRepository
    {
        Task SaveAsync();
        Task GetByIdAsync<TId>(TId id);
    }
}
