using QM_ItemCreatorTool.Interfaces;
using MGSC;

namespace QM_ItemCreatorTool.Providers;
internal class WeaponSubclassProvider : IDataProvider<string>
{
    public IEnumerable<string> GetData()
    {
        return Enum.GetNames(typeof(WeaponSubClass)).Order().ToList();
    }
}