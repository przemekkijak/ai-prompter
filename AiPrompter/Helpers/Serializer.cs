namespace AiPrompter.Helpers;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

public class LowercaseContractResolver : DefaultContractResolver
{
    protected override string ResolvePropertyName(string propertyName)
    {
        return propertyName.ToLower();
    }
}

public static class Serializer
{
    public static string SerializeObject(object data)
    {
        return JsonConvert.SerializeObject(data, new JsonSerializerSettings()
        {
            ContractResolver = new LowercaseContractResolver()
        });
    }
}