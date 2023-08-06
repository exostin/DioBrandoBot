﻿namespace DioBrandoBot.App.Services;

public interface IAiService
{
    string PredictNextLine(IEnumerable<string> previousLines);
    string Respond(string messageContent);
}