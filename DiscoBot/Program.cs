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
            services.GetRequiredService<CommandService>().Log += LogAsync;

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

        private Task ReadyAsync()
        {
            Console.WriteLine($"Connected as -> [{_client.CurrentUser}]");
            return Task.CompletedTask;
        }
    }
}
