using QM_ItemCreatorTool.Commands;
using QM_ItemCreatorTool.Interfaces;
using QM_ItemCreatorTool.Managers;
using System.IO;

namespace QM_ItemCreatorTool.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region ViewModels
        public RangedTabViewModel RangedTabViewModel { get; set; }
        public GeneralTabViewModel GeneralTabViewModel { get; set; }
        public MeleeTabViewModel MeleeTabViewModel { get; set; }
        public ItemReceiptTabViewModel ItemReceiptTabViewModel { get; set; }
        public LocalizationTabViewModel LocalizationTabViewModel { get; set; }
        public AmmoTabViewModel AmmoTabViewModel { get; set; }
        public FireModeTabViewModel FireModeTabViewModel { get; set; }
        #endregion

        #region Commands
        public DelegateCommand SaveProjectCommand {get;}
        public DelegateCommand LoadProjectCommand {get;}
        public DelegateCommand OpenGithubCommand {get;}
        public DelegateCommand OpenCreditsCommand {get;}
        #endregion

        private IMessageBoxHandler boxMessageManager;

        private SaveManager saveManager;
        public MainViewModel(
            RangedTabViewModel rangedViewModel,
            MeleeTabViewModel meleeTabViewModel,
            GeneralTabViewModel generalTabViewModel,
            ItemReceiptTabViewModel itemReceiptTabViewModel,
            LocalizationTabViewModel localizationTabViewModel,
            AmmoTabViewModel ammoTabViewModel,
            FireModeTabViewModel fireModeTabViewModel,
            IMessageBoxHandler boxMessager)
        {
            saveManager = new SaveManager();

            RangedTabViewModel = rangedViewModel;
            GeneralTabViewModel = generalTabViewModel;
            MeleeTabViewModel = meleeTabViewModel;
            ItemReceiptTabViewModel = itemReceiptTabViewModel;
            LocalizationTabViewModel = localizationTabViewModel;
            AmmoTabViewModel = ammoTabViewModel;
            FireModeTabViewModel = fireModeTabViewModel;

            // Message box
            boxMessageManager = boxMessager;

            // Commands
            SaveProjectCommand = new DelegateCommand(SaveProject);
            LoadProjectCommand = new DelegateCommand(LoadProject);
            OpenCreditsCommand = new DelegateCommand(OpenCredits);
            OpenGithubCommand = new DelegateCommand(OpenGit);
        }

        public void SaveProject(object? obj)
        {
            var path = FolderExplorerManager.SaveFileDialog("Save Project");
            if (path == null) return;
            saveManager.Save(path);
        }

        public void LoadProject(object? obj)
        {
            var path = FolderExplorerManager.GetPathToFile("Load Project");
            if (path == null) return;
            saveManager.Load(path);
        }

        public void OpenCredits(object? obj)
        {
            boxMessageManager.ThrowInfo("Credits", "UI, Programming and Design by Crynano.\n\nSpecial thanks to Lynchantiure and Raigir for testing and support");
        }

        public void OpenGit(object? obj)
        {
            // Huh
            boxMessageManager.ThrowInfo("GitHub Link", "https://github.com/Crynano/QM_ItemCreatorTool");
        }

        public override async Task LoadAsync()
        {
            saveManager.Initialize();
        }
    }
}