using MGSC;
using QM_ItemCreatorTool.Extensions;
using QM_ItemCreatorTool.Interfaces;
using QM_ItemCreatorTool.Model;
using QM_ItemCreatorTool.Properties;
using QM_WeaponImporter;
using QM_WeaponImporter.Templates;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.ViewModel
{
    // this class is the connection between the model and the viewmodel 
    public class ModDataViewModel : ViewModelBase<ModDataModel>
    {
        public ModDataViewModel(ModDataModel model) : base(model)
        {
            // Leave empty
        }

        public ConfigTemplate Configuration { get => this.GetModel.config; set => this.GetModel.config = value; }

        #region Descriptors

        public List<CustomItemContentDescriptor> GetDescriptors()
        {
            List<CustomItemContentDescriptor> localDesc = new List<CustomItemContentDescriptor>();
            var weaponsDescriptors = Weapons.Select(x => x.GetDescriptor()).ToList();
            var meleeDescriptors = Melee.Select(x => x.GetDescriptor()).ToList();
            var ammo = Ammo.Select(x => x.GetDescriptor()).ToList();

            localDesc.AddRange(weaponsDescriptors);
            localDesc.AddRange(meleeDescriptors);
            localDesc.AddRange(ammo);
            return localDesc;
        }

        public List<ItemProduceReceiptTemplate> GetCraftingSpecs()
        {
            foreach (var item in ItemReceipts)
            {
                item.PrepareExport();
            }
            return ItemReceipts.Select(x => x.GetModel).ToList();
        }

        public List<ItemTransformationRecord> GetItemTransforms()
        {
            List<ItemTransformationRecord> itemTransforms = new List<ItemTransformationRecord>();
            var descriptors = Weapons.Select(x => x.GetItemTransformationRecord());
            itemTransforms.AddRange(descriptors);

            descriptors = Melee.Select(x => x.GetItemTransformationRecord());
            itemTransforms.AddRange(descriptors);

            descriptors = Ammo.Select(x => x.GetItemTransformationRecord());
            itemTransforms.AddRange(descriptors);

            return itemTransforms;
        }

        public void SetFactionData(FactionTemplate factionData)
        {
            foreach (var factionEntry in factionData.FactionRewardList)
            {
                foreach (var contentRecord in factionEntry.contentRecords)
                {
                    IFactionData itemList = Weapons.First(x => x.ID.Equals(contentRecord.ContentIds[0]));
                    if (itemList != null) { itemList.AddFactionRule(new FactionEntry(factionEntry.FactionName, contentRecord)); continue; }

                    itemList = Melee.First(x => x.ID.Equals(contentRecord.ContentIds[0]));
                    if (itemList != null) { itemList.AddFactionRule(new FactionEntry(factionEntry.FactionName, contentRecord)); continue; }

                    itemList = Ammo.First(x => x.ID.Equals(contentRecord.ContentIds[0]));
                    if (itemList != null) { itemList.AddFactionRule(new FactionEntry(factionEntry.FactionName, contentRecord)); continue; }
                }
            }
        }

        public FactionTemplate GetFactionData()
        {
            FactionTemplate factionReward = new FactionTemplate();

            foreach (var singleWeapon in Weapons)
            {
                foreach (var factionRule in singleWeapon.FactionRules)
                {
                    FactionReward fReward = new FactionReward()
                    {
                        FactionName = factionRule.Name,
                        RewardType = ContentDropTableType.rewardEquipment.ToString(),
                        contentRecords = factionRule.GetContentDrop(singleWeapon.ID),
                    };
                    factionReward.FactionRewardList.Add(fReward);
                }
            }
            foreach (var singleEntry in Melee)
            {
                foreach (var factionRule in singleEntry.FactionRules)
                {
                    FactionReward fReward = new FactionReward()
                    {
                        FactionName = factionRule.Name,
                        RewardType = ContentDropTableType.rewardEquipment.ToString(),
                        contentRecords = factionRule.GetContentDrop(singleEntry.ID),
                    };
                    factionReward.FactionRewardList.Add(fReward);
                }
            }
            foreach (var singleEntry in Ammo)
            {
                foreach (var factionRule in singleEntry.FactionRules)
                {
                    FactionReward fReward = new FactionReward()
                    {
                        FactionName = factionRule.Name,
                        RewardType = ContentDropTableType.rewardEquipment.ToString(),
                        contentRecords = factionRule.GetContentDrop(singleEntry.ID),
                    };
                    factionReward.FactionRewardList.Add(fReward);
                }
            }
            return factionReward;
        }
        public List<DatadiskRecord> GetDataDisks()
        {
            // Here we compile all datadisks and add their respective weapons there
            List<DatadiskRecord> result = new List<DatadiskRecord>();
            // here we have a list of chips this weapon is part of
            foreach (var weapon in Weapons)
            {
                foreach (var chip in weapon.Chips)
                {
                    var currentChip = result.Find(x => x.Id.Equals(chip));

                    if (currentChip == null)
                    {
                        currentChip = new DatadiskRecord();
                        currentChip.UnlockIds = new List<string>();
                        currentChip.UnlockType = DatadiskUnlockType.ProductionItem;
                        currentChip.Id = chip;
                        result.Add(currentChip);
                    }
                    currentChip.UnlockIds.Add(weapon.ID);
                }
            }
            return result;
        }

        public void SetLocalization(string key, LocalizationTemplate file)
        {
            foreach (var idDataPair in file.name)
            {
                string ID = idDataPair.Key;
                var nameEntries = idDataPair.Value;
                file.shortdesc.TryGetValue(ID, out var descEntries);
                LocalizationViewModel a = new LocalizationViewModel();
                a.ID = ID;
                a.TableKey = key;
                foreach (var languageNameEntry in nameEntries)
                {
                    Enum.TryParse(typeof(Localization.Lang), languageNameEntry.Key, out var entryLanguage);
                    if (entryLanguage == null) break;
                    var descLanguagePair = descEntries.First(x => x.Key.Equals(languageNameEntry.Key));
                    CustomStringDictionary e = new CustomStringDictionary(languageNameEntry.Value, descLanguagePair.Value, (Localization.Lang)entryLanguage);
                    a.Entries.Add(e);
                }
                LocalizationEntries.Add(a);
            }
        }

        public LocalizationTemplate GetLocalization(string key)
        {
            LocalizationTemplate localizationFile = new LocalizationTemplate();
            localizationFile.name = new Dictionary<string, Dictionary<string, string>>();
            localizationFile.shortdesc = new Dictionary<string, Dictionary<string, string>>();
            var localizationEntries = LocalizationEntries.Where(x => x.TableKey.Equals(key, StringComparison.CurrentCulture));
            foreach (LocalizationViewModel item in localizationEntries)
            {
                var currentItemEntries = item.Entries.ToList();

                var nameDictionary = currentItemEntries.Select(x => x.GetName()).ToDictionary();
                var descDictionary = currentItemEntries.Select(x => x.GetDescription()).ToDictionary();

                localizationFile.name.Add(item.ID, nameDictionary);
                localizationFile.shortdesc.Add(item.ID, descDictionary);
            }
            return localizationFile;
        }
        #endregion

        #region Lists
        public ObservableCollection<RangedViewModel> Weapons
        {
            get => GetModel.WeaponList;
            set { GetModel.WeaponList = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<MeleeViewModel> Melee
        {
            get => GetModel.MeleeList;
            set { GetModel.MeleeList = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<ItemProduceViewModel> ItemReceipts
        {
            get => GetModel.ItemReceipts;
            set { GetModel.ItemReceipts = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<LocalizationViewModel> LocalizationEntries
        {
            get => GetModel.LocalizationEntries;
            set { GetModel.LocalizationEntries = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<AmmoViewModel> Ammo
        {
            get => GetModel.AmmoList;
            set { GetModel.AmmoList = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<FireModeViewModel> FireModes
        {
            get => GetModel.FireModesList;
            set { GetModel.FireModesList = value; RaisePropertyChanged(); }
        }

        public void AddItemToList(object item)
        {
            switch (item)
            {
                case RangedViewModel ranged: Weapons.Add(ranged); break;
                case MeleeViewModel melee: Melee.Add(melee); break;
                case ItemProduceViewModel itemProduce:
                    {
                        ItemReceipts.Add(itemProduce);
                        break;
                    }
                case LocalizationViewModel locFile: LocalizationEntries.Add(locFile); break;
                case AmmoViewModel ammoEntry: Ammo.Add(ammoEntry); break;
                case ItemTransformationRecordTemplate record:
                    {
                        if (!string.IsNullOrEmpty(record.Id))
                        {
                            BindCraftToItem(record);
                        }
                        break;
                    }
                case FireModeViewModel fmViewModel: FireModes.Add(fmViewModel); break;
                case DatadiskRecordTemplate dataDisk:
                    {
                        if (!string.IsNullOrEmpty(dataDisk.Id))
                        {
                            BindDatadiskToItem(dataDisk);
                        }
                        break;
                    }
                default: break;
            }
        }

        public void RemoveItemFromList(object item)
        {
            switch (item)
            {
                case RangedViewModel ranged: Weapons.Remove(ranged); break;
                case MeleeViewModel melee: Melee.Remove(melee); break;
                case ItemProduceViewModel itemProduce: ItemReceipts.Remove(itemProduce); break;
                case LocalizationViewModel locFile: LocalizationEntries.Remove(locFile); break;
                case AmmoViewModel ammoEntry: Ammo.Remove(ammoEntry); break;
                case FireModeViewModel fmViewModel: FireModes.Remove(fmViewModel); break;
                default: break;
            }
        }

        private void BindCraftToItem(ItemTransformationRecordTemplate itemReceipt)
        {
            var weapon = Weapons.First(x => x.ID.Equals(itemReceipt.Id));
            if (weapon != null) { weapon.SetItemTransformationRecord(itemReceipt); return; }

            var melee = Melee.First(x => x.ID.Equals(itemReceipt.Id));
            if (melee != null) { melee.SetItemTransformationRecord(itemReceipt); return; }

            var ammo = Ammo.First(x => x.ID.Equals(itemReceipt.Id));
            if (ammo != null) { ammo.SetItemTransformationRecord(itemReceipt); return; }
        }

        private void BindDatadiskToItem(DatadiskRecordTemplate dataDisk)
        {
            IChipData item = Weapons.First(x => dataDisk.UnlockIds.Contains(x.ID));
            if (item != null) { item.Chips.Add(dataDisk.Id); return; }

            item = Melee.First(x => dataDisk.UnlockIds.Contains(x.ID));
            if (item != null) { item.Chips.Add(dataDisk.Id); return; }

            item = Ammo.First(x => dataDisk.UnlockIds.Contains(x.ID));
            if (item != null) { item.Chips.Add(dataDisk.Id); return; }
        }

        #endregion

        #region Loading
        public void LoadNew(ModDataViewModel replacement)
        {
            ClearMod();
            replacement.Weapons.ToList().ForEach(this.Weapons.Add);
            replacement.Melee.ToList().ForEach(this.Melee.Add);
            replacement.ItemReceipts.ToList().ForEach(this.ItemReceipts.Add);
            replacement.LocalizationEntries.ToList().ForEach(this.LocalizationEntries.Add);
            replacement.Ammo.ToList().ForEach(this.Ammo.Add);
            replacement.FireModes.ToList().ForEach(this.FireModes.Add);
        }

        public void ClearMod()
        {
            this.Weapons.Clear();
            this.Melee.Clear();
            this.ItemReceipts.Clear();
            this.LocalizationEntries.Clear();
            this.Ammo.Clear();
            this.FireModes.Clear();
        }
        #endregion
    }
}