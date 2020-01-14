using OFA.Common.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Common.Repositories.IRepositories
{
    public interface IRepository<TId>
    {
        Task SaveAsync(string streamName, IEvent @event);
        Task<IEnumerable<IEvent>> GetAllAsync(TId id);
    }
}
