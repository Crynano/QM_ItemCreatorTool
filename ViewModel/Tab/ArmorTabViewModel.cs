using QM_ItemCreatorTool.Commands;
using QM_ItemCreatorTool.Managers;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.ViewModel;
public class ArmorTabViewModel : TabViewModel<ArmorViewModel>
{
    public ArmorTabViewModel(DataProviderManager dataProvider)
    {
        this.DataProvider = dataProvider;

        AddCommand = new DelegateCommand(Add);
        RemoveCommand = new DelegateCommand(Remove);
    }

    #region Collections
    public List<string> ArmorSlots { get => DataProvider.ArmorSlots; }
    public ObservableCollection<ArmorViewModel> Armors
    {
        get { return CurrentMod.Armors; }
    }
    #endregion
    protected override void Add(object? obj)
    {
        if (CurrentMod != null)
        {
            var newLocEntry = new ArmorViewModel();
            newLocEntry.LoadDefaults(DataProvider.Resistances);
            CurrentMod.AddItemToList(newLocEntry);
            CurrentValue = Armors.LastOrDefault();
        }
    }

    protected override void Remove(object? obj)
    {
        if (CurrentMod != null && CurrentValue != null)
        {
            CurrentMod.RemoveItemFromList(CurrentValue);
            CurrentValue = Armors.LastOrDefault();
        }
    }
}