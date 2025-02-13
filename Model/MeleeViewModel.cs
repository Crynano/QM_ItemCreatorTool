using QM_WeaponImporter;

namespace QM_ItemCreatorTool.ViewModel
{
    public class MeleeViewModel : WeaponViewModel<MeleeWeaponTemplate>
    {
        // Here lie all the properties for the model?
        public MeleeViewModel() : base()
        {

        }

        public MeleeViewModel(MeleeWeaponTemplate model) : base(model)
        {
            itemDescriptor.attachedId = ID;
            _model.itemClass = MGSC.ItemClass.Weapon;
        }

        public bool IsMelee
        {
            get => _model.isMelee;
            set { _model.isMelee = value; RaisePropertyChanged(); }
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



        //public float visualReachCellDuration { get; set; }

        //public List<string> entityFlySprites { get; set; }

        //public bool useCustomBullet { get; set; } = false;


        //public string bulletAssetPath { get; set; }
    }
}