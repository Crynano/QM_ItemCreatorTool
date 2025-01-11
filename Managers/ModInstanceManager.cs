using QM_ItemCreatorTool.Interfaces;
using QM_ItemCreatorTool.Model;
using QM_ItemCreatorTool.ViewModel;

namespace QM_ItemCreatorTool.Managers
{
    public static class ModInstanceManager
    {
        private static IMessageBoxHandler _adviceHandler = new ConfirmationManager();

        public static ModDataViewModel CurrentMod { get; set; } = new ModDataViewModel(new ModDataModel());

        public static void LoadNewMod(string configFilePath)
        {
            ModDataViewModel modDataViewModel = new ModDataViewModel(new ModDataModel());
            bool loadResult = ModDataManager.LoadMod(configFilePath, ref modDataViewModel);
            if (loadResult)
            {
                // Then store it.
                // Maybe trigger a warning that says it'll override the mod data.
                if (_adviceHandler.ThrowWarningConfirmation("Overriding existing data.",
                    "Are you sure you want to load a new mod and override existing data?" +
                    "\nAll unsaved data will be erased."))
                    CurrentMod.LoadNew(modDataViewModel);
            }
        }
    }
}