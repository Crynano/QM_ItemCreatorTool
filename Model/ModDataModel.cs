using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.Model
{
    // This instantiable class will serve as a big data storage unit
    // Here we can store whole modsets, or even use it as a "navigation" type just as weapons have
    public class ModDataModel
    {
        public string Name { get; set; } = "Default Name";

        public ModDataModel()
        {
           
        }

        // When exporting, we populate this.
        public List<WeaponViewModel> WeaponList;
    }
}