using DioBrandoBot.App.Services;
using DSharpPlus;
using DSharpPlus.EventArgs;
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
        _dc.MessageCreated += MessageCreatedHandler;
        _dc.TypingStarted += TypingStartedHandler;
    }

    #region Event handlers

    private async Task MessageCreatedHandler(DiscordClient c, MessageCreateEventArgs e)
    {
        _log.LogInformation("Dio message event triggered by: [{ArgsAuthor}]: [{MessageContent}]", e.Author, e.Message.Content);
        var response = _ai.GenerateResponse(e.Message.Content);
        if (e.Message.Content.ToLower().StartsWith("dio")) await e.Message.RespondAsync(response);
    }

    private async Task TypingStartedHandler(DiscordClient c, TypingStartEventArgs e)
    {
        if (e.Channel.Id != 784147362468593675) return;
        var channelMessages = await e.Channel.GetMessagesAsync(15);
        var previousUserMessages = channelMessages.Where(x => x.Author == e.User).Select(x => x.Content);
        var prediction = _ai.GenerateNextMessagePrediction(previousUserMessages);
        await e.Channel.SendMessageAsync($"{e.User.Mention} your next line is... \"{prediction}\"");
    }

    #endregion
}