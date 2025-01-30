using QM_WeaponImporter;

namespace QM_ItemCreatorTool.ViewModel
{
    public class RangedViewModel : WeaponViewModel<RangedWeaponTemplate>
    {
        // Here lie all the properties for the model?
        public RangedViewModel() : base()
        {
            _model.itemClass = MGSC.ItemClass.Weapon;
        }

        public RangedViewModel(RangedWeaponTemplate model) : base(model)
        {
            itemDescriptor.attachedId = ID;
            _model.itemClass = MGSC.ItemClass.Weapon;
        }

        public string? DefaultGrenadeID
        {
            get => _model.defaultGrenadeId;
            set { _model.defaultGrenadeId = value; RaisePropertyChanged(); }
        }

        public List<string> AllowedGrenadesID
        {
            get => _model.AllowedGrenadeIds;
            set { _model.AllowedGrenadeIds = value; RaisePropertyChanged(); }
        }

        public int Range
        {
            get => _model.range;
            set { _model.range = value; RaisePropertyChanged(); }
        }

        public int RampUpValue
        {
            get => _model.rampUpValue;
            set { _model.rampUpValue = value; RaisePropertyChanged(); }
        }

        //public float visualReachCellDuration { get; set; }

        //public List<string> entityFlySprites { get; set; }

        //public bool useCustomBullet { get; set; } = false;

        //public string bulletAssetPath { get; set; }
    }
}