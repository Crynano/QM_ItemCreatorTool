using QM_ItemCreatorTool.Commands;
using QM_ItemCreatorTool.Managers;
using QM_ItemCreatorTool.Model;
using QM_ItemCreatorTool.Properties;
using System.Collections.ObjectModel;
using QM_WeaponImporter.Templates;

namespace QM_ItemCreatorTool.ViewModel;
public class ItemReceiptTabViewModel : TabViewModel<ItemProduceViewModel>
{
    public ItemReceiptTabViewModel(DataProviderManager dataProvider)
    {
        this.DataProvider = dataProvider;

        AddIngredient = new DelegateCommand(CreateNewEntry);
        AddItemGrade = new DelegateCommand(AddGradeEntry);

        AddCommand = new DelegateCommand(Add);
        RemoveCommand = new DelegateCommand(Remove);
    }


    #region Collections
    public ObservableCollection<ItemProduceViewModel> ItemReceipts { get { return CurrentMod.ItemReceipts; } }
    #endregion

    #region Commands
    public DelegateCommand AddIngredient { get; set; }
    public DelegateCommand AddItemGrade { get; set; }
    private void AddGradeEntry(object? obj)
    {
        if (CurrentValue == null) return;
        CurrentValue.ModifyItemsGrades.Add(new CustomItemQuantity(string.Empty, 1));
    }
    private void CreateNewEntry(object? obj)
    {
        if (CurrentValue == null) return;
        CurrentValue.RequiredItems.Add(new CustomItemQuantity(string.Empty, 1));
    }
    protected override void Add(object? obj)
    {
        var newValue = new ItemProduceViewModel(new ItemProduceReceiptTemplate());
        CurrentMod.AddItemToList(newValue);
        CurrentValue = newValue;
    }
    protected override void Remove(object? parameter)
    {
        if (CurrentValue == null) return;

        var indexOfWeapon = ItemReceipts.IndexOf(CurrentValue) - 1;
        CurrentMod.RemoveItemFromList(CurrentValue);

        if (indexOfWeapon >= 0)
            CurrentValue = ItemReceipts[indexOfWeapon];
        else
            CurrentValue = ItemReceipts.FirstOrDefault();
    }
    #endregion
}