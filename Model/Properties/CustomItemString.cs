using MGSC;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QM_ItemCreatorTool.Properties;
public class CustomStringDictionary : INotifyPropertyChanged
{
    private Localization.Lang _language = Localization.Lang.EnglishUS;
    public Localization.Lang Language { get => _language; set { _language = value; RaisePropertyChanged(); } }

    private string _name = string.Empty;
    public string Name
    {
        get { return _name; }
        set { _name = value; RaisePropertyChanged(); }
    }
    private string _description = string.Empty;
    public string Description
    {
        get { return _description; }
        set { _description = value; RaisePropertyChanged(); }
    }

    public CustomStringDictionary() { }

    public CustomStringDictionary(Localization.Lang language)
    {
        Language = language;
    }
    public CustomStringDictionary(string name, string desc, Localization.Lang language)
    {
        this.Name = name;
        this.Description = desc;
        Language = language;
    }

    public KeyValuePair<string, string> GetName()
    {
        return new KeyValuePair<string, string>(Language.ToString(), Name);
    }

    public KeyValuePair<string, string> GetDescription()
    {
        return new KeyValuePair<string, string>(Language.ToString(), Description);
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