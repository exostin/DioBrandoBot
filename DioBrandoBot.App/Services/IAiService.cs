namespace DioBrandoBot.App.Services;

public interface IAiService
{
    /// <summary>
    /// Get a prediction from the LLM based on the previous messages.
    /// </summary>
    /// <param name="previousMessages">Messages that have been previously sent in the channel</param>
    /// <returns>Basically an autocomplete suggestion</returns>
    string GenerateNextMessagePrediction(IEnumerable<string> previousMessages);
    
    /// <summary>
    /// Get a response from the LLM based on the message content.
    /// </summary>
    /// <param name="messageContent">User message to get a response to</param>
    string GenerateResponse(string messageContent);
}