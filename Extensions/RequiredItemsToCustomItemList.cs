using MGSC;
using QM_ItemCreatorTool.Properties;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.Extensions;
public static class RequiredItemsToCustomItemList
{
    public static ObservableCollection<CustomItemQuantity> ToCustomList(this IEnumerable<ItemQuantity> originalList)
    {
        var result = new ObservableCollection<CustomItemQuantity>();
        if (originalList != null && originalList.Count() > 0)
            originalList.ToList().ForEach(item => result.Add(new CustomItemQuantity(item)));
        return result;
    }
    //public static List<ItemQuantity> FromCustomList(this ObservableCollection<CustomItemQuantity> originalList)
    //{
    //    var result = new List<ItemQuantity>();
    //    originalList.ToList().ForEach(item => result.Add(new ItemQuantity(item.ItemId, item.Count)));
    //    return result;
    //}
    public static List<ItemQuantity> FromCustomList(this List<CustomItemQuantity> originalList)
    {
        var result = new List<ItemQuantity>();
        originalList.ToList().ForEach(item => result.Add(new ItemQuantity(item.ItemId, item.Count)));
        return result;
    }
    public static ItemQuantity ToItemQuantity(this CustomItemQuantity original)
    {
        var result = new ItemQuantity(original.ItemId, original.Count);
        return result;
    }
}