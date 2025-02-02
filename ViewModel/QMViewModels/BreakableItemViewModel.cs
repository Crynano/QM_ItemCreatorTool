using QM_WeaponImporter.Templates;

namespace QM_ItemCreatorTool.ViewModel;
public class BreakableItemViewModel<T> : ItemViewModel<T> where T : BreakableItemRecordTemplate, new()
{
    public BreakableItemViewModel()
    {

    }
    public BreakableItemViewModel(T item) : base(item) 
    {

    }

    public string RepairCategory
    {
        get => _model.repairCategory;
        set { _model.repairCategory = value; RaisePropertyChanged(); }
    }
    public int Durability
    {
        get => _model.maxDurability;
        set { _model.maxDurability = value; RaisePropertyChanged(); }
    }

    public int MinDurabilityAfterRepair
    {
        get => _model.minDurabilityAfterRepair;
        set { _model.minDurabilityAfterRepair = value; RaisePropertyChanged(); }
    }

    public bool Unbreakable
    {
        get => _model.unbreakable;
        set { _model.unbreakable = value; RaisePropertyChanged(); }
    }
}