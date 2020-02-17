using System.Diagnostics.Contracts;

namespace Shared
{
    public struct UserState
    {
        public bool Subscribed { get; }
        public bool ShortInfo { get; }

        public UserState(bool subscribed, bool shortInfo)
        {
            Subscribed = subscribed;
            ShortInfo = shortInfo;
        }

        [Pure]
        public UserState ChangeSubscribe() => new UserState(!Subscribed, ShortInfo);

        [Pure]
        public UserState ChangeShortInfo() => new UserState(Subscribed, !ShortInfo);
    }
}