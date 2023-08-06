using DioBrandoBot.App.Services;
using DSharpPlus;
using Microsoft.Extensions.Logging;

namespace DioBrandoBot.App;

public class BotController
{
    private readonly DiscordClient _dc;
    private readonly ILogger _log;
    private readonly IAiService _ai;

    public BotController(DiscordClient dc, ILogger<BotController> log, IAiService ai)
    {
        _dc = dc;
        _log = log;
        _ai = ai;
    }

    public async Task Run()
    {
        _log.LogInformation("Connecting...");
        await _dc.ConnectAsync();
        _log.LogInformation("Connected!");
        
        _log.LogInformation("Plugging events in...");
        PlugEventsIn();
        _log.LogInformation("Events plugged in!");
        
        await Task.Delay(-1);
    }

    private void PlugEventsIn()
    {
        _dc.MessageCreated += async (s, e) =>
        {
            _log.LogInformation("Dio message event triggered by: [{ArgsAuthor}]: [{MessageContent}]", e.Author, e.Message.Content);
            var response = _ai.Respond(e.Message.Content);
            if (e.Message.Content.ToLower().StartsWith("dio")) await e.Message.RespondAsync(response);
        };

        _dc.TypingStarted += async (s, e) =>
        {
            if (e.StartedAt < DateTimeOffset.Now.AddSeconds(-5))
            {
                var channelMessages = await e.Channel.GetMessagesAsync(15);
                var previousUserMessages = channelMessages.Where(x => x.Author == e.User).Select(x => x.Content);
                var prediction = _ai.PredictNextLine(previousUserMessages);
                if (e.StartedAt < DateTimeOffset.Now.AddSeconds(-15))
                    await e.Channel.SendMessageAsync($"{e.User.Mention} your next line is... \"{prediction}\"");
            }
        };
    }
}