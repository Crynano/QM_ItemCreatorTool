using QM_ItemCreatorTool.Managers;
using QM_WeaponImporter;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.ViewModel
{
    public class MeleeTabViewModel : WeaponTabViewModel<MeleeViewModel, MeleeWeaponTemplate>
    {
        public ObservableCollection<MeleeViewModel> WeaponList { get { return CurrentMod.Melee; } }

        public MeleeTabViewModel(DataProviderManager dataProvider) : base(dataProvider)
        {

        }

        #region Commands Implementation
        protected override void Add(object? parameter)
        {
            var newWeapon = new MeleeViewModel(new MeleeWeaponTemplate());
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