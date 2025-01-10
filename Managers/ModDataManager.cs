using Newtonsoft.Json;
using QM_ItemCreatorTool.Interfaces;
using QM_ItemCreatorTool.Model;
using QM_ItemCreatorTool.ViewModel;
using QM_WeaponImporter;
using System.IO;

namespace QM_ItemCreatorTool.Managers
{
    // This library handles all data loading and storing.
    // It should use the Lite API for creating and using stuff...but at your own discretion...
    public static class ModDataManager
    {
        // Static and DI? still don't know how it works fine.
        private static IErrorHandler _errorHandler = new MessageBoxErrorHandler();
        public static bool LoadMod(string configPath, ref ModDataViewModel modifiedModData)
        {
            if (!File.Exists(configPath))
            {
                FileNotFoundException ex = new FileNotFoundException($"File the following path does not exist.\nPath: {configPath}");
                _errorHandler.ThrowError("Error when loading mod config file", ex);
                return false;
            }

            // Read the config file
            // Parse as a config file
            // Read the folders and return a valid structure

            var fileContents = File.ReadAllText(configPath);

            ConfigTemplate? configFile = JsonConvert.DeserializeObject<QM_WeaponImporter.ConfigTemplate>(fileContents);
            if (configFile == null || string.IsNullOrEmpty(configFile.descriptorsPath))
            {
                NullReferenceException ex = new NullReferenceException("Mod config file could not be deserialized.\nPlease check there aren't any errors and the file is a valid JSON format.");
                _errorHandler.ThrowError("Error during mod config parsing", ex);
                return false;
            }

            // First test! Success!
            configFile.folderPaths.TryGetValue("rangedweapons", out string? rangedWeaponsRelativePath);
            if (!string.IsNullOrEmpty(rangedWeaponsRelativePath))
            {
                // Get the data from the weapons
                // Parse them and add them to our database
                // Get the file name and filter
                var weaponsFolderPath = System.IO.Path.Combine(configPath.Replace(Path.GetFileName(configPath), string.Empty), rangedWeaponsRelativePath);
                var weaponFiles = Directory.GetFiles(weaponsFolderPath);
                string operationLog = string.Empty;
                foreach (var weaponFile in weaponFiles)
                {
                    // Read the file first...
                    string? fileContent = File.ReadAllText(weaponFile);
                    if (string.IsNullOrEmpty(fileContent))
                    {
                        operationLog += "Empty file? " + weaponFile + "\n";
                        continue;
                    }
                    // Load as the model
                    RangedWeaponTemplate? singleWeapon = JsonConvert.DeserializeObject<QM_WeaponImporter.RangedWeaponTemplate>(fileContent);
                    if (singleWeapon == null)
                    {
                        operationLog += "Could not be parsed. " + weaponFile + "\n";
                        continue;
                    }
                    // Transform into our viewmodel
                    // Then store
                    WeaponViewModel viewModel = new WeaponViewModel(singleWeapon);
                    modifiedModData.Weapons.Add(viewModel);
                }
                if (operationLog != string.Empty)
                    _errorHandler.ThrowWarning("Weapon Loading Issue", $"The following weapons could not be loaded:\n{operationLog}");
            }
            modifiedModData.Name = configPath;
            return true;
        }
    }
}