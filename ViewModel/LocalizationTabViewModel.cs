using QM_ItemCreatorTool.Commands;
using QM_ItemCreatorTool.Managers;
using QM_ItemCreatorTool.Model;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.ViewModel;
public class LocalizationTabViewModel : TabViewModel<LocalizationViewModel>
{
    public LocalizationTabViewModel(DataProviderManager dataProvider)
    {
        this._dataProvider = dataProvider;

        AddCommand = new DelegateCommand(Add);
        RemoveCommand = new DelegateCommand(Remove);
    }

    #region Collections
    public ObservableCollection<LocalizationViewModel> LocalizationEntries 
    { 
        get { return CurrentMod.LocalizationEntries; }
    }
    #endregion
    protected override void Add(object? obj)
    {
        if (CurrentMod != null)
        {
            var newLocEntry = new LocalizationViewModel();
            newLocEntry.LoadDefaults();
            CurrentMod.AddLocalizationEntry(newLocEntry);
            CurrentValue = LocalizationEntries.LastOrDefault();
        }
    }

    protected override void Remove(object? obj)
    {
        if (CurrentMod != null)
        {
            CurrentMod.RemoveLocalizationEntry(CurrentValue);
            CurrentValue = LocalizationEntries.LastOrDefault();
        }
    }
}