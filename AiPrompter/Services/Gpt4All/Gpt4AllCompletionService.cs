using System.Text;
using AiPrompter.Helpers;
using Gpt4All.Model;
using Gpt4All.Prediction;

namespace AiPrompter.Services.Gpt4All;

public class Gpt4AllCompletionService
{
    private readonly IGpt4AllModel Model;

    public Gpt4AllCompletionService(string modelPath)
    {
        var modelFactory = new Gpt4AllModelFactory();
        Model = modelFactory.LoadModel(modelPath);
    }
    
    public async Task GetCompletion(string prompt)
    {
        if (string.IsNullOrWhiteSpace(prompt))
        {
            Console.WriteLine("Prompt can't be empty");
            return;
        }

        var result = await Model.GetStreamingPredictionAsync(
            prompt,
            PredictRequestOptions.Defaults);

        var resultCompletion = new StringBuilder();

        resultCompletion.AppendLine("------------- [GPT4All RESPONSE] --------------------------");
        
        await foreach (var token in result.GetPredictionStreamingAsync())
        {
            resultCompletion.Append(token);
        }

        var stringResult = resultCompletion.ToString();
        Console.WriteLine(stringResult);
        Logger.SaveToLogFile(stringResult);
    }
}