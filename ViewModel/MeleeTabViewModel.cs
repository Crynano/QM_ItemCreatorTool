using QM_ItemCreatorTool.Managers;
using QM_ItemCreatorTool.Model;
using QM_WeaponImporter;
using Spellweaver.Commands;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.ViewModel
{
    public class MeleeTabViewModel : ViewModelBase
    {
        #region Data
        private DataProviderManager _dataProvider;
        #endregion

        public MeleeTabViewModel(DataProviderManager dataProvider)
        {
            _dataProvider = dataProvider;

            WeaponClassList = _dataProvider.WeaponClasses;
            WeaponSubclassList = _dataProvider.WeaponSubclasses;
            GripTypesList = _dataProvider.GripTypes;
            _dataProvider.Categories.ForEach(Tags.Add);

            // Commands
            AddWeaponToListCommand = new DelegateCommand(CreateNew, CanExecuteCommand);
            RemoveWeaponFromListCommand = new DelegateCommand(Remove, CanExecuteCommand);
            CreateNewWeaponCommand = new DelegateCommand(CreateNew, CanExecuteCommand);
            SpritePathCommand = new DelegateCommand(GetPathForImage);
            SmallSpritePathCommand = new DelegateCommand(GetPathForSmallImage);
            ShadowSpritePathCommand = new DelegateCommand(GetPathForShadowImage);
        }

        #region Selected Weapon

        private MeleeViewModel? _selectedWeapon;
        public MeleeViewModel? SelectedWeapon
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

        public ObservableCollection<MeleeViewModel> WeaponList { get { return CurrentMod.Melee; } }
        public List<string> WeaponClassList { get; set; }
        public List<string> WeaponSubclassList { get; set; }
        public List<string> GripTypesList { get; set; }
        public ObservableCollection<string> Tags { get; set; } = new ObservableCollection<string>();
        #endregion

        #region Commands

        public DelegateCommand AddWeaponToListCommand { get; }
        public DelegateCommand RemoveWeaponFromListCommand { get; }
        public DelegateCommand CreateNewWeaponCommand { get; }
        public DelegateCommand SpritePathCommand { get; }
        public DelegateCommand SmallSpritePathCommand { get; }
        public DelegateCommand ShadowSpritePathCommand { get; }

        #region Commands Implementation

        private void Add(object? parameter)
        {
            if (SelectedWeapon == null) return;
            //_modInstanceManager.CurrentMod.AddWeapon();
            SelectedWeapon = new MeleeViewModel(new MeleeWeaponTemplate());
        }

        private void Remove(object? parameter)
        {
            if (SelectedWeapon == null) return;
            var indexOfWeapon = WeaponList.IndexOf(SelectedWeapon) - 1;
            ModInstanceManager.CurrentMod.RemoveMelee(SelectedWeapon);
            if (indexOfWeapon >= 0)
                SelectedWeapon = WeaponList[indexOfWeapon];
            else
                SelectedWeapon = WeaponList.FirstOrDefault();
        }

        private void CreateNew(object? parameter)
        {
            var newWeapon = new MeleeViewModel(new MeleeWeaponTemplate());
            ModInstanceManager.CurrentMod.AddMelee(newWeapon);
            SelectedWeapon = newWeapon;
        }

        private bool CanExecuteCommand(object? obj) => ModInstanceManager.CurrentMod != null;

        private void GetPathForImage(object? parameter)
        {
            var path = GetPath("Select an image", "Image files (*.jpg, *.png)|*.png;*.jpg|All files (*.*)|*.*");
            if (path != null && SelectedWeapon != null) SelectedWeapon.SpritePath = path;
        }

        private void GetPathForSmallImage(object? parameter)
        {
            var path = GetPath("Select an image", "Image files (*.jpg, *.png)|*.png;*.jpg|All files (*.*)|*.*");
            if (path != null && SelectedWeapon != null) SelectedWeapon.SmallSpritePath = path;
        }

        private void GetPathForShadowImage(object? parameter)
        {
            var path = GetPath("Select an image", "Image files (*.jpg, *.png)|*.png;*.jpg|All files (*.*)|*.*");
            if (path != null && SelectedWeapon != null) SelectedWeapon.ShadowSpritePath = path;
        }
        // For the path searching
        private string? GetPath(string title, string extension)
        {
            return FolderExplorerManager.GetPathToFile(title, extension);
        }

        #endregion

        #endregion

        public override Task LoadAsync()
        {
            return Task.CompletedTask;
        }
    }
}
