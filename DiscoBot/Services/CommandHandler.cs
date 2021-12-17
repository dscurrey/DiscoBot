using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DiscoBot.Services
{
    public class CommandHandler
    {
        private readonly CommandService _cmdServ;
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _services;
        private readonly ILogger _logger;
        private readonly char _prefix;

        public CommandHandler(IServiceProvider services)
        {
            _services = services;
            var config = _services.GetRequiredService<IConfiguration>();
            _client = _services.GetRequiredService<DiscordSocketClient>();
            _cmdServ = _services.GetRequiredService<CommandService>();
            _logger = _services.GetRequiredService<ILogger<CommandHandler>>();

            _prefix = char.Parse(config["Discord:Prefix"]);

            // Hook
            _cmdServ.CommandExecuted += CommandExecutedAsync;
            _client.MessageReceived += MessageReceivedAsync;
        }

        public async Task InitAsync() => await _cmdServ.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

        private async Task MessageReceivedAsync(SocketMessage rawMessage)
        {
            // Check if message is valid
            if (rawMessage is not SocketUserMessage { Source: MessageSource.User } message)
                return;
            var argPos = 0;
            if (!message.HasCharPrefix(_prefix, ref argPos) &&
                !message.HasMentionPrefix(_client.CurrentUser, ref argPos)) return;

            var context = new SocketCommandContext(_client, message);
            // Execute Matching Commands
            await _cmdServ.ExecuteAsync(context, argPos, _services);
        }

        private async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (!command.IsSpecified)
            {
               _logger.LogInformation("Command ({CommandName}) failed to execute -> [{User}] on [{Server}]", command.Value.Name, context.User, context.Guild.Name);
                return;
            }

            if (result.IsSuccess)
            {
                _logger.LogInformation("Command ({Command}) successful -> [{User}] on [{Server}]", command.Value.Name, context.User, context.Guild.Name);
                return;
            }

            await context.Channel.SendMessageAsync($"Something went wrong: {result.ErrorReason}");
        }
    }
}
