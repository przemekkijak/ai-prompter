using AiPrompter;
using AiPrompter.Services.Gpt4All;
using AiPrompter.Services.OpenAi;

var client = new OpenAiClient();
var moderationService = new OpenAiModerationService(client);
var completionService = new OpenAiCompletionService(client, moderationService);

//GPT4All model path
var modelPath = "data_model.bin";
var gpt4all = new Gpt4AllCompletionService(modelPath);

while (true)
{
    Console.WriteLine("\nEnter prompt: (type :q to exit):");
    
    var prompt = Console.ReadLine();

    if (prompt == ":q")
        break;
    
    //OpenAI
    await completionService.GetChatCompletion(prompt);

    //GPT4all
    await gpt4all.GetCompletion(prompt);
}