using MGSC;
using QM_ItemCreatorTool.Properties;
using QM_ItemCreatorTool.ViewModel;
using QM_WeaponImporter.Templates;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.Model;
public class ItemProduceViewModel : ViewModelBase<ItemProduceReceiptTemplate>
{
    public ItemProduceViewModel() : base() { OutputItemId = "default_id"; }
    public ItemProduceViewModel(ItemProduceReceiptTemplate model) : base(model)
    {
        OutputItemId = model.OutputItem;
    }

    public string OutputItemId
    {
        get { return _model.OutputItem; }
        set { _model.OutputItem = value; RaisePropertyChanged(); }
    }
    public float ProductionTime
    {
        get { return _model.ProduceTimeInHours; }
        set { _model.ProduceTimeInHours = value; RaisePropertyChanged(); }
    }
    public int ModifyStartCost
    {
        get { return _model.ModifyStartCost; }
        set { _model.ModifyStartCost = value; RaisePropertyChanged(); }
    }
    public float ModifyStep
    {
        get { return _model.ModifyStep; }
        set { _model.ModifyStep = value; RaisePropertyChanged(); }
    }
    public int ModifyLevelLimit
    {
        get { return _model.ModifyLevelLimit; }
        set { _model.ModifyLevelLimit = value; RaisePropertyChanged(); }
    }
    public ObservableCollection<CustomItemQuantity> RequiredItems { get; set; } = new ObservableCollection<CustomItemQuantity>();
    public ObservableCollection<CustomItemQuantity> ModifyItemsGrades {  get; set; } = new ObservableCollection<CustomItemQuantity>();

    // Let's have something that returns the example one.
    // Used when exporting this shiet
    public void PrepareExport()
    {
        _model.RequiredItems = RequiredItems.ToList().Select(x => x.GetOriginal()).ToList();
        _model.ModifyItemsGrades = ModifyItemsGrades.ToList().Select(x => x.GetDictionary()).ToDictionary();
    }
    public ItemProduceReceipt GetExportable()
    {
        var original = (ItemProduceReceipt)GetModel;
        original.RequiredItems = RequiredItems.ToList().Select(x => x.GetOriginal()).ToList();
        original.ModifyItemsGrades = ModifyItemsGrades.ToList().Select(x => x.GetDictionary()).ToDictionary();
        return original;
    }
}
