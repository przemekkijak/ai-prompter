using System.Text;

namespace AiPrompter;

public class OpenAiClient
{
    public const string BaseUrl = "https://api.openai.com/v1";
    public const string DefaultRole = "system";
    private HttpClient Client { get; }

    public OpenAiClient()
    {
        Client = CreateClient();
    }

    public async Task<string> PostRequest(string url, string parameters)
    {
        try
        {
            var requestContent = new StringContent(parameters, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, requestContent);

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] {ex.Message}");
            throw;
        }
    }

    private static HttpClient CreateClient()
    {
        var client = new HttpClient();
        AuthorizeClient(client);
        return client;
    }

    private static void AuthorizeClient(HttpClient client)
    {
        //Enter your OpenAI API key here
        const string secretKey = "";
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {secretKey}");
    }
}