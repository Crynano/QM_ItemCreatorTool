using QM_ItemCreatorTool.Interfaces;

namespace QM_ItemCreatorTool.Providers;
internal class CategoriesTagsProvider : IDataProvider<string>
{
    public IEnumerable<string> GetData()
    {
        return new List<string>()
        {
            "none",
            "AnCom",
            "ChurchRevelation",
            "Common",
            "Communists",
            "CoreWard",
            "CResistance",
            "DaydreamChems",
            "Dilthey",
            "FrancheComte",
            "Grasshopper",
            "Managment",
            "Medical",
            "Mercury",
            "Military",
            "Miner",
            "Moon",
            "PlanetBridge",
            "Police",
            "Possesed",
            "Prison",
            "RealWare",
            "SBN",
            "Science",
            "SheduThousand",
            "Sunlight",
            "Tezctlan",
            "UnchainedBelt",
            "Worker",
            "Xiomara",
        };
    }
}
