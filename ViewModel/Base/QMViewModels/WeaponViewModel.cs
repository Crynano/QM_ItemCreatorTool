using MGSC;
using QM_ItemCreatorTool.Interfaces;
using QM_ItemCreatorTool.Properties;
using QM_WeaponImporter;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.ViewModel
{
    public class WeaponViewModel<T> : BreakableItemViewModel<T>, IFactionData, ICraftData, IChipData where T : WeaponTemplate, new()
    {
        public WeaponViewModel() { }
        public WeaponViewModel(T item) : base(item) { }

        #region Properties

        public bool IsImplicit
        {
            get => _model.isImplicit;
            set { _model.isImplicit = value; RaisePropertyChanged(); }
        }

        public WeaponClass WeaponClass
        {
            get => _model.weaponClass;
            set
            {
                _model.weaponClass = value;
                RaisePropertyChanged();
            }
        }

        public WeaponSubClass WeaponSubClass
        {
            get => _model.weaponSubClass;
            set
            {
                _model.weaponSubClass = value;
                RaisePropertyChanged();
            }
        }

        public string? RequiredAmmo
        {
            get => _model.requiredAmmo;
            set
            {
                _model.requiredAmmo = value;
                RaisePropertyChanged();
            }
        }

        public string? DefaultAmmoId
        {
            get => _model.defaultAmmoId;
            set
            {
                _model.defaultAmmoId = value;
                RaisePropertyChanged();
            }
        }

        public string? OverrideAmmo
        {
            get => _model.overrideAmmo;
            set
            {
                _model.overrideAmmo = value;
                RaisePropertyChanged();
            }
        }


        public int MinDamage
        {
            get => _model.minimumDamage;
            set
            {
                _model.minimumDamage = value;
                RaisePropertyChanged();
            }
        }

        public int MaxDamage
        {
            get => _model.maximumDamage;
            set
            {
                _model.maximumDamage = value;
                RaisePropertyChanged();
            }
        }

        public float ArmorPenetration
        {
            get => _model.armorPenetration;
            set { _model.armorPenetration = value; RaisePropertyChanged(); }
        }

        public float CriticalChance
        {
            get => _model.criticalChance;
            set
            {
                _model.criticalChance = value;
                RaisePropertyChanged();
            }
        }

        public float CriticalDamage
        {
            get => _model.criticalDamage;
            set
            {
                _model.criticalDamage = value;
                RaisePropertyChanged();
            }
        }

        public List<string> Firemodes
        {
            get => _model.firemodes;
            set
            {
                _model.firemodes = value;
                RaisePropertyChanged();
            }
        }

        public float BonusAccuracy
        {
            get => _model.bonusAccuracy;
            set { _model.bonusAccuracy = value; RaisePropertyChanged(); }
        }

        public int MagazineCapacity
        {
            get => _model.magazineCapacity;
            set { _model.magazineCapacity = value; RaisePropertyChanged(); }
        }

        public int ReloadDuration
        {
            get => _model.reloadDuration;
            set { _model.reloadDuration = value; RaisePropertyChanged(); }
        }

        public bool ReloadOneBulletAtATime
        {
            get => _model.reloadOneBulletAtATime;
            set { _model.reloadOneBulletAtATime = value; RaisePropertyChanged(); }
        }

        public bool IsSelfCharge
        {
            get => _model.isSelfCharge;
            set { _model.isSelfCharge = value; RaisePropertyChanged(); }
        }

        public int DotWoundsDamageBonus
        {
            get => _model.dotWoundsDamageBonus;
            set { _model.dotWoundsDamageBonus = value; RaisePropertyChanged(); }
        }

        public int FractureWoundDamageBonus
        {
            get => _model.fractureWoundDamageBonus;
            set { _model.fractureWoundDamageBonus = value; RaisePropertyChanged(); }
        }

        public float PainDamageMultiplier
        {
            get => _model.painDamageMultiplier;
            set { _model.painDamageMultiplier = value; RaisePropertyChanged(); }
        }

        public float CritPainDamageMultiplier
        {
            get => _model.critPainDamageMultiplier;
            set { _model.critPainDamageMultiplier = value; RaisePropertyChanged(); }
        }

        public float OffSlotCritChance
        {
            get => _model.offSlotCritChance;
            set { _model.offSlotCritChance = value; RaisePropertyChanged(); }
        }

        public float MinDmgCapBonus
        {
            get => _model.minDmgCapBonus;
            set { _model.minDmgCapBonus = value; RaisePropertyChanged(); }
        }

        public float FovLookAngleMult
        {
            get => _model.fovLookAngleMult;
            set { _model.fovLookAngleMult = value; RaisePropertyChanged(); }
        }

        public HandsGrip Grip
        {
            get => _model.grip;
            set { _model.grip = value; RaisePropertyChanged(); }
        }

        public bool HasBFGOverlay
        {
            get => _model.hasHFGOverlay;
            set { _model.hasHFGOverlay = value; RaisePropertyChanged(); }
        }

        #endregion
        #region Sounds
        public string? AttackSoundPath
        {
            get => itemDescriptor.shootSoundPath;
            set { itemDescriptor.shootSoundPath = value; RaisePropertyChanged(); }
        }
        public string? DryShotSoundPath
        {
            get => itemDescriptor.dryShotSoundPath;
            set { itemDescriptor.dryShotSoundPath = value; RaisePropertyChanged(); }
        }
        public string? FailedAttackSoundPath
        {
            get => itemDescriptor.failedAttackSoundPath;
            set { itemDescriptor.failedAttackSoundPath = value; RaisePropertyChanged(); }
        }
        public string? ReloadSoundPath
        {
            get => itemDescriptor.reloadSoundPath;
            set { itemDescriptor.reloadSoundPath = value; RaisePropertyChanged(); }
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

        #region Faction Data
        private ObservableCollection<FactionEntry> _factionRules = new ObservableCollection<FactionEntry>();
        public ObservableCollection<FactionEntry> FactionRules
        {
            get { return _factionRules; }
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
        public void SetItemTransformationRecord(ItemTransformationRecord record)
        {
            DisassemblyList.Clear();
            foreach (var item in record.OutputItems)
            {
                DisassemblyList.Add(new CustomItemQuantity(item));
            }
        }
        #endregion

        #region Functions


        private List<string> InstantiateIfEmpty(List<string>? List)
        {
            if (List == null || List.Count() <= 0) return new List<string>() { string.Empty };
            return List;
        }


        #endregion
    }
}