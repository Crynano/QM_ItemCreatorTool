using QM_ItemCreatorTool.Interfaces;

namespace QM_ItemCreatorTool.Providers;
internal class GrenadesProvider : IDataProvider<string>
{
    public IEnumerable<string> GetData()
    {
        return new List<string>()
        {
            "frag_grenade",
            "frost_grenade",
            "handmade_frag_grenade",
            "handmade_thermal_grenade",
            "hellfire_grenade",
            "nanorobot_grenade",
            "phase_bomb",
            "qwasi_grenade",
            "skrivnus_knife",
            "stun_grenade",
            "thermal_grenade",
            "toxic_grenade",
            "trowable_heart",
            "trowing_knife",
            "urparp_grenade",
        };
    }
}