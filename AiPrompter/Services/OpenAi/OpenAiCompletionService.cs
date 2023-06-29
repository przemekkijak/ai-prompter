using System.Text.RegularExpressions;
using AiPrompter.Helpers;
using AiPrompter.Models.OpenAi.Completion;
using Newtonsoft.Json;

namespace AiPrompter.Services.OpenAi;

public class OpenAiCompletionService
{
    private readonly OpenAiModerationService openAiModerationService;
    private readonly OpenAiClient client;

    public OpenAiCompletionService(OpenAiClient client, OpenAiModerationService openAiModerationService)
    {
        this.openAiModerationService = openAiModerationService;
        this.client = client;
    }

    public async Task GetChatCompletion(string prompt)
    {
        const string url = $"{OpenAiClient.BaseUrl}/chat/completions";

        if (string.IsNullOrWhiteSpace(prompt))
        {
            Console.WriteLine("Prompt can't be empty");
            return;
        }

        if (prompt.Length >= 100)
        {
            Console.WriteLine("Prompt too long");
            return;
        }

        var promptModeration = await openAiModerationService.ModeratePrompt(prompt);
        if (promptModeration.Flagged)
        {
            promptModeration.PrintResults(prompt);
            return;
        }

        var role = ExtractRoleFromPrompt(ref prompt);
        var parameters = PrepareCompletionParameters(prompt, role);
        var serializedParameters = Serializer.SerializeObject(parameters);

        var response = await client.PostRequest(url, serializedParameters);
        var responseModel = JsonConvert.DeserializeObject<CompletionResponseModel>(response) ??
                            new CompletionResponseModel();

        responseModel.UserPrompt = prompt;
        responseModel.PromptParameters = serializedParameters;
        responseModel.PrintResults();
    }

    private static object PrepareCompletionParameters(string prompt, string role)
    {
        if (string.IsNullOrWhiteSpace(role))
        {
            role = OpenAiClient.DefaultRole;
        }

        return new
        {
            model = "gpt-3.5-turbo",
            messages = new List<MessageModel>
            {
                new MessageModel()
                {
                    Role = role,
                    Content = prompt
                }
            },
            //max_tokens = 40
            temperature = 0.2,
            frequency_penalty = 2
        };
    }

    /// <summary>
    /// It allows to force role used in model, allowed values: user, system, assistant
    /// It is used by prefix in prompt, example: [user] provide recipe for a pancakes
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    private static string ExtractRoleFromPrompt(ref string prompt)
    {
        const string rolePattern = @"\[(\w+)\]";
        var match = Regex.Match(prompt, rolePattern);

        if (!match.Success)
            return string.Empty;

        prompt = prompt.Split(']')[1];
        var role = match.Groups[1].Value;
        return role.Trim();
    }
}