using MGSC;
using QM_ItemCreatorTool.Managers;
using QM_ItemCreatorTool.Model;
using Spellweaver.Commands;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace QM_ItemCreatorTool.ViewModel
{
    public class WeaponTabViewModel : ViewModelBase
    {
        #region Data
        private DataProviderManager _dataProvider;
        #endregion

        public WeaponTabViewModel(DataProviderManager dataProvider)
        {
            _dataProvider = dataProvider;

            WeaponClassList = _dataProvider.WeaponClasses;
            WeaponSubclassList = _dataProvider.WeaponSubclasses;
            GripTypesList = _dataProvider.GripTypes;

            // Commands
            AddWeaponToListCommand = new DelegateCommand(CreateNew, CanExecuteCommand);
            RemoveWeaponFromListCommand = new DelegateCommand(Remove, CanExecuteCommand);
            CreateNewWeaponCommand = new DelegateCommand(CreateNew, CanExecuteCommand);
        }

        #region Selected Weapon

        private WeaponViewModel? _selectedWeapon;
        public WeaponViewModel? SelectedWeapon
        {
            get
            {
                return _selectedWeapon;
            }
            set
            {
                _selectedWeapon = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Collections
        public ModDataViewModel CurrentMod
        {
            get => ModInstanceManager.CurrentMod;
            set
            {
                ModInstanceManager.CurrentMod = value;
                RaisePropertyChanged();
            }
        }

        private ICollectionView _weaponView;
        public ICollectionView WeaponView
        {
            get
            {
                return _weaponView;
            }
            set
            {
                _weaponView = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<WeaponViewModel> WeaponList { get { return CurrentMod.Weapons; } }

        public List<string> WeaponClassList { get; set; } = new List<string>();
        public List<string> WeaponSubclassList { get; set; } = new List<string>();
        public List<string> GripTypesList { get; set; } = new List<string>();
        #endregion

        #region Commands

        public DelegateCommand AddWeaponToListCommand { get; }
        public DelegateCommand RemoveWeaponFromListCommand { get; }
        public DelegateCommand CreateNewWeaponCommand { get; }

        #region Commands Implementation

        private void Add(object? parameter)
        {
            if (SelectedWeapon == null) return;
            //_modInstanceManager.CurrentMod.AddWeapon();
            SelectedWeapon = new WeaponViewModel(new QM_WeaponImporter.WeaponTemplate());
        }

        private void Remove(object? parameter)
        {
            if (SelectedWeapon == null) return;
            var indexOfWeapon = WeaponList.IndexOf(SelectedWeapon) - 1;
            ModInstanceManager.CurrentMod.RemoveWeapon(SelectedWeapon);
            if (indexOfWeapon >= 0)
                SelectedWeapon = WeaponList[indexOfWeapon];
            else
                SelectedWeapon = WeaponList.FirstOrDefault();
        }

        private void CreateNew(object? parameter)
        {
            var newWeapon = new WeaponViewModel(new QM_WeaponImporter.WeaponTemplate());
            ModInstanceManager.CurrentMod.AddWeapon(newWeapon);
            SelectedWeapon = newWeapon;
        }

        private bool CanExecuteCommand(object? obj) => ModInstanceManager.CurrentMod != null;

        #endregion

        #endregion

        public override Task LoadAsync()
        {
            return Task.CompletedTask;
        }
    }
}
