using DioBrandoBot.App.Commands;
using DioBrandoBot.App.Services;

namespace DioBrandoBot.Tests.UnitTests;

public class SlashCommandsTests
{
    private readonly SlashCommands _slashCommands = new(new AiService());
}