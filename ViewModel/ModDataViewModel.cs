using QM_ItemCreatorTool.Model;
using QM_WeaponImporter;
using QM_WeaponImporter.Templates;
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

        public ConfigTemplate Configuration { get => this.GetModel.config; set => this.GetModel.config = value; }
        public List<CustomItemContentDescriptor> Descriptors { get { return this.GetModel.descriptors; } set { this.GetModel.descriptors = value; } }

        #region Descriptors
        public void AddDescriptor(CustomItemContentDescriptor descriptor)
        {
            if (descriptor == null) return;
            Descriptors.Add(descriptor);
        }

        public CustomItemContentDescriptor? GetItemDescriptor(string ID)
        {
            return Descriptors.Find(x => x.attachedId == ID);
        }

        private void BindItemDescriptors()
        {
            foreach (var weapon in Weapons)
            {
                weapon.SetDescriptor(this.GetItemDescriptor(weapon.ID));
            }
        }
        #endregion

        #region Weapons

        //private ObservableCollection<WeaponViewModel> _weaponList = new ObservableCollection<WeaponViewModel>();
        //public ObservableCollection<WeaponViewModel> Weapons
        //{
        //    get
        //    {
        //        return _weaponList;
        //    }
        //    set
        //    {
        //        _weaponList = value;
        //        RaisePropertyChanged();
        //    }
        //}
        public ObservableCollection<WeaponViewModel> Weapons
        {
            get
            {
                return GetModel.WeaponList;
            }
            set
            {
                GetModel.WeaponList = value;
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
            replacement.Descriptors.ToList().ForEach(this.Descriptors.Add);
            BindItemDescriptors();
        }

        public void ClearMod()
        {
            this.Weapons.Clear();
            this.Descriptors.Clear();
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