using QM_ItemCreatorTool.Managers;

namespace QM_ItemCreatorTool.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region ViewModels
        public WeaponTabViewModel WeaponTabViewModel { get; set; }
        public GeneralTabViewModel GeneralTabViewModel { get; set; }
        public MeleeTabViewModel MeleeTabViewModel { get; set; }
        public ItemReceiptTabViewModel ItemReceiptTabViewModel { get; set; }
        public LocalizationTabViewModel LocalizationTabViewModel { get; set; }
        #endregion

        private UserConfigAndSaviourManager userSaviour;
        public MainViewModel(
            WeaponTabViewModel weaponViewModel,
            MeleeTabViewModel meleeTabViewModel,
            GeneralTabViewModel generalTabViewModel,
            ItemReceiptTabViewModel itemReceiptTabViewModel,
            LocalizationTabViewModel localizationTabViewModel)
        {
            userSaviour = new UserConfigAndSaviourManager();

            WeaponTabViewModel = weaponViewModel;
            GeneralTabViewModel = generalTabViewModel;
            MeleeTabViewModel = meleeTabViewModel;
            ItemReceiptTabViewModel = itemReceiptTabViewModel;
            LocalizationTabViewModel = localizationTabViewModel;
        }

        public override async Task LoadAsync()
        {
            userSaviour.Initialize();
        }
    }
}