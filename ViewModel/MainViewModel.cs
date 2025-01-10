namespace QM_ItemCreatorTool.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region ViewModels
        private WeaponTabViewModel _weaponTabViewModel;
        public WeaponTabViewModel WeaponTabViewModel
        {
            get
            {
                return _weaponTabViewModel;
            }
            set
            {
                if (value != _weaponTabViewModel)
                {
                    _weaponTabViewModel = value;
                    RaisePropertyChanged();
                }
            }
        }

        private GeneralTabViewModel _generalTabViewModel;
        public GeneralTabViewModel GeneralTabViewModel
        {
            get
            {
                return _generalTabViewModel;
            }
            set
            {
                if (value != _generalTabViewModel)
                {
                    _generalTabViewModel = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        public MainViewModel(
            WeaponTabViewModel weaponViewModel, 
            GeneralTabViewModel generalTabViewModel)
        {
            WeaponTabViewModel = weaponViewModel;
            GeneralTabViewModel = generalTabViewModel;
        }
    }
}