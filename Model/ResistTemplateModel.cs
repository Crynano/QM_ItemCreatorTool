using MGSC;

namespace QM_ItemCreatorTool.Model
{
    public class ResistTemplateModel : ResistRecord, IArmorRecord
    {
        public ArmorClass ArmorClass { get; set; }
        public ArmorSubClass ArmorSubClass { get; set; }

        public T GetOriginal<T>() where T : ResistRecord, new()
        {
            return new T()
            {
                ResistSheet = this.ResistSheet,
                MaxDurability = this.MaxDurability,
                MinDurabilityAfterRepair = this.MinDurabilityAfterRepair,
                Unbreakable = this.Unbreakable,
                RepairCategory = this.RepairCategory,
                Categories = this.Categories,
                TechLevel = this.TechLevel,
                Price = this.Price,
                Weight = this.Weight,
                InventoryWidthSize = this.InventoryWidthSize,
                ItemClass = this.ItemClass,
                Id = this.Id,
            };
        }
    }
}