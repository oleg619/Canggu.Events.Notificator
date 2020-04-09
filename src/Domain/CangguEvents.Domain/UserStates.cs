using System.Threading;
using System.Threading.Tasks;

namespace CangguEvents.Domain
{
    public interface IUserStateRepository
    {
        Task<UserState> GetUserState(long userId, CancellationToken cancellationToken = default);
        Task ChangeUserState(long userId, UserState state, CancellationToken cancellationToken = default);
        Task CreateUser(long userId, UserState state, CancellationToken cancellationToken = default);
    }
}