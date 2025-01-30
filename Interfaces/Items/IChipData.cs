using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.Interfaces;
public interface IChipData
{
    public ObservableCollection<string> Chips { get; set; }
}