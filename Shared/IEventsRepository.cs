using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shared
{
    public interface IEventsRepository
    {
        Task<EventInfo> AddEvent(EventInfo eventInfos, CancellationToken cancellationToken = default);
        Task AddEvents(IEnumerable<EventInfo> eventInfos, CancellationToken cancellationToken = default);
        Task<List<EventInfo>> GetAllEvents(CancellationToken cancellationToken = default);
        Task<List<EventInfo>> GetEvents(DayOfWeek dayOfWeek, CancellationToken cancellationToken = default);
        Task<EventInfo> GetEvent(int id, CancellationToken cancellationToken);
    }
}