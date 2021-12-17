using System;
using System.Threading.Tasks;
using DiscoBot.Services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DiscoBot
{
    public class Program
    {
        private DiscordSocketClient _client;
        private readonly IConfiguration _config;

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logs/DiscoBot.log", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .CreateLogger();

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

            services.GetRequiredService<LoggingService>();

            await _client.LoginAsync(TokenType.Bot, _config["Discord:BotToken"]);
            await _client.StartAsync();

            await services.GetRequiredService<CommandHandler>().InitAsync();

            await Task.Delay(-1);
        }

        private IServiceProvider ConfigureServices() => new ServiceCollection()
            .AddSingleton(_config)
            .AddLogging(logging => logging.AddSerilog())
            .AddSingleton<LoggingService>()
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton<CommandService>()
            .AddSingleton<CommandHandler>()
            .BuildServiceProvider();

        private static Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }
    }
}
