using DioBrandoBot.App.Services;
using DSharpPlus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DioBrandoBot.App
{
    public static class Program
    {
        private static async Task Main(string[] args)
        {
            var baseDirectory = Directory.GetParent(AppContext.BaseDirectory) ?? throw new InvalidOperationException("Couldn't get the base directory or its parent!");
            // TODO: get it in a more sensible way
            var baseDirectoryPath = baseDirectory.Parent?.Parent?.Parent; 
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(baseDirectoryPath!.FullName)
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            var services = new ServiceCollection();
            ConfigureServices(services, configuration);
            await using var serviceProvider = services.BuildServiceProvider();
            var bot = ActivatorUtilities.CreateInstance<BotController>(serviceProvider);
            await bot.Run();
        }
        
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var discordClientConfiguration = new DiscordConfiguration
            {
                Token = configuration.GetSection("DISCORD_BOT_TOKEN").Value ?? throw new InvalidOperationException("Couldn't get the Discord bot token!"),
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
            };
            var discordClient = new DiscordClient(discordClientConfiguration);
            services.AddScoped<DiscordClient>(x => discordClient);
            
            services.AddScoped<IAiService, AiService>();
            services.AddLogging(x => x.AddConsole()).AddTransient<BotController>();
        }
    }
}