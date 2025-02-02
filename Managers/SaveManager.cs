using QM_ItemCreatorTool.Interfaces;
using QM_ItemCreatorTool.Model;
using QM_ItemCreatorTool.ViewModel;
using System.IO;

namespace QM_ItemCreatorTool.Managers
{
    public class SaveManager
    {
        private string AppFolder => Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "QM_ItemCreatorTool");

        private string LastMod => Path.Combine(AppFolder, "lastMod.json");
        private IErrorHandler _errorHandler = new MessageBoxErrorHandler();

        public SaveManager()
        {

        }

        public void Initialize()
        {
            CreateDefaultFolder();
            App.Current.MainWindow.Closed += SaveOnExit;
            LoadLastData();
        }

        private void CreateDefaultFolder()
        {
            if (!Directory.Exists(AppFolder))
                Directory.CreateDirectory(AppFolder);
        }

        #region Load
        private void LoadLastData()
        {
            Load(LastMod);
        }

        public bool Load(string path)
        {
            if (!File.Exists(path)) return false;
            try
            {
                var deserializedMod = FileImporter.LoadAndDeserialize<ModDataModel>(path);
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
        #endregion

        #region Save
        private void SaveOnExit(object? sender, EventArgs e)
        {
            Save(LastMod);
        }

        public void Save(string path)
        {
            var data = ModInstanceManager.CurrentMod.GetModel;
            data.PrepareExport();
            FileImporter.SaveAndSerialize(path, data);
        }
        #endregion
    }
}
