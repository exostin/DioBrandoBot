using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.Json;
using DSharpPlus;
using Microsoft.Extensions.Configuration;

namespace DioBrando_DiscordBot
{
    class Startup
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            DirectoryInfo baseDirectory = Directory.GetParent(AppContext.BaseDirectory) ?? throw new InvalidOperationException();
            IConfiguration configuration = new ConfigurationBuilder()
                // TODO: get it in a more sensible way
                .SetBasePath(baseDirectory.Parent!.Parent!.Parent!.FullName)
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            
            var botLauncher = ActivatorUtilities.CreateInstance<BotController>(serviceCollection.BuildServiceProvider());
            await botLauncher.Run();
        }
    }
}