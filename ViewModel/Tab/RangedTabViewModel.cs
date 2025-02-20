using QM_ItemCreatorTool.Managers;
using QM_WeaponImporter;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.ViewModel
{
    public class RangedTabViewModel : WeaponTabViewModel<RangedViewModel, RangedWeaponTemplate>
    {
        public ObservableCollection<RangedViewModel> WeaponList { get { return CurrentMod.Weapons; } }

        public RangedTabViewModel(DataProviderManager dataProvider) : base(dataProvider)
        {

        }

        #region Commands Implementation
        protected override void Add(object? parameter)
        {
            var newWeapon = new RangedViewModel(new RangedWeaponTemplate());
            CurrentMod.AddItemToList(newWeapon);
            CurrentValue = newWeapon;
        }

        protected override void Remove(object? parameter)
        {
            if (CurrentValue == null) return;
            var indexOfWeapon = WeaponList.IndexOf(CurrentValue) - 1;
            CurrentMod.RemoveItemFromList(CurrentValue);
            if (indexOfWeapon >= 0)
                CurrentValue = WeaponList[indexOfWeapon];
            else
                CurrentValue = WeaponList.FirstOrDefault();
        }
        #endregion 
    }
}