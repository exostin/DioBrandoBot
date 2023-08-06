using System.Runtime.CompilerServices;
using DioBrandoBot.Services;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DioBrandoBot;

public class BotController
{
    private readonly DiscordClient _dc;
    private readonly ILogger _logger;
    private readonly IAiService _ai;

    public BotController(DiscordClient dc, ILogger logger, IAiService ai)
    {
        _dc = dc;
        _logger = logger;
        _ai = ai;
    }

    public async Task Run()
    {
        _logger.LogInformation("Connecting...");
        await _dc.ConnectAsync();
        _logger.LogInformation("Connected!");
        
        _logger.LogInformation("Plugging events in...");
        PlugEventsIn();
        _logger.LogInformation("Events plugged in!");
        
        await Task.Delay(-1);
    }

    private void PlugEventsIn()
    {
        _dc.MessageCreated += async (s, e) =>
        {
            _logger.LogInformation("Dio message event triggered by: [{ArgsAuthor}]: [{MessageContent}]", e.Author, e.Message.Content);
            if (e.Message.Content.ToLower().StartsWith("dio")) await e.Message.RespondAsync("WRRRRYYYYYYYYYYY");
        };

    }
}