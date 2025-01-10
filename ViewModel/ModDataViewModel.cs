using QM_ItemCreatorTool.Model;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace QM_ItemCreatorTool.ViewModel
{
    // this class is the connection between the model and the viewmodel
    public class ModDataViewModel : ViewModelBase<ModDataModel>
    {
        private object _lockObject = new object();
        public ModDataViewModel(ModDataModel model) : base(model)
        {
            // Leave empty
            BindingOperations.EnableCollectionSynchronization(_model.WeaponList, _lockObject);
        }

        // Now we can set properties.
        #region Weapons
        public ObservableCollection<WeaponViewModel> Weapons
        {
            get
            {
                lock(_model.WeaponList)
                {
                    return _model.WeaponList;
                }
            }
        }

        public void AddWeapon(WeaponViewModel weapon)
        {
            if (weapon == null) return;
            lock (_lockObject)
            {
                Weapons.Add(weapon);
            }
        }

        public void RemoveWeapon(WeaponViewModel weapon)
        {
            if (weapon == null) return;
            lock (_lockObject)
            {
                Weapons.Remove(weapon);
            }
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
