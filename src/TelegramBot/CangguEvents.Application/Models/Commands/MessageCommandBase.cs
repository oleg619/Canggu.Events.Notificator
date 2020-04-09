using System.Collections.Generic;
using CangguEvents.Application.Services.Base;
using MediatR;

namespace CangguEvents.Application.Models.Commands
{
    public abstract class MessageCommandBase : IRequest<IReadOnlyCollection<ITelegramResponse>>
    {
        public readonly long UserId;

        protected MessageCommandBase(long userId)
        {
            UserId = userId;
        }
    }
}