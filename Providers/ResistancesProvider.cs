using QM_ItemCreatorTool.Interfaces;

namespace QM_ItemCreatorTool.Providers
{
    public class ResistancesProvider : IDataProvider<string>
    {
        public IEnumerable<string> GetData()
        {
            return new List<string>()
            {
                "blunt",
                "pierce",
                "lacer",
                "fire",
                "cold",
                "poison",
                "shock",
                "beam"
            };
        }
    }
}