using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Common.Repositories.IRepositories
{
    public interface IRepository
    {
        Task SaveAsync();
        Task GetByIdAsync<TId>(TId id);
    }
}
