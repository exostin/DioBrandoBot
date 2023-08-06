using DSharpPlus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DioBrandoBot.Tests.Tests;

public class BotControllerTests
{
    private readonly DiscordClient _dc;
    private readonly ulong _botChannelId;

    public BotControllerTests()
    {
        var baseDirectory = Directory.GetParent(AppContext.BaseDirectory) ?? throw new InvalidOperationException("Couldn't get the base directory or its parent!");
        var baseDirectoryPath = baseDirectory.Parent?.Parent?.Parent;
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(baseDirectoryPath!.FullName)
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        var discordClientConfiguration = new DiscordConfiguration
        {
            Token = configuration.GetSection("DISCORD_BOT_TOKEN").Value ?? throw new InvalidOperationException("Couldn't get the Discord bot token!"),
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents,
            MinimumLogLevel = LogLevel.Information
        };
        _dc = new DiscordClient(discordClientConfiguration);
        var botChannelConfigValue = configuration.GetSection("BOT_CHANNEL_ID").Value ?? throw new InvalidOperationException("Couldn't get the bot channel ID!");
        _botChannelId = Convert.ToUInt64(botChannelConfigValue);
    }

    [Fact]
    public async void DiscordClient_CanConnect()
    {
        await _dc.ConnectAsync();
        var currentUser = _dc.CurrentUser;
        await _dc.DisconnectAsync();
        Assert.NotNull(currentUser);
    }

    [Fact]
    public async void DiscordClient_CanSeeBotsChannel()
    {
        await _dc.ConnectAsync();
        var botChannel = await _dc.GetChannelAsync(_botChannelId);
        await _dc.DisconnectAsync();
        Assert.NotNull(botChannel);
    }
}