using QM_ItemCreatorTool.Properties;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.Interfaces;
public interface IFactionData
{
    public ObservableCollection<FactionEntry> FactionRules { get; set; }

    public virtual void AddFactionRule()
    {
        if (FactionRules == null) return;
        FactionRules.Add(new FactionEntry());
    }

    public virtual void RemoveFactionRule(FactionEntry entry)
    {
        if (FactionRules == null) return;
        if (entry == null) return;
        FactionRules.Remove(entry);
    }
}