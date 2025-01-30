using MGSC;
using QM_ItemCreatorTool.Interfaces;
using QM_ItemCreatorTool.Properties;
using QM_WeaponImporter.Templates;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.ViewModel;
public class AmmoViewModel : ItemViewModel<AmmoRecordTemplate>, IFactionData, ICraftData, IChipData
{
    public AmmoViewModel()
    {

    }

    public AmmoViewModel(AmmoRecordTemplate item) : base(item)
    {

    }

    public int InventorySortOrder
    {
        get => _model.inventorySortOrder;
        set { _model.inventorySortOrder = value; RaisePropertyChanged(); }
    }

    public bool IsImplictedAmmo
    {
        get => _model.IsImplictedAmmo;
        set { _model.IsImplictedAmmo = value; RaisePropertyChanged(); }
    }

    public bool ThrowBackTarget
    {
        get => _model.ThrowBackTarget;
    }

    public AmmoBallisticType BallisticType
    {
        get => _model.BallisticType;
        set { _model.BallisticType = value; RaisePropertyChanged(); }
    }

    public int MinAmmoAmount
    {
        get => _model.MinAmmoAmount;
        set { _model.MinAmmoAmount = value; RaisePropertyChanged(); }
    }

    public int MaxAmmoAmount
    {
        get => _model.MaxAmmoAmount;
        set { _model.MaxAmmoAmount = value; RaisePropertyChanged(); }
    }

    public short MaxStack
    {
        get => _model.MaxStack;
        set { _model.MaxStack = value; RaisePropertyChanged(); }
    }

    public string AmmoType
    {
        get => _model.AmmoType;
        set { _model.AmmoType = value; RaisePropertyChanged(); }
    }

    public string DmgType
    {
        get => _model.DmgType;
        set { _model.DmgType = value; RaisePropertyChanged(); }
    }

    public float DmgCritChance
    {
        get => _model.DmgCritChance;
        set { _model.DmgCritChance = value; RaisePropertyChanged(); }
    }

    public float AccuracyMult
    {
        get => _model.AccuracyMult;
        set { _model.AccuracyMult = value; RaisePropertyChanged(); }
    }

    public float DamageMult
    {
        get => _model.DamageMult;
        set { _model.DamageMult = value; RaisePropertyChanged(); }
    }

    public int BulletCastsPerShot
    {
        get => _model.BulletCastsPerShot;
        set { _model.BulletCastsPerShot = value; RaisePropertyChanged(); }
    }

    public float ThrowBackChance
    {
        get => _model.ThrowBackChance;
        set { _model.ThrowBackChance = value; RaisePropertyChanged(); }
    }

    public float TargetBurningChance
    {
        get => _model.TargetBurningChance;
        set { _model.TargetBurningChance = value; RaisePropertyChanged(); }
    }

    public float TileBurningChance
    {
        get => _model.TileBurningChance;
        set { _model.TileBurningChance = value; RaisePropertyChanged(); }
    }

    public float TileToxicLiquidChance
    {
        get => _model.TileToxicLiquidChance;
        set { _model.TileToxicLiquidChance = value; RaisePropertyChanged(); }
    }

    public float PierceCreaturesChance
    {
        get => _model.PierceCreaturesChance;
        set { _model.PierceCreaturesChance = value; RaisePropertyChanged(); }
    }

    public float StunChance
    {
        get => _model.StunChance;
        set { _model.StunChance = value; RaisePropertyChanged(); }
    }

    public int StunDuration
    {
        get => _model.StunDuration;
        set { _model.StunDuration = value; RaisePropertyChanged(); }
    }

    #region Faction Data

    private ObservableCollection<FactionEntry> _factionRules = new ObservableCollection<FactionEntry>();
    public ObservableCollection<FactionEntry> FactionRules
    {
        get => _factionRules;
        set { _factionRules = value; RaisePropertyChanged(); }
    }
    #endregion

    #region Crafting Data
    private ObservableCollection<CustomItemQuantity> _disassemblyList = new ObservableCollection<CustomItemQuantity>();
    public ObservableCollection<CustomItemQuantity> DisassemblyList
    {
        get { return _disassemblyList; }
        set { _disassemblyList = value; RaisePropertyChanged(); }
    }

    public ItemTransformationRecord GetItemTransformationRecord()
    {
        return new ItemTransformationRecord()
        {
            Id = ID,
            OutputItems = DisassemblyList.Select(x => x.GetOriginal()).ToList()
        };
    }
    #endregion

    #region Chips
    private ObservableCollection<string> _chips = new ObservableCollection<string>();
    public ObservableCollection<string> Chips
    {
        get { return _chips; }
        set { _chips = value; RaisePropertyChanged(); }
    }
    #endregion
}