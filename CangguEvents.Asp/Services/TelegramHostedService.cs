using System.Threading;
using System.Threading.Tasks;
using CangguEvents.Asp.Configurations;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace CangguEvents.Asp.Services
{
    public sealed class TelegramHostedService : IHostedService
    {
        private readonly ITelegramBotClient _client;
        private readonly BotConfiguration _configuration;

        public TelegramHostedService(ITelegramBotClient client, BotConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _client.DeleteWebhookAsync(cancellationToken);
            await _client.SetWebhookAsync($"{_configuration.WebhookUrl}/{_configuration.BotToken}",
                cancellationToken: cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _client.DeleteWebhookAsync(cancellationToken);
        }
    }
}