using QM_ItemCreatorTool.Interfaces;
using MGSC;

namespace QM_ItemCreatorTool.Providers;
internal class GripTypesProvider : IDataProvider<string>
{
    public IEnumerable<string> GetData()
    {
        return Enum.GetNames(typeof(GripType)).Order().ToList();
    }
}