using QM_WeaponImporter;
using QM_WeaponImporter.Templates;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace QM_ItemCreatorTool.Model
{
    // This instantiable class will serve as a big data storage unit
    // Here we can store whole modsets, or even use it as a "navigation" type just as weapons have
    [Serializable]
    public class ModDataModel
    {
        public string Name { get; set; } = "Default ModDataModel";

        public ModDataModel()
        {
            WeaponList = new ObservableCollection<WeaponViewModel>();
            MeleeList = new ObservableCollection<MeleeViewModel>();
        }

        public ConfigTemplate config = new ConfigTemplate();
        public List<CustomItemContentDescriptor> descriptors = new List<CustomItemContentDescriptor>();
        // When exporting, we populate this.
        [JsonIgnore]
        public ObservableCollection<WeaponViewModel> WeaponList;
        [JsonIgnore]
        public ObservableCollection<MeleeViewModel> MeleeList;

        public List<RangedWeaponTemplate> RangedWeaponList = new List<RangedWeaponTemplate>();
        public List<MeleeWeaponTemplate> MeleeWeaponList = new List<MeleeWeaponTemplate>();

        public void PrepareExport()
        {
            foreach (var weapon in WeaponList)
            {
                RangedWeaponList.Add(weapon.GetModel);
                descriptors.Add(weapon.GetDescriptor());
            }
            foreach (var weapon in MeleeList)
            {
                MeleeWeaponList.Add(weapon.GetModel);
                descriptors.Add(weapon.GetDescriptor());
            }
        }

        public void LoadFromDeserialize()
        {
            if (RangedWeaponList != null) 
                RangedWeaponList.ForEach(x => WeaponList.Add(new WeaponViewModel(x)));
            if (MeleeWeaponList != null)
                MeleeWeaponList.ForEach(x => MeleeList.Add(new MeleeViewModel(x)));

            foreach (var descriptor in descriptors)
            {
                WeaponList.ToList().Find(x => x.ID.Equals(descriptor.attachedId))?.SetDescriptor(descriptor);
                // Just in case? Double the fun? Double the processing capabilities
                MeleeList.ToList().Find(x => x.ID.Equals(descriptor.attachedId))?.SetDescriptor(descriptor);
            }
        }
    }
}