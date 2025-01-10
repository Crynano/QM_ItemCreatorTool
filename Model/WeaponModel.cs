using QM_ItemCreatorTool.ViewModel;
using QM_WeaponImporter;

namespace QM_ItemCreatorTool.Model
{
    public class WeaponViewModel : ViewModelBase<WeaponTemplate>
    {
        // Here lie all the properties for the model?
        public WeaponViewModel(WeaponTemplate model) : base(model)
        {
            
        }

        public string? ID
        {
            get => _model.id;
            set
            {
                _model.id = value;
                RaisePropertyChanged();
            }
        }

        public int InventoryWidth
        {
            get => _model.inventoryWidth;
            set
            {
                _model.inventoryWidth = value;
                RaisePropertyChanged();
            }
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

        public int Range
        {
            get => _model.range;
            set
            {
                _model.range = value;
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
            set
            {
                _model.fractureWoundDamageBonus = value;
                RaisePropertyChanged();
            }
        }

        public float PainDamageMultiplier
        {
            get => _model.painDamageMultiplier;
            set
            {
                _model.painDamageMultiplier = value;
                RaisePropertyChanged();
            }
        }

        public float CritPainDamageMultiplier
        {
            get => _model.critPainDamageMultiplier;
            set
            {
                _model.critPainDamageMultiplier = value;
                RaisePropertyChanged();
            }
        }

        public float OffSlotCritChance
        {
            get => _model.offSlotCritChance;
            set
            {
                _model.offSlotCritChance = value;
                RaisePropertyChanged();
            }
        }

        public float MinDmgCapBonus
        {
            get => _model.minDmgCapBonus;
            set
            {
                _model.minDmgCapBonus = value;
                RaisePropertyChanged();
            }
        }

        public int RampUpValue
        {
            get => _model.rampUpValue;
            set
            {
                _model.rampUpValue = value;
                RaisePropertyChanged();
            }
        }

        public float FovLookAngleMult
        {
            get => _model.fovLookAngleMult;
            set
            {
                _model.fovLookAngleMult = value;
                RaisePropertyChanged();
            }
        }

        public string? Grip
        {
            get => _model.grip;
            set
            {
                _model.grip = value;
                RaisePropertyChanged();
            }
        }

        public bool HasBFGOverlay
        {
            get => _model.hasHFGOverlay;
            set
            {
                _model.hasHFGOverlay = value;
                RaisePropertyChanged();
            }
        }

        // Paths come later?

        //public List<string> randomAttackSoundBank { get; set; }

        //public List<string> randomDryShotSoundBank { get; set; }

        //public List<string> randomFailedAttackSoundBank { get; set; }

        //public List<string> randomReloadSoundBank { get; set; }

        //public float visualReachCellDuration { get; set; }

        //public List<string> entityFlySprites { get; set; }

        //public bool useCustomBullet { get; set; } = false;


        //public string bulletAssetPath { get; set; }
    }
}
