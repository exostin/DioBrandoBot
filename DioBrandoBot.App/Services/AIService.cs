namespace DioBrandoBot.App.Services;

public class AiService : IAiService
{
    public string Respond(string messageContent)
    {
        return "WRRRYYYYYYY";
    }

    public string PredictNextMessage(IEnumerable<string> previousMessages)
    {
        return "";
    }
}