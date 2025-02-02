using QM_ItemCreatorTool.Commands;
using QM_ItemCreatorTool.Interfaces;
using QM_ItemCreatorTool.Managers;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.ViewModel
{
    public class AmmoTabViewModel : TabViewModel<AmmoViewModel>
    {
        public AmmoTabViewModel(DataProviderManager dataProvider)
        {
            DataProvider = dataProvider;


            FactionList = DataProvider.Factions;
            DataProvider.Categories.ForEach(Tags.Add);
            DataProvider.Chips.ForEach(ChipList.Add);

            // Commands
            AddCommand = new DelegateCommand(Add, CanExecuteCommand);
            RemoveCommand = new DelegateCommand(Remove, CanExecuteCommand);

            // Images
            SpritePathCommand = new DelegateCommand(GetPathForImage);
            SmallSpritePathCommand = new DelegateCommand(GetPathForSmallImage);
            ShadowSpritePathCommand = new DelegateCommand(GetPathForShadowImage);

            // Faction
            AddFactionEntryCommand = new DelegateCommand(AddFactionEntry);
            // Uncrafting
            AddUncraftingEntryCommand = new DelegateCommand(AddUncraftingEntry);
        }


        #region Collections
        public ObservableCollection<AmmoViewModel> AmmoList { get { return CurrentMod.Ammo; } }
        public List<string> FactionList { get; set; }
        public ObservableCollection<string> Tags { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> ChipList { get; set; } = new ObservableCollection<string>();
        #endregion

        #region Commands
        public DelegateCommand AddFactionEntryCommand { get; }
        public DelegateCommand AddUncraftingEntryCommand { get; }
        public DelegateCommand SpritePathCommand { get; }
        public DelegateCommand SmallSpritePathCommand { get; }
        public DelegateCommand ShadowSpritePathCommand { get; }

        #region Commands Implementation
        protected override void Add(object? parameter)
        {
            var ammoItem = new AmmoViewModel();
            CurrentMod.AddItemToList(ammoItem);
            CurrentValue = ammoItem;
        }

        protected override void Remove(object? parameter)
        {
            if (CurrentValue == null) return;
            var indexOfWeapon = AmmoList.IndexOf(CurrentValue) - 1;
            CurrentMod.RemoveItemFromList(CurrentValue);
            if (indexOfWeapon >= 0)
                CurrentValue = AmmoList[indexOfWeapon];
            else
                CurrentValue = AmmoList.FirstOrDefault();
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
