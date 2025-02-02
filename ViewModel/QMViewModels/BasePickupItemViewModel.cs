using QM_WeaponImporter.Templates;

namespace QM_ItemCreatorTool.ViewModel;
public class BasePickupItemViewModel<T> : ConfigTableViewModel<T> where T : BasePickupItemRecordTemplate, new()
{
    public BasePickupItemViewModel()
    {

    }
    public BasePickupItemViewModel(T item) : base(item)
    {

    }
    public string? InheritedID
    {
        get => itemDescriptor.baseItemId;
        set
        {
            itemDescriptor.baseItemId = value;
            RaisePropertyChanged();
        }
    }
    public string? SpritePath
    {
        get => itemDescriptor.iconSpritePath;
        set
        {
            itemDescriptor.iconSpritePath = value;
            RaisePropertyChanged();
        }
    }

    public string? SmallSpritePath
    {
        get => itemDescriptor.smallIconSpritePath;
        set
        {
            itemDescriptor.smallIconSpritePath = value;
            RaisePropertyChanged();
        }
    }

    public string? ShadowSpritePath
    {
        get => itemDescriptor.shadowOnFloorSpritePath;
        set
        {
            itemDescriptor.shadowOnFloorSpritePath = value;
            RaisePropertyChanged();
        }
    }
}
