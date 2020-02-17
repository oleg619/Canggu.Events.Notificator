using System.Threading.Tasks;
using CangguEvents.Asp.Services.Implementation;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace CangguEvents.Asp.Controllers
{
    [Route("api/[controller]")]
    public class TelegramController : Controller
    {
        private readonly MessageHandler _messageHandler;

        public TelegramController(MessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var cangguTimeNow = DateTimeService.CangguTimeNow;
            return Ok($"Hello time in canggu {cangguTimeNow}");
        }

        [HttpPost("update/{token}")]
        public async Task Update([FromRoute] string token, [FromBody] Update update)
        {
            await _messageHandler.Handle(update);
        }
    }
}