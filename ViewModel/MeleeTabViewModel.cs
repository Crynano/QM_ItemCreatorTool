using QM_ItemCreatorTool.Managers;
using QM_ItemCreatorTool.Model;
using QM_WeaponImporter;
using QM_ItemCreatorTool.Commands;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.ViewModel
{
    public class MeleeTabViewModel : TabViewModel<MeleeViewModel>
    {
        public MeleeTabViewModel(DataProviderManager dataProvider)
        {
            this._dataProvider = dataProvider;

            WeaponClassList = _dataProvider.WeaponClasses;
            WeaponSubclassList = _dataProvider.WeaponSubclasses;
            GripTypesList = _dataProvider.GripTypes;
            _dataProvider.Categories.ForEach(Tags.Add);

            // Commands
            AddCommand = new DelegateCommand(Add, CanExecuteCommand);
            RemoveCommand = new DelegateCommand(Remove, CanExecuteCommand);

            SpritePathCommand = new DelegateCommand(GetPathForImage);
            SmallSpritePathCommand = new DelegateCommand(GetPathForSmallImage);
            ShadowSpritePathCommand = new DelegateCommand(GetPathForShadowImage);
        }

        #region Selected Weapon


        #endregion

        #region Collections
        public ObservableCollection<MeleeViewModel> WeaponList { get { return CurrentMod.Melee; } }
        public List<string> WeaponClassList { get; set; }
        public List<string> WeaponSubclassList { get; set; }
        public List<string> GripTypesList { get; set; }
        public ObservableCollection<string> Tags { get; set; } = new ObservableCollection<string>();
        #endregion

        #region Commands
        public DelegateCommand SpritePathCommand { get; }
        public DelegateCommand SmallSpritePathCommand { get; }
        public DelegateCommand ShadowSpritePathCommand { get; }

        #region Commands Implementation
        protected override void Add(object? parameter)
        {
            var newWeapon = new MeleeViewModel(new MeleeWeaponTemplate());
            ModInstanceManager.CurrentMod.AddMelee(newWeapon);
            CurrentValue = newWeapon;
        }
        protected override void Remove(object? parameter)
        {
            if (CurrentValue == null) return;
            var indexOfWeapon = WeaponList.IndexOf(CurrentValue) - 1;
            ModInstanceManager.CurrentMod.RemoveMelee(CurrentValue);
            if (indexOfWeapon >= 0)
                CurrentValue = WeaponList[indexOfWeapon];
            else
                CurrentValue = WeaponList.FirstOrDefault();
        }

        private bool CanExecuteCommand(object? obj) => ModInstanceManager.CurrentMod != null;

        private void GetPathForImage(object? parameter)
        {
            var path = GetPath("Select an image", "Image files (*.jpg, *.png)|*.png;*.jpg|All files (*.*)|*.*");
            if (path != null && CurrentValue != null) CurrentValue.SpritePath = path;
        }

        private void GetPathForSmallImage(object? parameter)
        {
            var path = GetPath("Select an image", "Image files (*.jpg, *.png)|*.png;*.jpg|All files (*.*)|*.*");
            if (path != null && CurrentValue != null) CurrentValue.SmallSpritePath = path;
        }

        private void GetPathForShadowImage(object? parameter)
        {
            var path = GetPath("Select an image", "Image files (*.jpg, *.png)|*.png;*.jpg|All files (*.*)|*.*");
            if (path != null && CurrentValue != null) CurrentValue.ShadowSpritePath = path;
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
