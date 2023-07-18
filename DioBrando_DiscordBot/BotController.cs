using System.Runtime.CompilerServices;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;
using Microsoft.Extensions.Configuration;

namespace DioBrando_DiscordBot;

public class BotController
{
    private readonly IConfiguration _configuration;
    private DiscordClient _discordClient;
    public BotController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task Run()
    {
        var discordClientConfiguration = new DiscordConfiguration()
        {
            Token = _configuration.GetSection("DISCORD_BOT_TOKEN").Value,
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
        };

        _discordClient = new DiscordClient(discordClientConfiguration);
        
        _discordClient.MessageCreated += async (s, e) =>
        {
            if (e.Message.Content.ToLower().StartsWith("dio")) await e.Message.RespondAsync("WRRRRYYYYYYYYYYY");
        };
        
        await _discordClient.ConnectAsync();
        await Task.Delay(-1);
    }
}