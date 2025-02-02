using AutoCompleteTextBox.Editors;
using QM_ItemCreatorTool.Interfaces;
using System.Collections;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.Providers;
public abstract class BaseDataProvider<T> : IDataProvider<T>, ISuggestionProvider where T : class
{
    protected readonly ObservableCollection<T> managedList;
    public BaseDataProvider(ref ObservableCollection<T> firemodes)
    {
        this.managedList = firemodes;

        if (managedList == null)
            managedList = new ObservableCollection<T>();

        GetData().ToList().ForEach(managedList.Add);
    }

    public void Add(T item)
    {
        if (item != null)
            managedList.Add(item);
    }

    public bool Contains(T item)
    {
        if (item == null) return false;
        return managedList.Contains(item);
    }

    public abstract IEnumerable<T> GetData();

    public abstract IEnumerable GetSuggestions(string filter);

}
