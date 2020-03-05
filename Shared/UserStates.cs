using System.Threading;
using System.Threading.Tasks;

namespace Shared
{
    public interface IUserStateRepository
    {
        Task<UserState> GetUserState(long userId, CancellationToken cancellationToken);
        Task ChangeUserState(long userId, UserState state, CancellationToken cancellationToken);
        Task CreateUser(long userId, UserState state, CancellationToken cancellationToken);
    }
}