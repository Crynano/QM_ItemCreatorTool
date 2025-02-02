using QM_WeaponImporter.Templates;

namespace QM_ItemCreatorTool.ViewModel;
public class FireModeViewModel : ConfigTableViewModel<FireModeRecordTemplate>
{
    public float Accuracy
    {
        get { return _model.Accuracy; }
        set { _model.Accuracy = value; RaisePropertyChanged(); }
    }
    public int AmmoPerShot
    {
        get { return _model.AmmoPerShot; }
        set { _model.AmmoPerShot = value; RaisePropertyChanged(); }
    }
    public float DamageMultiplier
    {
        get { return _model.DamageMult; }
        set { _model.DamageMult = value; RaisePropertyChanged(); }
    }
    public float DelayInSecsBetweenShots
    {
        get { return _model.DelayInSecsBetweenShots; }
        set { _model.DelayInSecsBetweenShots = value; RaisePropertyChanged(); }
    }
    public bool RequiredAllAmmoToShot
    {
        get { return _model.RequiredAllAmmoToShot; }
        set { _model.RequiredAllAmmoToShot = value; RaisePropertyChanged(); }
    }
    public float ScatterAngle
    {
        get { return _model.ScatterAngle; }
        set { _model.ScatterAngle = value; RaisePropertyChanged(); }
    }
    public int WeaponCastsCount
    {
        get { return _model.WeaponCastsCount; }
        set { _model.WeaponCastsCount = value; RaisePropertyChanged(); }
    }
    public string SpritePath
    {
        get { return _model.FireModeSpritePath; }
        set { _model.FireModeSpritePath = value; RaisePropertyChanged(); }
    }
}