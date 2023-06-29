using System.Text;
using AiPrompter.Helpers;

namespace AiPrompter.Models.OpenAi.Moderation;

public class ModerationResultModel
{
    public ModerationCategoriesModel Categories { get; set; }

    public bool Flagged { get; set; }

    public void PrintResults(string prompt)
    {
        if (!Flagged)
            return;

        var sb = new StringBuilder();
        sb.AppendLine($"Prompt '{prompt}' is flagged for:");

        if (Categories.Hate)
            sb.AppendLine("Hate");

        if (Categories.HateThreatening)
            sb.AppendLine("Hate / Threatening");

        if (Categories.SelfHarm)
            sb.AppendLine("Self-Harm");

        if (Categories.Sexual)
            sb.AppendLine("Sexual");

        if (Categories.SexualMinors)
            sb.AppendLine("Sexual / Minors");

        if (Categories.Violence)
            sb.AppendLine("Violence");

        if (Categories.ViolenceGraphic)
            sb.AppendLine("Violence / Graphic");

        var results = sb.ToString();
        Console.WriteLine(results);
        Logger.SaveToLogFile(results);
    }
}