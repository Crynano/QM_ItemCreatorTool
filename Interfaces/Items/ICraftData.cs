using MGSC;
using QM_ItemCreatorTool.Properties;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.Interfaces;
public interface ICraftData
{
    public ObservableCollection<CustomItemQuantity> DisassemblyList { get; set; }

    public void AddCraftEntry()
    {
        DisassemblyList.Add(new CustomItemQuantity());
    }

    public ItemTransformationRecord GetItemTransformationRecord();
}