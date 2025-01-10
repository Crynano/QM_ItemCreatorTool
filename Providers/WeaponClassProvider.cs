using QM_ItemCreatorTool.Interfaces;
using MGSC;

namespace QM_ItemCreatorTool.Providers;
internal class WeaponClassProvider : IDataProvider<string>
{
    public IEnumerable<string> GetData()
    {
        return Enum.GetNames(typeof(WeaponClass)).Order().ToList();
    }
}