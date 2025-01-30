using MGSC;
using QM_ItemCreatorTool.Extensions;
using QM_ItemCreatorTool.Model;
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
                    // Add every chip
                    // Add weapon
                    // if chip exists, do not add and add weapon
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

        public void AddItemToList(object item)
        {
            switch (item)
            {
                case RangedViewModel ranged: Weapons.Add(ranged); break;
                case MeleeViewModel melee: Melee.Add(melee); break;
                case ItemProduceViewModel itemProduce: ItemReceipts.Add(itemProduce); break;
                case LocalizationViewModel locFile: LocalizationEntries.Add(locFile); break;
                case AmmoViewModel ammoEntry: Ammo.Add(ammoEntry); break;
                case ItemTransformationRecord record: Weapons.ToList().Find(x => x.ID.Equals(record.Id))?.SetItemTransformationRecord(record); break;
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
                default: break;
            }
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
        }

        public void ClearMod()
        {
            this.Weapons.Clear();
            this.Melee.Clear();
            this.ItemReceipts.Clear();
            this.LocalizationEntries.Clear();
            this.Ammo.Clear();
        }
        #endregion
    }
}