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
        //public List<CustomItemContentDescriptor> Descriptors { get { return this.GetModel.descriptors; } set { this.GetModel.descriptors = value; } }

        #region Descriptors
        //public void AddDescriptor(CustomItemContentDescriptor descriptor)
        //{
        //    if (descriptor == null) return;
        //    Descriptors.Add(descriptor);
        //}

        //public CustomItemContentDescriptor? GetItemDescriptor(string ID)
        //{
        //    return Descriptors.Find(x => x.attachedId == ID);
        //}

        private void BindItemDescriptors(List<CustomItemContentDescriptor> descriptors)
        {
            foreach (var weapon in Weapons)
            {
                weapon.SetDescriptor(descriptors.Find(x => x.attachedId == weapon.ID));
            }
        }

        public List<CustomItemContentDescriptor> GetDescriptors()
        {
            List<CustomItemContentDescriptor> localDesc = new List<CustomItemContentDescriptor>();
            var weaponsDescriptors = Weapons.Select(x => x.GetDescriptor()).ToList();
            var meleeDescriptors = Melee.Select(x => x.GetDescriptor()).ToList();

            localDesc.AddRange(weaponsDescriptors);
            localDesc.AddRange(meleeDescriptors);
            return localDesc;
        }

        #endregion

        #region Weapons
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

        // Melee
        public ObservableCollection<MeleeViewModel> Melee
        {
            get
            {
                return GetModel.MeleeList;
            }
            set
            {
                GetModel.MeleeList = value;
                RaisePropertyChanged();
            }
        }
        public void AddMelee(MeleeViewModel weapon)
        {
            if (weapon == null) return;
            Melee.Add(weapon);
        }

        public void RemoveMelee(MeleeViewModel weapon)
        {
            if (weapon == null) return;
            Melee.Remove(weapon);
        }

        public void LoadNew(ModDataViewModel replacement)
        {
            this.Weapons.Clear();
            this.Melee.Clear();
            replacement.Weapons.ToList().ForEach(this.Weapons.Add);
            replacement.Melee.ToList().ForEach(this.Melee.Add);
        }

        public void ClearMod()
        {
            this.Weapons.Clear();
            this.Melee.Clear();
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