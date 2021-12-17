using System;
using System.Threading.Tasks;
using DiscoBot.Services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscoBot
{
    public class Program
    {
        private DiscordSocketClient _client;
        private readonly IConfiguration _config;

        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public Program()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(path: "appsettings.json");
            _config = builder.Build();
        }

        public async Task MainAsync()
        {
            var services = ConfigureServices();
            _client = services.GetRequiredService<DiscordSocketClient>();

            // Hook into events
            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;
            _client.MessageReceived += MessageReceivedAsync;

            await _client.LoginAsync(TokenType.Bot, _config["Discord:BotToken"]);
            await _client.StartAsync();

            await services.GetRequiredService<CommandHandler>().InitAsync();

            await Task.Delay(-1);
        }

        private IServiceProvider ConfigureServices() => new ServiceCollection()
            .AddSingleton(_config)
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton<CommandService>()
            .AddSingleton<CommandHandler>()
            .BuildServiceProvider();

        private static Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private static Task ReadyAsync()
        {
            Console.WriteLine($"Connected as -> [] :)");
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(SocketMessage message)
        {
            if (message.Author.Id == _client.CurrentUser.Id || message.Author.IsBot)
                return;
            if (message.Content == ".hello")
            {
                await message.Channel.SendMessageAsync("world!");
            }
        }
    }
}
