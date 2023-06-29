namespace AiPrompter.Helpers;

public static class Logger
{
    public static void SaveToLogFile(string output)
    {
        const string fileName = "logs.txt";
        var filePath = $"{fileName}";
        File.AppendAllText(filePath, $"-------------------------------------- \n{output}");
    }
}