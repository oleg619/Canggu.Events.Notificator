using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CangguEvents.SQLite.Entities;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace CangguEvents.SQLite
{
    public class SqlLiteRepository : IEventsRepository
    {
        private readonly EventsContext _context;
        private readonly IMapper _mapper;

        public SqlLiteRepository(EventsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EventInfo>> GetAllEvents(CancellationToken cancellationToken)
        {
            var events = await _context.Events
                .IncludeAllInfo()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<EventInfo>>(events);
        }

        public async Task<List<EventInfo>> GetEvents(DayOfWeek dayOfWeek, CancellationToken cancellationToken)
        {
            var events = await _context.Events
                .Where(info => info.DayOfWeeks.Any(entity => entity.DayOfWeek == dayOfWeek))
                .IncludeAllInfo()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<EventInfo>>(events);
        }

        public async Task<EventInfo> GetEvent(int id, CancellationToken cancellationToken)
        {
            var events = await _context.Events
                .IncludeAllInfo()
                .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

            return _mapper.Map<EventInfo>(events);
        }

        public async Task AddEvent(EventInfo eventInfos, CancellationToken cancellationToken)
        {
            var eventEntity = _mapper.Map<EventEntity>(eventInfos);
            await _context.Events.AddAsync(eventEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddEvents(IEnumerable<EventInfo> eventInfos, CancellationToken cancellationToken)
        {
            var eventEntities = _mapper.Map<IEnumerable<EventEntity>>(eventInfos);
            await _context.Events.AddRangeAsync(eventEntities, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}