using AiPrompter.Helpers;
using AiPrompter.Models.OpenAi.Moderation;
using Newtonsoft.Json;

namespace AiPrompter.Services.OpenAi;

public class OpenAiModerationService
{
    private readonly OpenAiClient client;

    public OpenAiModerationService(OpenAiClient client)
    {
        this.client = client;
    }
    
    public async Task<ModerationResultModel> ModeratePrompt(string prompt)
    {
        //Works best in english
        const string url = $"{OpenAiClient.BaseUrl}/moderations";

        var serializedParameters = Serializer.SerializeObject(new
        {
            input = prompt
        });
        
        var response = await client.PostRequest(url, serializedParameters);
        var responseModel = JsonConvert.DeserializeObject<ModerationResponseModel>(response) ?? new ModerationResponseModel();
        
        return responseModel.Results.FirstOrDefault() ?? new ModerationResultModel();
    }
}