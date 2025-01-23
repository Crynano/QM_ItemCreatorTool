using MGSC;
using QM_ItemCreatorTool.ViewModel;
using QM_WeaponImporter;
using QM_WeaponImporter.Templates;

namespace QM_ItemCreatorTool.Model
{
    public class MeleeViewModel : QMItemViewModel<MeleeWeaponTemplate>
    {
        // Here lie all the properties for the model?
        public MeleeViewModel() : base()
        {

        }

        public MeleeViewModel(MeleeWeaponTemplate model) : base(model)
        {
            weaponDescriptor.attachedId = ID;
            _model.itemClass = ItemClass.Weapon.ToString();
        }

        public string? WeaponClass
        {
            get => _model.weaponClass;
            set
            {
                _model.weaponClass = value;
                RaisePropertyChanged();
            }
        }

        public string? WeaponSubClass
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

        public string? OverrideAmmo
        {
            get => _model.overrideAmmo;
            set
            {
                _model.overrideAmmo = value;
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

        public float BonusAccuracy
        {
            get => _model.bonusAccuracy;
            set
            {
                _model.bonusAccuracy = value;
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

        public int MagazineCapacity
        {
            get => _model.magazineCapacity;
            set
            {
                _model.magazineCapacity = value;
                RaisePropertyChanged();
            }
        }
        public int ReloadDuration
        {
            get => _model.reloadDuration;
            set
            {
                _model.reloadDuration = value;
                RaisePropertyChanged();
            }
        }
        public bool ReloadOneBulletAtATime
        {
            get => _model.reloadOneBulletAtATime;
            set
            {
                _model.reloadOneBulletAtATime = value;
                RaisePropertyChanged();
            }
        }
        public bool IsSelfCharge
        {
            get => _model.isSelfCharge;
            set
            {
                _model.isSelfCharge = value;
                RaisePropertyChanged();
            }
        }

        public int DotWoundsDamageBonus
        {
            get => _model.dotWoundsDamageBonus;
            set
            {
                _model.dotWoundsDamageBonus = value;
                RaisePropertyChanged();
            }
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
        public string? Grip
        {
            get => _model.grip;
            set { _model.grip = value; RaisePropertyChanged(); }
        }
        public bool HasBFGOverlay
        {
            get => _model.hasHFGOverlay;
            set { _model.hasHFGOverlay = value; RaisePropertyChanged(); }
        }
        public int Durability
        {
            get { return _model.maxDurability; }
            set { _model.maxDurability = value; RaisePropertyChanged(); }
        }
        public bool Unbreakable
        {
            get { return _model.unbreakable; }
            set { _model.unbreakable = value; RaisePropertyChanged(); }
        }
        public bool DoesMeleeSplash
        {
            get { return _model.doesMeleeSplash; }
            set { _model.doesMeleeSplash = value; RaisePropertyChanged(); }
        }
        public bool CanThrow
        {
            get { return _model.doesMeleeSplash; }
            set { _model.doesMeleeSplash = value; RaisePropertyChanged(); }
        }
        public bool GuaranteedThrow
        {
            get { return _model.throwGuaranteedHit; }
            set { _model.throwGuaranteedHit = value; RaisePropertyChanged(); }
        }
        public bool PiercingThrow
        {
            get { return _model.doesThrowPierce; }
            set { _model.doesThrowPierce = value; RaisePropertyChanged(); }
        }
        public int ThrowRange
        {
            get { return _model.throwRange; }
            set { _model.throwRange = value; RaisePropertyChanged(); }
        }
        public int DurabilityLossOnThrow
        {
            get { return _model.durabilityLossOnThrow; }
            set { _model.durabilityLossOnThrow = value; RaisePropertyChanged(); }
        }
        public bool CanAmputate
        {
            get { return _model.canMeleeAmputate; }
            set { _model.canMeleeAmputate = value; RaisePropertyChanged(); }
        }
        public bool AmputateOnWound
        {
            get { return _model.amputationOnWound; }
            set { _model.amputationOnWound = value; RaisePropertyChanged(); }
        }

        // Paths come later?

        #region Sounds
        public string? AttackSoundPath
        {
            get => weaponDescriptor.shootSoundPath;
            set
            {
                weaponDescriptor.shootSoundPath = value;
                RaisePropertyChanged();
            }
        }
        public string? DryShotSoundPath
        {
            get => weaponDescriptor.dryShotSoundPath;
            set
            {
                weaponDescriptor.dryShotSoundPath = value;
                RaisePropertyChanged();
            }
        }
        public string? FailedAttackSoundPath
        {
            get => weaponDescriptor.failedAttackSoundPath;
            set
            {
                weaponDescriptor.failedAttackSoundPath = value;
                RaisePropertyChanged();
            }
        }
        public string? ReloadSoundPath
        {
            get => weaponDescriptor.reloadSoundPath;
            set
            {
                weaponDescriptor.reloadSoundPath = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Functions
        public void SetDescriptor(CustomItemContentDescriptor? newDescriptor)
        {
            if (newDescriptor == null)
            {
                weaponDescriptor = new CustomItemContentDescriptor();
                weaponDescriptor.attachedId = ID;
                return;
            }
            weaponDescriptor = newDescriptor;
        }
        public CustomItemContentDescriptor GetDescriptor()
        {
            return weaponDescriptor;
        }

        private List<string> InstantiateIfEmpty(List<string>? List)
        {
            if (List == null || List.Count() <= 0) return new List<string>() { string.Empty };
            return List;
        }
        #endregion

        //public float visualReachCellDuration { get; set; }

        //public List<string> entityFlySprites { get; set; }

        //public bool useCustomBullet { get; set; } = false;


        //public string bulletAssetPath { get; set; }
    }
}
