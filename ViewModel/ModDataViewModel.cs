using QM_ItemCreatorTool.Model;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.ViewModel
{
    // this class is the connection between the model and the viewmodel
    public class ModDataViewModel : ViewModelBase<ModDataModel>
    {
        public ModDataViewModel(ModDataModel model) : base(model)
        {
            // Leave empty

        }

        // Now we can set properties.
        #region Weapons
        private ObservableCollection<WeaponViewModel> _weaponList = new ObservableCollection<WeaponViewModel>();
        public ObservableCollection<WeaponViewModel> Weapons
        {
            get
            {
                return _weaponList;
            }
            set
            {
                _weaponList = value;
                RaisePropertyChanged();
            }
        }

        public void AddWeapon(WeaponViewModel weapon)
        {
            if (weapon == null) return;
            Weapons.Add(weapon);
        }

        public void RemoveWeapon(WeaponViewModel weapon)
        {
            if (weapon == null) return;
            Weapons.Remove(weapon);
        }

        public void LoadNew(ModDataViewModel replacement)
        {
            this.Weapons.Clear();
            replacement.Weapons.ToList().ForEach(this.Weapons.Add);
        }

        #endregion

        public string Name
        {
            get => _model.Name;
            set
            {
                _model.Name = value;
                RaisePropertyChanged();
            }
        }


    }
}
