namespace DioBrandoBot.App.Services;

public class AiService : IAiService
{
    public string GenerateResponse(string messageContent)
    {
        return "WRRRYYYYYYY";
    }

    public string GenerateNextMessagePrediction(IEnumerable<string> previousMessages)
    {
        return "";
    }
}