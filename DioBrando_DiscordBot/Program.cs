using System;
using System.Threading.Tasks;
using DSharpPlus;

namespace DioBrando_DiscordBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var discordClientConfiguration = new DiscordConfiguration()
            {
                Token = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN"),
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            };
            var discordClient = new DiscordClient(discordClientConfiguration);
        }
    }
}