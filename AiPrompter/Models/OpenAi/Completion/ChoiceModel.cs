using Newtonsoft.Json;

namespace AiPrompter.Models.OpenAi.Completion;

public class ChoiceModel
{
    public int Index { get; set; }

    public MessageModel Message { get; set; }

    [JsonProperty("finish_reason")]
    public string FinishReason { get; set; }
}