using AutoCompleteTextBox.Editors;
using QM_ItemCreatorTool.Commands;
using QM_ItemCreatorTool.Interfaces;
using QM_ItemCreatorTool.Managers;
using QM_ItemCreatorTool.Providers;
using QM_WeaponImporter;
using System.Collections.ObjectModel;
using System.Windows;

namespace QM_ItemCreatorTool.ViewModel
{
    public class RangedTabViewModel : TabViewModel<RangedViewModel>
    {
        public RangedTabViewModel(DataProviderManager dataProvider)
        {
            DataProvider = dataProvider;

            FactionList = DataProvider.Factions;
            GripTypesList = DataProvider.GripTypes;
            WeaponClassList = DataProvider.WeaponClasses;
            WeaponSubclassList = DataProvider.WeaponSubclasses;

            DataProvider.Chips.ForEach(ChipList.Add);
            DataProvider.Categories.ForEach(Tags.Add);
            DataProvider.Grenades.ForEach(GrenadesList.Add);
            //_dataProvider.FireModes.ForEach(FireModesList.Add);

            // Commands
            AddCommand = new DelegateCommand(Add, CanExecuteCommand);
            RemoveCommand = new DelegateCommand(Remove, CanExecuteCommand);

            // Images
            SpritePathCommand = new DelegateCommand((obj) => { CurrentValue.SpritePath = GetImagePath(); });
            SmallSpritePathCommand = new DelegateCommand((obj) => { CurrentValue.SmallSpritePath = GetImagePath(); });
            ShadowSpritePathCommand = new DelegateCommand((obj) => { CurrentValue.ShadowSpritePath = GetImagePath(); });

            // Sounds
            GetAttackSoundPathCommand = new DelegateCommand((obj) => { CurrentValue.AttackSoundPath = GetSoundPath(); });
            GetReloadSoundPathCommand = new DelegateCommand((obj) => { CurrentValue.ReloadSoundPath = GetSoundPath(); });
            GetDrySoundPathCommand = new DelegateCommand((obj) => { CurrentValue.DryShotSoundPath = GetSoundPath(); });
            GetFailedSoundPathCommand = new DelegateCommand((obj) => { CurrentValue.FailedAttackSoundPath = GetSoundPath(); });

            // Faction
            AddFactionEntryCommand = new DelegateCommand(AddFactionEntry);
            // Uncrafting
            AddUncraftingEntryCommand = new DelegateCommand(AddUncraftingEntry);
            //Firemode
            AddFireModeOne = new DelegateCommand((obj) => { CurrentValue.FireModeOne = AddFireMode(CurrentValue.FireModeOne); });
            AddFireModeTwo = new DelegateCommand((obj) => { CurrentValue.FireModeTwo = AddFireMode(CurrentValue.FireModeTwo); });

            // Unity-related
            GetBundlePath = new DelegateCommand((obj) => { CurrentValue.BundlePath = GetGenericPath(); });
        }

        #region Properties
        private string _typedFireMode;

        public string TypedFireMode
        {
            get { return _typedFireMode; }
            set { _typedFireMode = value; }
        }

        #endregion

        #region Collections
        public ObservableCollection<RangedViewModel> WeaponList { get { return CurrentMod.Weapons; } }
        public List<string> WeaponClassList { get; set; }
        public List<string> WeaponSubclassList { get; set; }
        public List<string> GripTypesList { get; set; }
        public List<string> FactionList { get; set; }
        public BaseDataProvider<string> FireModeProvider { get => DataProvider.FireModeDataProvider; }
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
        public DelegateCommand AddFireModeOne { get; }
        public DelegateCommand AddFireModeTwo { get; }
        public DelegateCommand GetBundlePath { get; }
        public DelegateCommand GetPrefabPath { get; }
        public DelegateCommand GetTexturePath { get; }

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

        public string GetGenericPath()
        {
            var path = GetPath("Select a Unity AssetBundle", "All files (*.*)|*.*");
            if (path != null) return path;
            return string.Empty;
        }

        public string GetImagePath()
        {
            var path = GetPath("Select an Image file", "Image files (*.jpg, *.png)|*.png;*.jpg|All files (*.*)|*.*");
            if (path != null) return path;
            return string.Empty;
        }

        private string GetSoundPath()
        {
            var path = GetPath("Select a Sound file", "Sound files (*.wav)|*.wav");
            if (path != null) return path;
            return string.Empty;
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

        private string AddFireMode(string original)
        {
            if (!string.IsNullOrEmpty(TypedFireMode) && !FireModeProvider.Contains(TypedFireMode))
            {
                if (MessageBox.Show($"Firemode {TypedFireMode} isn't in the list. Do you want to add it?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    FireModeProvider.Add(TypedFireMode);
                    return TypedFireMode;
                }
            }
            return original;
        }
        #endregion

        #endregion

        public override Task LoadAsync()
        {
            return Task.CompletedTask;
        }
    }
}
