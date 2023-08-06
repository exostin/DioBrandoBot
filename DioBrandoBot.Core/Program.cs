using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.Json;
using DSharpPlus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace DioBrandoBot
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
            
            var serviceCollection = new ServiceCollection();
            
            var discordClientConfiguration = new DiscordConfiguration
            {
                Token = configuration.GetSection("DISCORD_BOT_TOKEN").Value,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
            };
            var discordClient = new DiscordClient(discordClientConfiguration);
            serviceCollection.AddScoped<DiscordClient>(x => discordClient);

            serviceCollection.AddScoped<ILogger>();
            
            var buildServiceProvider = serviceCollection.BuildServiceProvider();
            var bot = ActivatorUtilities.CreateInstance<BotController>(buildServiceProvider);
            
            await bot.Run();
        }
    }
}