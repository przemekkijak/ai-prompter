namespace Gpt4All.Prediction;

public interface ITextPredictionStreamingResult : ITextPredictionResult
{
    IAsyncEnumerable<string> GetPredictionStreamingAsync(CancellationToken cancellationToken = default);
}
