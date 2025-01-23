using QM_WeaponImporter;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.Model
{
    // This instantiable class will serve as a big data storage unit
    // Here we can store whole modsets, or even use it as a "navigation" type just as weapons have
    [Serializable]
    public class ModDataModel
    {
        public ModDataModel()
        {

        }

        #region App Collections
        //[JsonIgnore]
        public ObservableCollection<WeaponViewModel> WeaponList = new ObservableCollection<WeaponViewModel>();
        //[JsonIgnore]
        public ObservableCollection<MeleeViewModel> MeleeList = new ObservableCollection<MeleeViewModel>();
        //[JsonIgnore]
        public ObservableCollection<ItemProduceViewModel> ItemReceipts = new ObservableCollection<ItemProduceViewModel>();
        //[JsonIgnore]
        public ObservableCollection<LocalizationViewModel> LocalizationEntries = new ObservableCollection<LocalizationViewModel>();
        #endregion

        public ConfigTemplate config = new ConfigTemplate();

        // When exporting, we populate this.
        #region Exporting Lists
        //public List<CustomItemContentDescriptor> descriptors = new List<CustomItemContentDescriptor>();
        //public List<RangedWeaponTemplate> RangedWeaponList = new List<RangedWeaponTemplate>();
        //public List<MeleeWeaponTemplate> MeleeWeaponList = new List<MeleeWeaponTemplate>();
        //public List<ItemProduceReceiptTemplate> ItemReceiptsList = new List<ItemProduceReceiptTemplate>();
        //public List<LocalizationViewModel> LocalizationList = new List<LocalizationViewModel>();
        #endregion

        public void PrepareExport()
        {
            //foreach (var weapon in WeaponList)
            //{
            //    descriptors.Add(weapon.GetDescriptor());
            //}
            //foreach (var weapon in MeleeList)
            //{
            //    descriptors.Add(weapon.GetDescriptor());
            //}
            foreach (var item in ItemReceipts)
            {
                item.PrepareExport();
            }
        }

        public void LoadFromDeserialize()
        {
            try
            {
                //if (RangedWeaponList != null)
                //    RangedWeaponList.ForEach(x => WeaponList.Add(new WeaponViewModel(x)));
                //if (MeleeWeaponList != null)
                //    MeleeWeaponList.ForEach(x => MeleeList.Add(new MeleeViewModel(x)));
                //if (ItemReceiptsList != null)
                //{
                //    foreach (var item in ItemReceiptsList)
                //    {
                //        ConfigTableRecordTemplate foundItem = RangedWeaponList?.ToList().Find(x => x.id.Equals(item.Id));
                //        if (foundItem == null)
                //        {
                //            foundItem = MeleeWeaponList?.Find(x => x.id.Equals(item.Id));
                //        }
                //        if (foundItem == null) continue;
                //        ItemReceipts.Add(new ItemProduceViewModel(new ItemProduceReceiptTemplate(), foundItem.id));
                //    }
                //}

                //foreach (var descriptor in descriptors)
                //{
                //    WeaponList.ToList().Find(x => x.ID.Equals(descriptor.attachedId))?.SetDescriptor(descriptor);
                //    // Just in case? Double the fun? Double the processing capabilities
                //    MeleeList.ToList().Find(x => x.ID.Equals(descriptor.attachedId))?.SetDescriptor(descriptor);
                //}
            }
            catch (Exception ex)
            {
                // Ignore???
            }
        }
    }
}