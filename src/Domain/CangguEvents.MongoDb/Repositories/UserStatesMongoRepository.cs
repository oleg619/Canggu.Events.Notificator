using System;
using System.Threading;
using System.Threading.Tasks;
using CangguEvents.Domain;
using CangguEvents.MongoDb.Entities;
using MongoDB.Driver;

namespace CangguEvents.MongoDb.Repositories
{
    public class UserStatesMongoRepository : IUserStateRepository
    {
        private IMongoCollection<UserStateEntity> UserStates { get; }

        public UserStatesMongoRepository(IMongoDatabase database, string collectionName)
        {
            UserStates = database.GetCollection<UserStateEntity>(collectionName);
        }

        public Task<UserState> GetUserState(long userId, CancellationToken cancellationToken) =>
            UserStates.Find(entity => entity.UserId == userId).Project(entity => entity.ToDomain())
                .SingleOrDefaultAsync(cancellationToken);

        public Task ChangeUserState(long userId, UserState state, CancellationToken cancellationToken) =>
            UserStates.ReplaceOneAsync(entity => entity.UserId == userId, UserStateEntity.From(state, userId),
                cancellationToken: cancellationToken);

        public async Task CreateUser(long userId, UserState state, CancellationToken cancellationToken)
        {
            try
            {
                await UserStates.InsertOneAsync(UserStateEntity.From(state, userId),
                    cancellationToken: cancellationToken);
            }
            catch (Exception e) when (e.InnerException is MongoBulkWriteException)
            {
                // Ignore
            }
        }
    }
}