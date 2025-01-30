﻿using QM_ItemCreatorTool.Interfaces;
using QM_ItemCreatorTool.Model;
using QM_ItemCreatorTool.ViewModel;
using System.IO;

namespace QM_ItemCreatorTool.Managers
{
    public class UserConfigAndSaviourManager
    {
        private string AppFolder => Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "QM_ItemCreatorTool");

        private string LastMod => Path.Combine(AppFolder, "lastMod.json");
        private IErrorHandler _errorHandler = new MessageBoxErrorHandler();

        public UserConfigAndSaviourManager()
        {

        }

        public void Initialize()
        {
            CreateDefaultFolder();
            App.Current.MainWindow.Closed += Save;
            Load();
        }

        private void CreateDefaultFolder()
        {
            if (!Directory.Exists(AppFolder))
                Directory.CreateDirectory(AppFolder);
        }

        public void Load()
        {
            LoadLastData();
        }

        private bool LoadLastData()
        {
            if (!File.Exists(LastMod)) return false;
            try
            {
                var deserializedMod = FileImporter.LoadAndDeserialize<ModDataModel>(LastMod);
                if (deserializedMod == null) return false;
                deserializedMod.LoadFromDeserialize();

                if (deserializedMod == null) return false;
                var viewModel = new ModDataViewModel(deserializedMod);
                ModInstanceManager.SetMod(viewModel);
                return true;
            }
            catch (Exception ex)
            {
                _errorHandler.ThrowError("Error when loading last saved data.", ex);
                return false;
            }
        }

        public void Save(object? sender, EventArgs e)
        {
            var data = ModInstanceManager.CurrentMod.GetModel;
            data.PrepareExport();
            FileImporter.SaveAndSerialize(LastMod, data);
        }
    }
}
