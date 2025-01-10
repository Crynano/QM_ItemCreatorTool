﻿using QM_ItemCreatorTool.Interfaces;
using QM_ItemCreatorTool.Managers;
using QM_ItemCreatorTool.Model;
using Spellweaver.Commands;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.ViewModel;

public class GeneralTabViewModel : ViewModelBase
{
    private IErrorHandler _errorHandler;
    private ModInstanceManager _modInstanceManager;
	public GeneralTabViewModel(IErrorHandler errorHandler, ModInstanceManager modInstanceManager)
	{
        _errorHandler = errorHandler;
        _modInstanceManager = modInstanceManager;

        // Commands
        SelectRootFolderCommand = new DelegateCommand(StoreRootPath);
        CreateModCommand = new DelegateCommand(CreateMod);
        LoadModCommand = new DelegateCommand(LoadMod);
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

    public ObservableCollection<ModDataViewModel> ModList
    {
        get => _modInstanceManager.ModCollection;
    }

    public ModDataViewModel SelectedMod
    {
        get => _modInstanceManager.CurrentMod;
        set
        {
            _modInstanceManager.CurrentMod = value;
            RaisePropertyChanged();
        }
    }

    #endregion

    #region Commands

    public DelegateCommand SelectRootFolderCommand { get; set; }
    public DelegateCommand CreateModCommand { get; set; }
    public DelegateCommand LoadModCommand { get; set; }

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
        var result = FolderExplorerManager.GetPathToFolder();
        if (result == null)
        {
            // Error handler?
            Exception ex = new Exception("Invalid path to folder was selected", new NullReferenceException());
            _errorHandler.ThrowError("Error upon importing root path", ex);
            return;
        }
        // Call API to create mod using the data provided.
        // TODO FinalBoss!
    }

    private void LoadMod(object? obj)
    {
        var result = FolderExplorerManager.GetPathToFile("Load mod config file", "Json Files (*.json)|*.json");
        if (result == null)
        {
            // Error handler?
            //Exception ex = new Exception("Invalid path to folder was selected", new NullReferenceException());
            //_errorHandler.ThrowError("Error upon importing root path", ex);
            return;
        }
        _modInstanceManager.LoadNewMod(result);
        // Load weapons atm.
    }

    #endregion
}