﻿using MGSC;
using Newtonsoft.Json;
using QM_ItemCreatorTool.Interfaces;
using QM_ItemCreatorTool.Model;
using QM_ItemCreatorTool.ViewModel;
using QM_WeaponImporter;
using QM_WeaponImporter.Templates;
using System.IO;

namespace QM_ItemCreatorTool.Managers
{
    public static class ModDataExporter
    {
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
            string? folderRelativePath = configFile.descriptorsPath;
            if (!string.IsNullOrEmpty(folderRelativePath))
            {
                var descriptorsFolderPath = System.IO.Path.Combine(configPath.Replace(Path.GetFileName(configPath), string.Empty), folderRelativePath);
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

            foreach (var localizationEntry in configFile.localizationPaths)
            {
                if (!string.IsNullOrEmpty(folderRelativePath))
                {
                    var folderPath = System.IO.Path.Combine(configPath.Replace(Path.GetFileName(configPath), string.Empty), Path.Combine(localizationEntry.Value, localizationEntry.Key));
                    if (Directory.Exists(folderPath))
                    {
                        var localizationFiles = Directory.GetFiles(folderPath);
                        string operationLog = string.Empty;
                        foreach (var file in localizationFiles)
                        {
                            string? fileContent = File.ReadAllText(file);
                            if (string.IsNullOrEmpty(fileContent))
                            {
                                operationLog += "Empty file? " + file + "\n";
                                continue;
                            }
                            LocalizationTemplate locFile = JsonConvert.DeserializeObject<LocalizationTemplate>(fileContent);
                            if (locFile == null)
                            {
                                operationLog += "Could not be parsed. " + file + "\n";
                                continue;
                            }
                            modifiedModData.SetLocalization(localizationEntry.Key, locFile);
                        }
                        if (operationLog != string.Empty)
                            _errorHandler.ThrowWarning("Weapon Loading Issue", $"The following weapons could not be loaded:\n{operationLog}");
                    }
                }
            }

            configFile.folderPaths.TryGetValue("rangedweapons", out folderRelativePath);
            if (!string.IsNullOrEmpty(folderRelativePath))
            {
                // Get the data from the weapons
                // Parse them and add them to our database
                // Get the file name and filter
                var weaponsFolderPath = System.IO.Path.Combine(configPath.Replace(Path.GetFileName(configPath), string.Empty), folderRelativePath);
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
                    RangedViewModel viewModel = new RangedViewModel(singleWeapon);
                    viewModel.SetDescriptor(localDescriptors.Find(x => x.attachedId.Equals(viewModel.ID)));
                    modifiedModData.AddItemToList(viewModel);
                }
                if (operationLog != string.Empty)
                    _errorHandler.ThrowWarning("Weapon Loading Issue", $"The following weapons could not be loaded:\n{operationLog}");
            }

            configFile.folderPaths.TryGetValue("meleeweapons", out folderRelativePath);
            if (!string.IsNullOrEmpty(folderRelativePath))
            {
                // Get the data from the weapons
                // Parse them and add them to our database
                // Get the file name and filter
                var weaponsFolderPath = System.IO.Path.Combine(configPath.Replace(Path.GetFileName(configPath), string.Empty), folderRelativePath);
                if (Directory.Exists(weaponsFolderPath))
                {
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
                        MeleeWeaponTemplate? singleWeapon = JsonConvert.DeserializeObject<MeleeWeaponTemplate>(fileContent);
                        if (singleWeapon == null)
                        {
                            operationLog += "Could not be parsed. " + weaponFile + "\n";
                            continue;
                        }
                        // Transform into our viewmodel
                        // Then store
                        var viewModel = new MeleeViewModel(singleWeapon);
                        viewModel.SetDescriptor(localDescriptors.Find(x => x.attachedId.Equals(viewModel.ID)));
                        modifiedModData.AddItemToList(viewModel);
                    }
                    if (operationLog != string.Empty)
                        _errorHandler.ThrowWarning("Weapon Loading Issue", $"The following weapons could not be loaded:\n{operationLog}");
                }
            }

            configFile.folderPaths.TryGetValue("ammo", out folderRelativePath);
            if (!string.IsNullOrEmpty(folderRelativePath))
            {
                var folderPath = System.IO.Path.Combine(configPath.Replace(Path.GetFileName(configPath), string.Empty), folderRelativePath);
                if (Directory.Exists(folderPath))
                {
                    var weaponFiles = Directory.GetFiles(folderPath);
                    string operationLog = string.Empty;
                    foreach (var weaponFile in weaponFiles)
                    {
                        string? fileContent = File.ReadAllText(weaponFile);
                        if (string.IsNullOrEmpty(fileContent))
                        {
                            operationLog += "Empty file? " + weaponFile + "\n";
                            continue;
                        }
                        // Load as the model
                        var template = JsonConvert.DeserializeObject<AmmoRecordTemplate>(fileContent);
                        if (template == null)
                        {
                            operationLog += "Could not be parsed. " + weaponFile + "\n";
                            continue;
                        }
                        // Transform into our viewmodel
                        // Then store
                        var viewModel = new AmmoViewModel(template);
                        viewModel.SetDescriptor(localDescriptors.Find(x => x.attachedId.Equals(viewModel.ID)));
                        modifiedModData.AddItemToList(viewModel);
                    }
                    if (operationLog != string.Empty)
                        _errorHandler.ThrowWarning("Weapon Loading Issue", $"The following weapons could not be loaded:\n{operationLog}");
                }
            }

            configFile.folderPaths.TryGetValue("factionconfig", out folderRelativePath);
            if (!string.IsNullOrEmpty(folderRelativePath))
            {
                var weaponsFolderPath = System.IO.Path.Combine(configPath.Replace(Path.GetFileName(configPath), string.Empty), folderRelativePath);
                var weaponFiles = Directory.GetFiles(weaponsFolderPath);
                string operationLog = string.Empty;
                foreach (var weaponFile in weaponFiles)
                {
                    string? fileContent = File.ReadAllText(weaponFile);
                    if (string.IsNullOrEmpty(fileContent))
                    {
                        operationLog += "Empty file? " + weaponFile + "\n";
                        continue;
                    }
                    // Load as the model
                    var template = JsonConvert.DeserializeObject<FactionTemplate>(fileContent);
                    if (template == null)
                    {
                        operationLog += "Could not be parsed. " + weaponFile + "\n";
                        continue;
                    }
                    // Transform into our viewmodel
                    // Then store
                    modifiedModData.SetFactionData(template);
                }
                if (operationLog != string.Empty)
                    _errorHandler.ThrowWarning("Weapon Loading Issue", $"The following weapons could not be loaded:\n{operationLog}");
            }

            configFile.folderPaths.TryGetValue("itemreceipts", out folderRelativePath);
            LoadGeneric<ItemProduceReceiptTemplate>(folderRelativePath, configPath, localDescriptors, ref modifiedModData);

            configFile.folderPaths.TryGetValue("itemtransforms", out folderRelativePath);
            LoadGeneric<ItemTransformationRecordTemplate>(folderRelativePath, configPath, localDescriptors, ref modifiedModData);

            configFile.folderPaths.TryGetValue("datadisks", out folderRelativePath);
            LoadGeneric<DatadiskRecordTemplate>(folderRelativePath, configPath, localDescriptors, ref modifiedModData);
            return true;
        }

        private static void LoadGeneric<T>(string folderRelativePath, string configPath,
            List<CustomItemContentDescriptor> localDescriptors, ref ModDataViewModel modifiedModData) where T : ConfigTableRecord
        {
            if (!string.IsNullOrEmpty(folderRelativePath))
            {
                // Get the data from the weapons
                // Parse them and add them to our database
                // Get the file name and filter
                var folderPath = System.IO.Path.Combine(configPath.Replace(Path.GetFileName(configPath), string.Empty), folderRelativePath);
                if (Directory.Exists(folderPath))
                {
                    var files = Directory.GetFiles(folderPath);
                    string operationLog = string.Empty;
                    foreach (var singleFile in files)
                    {
                        // Read the file first...
                        string? fileContent = File.ReadAllText(singleFile);
                        if (string.IsNullOrEmpty(fileContent))
                        {
                            operationLog += "Empty file? " + singleFile + "\n";
                            continue;
                        }
                        // Load as the model
                        T? template = JsonConvert.DeserializeObject<T>(fileContent);
                        if (template == null)
                        {
                            operationLog += "Could not be parsed. " + singleFile + "\n";
                            continue;
                        }
                        // Transform into our viewmodel
                        // Then store
                        object newItem = template;
                        switch (template)
                        {
                            // Depending on type?
                            case RangedWeaponTemplate ranged: newItem = new RangedViewModel(ranged); break;
                            case MeleeWeaponTemplate melee: newItem = new MeleeViewModel(melee); break;
                            case ItemProduceReceiptTemplate itemProduce: newItem = new ItemProduceViewModel(itemProduce); break;
                            case AmmoRecordTemplate ammoEntry: newItem = new AmmoViewModel(ammoEntry); break;
                        }
                        if (newItem is ConfigTableViewModel<WeaponTemplate> weaponItem)
                        {
                            weaponItem.SetDescriptor(localDescriptors.Find(x => x.attachedId.Equals(weaponItem.ID)));
                        }
                        modifiedModData.AddItemToList(newItem);
                    }
                    if (operationLog != string.Empty)
                        _errorHandler.ThrowWarning("Weapon Loading Issue", $"The following weapons could not be loaded:\n{operationLog}");
                }
                else
                {
                    _errorHandler.ThrowWarning("Weapon Loading Issue", $"Folder not found at {folderPath}");
                    return;
                }
            }
        }

        private static string CreateModReport = string.Empty;
        private static string BaseDirectory = string.Empty;
        private static Dictionary<string, string> FolderPaths = new Dictionary<string, string>();
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
            BaseDirectory = folderPath;
            FolderPaths = modifiedModData.Configuration.folderPaths;
            // Attention this process with overwrite all existing config files in this folder.
            //!!!! TODO ADD WARNING !!!!
            /// Print the mastermind first
            // Deserialize the mastermind
            FileImporter.SaveAndSerialize(Path.Combine(baseDirectory, "global_config.json"), modifiedModData.Configuration);

            // Now, create all foldersystem
            foreach (var item in modifiedModData.Configuration.folderPaths.Values)
            {
                Directory.CreateDirectory(Path.Combine(baseDirectory, item));
            }
            // The localization file must be a json, not a folder! Dum dum
            foreach (var item in modifiedModData.Configuration.localizationPaths.Values)
            {
                Directory.CreateDirectory(Path.Combine(baseDirectory, item));
            }
            // Descriptors
            var fullDescriptorsPath = Path.Combine(baseDirectory, modifiedModData.Configuration.descriptorsPath);
            Directory.CreateDirectory(fullDescriptorsPath);

            // Start deserializing data.
            string serializedItem = string.Empty;

            modifiedModData.Configuration.folderPaths.TryGetValue("rangedweapons", out string? rangedWeaponsRelativePath);
            foreach (var item in modifiedModData.Weapons)
            {
                FileImporter.SaveAndSerialize(Path.Combine(baseDirectory, rangedWeaponsRelativePath, item.ID + ".json"), item.GetModel);
            }

            modifiedModData.Configuration.folderPaths.TryGetValue("meleeweapons", out string? meleeWeaponsRelativePath);
            foreach (var item in modifiedModData.Melee)
            {
                FileImporter.SaveAndSerialize(Path.Combine(baseDirectory, meleeWeaponsRelativePath, item.ID + ".json"), item.GetModel);
            }

            //modifiedModData.Configuration.localizationPaths.TryGetValue("item", out string? localizationFilePath);
            //LocalizationTemplate localizationFile = new LocalizationTemplate();
            //localizationFile.name = new Dictionary<string, Dictionary<string, string>>();
            //localizationFile.shortdesc = new Dictionary<string, Dictionary<string, string>>();
            //foreach (LocalizationViewModel item in modifiedModData.LocalizationEntries)
            //{
            //    var currentItemEntries = item.Entries.ToList();

            //    var nameDictionary = currentItemEntries.Select(x => x.GetName()).ToDictionary();
            //    var descDictionary = currentItemEntries.Select(x => x.GetDescription()).ToDictionary();

            //    localizationFile.name.Add(item.ID, nameDictionary);
            //    localizationFile.shortdesc.Add(item.ID, descDictionary);
            //}
            foreach (var entry in modifiedModData.Configuration.localizationPaths)
            {
                var localizationFile = modifiedModData.GetLocalization(entry.Key);
                if (localizationFile == null) continue;
                FileImporter.SaveAndSerialize(Path.Combine(baseDirectory, entry.Value, $"{entry.Key}_localization.json"), localizationFile);
            }
            // Print the localization file

            string descriptorsRelativePath = modifiedModData.Configuration.descriptorsPath;
            // Before deserializing, change the paths to be relative ones and include the file name!
            foreach (var item in modifiedModData.GetDescriptors())
            {
                // Icons
                item.iconSpritePath = CopyFileToRelative(item.iconSpritePath, baseDirectory, "Assets/Images");
                item.smallIconSpritePath = CopyFileToRelative(item.smallIconSpritePath, baseDirectory, "Assets/Images");
                item.shadowOnFloorSpritePath = CopyFileToRelative(item.shadowOnFloorSpritePath, baseDirectory, "Assets/Images");
                // Sounds
                item.shootSoundPath = CopyFileToRelative(item.shootSoundPath, baseDirectory, "Assets/Sounds");
                item.dryShotSoundPath = CopyFileToRelative(item.dryShotSoundPath, baseDirectory, "Assets/Sounds");
                item.failedAttackSoundPath = CopyFileToRelative(item.failedAttackSoundPath, baseDirectory, "Assets/Sounds");
                item.reloadSoundPath = CopyFileToRelative(item.reloadSoundPath, baseDirectory, "Assets/Sounds");
                //Path and bundle
                item.bundlePath = CopyFileToRelative(item.bundlePath, baseDirectory, "Assets/Bundles");
                // Finalize
                FileImporter.SaveAndSerialize(Path.Combine(baseDirectory, descriptorsRelativePath, item.attachedId + "_descriptor.json"), item);
            }

            // Crafting specs
            modifiedModData.Configuration.folderPaths.TryGetValue("itemreceipts", out string? craftingSpecsPath);
            foreach (ItemProduceReceiptTemplate item in modifiedModData.GetCraftingSpecs())
            {
                FileImporter.SaveAndSerialize(Path.Combine(baseDirectory, craftingSpecsPath, item.OutputItem + "_receipt.json"), item);
            }

            modifiedModData.Configuration.folderPaths.TryGetValue("factionconfig", out string? factionPath);
            FileImporter.SaveAndSerialize(Path.Combine(baseDirectory, factionPath, "FactionData.json"), modifiedModData.GetFactionData());

            // This is for when destroying an object?
            ExportCategory(modifiedModData.GetItemTransforms(), "itemtransforms", "transformationData");
            ExportCategory(modifiedModData.GetDataDisks(), "datadisks", "diskData");
            ExportCustomCategory(modifiedModData.Ammo.Select(x => x.GetModel), "ammo", "ammoData");

            modifiedModData.Configuration.folderPaths.TryGetValue("firemodes", out string? fireModesRelativePath);
            foreach (var item in modifiedModData.FireModes.Select(x => x.GetModel))
            {
                // Icons
                item.FireModeSpritePath = CopyFileToRelative(item.FireModeSpritePath, baseDirectory, "Assets/Images");

                // Finalize
                FileImporter.SaveAndSerialize(Path.Combine(baseDirectory, fireModesRelativePath, item.id + "_firemode.json"), item);
            }

            if (!string.IsNullOrEmpty(CreateModReport))
                _errorHandler.ThrowWarning("Errors occurred while creating mod", CreateModReport);

            return true;
        }
        #region Export Functions
        private static void ExportCategory<T>(List<T> exportList, string folderKey, string fileName) where T : ConfigTableRecord
        {
            try
            {
                FolderPaths.TryGetValue(folderKey, out string? folderPath);
                foreach (var item in exportList)
                {
                    FileImporter.SaveAndSerialize(Path.Combine(BaseDirectory, folderPath, $"{item.Id}_{fileName}.json"), item);
                }
            }
            catch (Exception ex)
            {
                _errorHandler.ThrowError("Error when exporting category", ex);
            }
        }

        private static void ExportCustomCategory<T>(IEnumerable<T> exportList, string folderKey, string fileName) where T : ConfigTableRecordTemplate
        {
            try
            {
                FolderPaths.TryGetValue(folderKey, out string? folderPath);
                foreach (var item in exportList)
                {
                    FileImporter.SaveAndSerialize(Path.Combine(BaseDirectory, folderPath, $"{item.id}_{fileName}.json"), item);
                }
            }
            catch (Exception ex)
            {
                _errorHandler.ThrowError("Error when exporting category", ex);
            }
        }
        #endregion

        private static string CopyFileToRelative(string objectPath, string baseDirectory, string relativeDestinationFolderPath)
        {
            // This is to not alter IDs or bs
            if (Path.HasExtension(objectPath) && Path.IsPathRooted(objectPath) && !File.Exists(objectPath))
            {
                // Path to file does not exist.
                CreateModReport += $"File at {objectPath} does not exist. Setting full path on properties.";
                return objectPath;
            }
            else if (Path.IsPathRooted(objectPath))
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