using QM_ItemCreatorTool.Model;
using QM_ItemCreatorTool.Properties;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.ViewModel;
public class ArmorViewModel : ViewModelBase<ResistTemplateModel>
{
    public ArmorViewModel()
    {

    }

    public void LoadDefaults(List<string> resists)
    {
        foreach (string resist in resists)
        {
            var a = new CustomDamageRecordDictionary(resist);
            Entries.Add(a);
        }
    }

    private string _id = string.Empty;
    public string ID
    {
        get { return _id; }
        set { _id = value; RaisePropertyChanged(); }
    }

    public string ArmorSlot { get; set; } = string.Empty;
    public ObservableCollection<CustomDamageRecordDictionary> Entries { get; set; } = new ObservableCollection<CustomDamageRecordDictionary>();
}
