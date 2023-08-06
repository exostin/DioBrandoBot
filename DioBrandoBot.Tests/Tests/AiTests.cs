using DioBrandoBot.App.Services;

namespace DioBrandoBot.Tests.Tests;

public class AiTests
{
    private readonly IAiService _ai = new AiService();

    [Fact]
    public void Respond_ReturnsNotNullValue()
    {
        // Arrange
        const string messageContent = "Hello";
        // Act
        var response = _ai.GenerateResponse(messageContent);
        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public void PredictNextMessage_ReturnsNotNullValue()
    {
        // Arrange
        var previousMessages = new List<string>
        {
            "Hello",
            "World"
        };
        // Act
        var prediction = _ai.GenerateNextMessagePrediction(previousMessages);
        // Assert
        Assert.NotNull(prediction);
    }
}