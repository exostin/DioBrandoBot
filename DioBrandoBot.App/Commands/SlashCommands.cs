using DioBrandoBot.App.Services;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace DioBrandoBot.App.Commands;

public class SlashCommands : ApplicationCommandModule
{
    private readonly IAiService _ai;

    public SlashCommands(IAiService ai)
    {
        _ai = ai;
    }
    
    [SlashCommand("Predict", "Predicts your next message Joseph Joestar style")]
    public async Task PredictNextMessage(InteractionContext ctx)
    {
        var previousMessages = await ctx.Channel.GetMessagesAsync(30);
        var prediction = _ai.GenerateNextMessagePrediction(previousMessages.Select(x => x.Content));
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
            .WithContent($"{ctx.User.Mention} next you'll say... {prediction}"));
    }
}