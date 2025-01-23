using MGSC;
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

            localDesc.AddRange(weaponsDescriptors);
            localDesc.AddRange(meleeDescriptors);
            return localDesc;
        }

        public List<ItemProduceReceiptTemplate> GetCraftingSpecs()
        {
            foreach(var item in ItemReceipts)
            {
                item.PrepareExport();
            }
            return ItemReceipts.Select(x => x.GetModel).ToList();
        }
        public List<ItemTransformationRecord> GetItemTransforms()
        {
            List<ItemTransformationRecord> itemTransforms = new List<ItemTransformationRecord>();
            var weaponsDescriptors = Weapons.Select(x => x.GetItemTransformationRecord());
            var meleeDescriptors = Melee.Select(x => x.GetItemTransformationRecord());

            itemTransforms.AddRange(weaponsDescriptors);
            itemTransforms.AddRange(meleeDescriptors);
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
                        // TODO unhardcode this
                        RewardType = ContentDropTableType.rewardEquipment.ToString(),
                        contentRecords = factionRule.GetContentDrop(singleWeapon.ID),
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

        #region Weapons
        public ObservableCollection<WeaponViewModel> Weapons
        {
            get
            {
                return GetModel.WeaponList;
            }
            set
            {
                GetModel.WeaponList = value;
                RaisePropertyChanged();
            }
        }

        public void AddWeapon(WeaponViewModel weapon)
        {
            if (weapon == null) return;
            Weapons.Add(weapon);
            //AddToAdditionalData(weapon.ID);
        }
        public void RemoveWeapon(WeaponViewModel weapon)
        {
            if (weapon == null) return;
            Weapons.Remove(weapon);
            //RemoveAdditionalData(weapon.ID);
        }

        // Melee
        public ObservableCollection<MeleeViewModel> Melee
        {
            get
            {
                return GetModel.MeleeList;
            }
            set
            {
                GetModel.MeleeList = value;
                RaisePropertyChanged();
            }
        }
        public void AddMelee(MeleeViewModel weapon)
        {
            if (weapon == null) return;
            Melee.Add(weapon);
            //AddToAdditionalData(weapon.ID);
        }
        public void RemoveMelee(MeleeViewModel weapon)
        {
            if (weapon == null) return;
            Melee.Remove(weapon);
            //RemoveAdditionalData(weapon.ID);
        }
        #endregion

        #region Item Receipts
        public ObservableCollection<ItemProduceViewModel> ItemReceipts
        {
            get
            {
                return GetModel.ItemReceipts;
            }
            set
            {
                GetModel.ItemReceipts = value;
                RaisePropertyChanged();
            }
        }
        // This function will add a localization and all files needed when creating a weapon or melee
        public void AddRecipe(ItemProduceViewModel data)
        {
            ItemReceipts.Add(data);
        }
        public void RemoveRecipe(ItemProduceViewModel data)
        {
            if (data != null)
                ItemReceipts.Remove(data);
        }

        public ObservableCollection<LocalizationViewModel> LocalizationEntries
        {
            get => GetModel.LocalizationEntries;
            set { GetModel.LocalizationEntries = value; RaisePropertyChanged(); }
        }
        public void AddLocalizationEntry(LocalizationViewModel data)
        {
            if(data != null)
                LocalizationEntries.Add(data);
        }
        public void RemoveLocalizationEntry(LocalizationViewModel data)
        {
            if (data != null)
                LocalizationEntries.Remove(data);
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
        }

        public void ClearMod()
        {
            this.Weapons.Clear();
            this.Melee.Clear();
            this.ItemReceipts.Clear();
            this.LocalizationEntries.Clear();
        }
        #endregion
    }
}