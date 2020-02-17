using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Shared
{
    public interface IUserStateRepository
    {
        Task<UserState> GetUserState(long userId);
        Task ChangeUserState(long userId, UserState state);
        Task CreateUser(long requestUserId);
    }

    public class InMemoryUserStateRepository : IUserStateRepository
    {
        private readonly ConcurrentDictionary<long, UserState> _states;
        private const long MyId = 197056415;

        public InMemoryUserStateRepository()
        {
            _states = new ConcurrentDictionary<long, UserState>()
            {
                [MyId] = new UserState(true, true)
            };
        }

        public Task<UserState> GetUserState(long userId)
        {
            _states.TryGetValue(userId, out var state);
            return Task.FromResult(state);
        }

        public Task ChangeUserState(long userId, UserState state)
        {
            _states[userId] = state;
            return Task.CompletedTask;
        }

        public Task CreateUser(long userId)
        {
            _states[userId] = new UserState(true, true);
            return Task.CompletedTask;
        }
    }
}