using QM_ItemCreatorTool.Commands;
using QM_ItemCreatorTool.Managers;

namespace QM_ItemCreatorTool.ViewModel;
public abstract class TabViewModel<T> : ViewModelBase where T : class
{
    protected DataProviderManager DataProvider { get; set; }

    #region Properties
    protected T? _currentValue;
    public T? CurrentValue
    {
        get { return _currentValue; }
        set { _currentValue = value; RaisePropertyChanged(); }
    }

    public ModDataViewModel CurrentMod
    {
        get => ModInstanceManager.CurrentMod;
        set
        {
            ModInstanceManager.CurrentMod = value;
            RaisePropertyChanged();
        }
    }
    #endregion

    #region Commands
    public DelegateCommand AddCommand { get; set; }
    public DelegateCommand RemoveCommand { get; set; }
    #endregion

    #region Commands Implementation
    protected abstract void Add(object? obj);
    protected abstract void Remove(object? obj);
    #endregion
}