using DioBrandoBot.App.Services;

namespace DioBrandoBot.Tests;

public class AiTests
{
    private readonly IAiService _ai = new AiService();

    [Fact]
    public void Respond_ReturnsNotNull()
    {
        // Arrange
        const string messageContent = "Hello";
        // Act
        var response = _ai.Respond(messageContent);
        // Assert
        Assert.NotNull(response);
    }
    
    [Fact]
    public void PredictNextMessage_ReturnsNotNull()
    {
        // Arrange
        var previousMessages = new List<string>
        {
            "Hello",
            "World"
        };
        // Act
        var prediction = _ai.PredictNextMessage(previousMessages);
        // Assert
        Assert.NotNull(prediction);
    }
}