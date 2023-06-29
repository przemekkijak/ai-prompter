using System.Text;
using AiPrompter.Helpers;

namespace AiPrompter.Models.OpenAi.Completion;

public class CompletionResponseModel
{
    public List<ChoiceModel> Choices { get; init; }

    public UsageModel Usage { get; init; }

    public string UserPrompt { get; set; }

    public string PromptParameters { get; set; }

    public void PrintResults()
    {
        var sb = new StringBuilder();
        sb.AppendLine("------------- [OpenAI RESPONSE] --------------------------");
        sb.AppendLine($"[User prompt]: {UserPrompt}");
        sb.AppendLine($"[Prompt parameters]: {PromptParameters}");
        
        if (Choices.Any())
        {
            sb.AppendLine($"[Response]: {Choices.First().Message.Content}");
        }
        else
        {
            sb.AppendLine("Response is empty");
        }
        
        sb.AppendLine($"[Used tokens]: {Usage.TotalTokens} (Prompt: {Usage.PromptTokens} / Response: {Usage.CompletionTokens})");

        var results = sb.ToString();
        Console.WriteLine(results);
        Logger.SaveToLogFile(results);
    }
}