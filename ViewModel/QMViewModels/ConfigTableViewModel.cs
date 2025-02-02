using MGSC;
using QM_WeaponImporter.Templates;

namespace QM_ItemCreatorTool.ViewModel;
public class ConfigTableViewModel<T> : ViewModelBase<T> where T : ConfigTableRecordTemplate, new()
{
    protected CustomItemContentDescriptor itemDescriptor = new CustomItemContentDescriptor();

    public ConfigTableViewModel()
	{

	}
    public ConfigTableViewModel(T item) : base(item)
    {

    }

    public string ID
    {
        get => _model.id;
        set
        {
            _model.id = value;
            itemDescriptor.attachedId = value;
            RaisePropertyChanged();
        }
    }

    public void SetDescriptor(CustomItemContentDescriptor? newDescriptor)
    {
        if (newDescriptor == null)
        {
            itemDescriptor = new CustomItemContentDescriptor();
            itemDescriptor.attachedId = ID;
            return;
        }
        itemDescriptor = newDescriptor;
    }
    public CustomItemContentDescriptor GetDescriptor()
    {
        return itemDescriptor;
    }
}
