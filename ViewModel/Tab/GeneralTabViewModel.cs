using QM_ItemCreatorTool.Interfaces;
using QM_ItemCreatorTool.Managers;
using QM_ItemCreatorTool.Commands;

namespace QM_ItemCreatorTool.ViewModel;

public class GeneralTabViewModel : ViewModelBase
{
    private IErrorHandler _errorHandler;
    private IMessageBoxHandler _messageBoxHandler;
    public GeneralTabViewModel(IErrorHandler errorHandler, IMessageBoxHandler messageBoxHandler)
    {
        _errorHandler = errorHandler;
        _messageBoxHandler = messageBoxHandler;

        // Commands
        CreateModCommand = new DelegateCommand(CreateMod);
        LoadModCommand = new DelegateCommand(LoadMod);
        ClearModCommand = new DelegateCommand(ClearMod);
    }

    #region Properties

    public string LoadModInfo { get; set; } = 
        "Use the Load Mod button to import a mod.\n" +
        "The file that must be selected is the global_config.json that a mod includes in its root folder.\n" +
        "NOTE: This action will override all current data in this app, including custom items, localization and so on.";
    public string CreateModInfo { get; set; } =
        "Use the Create Mod button to create a mod.\n" +
        "First, a Folder directory will open, where you will select the folder where the mod will be stored.\n" +
        "Then, a global config json and the mod folder structure will be created in that folder.\n" +
        "NOTE: All files already existing in the mod folder will be overwritten, but none will be deleted.\n" +
        "This means that you can create a mod on top of an existing one to add or overwrite desired items.";
    public string ClearModDataInfo { get; set; } =
        "Use the Clear Mod Data button to CLEAR all items created in this app.\n" +
        "Please note that this action will be irreversible.\n" +
        "Mod author and colleagues take no responsibility for data lost using this app or function.";

    #endregion

    #region Commands

    public DelegateCommand CreateModCommand { get; set; }
    public DelegateCommand LoadModCommand { get; set; }
    public DelegateCommand ClearModCommand { get; set; }

    private void CreateMod(object? obj)
    {
        var selectedPath = FolderExplorerManager.GetPathToFolder();
        if (selectedPath == null)
        {
            // Error handler?
            //Exception ex = new Exception("Invalid path to folder was selected", new NullReferenceException());
            //_errorHandler.ThrowError("Error upon importing root path", ex);
            return;
        }
        // Call API to create mod using the data provided.
        ModInstanceManager.CreateMod(selectedPath);
    }

    private void LoadMod(object? obj)
    {
        var selectedPath = FolderExplorerManager.GetPathToFile("Load mod config file", "Json Files (*.json)|*.json");
        if (selectedPath == null)
        {
            // Error handler?
            //Exception ex = new Exception("Invalid path to folder was selected", new NullReferenceException());
            //_errorHandler.ThrowError("Error upon importing root path", ex);
            return;
        }
        ModInstanceManager.LoadNewMod(selectedPath);
        // Load weapons atm.
    }

    private void ClearMod(object? obj)
    {
        var result = _messageBoxHandler.ThrowWarningConfirmation("Clear Mod Data", "You are about to clear all data.\n\nDo you want to continue?");
        if (result)
        {
            // Error handler?
            //Exception ex = new Exception("Invalid path to folder was selected", new NullReferenceException());
            //_errorHandler.ThrowError("Error upon importing root path", ex);
            ModInstanceManager.ClearMod();
            return;
        }
        // Call API to create mod using the data provided.
    }

    #endregion
}