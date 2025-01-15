using QM_ItemCreatorTool.Model;
using QM_ItemCreatorTool.ViewModel;
using System.IO;

namespace QM_ItemCreatorTool.Managers
{
    public class UserConfigAndSaviourManager
    {
        private string AppFolder => Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "QM_ItemCreatorTool");

        private string LastMod => Path.Combine(AppFolder, "lastMod.json");


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

            var deserializedMod = FileImporter.LoadAndDeserialize<ModDataModel>(LastMod);
            deserializedMod.LoadFromDeserialize();

            if (deserializedMod == null) return false;
            var viewModel = new ModDataViewModel(deserializedMod);
            ModInstanceManager.SetMod(viewModel);
            return true;
        }

        public void Save(object? sender, EventArgs e)
        {
            var data = ModInstanceManager.CurrentMod.GetModel;
            data.PrepareExport();
            FileImporter.SaveAndDeserialize(LastMod, data);
        }
    }
}
