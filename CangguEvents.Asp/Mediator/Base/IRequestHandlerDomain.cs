using System.Collections.Generic;
using CangguEvents.Asp.Models.Commands;
using CangguEvents.Asp.Services.Base;
using MediatR;

namespace CangguEvents.Asp.Mediator.Base
{
    public interface IRequestHandlerDomain<in TRequest> : IRequestHandler<TRequest, IReadOnlyCollection<ITelegramResponse>>
        where TRequest : MessageCommandBase
    {
    }
}