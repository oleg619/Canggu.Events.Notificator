﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CangguEvents.SQLite.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Shared;

namespace CangguEvents.SQLite
{
    public class SqlLiteRepository : IEventsRepository, IUserStateRepository
    {
        private readonly EventsContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SqlLiteRepository(EventsContext context, IMapper mapper, ILogger logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
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

        public async Task<EventInfo> AddEvent(EventInfo eventInfos, CancellationToken cancellationToken)
        {
            var eventEntity = _mapper.Map<EventEntity>(eventInfos);
            await _context.Events.AddAsync(eventEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventInfo>(eventEntity);
        }

        public async Task AddEvents(IEnumerable<EventInfo> eventInfos, CancellationToken cancellationToken)
        {
            var eventEntities = _mapper.Map<IEnumerable<EventEntity>>(eventInfos);
            await _context.Events.AddRangeAsync(eventEntities, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<UserState> GetUserState(long userId, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstAsync(entity => entity.UserId == userId, cancellationToken);

            return user.ToDomain();
        }

        public async Task ChangeUserState(long userId, UserState state, CancellationToken cancellationToken)
        {
            var oldUserState = await _context.Users.FirstAsync(entity => entity.UserId == userId, cancellationToken);

            oldUserState.Subscribed = state.Subscribed;
            oldUserState.ShortInfo = state.ShortInfo;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CreateUser(long userId, UserState userState, CancellationToken token)
        {
            try
            {
                var userStateEntity = new UserStateEntity
                {
                    UserId = userId, Subscribed = userState.Subscribed, ShortInfo = userState.ShortInfo
                };
                _context.Users.Add(userStateEntity);

                await _context.SaveChangesAsync(token);
            }
            catch (DbUpdateException exception)
            {
                if (exception.InnerException is SqliteException sqliteException &&
                    sqliteException.SqliteErrorCode == 19)
                {
                    _logger.Information("We try to create existing user");
                }
                else
                {
                    throw;
                }
            }
        }
    }
}