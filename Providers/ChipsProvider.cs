using QM_ItemCreatorTool.Interfaces;

namespace QM_ItemCreatorTool.Providers
{
    public class ChipsProvider : IDataProvider<string>
    {
        public IEnumerable<string> GetData()
        {
            return new List<string>()
            {
                "itemChip",
                "mediumItemChip",
                "highItemChip",
                "merkUSB",
                "classUSB",
                "AncomItemChip",
                "ChurchItemChip",
                "CoreItemChip",
                "DayDreamItemChip",
                "DiltheyItemChip",
                "FrancheItemChip",
                "GrassHopperItemChip",
                "PlanetBridgeItemChip",
                "RealWareItemChip",
                "SBNItemChip",
                "SunLightItemChip",
            };
        }
    }
}