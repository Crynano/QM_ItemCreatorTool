using MGSC;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QM_ItemCreatorTool.Properties;

[Serializable]
public class CustomItemQuantity : INotifyPropertyChanged
{
    private string _itemID = string.Empty;
    public string ItemId
    {
        get { return _itemID; }
        set
        {
            _itemID = value; RaisePropertyChanged();
        }
    }
    private int _count = 0;
    public int Count
    {
        get { return _count; }
        set
        {
            _count = value; RaisePropertyChanged();
        }
    }

    public CustomItemQuantity() { }
    public CustomItemQuantity(ItemQuantity itemQuant)
    {
        ItemId = itemQuant.ItemId;
        Count = itemQuant.Count;
    }
    public CustomItemQuantity(string text, int count)
    {
        ItemId = text;
        Count = count;
    }

    public ItemQuantity GetOriginal()
    {
        return new ItemQuantity(ItemId, Count);
    }

    public KeyValuePair<string, int> GetDictionary()
    {
        return new KeyValuePair<string, int>(ItemId, Count);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        try
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.WriteLine("Property failed was: " + propertyName);
        }
    }
}
