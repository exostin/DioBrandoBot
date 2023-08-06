namespace DioBrandoBot.Services;

public interface IAiService
{
    string PredictNextLine(IEnumerable<string> previousLines);
}