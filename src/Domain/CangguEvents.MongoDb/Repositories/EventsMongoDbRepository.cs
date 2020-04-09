using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CangguEvents.Domain;
using MongoDB.Driver;

namespace CangguEvents.MongoDb.Repositories
{
    public class EventsMongoDbRepository : IEventsRepository
    {
        private readonly IMapper _mapper;
        private IMongoCollection<EventEntity> Events { get; }

        public EventsMongoDbRepository(IMongoDatabase database, IMapper mapper, string collectionName)
        {
            _mapper = mapper;
            Events = database.GetCollection<EventEntity>(collectionName);
        }

        public Task<List<EventInfo>> GetAllEvents(CancellationToken cancellationToken = default) =>
            Events.Find(_ => true)
                .Project(info => _mapper.Map<EventInfo>(info))
                .ToListAsync(cancellationToken);

        public Task<List<EventInfo>> GetEvents(DayOfWeek dayOfWeek, CancellationToken cancellationToken = default)
            => Events.Find(info => info.DayOfWeeks.Contains(dayOfWeek))
                .Project(info => _mapper.Map<EventInfo>(info))
                .ToListAsync(cancellationToken);

        public Task<EventInfo> GetEvent(int id, CancellationToken cancellationToken = default)
            => Events.Find(info => info.Id == id)
                .Project(info => _mapper.Map<EventInfo>(info))
                .SingleOrDefaultAsync(cancellationToken);

        public Task AddEvent(EventInfo eventInfo, CancellationToken cancellationToken = default) =>
            Events.InsertOneAsync(_mapper.Map<EventEntity>(eventInfo), cancellationToken: cancellationToken);
    }
}