using Newtonsoft.Json;
using QM_ItemCreatorTool.Interfaces;
using System.IO;

namespace QM_ItemCreatorTool.Managers;
internal static class FileImporter
{
    private static IErrorHandler errorHandler = new MessageBoxErrorHandler();
    public static T? LoadAndDeserialize<T>(string path) where T : class
    {
        try
        {
            if (!File.Exists(path)) return null;
            var rawText = File.ReadAllText(path);
            T? result = JsonConvert.DeserializeObject<T>(rawText);
            return result;
        }
        catch (Exception ex)
        {
            errorHandler.ThrowError("Error when deserializing object", ex);
            return null;
        }
    }

    public static void SaveAndSerialize<T>(string path, T content) where T : class
    {
        try
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(content, Formatting.Indented));
        }
        catch (Exception ex)
        {
            errorHandler.ThrowError($"Error upon serializing content", ex);
        }
    }
}