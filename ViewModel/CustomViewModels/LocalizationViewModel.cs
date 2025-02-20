using MGSC;
using QM_ItemCreatorTool.Properties;
using QM_ItemCreatorTool.ViewModel;
using System.Collections.ObjectModel;
using System.Security.Policy;

namespace QM_ItemCreatorTool.Model;
public class LocalizationViewModel : ViewModelBase
{
    public LocalizationViewModel()
    {

    }

    public void LoadDefaults()
    {
        var languages = Enum.GetValues(typeof(MGSC.Localization.Lang));
        foreach (Localization.Lang language in languages)
        {
            var a = new CustomLanguageDictionary(language);
            Entries.Add(a);
        }
    }

    private string _id = string.Empty;
    public string ID
    {
        get { return _id; }
        set { _id = value; RaisePropertyChanged(); }
    }

    public string TableKey { get; set; } = "item";
    public ObservableCollection<CustomLanguageDictionary> Entries { get; set; } = new ObservableCollection<CustomLanguageDictionary>();
}