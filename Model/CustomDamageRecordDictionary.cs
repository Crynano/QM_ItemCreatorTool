using MGSC;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QM_ItemCreatorTool.Properties;
public class CustomDamageRecordDictionary : INotifyPropertyChanged
{
    private string _name = string.Empty;
    public string Name
    {
        get { return _name; }
        set { _name = value; RaisePropertyChanged(); }
    }

    private string _description = string.Empty;
    public string Value
    {
        get { return _description; }
        set { _description = value; RaisePropertyChanged(); }
    }

    public CustomDamageRecordDictionary() { }

    public CustomDamageRecordDictionary(string name)
    {
        this.Name = name;
    }

    public CustomDamageRecordDictionary(string name, string value)
    {
        this.Name = name;
        this.Value = value;
    }

    public KeyValuePair<string, string> ToDictionary()
    {
        return new KeyValuePair<string, string>(Name, Value);
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