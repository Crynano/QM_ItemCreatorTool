using QM_ItemCreatorTool.Interfaces;

namespace QM_ItemCreatorTool.Providers
{
    public class ArmorSlotsProvider : IDataProvider<string>
    {
        public IEnumerable<string> GetData()
        {
            return new List<string>()
            {
                "helmet",
                "armor",
                "leggings",
                "boots"
            };
        }
    }
}