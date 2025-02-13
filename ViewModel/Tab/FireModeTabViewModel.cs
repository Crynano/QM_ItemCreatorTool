using QM_ItemCreatorTool.Commands;
using QM_ItemCreatorTool.Managers;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.ViewModel
{
    public class FireModeTabViewModel : TabViewModel<FireModeViewModel>
    {
        public FireModeTabViewModel(DataProviderManager dataProvider)
        {
            DataProvider = dataProvider;

            // Commands
            AddCommand = new DelegateCommand(Add, CanExecuteCommand);
            RemoveCommand = new DelegateCommand(Remove, CanExecuteCommand);

            // Images
            SpritePathCommand = new DelegateCommand(GetPathForImage);
        }

        #region Collections
        public ObservableCollection<FireModeViewModel> FireModeList { get { return CurrentMod.FireModes; } }
        #endregion

        #region Commands
        public DelegateCommand SpritePathCommand { get; }

        #region Commands Implementation
        protected override void Add(object? parameter)
        {
            var item = new FireModeViewModel();
            CurrentMod.AddItemToList(item);
            CurrentValue = item;
        }

        protected override void Remove(object? parameter)
        {
            if (CurrentValue == null) return;
            var indexOfWeapon = FireModeList.IndexOf(CurrentValue) - 1;
            CurrentMod.RemoveItemFromList(CurrentValue);
            if (indexOfWeapon >= 0)
                CurrentValue = FireModeList[indexOfWeapon];
            else
                CurrentValue = FireModeList.FirstOrDefault();
        }

        private bool CanExecuteCommand(object? obj) => ModInstanceManager.CurrentMod != null;

        private void GetPathForImage(object? parameter)
        {
            var path = GetPath("Select an image", "Image files (*.jpg, *.png)|*.png;*.jpg|All files (*.*)|*.*");
            if (path != null && CurrentValue != null) CurrentValue.SpritePath = path;
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
