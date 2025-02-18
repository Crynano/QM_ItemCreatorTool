﻿using System.Collections;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.Providers;

public class FiremodesDataProvider : BaseDataProvider<string>
{
    public FiremodesDataProvider(ref ObservableCollection<string> baseList) : base(ref baseList)
    {

    }

    public override IEnumerable<string> GetData()
    {
        // Just a list here, hardcoded?
        // Heh, yeah, it has always been.
        var gameFireModes = new List<string>()
        {
            "advanced_energy_pistol_burst",
            "advanced_energy_pistol_single",
            "advanced_energy_rifle_auto",
            "advanced_energy_rifle_burst",
            "advanced_energy_rifle_single",
            "advanced_energy_shotgun_burst",
            "advanced_energy_shotgun_single",
            "advanced_energy_smg_auto",
            "advanced_energy_smg_shortauto",
            "advanced_machinegun_auto",
            "advanced_machinegun_longauto",
            "advanced_machinegun_shortauto",
            "advanced_pistol_burst",
            "advanced_pistol_shortauto",
            "advanced_pistol_single",
            "advanced_rifle_auto",
            "advanced_rifle_burst",
            "advanced_rifle_shortauto",
            "advanced_rifle_single",
            "advanced_sawtrower_burst",
            "advanced_sawtrower_single",
            "advanced_shotgun_auto",
            "advanced_shotgun_burst",
            "advanced_shotgun_duble",
            "advanced_shotgun_single",
            "advanced_smg_auto",
            "advanced_smg_shortauto",
            "advanced_smg_single",
            "basic_energy_machinegun_auto",
            "basic_energy_machinegun_shortauto",
            "basic_energy_pistol_burst",
            "basic_energy_pistol_single",
            "basic_energy_rifle_burst",
            "basic_energy_rifle_shortauto",
            "basic_energy_rifle_single",
            "basic_energy_shotgun_burst",
            "basic_energy_shotgun_single",
            "basic_energy_smg_shortauto",
            "basic_energy_smg_single",
            "basic_grenade_single",
            "basic_machinegun_auto",
            "basic_machinegun_longauto",
            "basic_machinegun_shortauto",
            "basic_pistol_burst",
            "basic_pistol_shortauto",
            "basic_pistol_single",
            "basic_rifle_burst",
            "basic_rifle_shortauto",
            "basic_rifle_single",
            "basic_sawtrower_single",
            "basic_shotgun_auto",
            "basic_shotgun_burst",
            "basic_shotgun_single",
            "basic_smg_auto",
            "basic_smg_shortauto",
            "basic_smg_single",
            "energy_hfg_single",
            "flamethrower_single",
            "plasma_energy_burst",
            "plasma_energy_shortauto",
            "plasma_energy_single",
            "quasi_pistol_burst",
            "quasi_pistol_single",
            "quasi_rifle_auto",
            "quasi_rifle_shortauto",
            "quasi_rifle_single",
            "tezktlan_spear_auto",
            "thunder_pistol_single",
            "thunder_rifle_auto",
            "thunder_rifle_shortauto",
            "thunder_rifle_single",
            "toxicthrower_single"
        };

        return gameFireModes;
    }

    public override IEnumerable GetSuggestions(string filter)
    {
        return managedList.Where(x => x.Contains(filter, StringComparison.CurrentCultureIgnoreCase));
    }
}