using Newtonsoft.Json;
using QM_ItemCreatorTool.Interfaces;
using QM_ItemCreatorTool.Model;
using QM_ItemCreatorTool.ViewModel;
using QM_WeaponImporter;
using QM_WeaponImporter.Templates;
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

            ConfigTemplate? configFile = JsonConvert.DeserializeObject<ConfigTemplate>(fileContents);
            if (configFile == null || string.IsNullOrEmpty(configFile.descriptorsPath))
            {
                NullReferenceException ex = new NullReferenceException("Mod config file could not be deserialized.\nPlease check there aren't any errors and the file is a valid JSON format.");
                _errorHandler.ThrowError("Error during mod config parsing", ex);
                return false;
            }
            modifiedModData.Configuration = configFile;
            List<CustomItemContentDescriptor> localDescriptors = new List<CustomItemContentDescriptor>();
            // Load descriptors first of all!
            string? descriptorsRelativePath = configFile.descriptorsPath;
            if (!string.IsNullOrEmpty(descriptorsRelativePath))
            {
                var descriptorsFolderPath = System.IO.Path.Combine(configPath.Replace(Path.GetFileName(configPath), string.Empty), descriptorsRelativePath);
                var descriptorsFiles = Directory.GetFiles(descriptorsFolderPath);
                string operationLog = string.Empty;
                foreach (var descriptor in descriptorsFiles)
                {
                    // Read the file first...
                    string? fileContent = File.ReadAllText(descriptor);
                    if (string.IsNullOrEmpty(fileContent))
                    {
                        operationLog += "Empty file? " + descriptor + "\n";
                        continue;
                    }
                    // Load as the model
                    CustomItemContentDescriptor? singleDescriptor = JsonConvert.DeserializeObject<CustomItemContentDescriptor>(fileContent);
                    if (singleDescriptor == null)
                    {
                        operationLog += "Could not be parsed. " + descriptor + "\n";
                        continue;
                    }
                    // Transform into our viewmodel
                    // Then store
                    localDescriptors.Add(singleDescriptor);
                }
                if (operationLog != string.Empty)
                    _errorHandler.ThrowWarning("Descriptor Loading Issue", $"The following data could not be loaded:\n{operationLog}");
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
                    RangedWeaponTemplate? singleWeapon = JsonConvert.DeserializeObject<RangedWeaponTemplate>(fileContent);
                    if (singleWeapon == null)
                    {
                        operationLog += "Could not be parsed. " + weaponFile + "\n";
                        continue;
                    }
                    // Transform into our viewmodel
                    // Then store
                    WeaponViewModel viewModel = new WeaponViewModel(singleWeapon);
                    viewModel.SetDescriptor(localDescriptors.Find(x => x.attachedId.Equals(viewModel.ID)));
                    modifiedModData.AddWeapon(viewModel);
                }
                if (operationLog != string.Empty)
                    _errorHandler.ThrowWarning("Weapon Loading Issue", $"The following weapons could not be loaded:\n{operationLog}");
            }
            modifiedModData.Name = configPath;
            return true;
        }

        private static string CreateModReport = string.Empty;

        public static bool CreateMod(string folderPath, ModDataViewModel modifiedModData)
        {
            CreateModReport = string.Empty;
            // Just, print stuff lol.
            if (!Directory.Exists(folderPath))
            {
                DirectoryNotFoundException ex = new DirectoryNotFoundException($"Directory does not exist.\nPath: {folderPath}");
                _errorHandler.ThrowError("Error when creating mod.", ex);
                return false;
            }

            string baseDirectory = folderPath;
            // Attention this process with overwrite all existing config files in this folder.
            //!!!! TODO ADD WARNING !!!!
            /// Print the mastermind first
            // Deserialize the mastermind
            var configDeserialized = JsonConvert.SerializeObject(modifiedModData.Configuration, Formatting.Indented);
            File.WriteAllText(Path.Combine(baseDirectory, "global_config.json"), configDeserialized);

            // Now, create all foldersystem
            foreach (var item in modifiedModData.Configuration.folderPaths.Values)
            {
                Directory.CreateDirectory(Path.Combine(baseDirectory, item));
            }
            // T he localization file must be a json, not a folder! Dum dum
            //foreach (var item in modifiedModData.Configuration.localizationPaths.Values)
            //{
            //    Directory.CreateDirectory(Path.Combine(baseDirectory, item));
            //}
            var fullDescriptorsPath = Path.Combine(baseDirectory, modifiedModData.Configuration.descriptorsPath);
            Directory.CreateDirectory(fullDescriptorsPath);

            // DONE?
            // NO!
            // WRITE DEM WEAPONS YOU BASTARD!
            modifiedModData.Configuration.folderPaths.TryGetValue("rangedweapons", out string? rangedWeaponsRelativePath);
            foreach (var item in modifiedModData.Weapons)
            {
                string serializedWeapon = JsonConvert.SerializeObject(item.GetModel, Formatting.Indented);
                File.WriteAllText(Path.Combine(baseDirectory, rangedWeaponsRelativePath, item.ID + ".json"), serializedWeapon);
            }

            string descriptorsRelativePath = modifiedModData.Configuration.descriptorsPath;
            // Before deserializing, change the paths to be relative ones and include the file name!
            // 
            // There's 4 properties that need copying
            // First try the sound.

            foreach (var item in modifiedModData.GetDescriptors())
            {
                item.iconSpritePath = CopyFileToRelative(item.iconSpritePath, baseDirectory, "Assets/Images");
                item.smallIconSpritePath = CopyFileToRelative(item.smallIconSpritePath, baseDirectory, "Assets/Images");
                item.shadowOnFloorSpritePath = CopyFileToRelative(item.shadowOnFloorSpritePath, baseDirectory, "Assets/Images");
                // Sounds
                item.shootSoundPath = CopyFileToRelative(item.shootSoundPath, baseDirectory, "Assets/Sounds");
                item.dryShotSoundPath = CopyFileToRelative(item.dryShotSoundPath, baseDirectory, "Assets/Sounds");
                item.failedAttackSoundPath = CopyFileToRelative(item.failedAttackSoundPath, baseDirectory, "Assets/Sounds");
                item.reloadSoundPath = CopyFileToRelative(item.reloadSoundPath, baseDirectory, "Assets/Sounds");
                // Finalize
                string serializedDesc = JsonConvert.SerializeObject(item, Formatting.Indented);
                File.WriteAllText(Path.Combine(baseDirectory, descriptorsRelativePath, item.attachedId + "_descriptor.json"), serializedDesc);
            }

            if (!string.IsNullOrEmpty(CreateModReport))
                _errorHandler.ThrowWarning("Errors occurred while creating mod", CreateModReport);

            return true;
        }

        private static string CopyFileToRelative(string objectPath, string baseDirectory, string relativeDestinationFolderPath)
        {
            // This is to not alter IDs or bs
            if (!File.Exists(objectPath))
            {
                // Path to file does not exist.
                CreateModReport += $"File at {objectPath} does not exist. Setting full path on properties.";
                return objectPath;
            }
            if (Path.IsPathRooted(objectPath))
            {
                // Then we copy the file and transform to absolute, then reassign
                var fileExtension = Path.GetExtension(objectPath);
                var fileName = Path.GetFileName(objectPath); // Let's use the same fileName, no need to change or copy if there's repeated ones
                var folderPath = Path.Combine(baseDirectory, relativeDestinationFolderPath);
                var fullItemPath = Path.Combine(baseDirectory, relativeDestinationFolderPath, fileName);
                // Just in case
                Directory.CreateDirectory(folderPath);
                File.Copy(objectPath, fullItemPath, true);
                return Path.Combine(relativeDestinationFolderPath, fileName);
            }
            return objectPath;
        }
    }
}