namespace QM_ItemCreatorTool.Extensions
{
    public static class DictionaryExtension
    {
        public static void Add(this Dictionary<string, string> currentDictionary, KeyValuePair<string, string> newEntry)
        {
            currentDictionary.Add(newEntry.Key, newEntry.Value);
        }
    }
}