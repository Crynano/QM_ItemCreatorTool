using QM_ItemCreatorTool.Managers;

namespace QM_ItemCreatorTool.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region ViewModels
        public RangedTabViewModel RangedTabViewModel { get; set; }
        public GeneralTabViewModel GeneralTabViewModel { get; set; }
        public MeleeTabViewModel MeleeTabViewModel { get; set; }
        public ItemReceiptTabViewModel ItemReceiptTabViewModel { get; set; }
        public LocalizationTabViewModel LocalizationTabViewModel { get; set; }
        public AmmoTabViewModel AmmoTabViewModel { get; set; }
        #endregion

        private UserConfigAndSaviourManager userSaviour;
        public MainViewModel(
            RangedTabViewModel rangedViewModel,
            MeleeTabViewModel meleeTabViewModel,
            GeneralTabViewModel generalTabViewModel,
            ItemReceiptTabViewModel itemReceiptTabViewModel,
            LocalizationTabViewModel localizationTabViewModel,
            AmmoTabViewModel ammoTabViewModel)
        {
            userSaviour = new UserConfigAndSaviourManager();

            RangedTabViewModel = rangedViewModel;
            GeneralTabViewModel = generalTabViewModel;
            MeleeTabViewModel = meleeTabViewModel;
            ItemReceiptTabViewModel = itemReceiptTabViewModel;
            LocalizationTabViewModel = localizationTabViewModel;
            AmmoTabViewModel = ammoTabViewModel;
        }

        public override async Task LoadAsync()
        {
            userSaviour.Initialize();
        }
    }
}