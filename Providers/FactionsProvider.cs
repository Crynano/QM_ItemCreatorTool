using QM_ItemCreatorTool.Interfaces;

namespace QM_ItemCreatorTool.Providers
{
    public class FactionsProvider : IDataProvider<string>
    {
        public IEnumerable<string> GetData()
        {
            return new List<string>()
            {
                "AnCom", "Tezctlan", "XiomaraMasks", "SBN", "RealWare", "DaydreamChems", "PlanetBridge", "Grasshopper", "Sunlight", "FrancheComte", "ChurchRevelation", "CoreWard", "CResistance", "Dilthey", "UnchainedBelt", "SheduThousand", "EipschwitzHead", "Unknown", "Magnum" 
            }.Order();
        }
    }
}