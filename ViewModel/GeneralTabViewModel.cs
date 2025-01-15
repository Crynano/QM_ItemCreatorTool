using QM_ItemCreatorTool.Interfaces;
using QM_ItemCreatorTool.Managers;
using Spellweaver.Commands;

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
        SelectRootFolderCommand = new DelegateCommand(StoreRootPath);
        CreateModCommand = new DelegateCommand(CreateMod);
        LoadModCommand = new DelegateCommand(LoadMod);
        ClearModCommand = new DelegateCommand(ClearMod);
    }

    #region Properties

    private string? _configFilePath;
    public string? ConfigFilePath
    {
        get => _configFilePath;
        set
        {
            _configFilePath = value;
            RaisePropertyChanged();
        }
    }

    #endregion

    #region Commands

    public DelegateCommand SelectRootFolderCommand { get; set; }
    public DelegateCommand CreateModCommand { get; set; }
    public DelegateCommand LoadModCommand { get; set; }
    public DelegateCommand ClearModCommand { get; set; }

    private void StoreRootPath(object? obj)
    {
        var result = FolderExplorerManager.GetPathToFolder();
        if (result == null)
        {
            // Error handler?
            Exception ex = new Exception("Invalid path to folder was selected", new NullReferenceException());
            _errorHandler.ThrowError("Error upon importing root path", ex);
            return;
        }
        ConfigFilePath = result;
    }

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