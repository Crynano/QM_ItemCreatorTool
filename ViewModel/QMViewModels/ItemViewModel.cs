using MGSC;
using QM_WeaponImporter.Templates;

namespace QM_ItemCreatorTool.ViewModel;
public class ItemViewModel<T> : BasePickupItemViewModel<T> where T : ItemRecordTemplate, new()
{
    public ItemViewModel()
    {

    }
    public ItemViewModel(T item) : base(item)
    {

    }

    public List<string> Categories
    {
        get => _model.categories;
        set { _model.categories = value; RaisePropertyChanged(); }
    }

    public int TechLevel
    {
        get => _model.techLevel;
        set { _model.techLevel = value; RaisePropertyChanged(); }
    }

    public float Price
    {
        get => _model.price;
        set { _model.price = value; RaisePropertyChanged(); }
    }

    public float Weight
    {
        get => _model.weight;
        set { _model.weight = value; RaisePropertyChanged(); }
    }

    public int InventoryWidth
    {
        get => _model.inventoryWidthSize;
        set { _model.inventoryWidthSize = value; RaisePropertyChanged(); }
    }

    public ItemClass ItemClass
    {
        get => _model.itemClass;
        set { _model.itemClass = value; RaisePropertyChanged(); }
    }
}