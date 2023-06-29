using Newtonsoft.Json;

namespace AiPrompter.Models.OpenAi.Moderation;

public class ModerationCategoriesModel
{
    public bool Hate { get; set; }

    [JsonProperty("hate/threatening")] 
    public bool HateThreatening { get; set; }

    [JsonProperty("self-harm")] 
    public bool SelfHarm { get; set; }

    public bool Sexual { get; set; }

    [JsonProperty("sexual/minors")] 
    public bool SexualMinors { get; set; }

    public bool Violence { get; set; }

    [JsonProperty("violence/graphic")] 
    public bool ViolenceGraphic { get; set; }
}