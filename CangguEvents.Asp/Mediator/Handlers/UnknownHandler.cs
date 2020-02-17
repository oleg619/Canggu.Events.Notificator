using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CangguEvents.Asp.Mediator.Base;
using CangguEvents.Asp.Models.Commands;
using CangguEvents.Asp.Models.Responses;
using CangguEvents.Asp.Services.Base;

namespace CangguEvents.Asp.Mediator.Handlers
{
    public class UnknownHandler : IRequestHandlerDomain<UnknownCommand>
    {
        public async Task<IEnumerable<ITelegramResponse>> Handle(UnknownCommand request,
            CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            return new[] {new TextTelegramResponse("Unknown error")};
        }
    }
}