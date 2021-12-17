using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DiscoBot.Services
{
    public class LoggingService
    {
        private readonly ILogger<LoggingService> _logger;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        public LoggingService(IServiceProvider services)
        {
            _client = services.GetRequiredService<DiscordSocketClient>();
            _logger = services.GetRequiredService<ILogger<LoggingService>>();
            _commands = services.GetRequiredService<CommandService>();

            _client.Ready += OnReadyAsync;
            _client.Log += OnLogAsync;
            _commands.Log += OnLogAsync;
        }

        public Task OnReadyAsync()
        {
            _logger.LogInformation
                ("Connected as -> [{User}] :)", _client.CurrentUser.Username + "#" + _client.CurrentUser.Discriminator);
            _logger.LogInformation("We are on [{Count}] servers", _client.Guilds.Count);
            return Task.CompletedTask;
        }

        public Task OnLogAsync(LogMessage msg)
        {
            var logText = $": {msg.Exception?.ToString() ?? msg.Message}";
            switch (msg.Severity.ToString())
            {
                case "Critical":
                    _logger.LogCritical("{Log}", logText);
                    break;
                case "Warning":
                    _logger.LogWarning("{Log}", logText);
                    break;
                case "Info":
                    _logger.LogInformation("{Log}", logText);
                    break;
                case "Verbose":
                    _logger.LogTrace("{Log}", logText);
                    break;
                case "Debug":
                    _logger.LogDebug("{Log}", logText);
                    break;
                case "Error":
                    _logger.LogError("{Log}", logText);
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
