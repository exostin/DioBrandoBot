namespace DioBrandoBot.App.Services;

public interface IAiService
{
    string PredictNextMessage(IEnumerable<string> previousMessages);
    string Respond(string messageContent);
}