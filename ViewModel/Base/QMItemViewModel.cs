using MGSC;
using QM_ItemCreatorTool.Properties;
using QM_WeaponImporter.Templates;
using System.Collections.ObjectModel;

namespace QM_ItemCreatorTool.ViewModel
{
    public class QMItemViewModel<T> : ViewModelBase<T> where T : BreakableItemRecordTemplate, new()
    {
        protected CustomItemContentDescriptor weaponDescriptor = new CustomItemContentDescriptor();
        public QMItemViewModel() { }
        public QMItemViewModel(T item) : base(item) { }

        #region Properties
        public string? SpritePath
        {
            get => weaponDescriptor.iconSpritePath;
            set
            {
                weaponDescriptor.iconSpritePath = value;
                RaisePropertyChanged();
            }
        }

        public string? SmallSpritePath
        {
            get => weaponDescriptor.smallIconSpritePath;
            set
            {
                weaponDescriptor.smallIconSpritePath = value;
                RaisePropertyChanged();
            }
        }

        public string? ShadowSpritePath
        {
            get => weaponDescriptor.shadowOnFloorSpritePath;
            set
            {
                weaponDescriptor.shadowOnFloorSpritePath = value;
                RaisePropertyChanged();
            }
        }

        public string? InheritedID
        {
            get => weaponDescriptor.baseItemId;
            set
            {
                weaponDescriptor.baseItemId = value;
                RaisePropertyChanged();
            }
        }

        public string? ID
        {
            get => _model.id;
            set
            {
                _model.id = value;
                weaponDescriptor.attachedId = value;
                RaisePropertyChanged();
            }
        }

        public float Price
        {
            get => _model.price;
            set { _model.price = value; RaisePropertyChanged(); }
        }
        public float Weight
        {
            get => _model.weight;
            set { _model.weight = value; RaisePropertyChanged(); }
        }
        public List<string> Categories
        {
            get => _model.categories;
            set
            {
                _model.categories = value;
                RaisePropertyChanged();
            }
        }
        public int TechLevel
        {
            get => _model.techLevel;
            set
            {
                _model.techLevel = value;
                RaisePropertyChanged();
            }
        }
        public int InventoryWidth
        {
            get => _model.inventoryWidthSize;
            set
            {
                _model.inventoryWidthSize = value;
                RaisePropertyChanged();
            }
        }
        public string RepairCategory
        {
            get => _model.repairCategory;
            set { _model.repairCategory = value; RaisePropertyChanged(); }
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

        #region Uncrafting Data (item transform)
        public ObservableCollection<CustomItemQuantity> DisasemblyList { get; set; } = new ObservableCollection<CustomItemQuantity>();

        public void AddUncraftingEntry()
        {
            DisasemblyList.Add(new CustomItemQuantity());
        }
        public ItemTransformationRecord GetItemTransformationRecord()
        {
            return new ItemTransformationRecord()
            {
                Id = ID,
                OutputItems = DisasemblyList.Select(x => x.GetOriginal()).ToList()
            };
        }
        #endregion

        #region Faction Data
        private ObservableCollection<FactionEntry> _factionRules = new ObservableCollection<FactionEntry>();
        public ObservableCollection<FactionEntry> FactionRules
        {
            get { return _factionRules; }
            set { _factionRules = value; RaisePropertyChanged(); }
        }

        public void AddFactionRule()
        {
            if (_factionRules == null) return;
            FactionRules.Add(new FactionEntry());
        }

        public void RemoveFactionRule(FactionEntry entry)
        {
            if (_factionRules == null) return;
            if (entry == null) return;
            FactionRules.Remove(entry);
        }
        #endregion
    }
}