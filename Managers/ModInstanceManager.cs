using QM_ItemCreatorTool.Model;
using QM_ItemCreatorTool.ViewModel;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.Managers
{
    public class ModInstanceManager : ViewModelBase
    {
        public ObservableCollection<ModDataViewModel> ModCollection { get; set; } = new ObservableCollection<ModDataViewModel>();

        private ModDataViewModel _currentMod = new ModDataViewModel(new ModDataModel());
        public ModDataViewModel CurrentMod
        {
            get { return _currentMod; }
            set
            {
                _currentMod = value;
                RaisePropertyChanged();
            }
        }
  
        public void LoadNewMod(string configFilePath)
        {
            ModDataViewModel modDataViewModel = new ModDataViewModel(new ModDataModel());
            bool loadResult = ModDataManager.LoadMod(configFilePath, ref modDataViewModel);
            if (loadResult)
            {
                // Then store it.
                ModCollection.Add(modDataViewModel);
            }
        }
    }
}