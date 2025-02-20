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

    public static void SaveAndSerialize<T>(string folderPath, string fileNameAndExtension, T content) where T : class
    {
        Directory.CreateDirectory(folderPath);
        try
        {
            var fullPath = Path.Combine(folderPath, fileNameAndExtension);
            File.WriteAllText(fullPath, JsonConvert.SerializeObject(content, Formatting.Indented));
        }
        catch (Exception ex)
        {
            errorHandler.ThrowError($"Error upon serializing content", ex);
        }
    }

    public static void SaveAndSerialize<T>(string fullPath, T content) where T : class
    {
        try
        {
            File.WriteAllText(fullPath, JsonConvert.SerializeObject(content, Formatting.Indented));
        }
        catch (Exception ex)
        {
            errorHandler.ThrowError($"Error upon serializing content", ex);
        }
    }
}