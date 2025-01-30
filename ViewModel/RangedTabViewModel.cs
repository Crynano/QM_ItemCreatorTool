using QM_ItemCreatorTool.Commands;
using QM_ItemCreatorTool.Interfaces;
using QM_ItemCreatorTool.Managers;
using QM_WeaponImporter;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.ViewModel
{
    public class RangedTabViewModel : TabViewModel<RangedViewModel>
    {
        public RangedTabViewModel(DataProviderManager dataProvider)
        {
            _dataProvider = dataProvider;

            WeaponClassList = _dataProvider.WeaponClasses;
            WeaponSubclassList = _dataProvider.WeaponSubclasses;
            GripTypesList = _dataProvider.GripTypes;
            FactionList = _dataProvider.Factions;

            _dataProvider.FireModes.ForEach(FireModesList.Add);
            _dataProvider.Categories.ForEach(Tags.Add);
            _dataProvider.Grenades.ForEach(GrenadesList.Add);
            _dataProvider.Chips.ForEach(ChipList.Add);

            // Commands
            AddCommand = new DelegateCommand(Add, CanExecuteCommand);
            RemoveCommand = new DelegateCommand(Remove, CanExecuteCommand);

            // Images
            SpritePathCommand = new DelegateCommand(GetPathForImage);
            SmallSpritePathCommand = new DelegateCommand(GetPathForSmallImage);
            ShadowSpritePathCommand = new DelegateCommand(GetPathForShadowImage);

            // Sounds
            GetAttackSoundPathCommand = new DelegateCommand(GetPathForAttackSound);
            GetReloadSoundPathCommand = new DelegateCommand(GetPathForReloadSound);
            GetDrySoundPathCommand = new DelegateCommand(GetPathForDrySound);
            GetFailedSoundPathCommand = new DelegateCommand(GetPathForFailedSound);

            // Faction
            AddFactionEntryCommand = new DelegateCommand(AddFactionEntry);
            // Uncrafting
            AddUncraftingEntryCommand = new DelegateCommand(AddUncraftingEntry);
        }


        #region Collections
        public ObservableCollection<RangedViewModel> WeaponList { get { return CurrentMod.Weapons; } }
        public List<string> WeaponClassList { get; set; }
        public List<string> WeaponSubclassList { get; set; }
        public List<string> GripTypesList { get; set; }
        public List<string> FactionList { get; set; }
        public ObservableCollection<string> FireModesList { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> Tags { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> GrenadesList { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> ChipList { get; set; } = new ObservableCollection<string>();
        #endregion

        #region Commands
        public DelegateCommand AddFactionEntryCommand { get; }
        public DelegateCommand AddUncraftingEntryCommand { get; }
        public DelegateCommand SpritePathCommand { get; }
        public DelegateCommand SmallSpritePathCommand { get; }
        public DelegateCommand ShadowSpritePathCommand { get; }
        public DelegateCommand GetAttackSoundPathCommand { get; }
        public DelegateCommand GetReloadSoundPathCommand { get; }
        public DelegateCommand GetDrySoundPathCommand { get; }
        public DelegateCommand GetFailedSoundPathCommand { get; }

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

        private void GetPathForReloadSound(object? obj)
        {
            var path = GetPath("Select a sound file", "Sound files (*.wav)|*.wav");
            if (path != null && CurrentValue != null) CurrentValue.ReloadSoundPath = path;
        }
        private void GetPathForAttackSound(object? obj)
        {
            var path = GetPath("Select a sound file", "Sound files (*.wav)|*.wav");
            if (path != null && CurrentValue != null) CurrentValue.AttackSoundPath = path;
        }
        private void GetPathForFailedSound(object? obj)
        {
            var path = GetPath("Select a sound file", "Sound files (*.wav)|*.wav");
            if (path != null && CurrentValue != null) CurrentValue.FailedAttackSoundPath = path;
        }
        private void GetPathForDrySound(object? obj)
        {
            var path = GetPath("Select a sound file", "Sound files (*.wav)|*.wav");
            if (path != null && CurrentValue != null) CurrentValue.DryShotSoundPath = path;
        }

        // For the path searching
        private string? GetPath(string title, string extension)
        {
            return FolderExplorerManager.GetPathToFile(title, extension);
        }

        private void AddFactionEntry(object? obj)
        {
            // Add faction Entry
            ((IFactionData)CurrentValue).AddFactionRule();
        }
        private void AddUncraftingEntry(object? obj)
        {
            // Add faction Entry
            ((ICraftData)CurrentValue).AddCraftEntry();
        }
        #endregion

        #endregion

        public override Task LoadAsync()
        {
            return Task.CompletedTask;
        }
    }
}
