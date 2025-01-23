using QM_ItemCreatorTool.Providers;

namespace QM_ItemCreatorTool.Managers
{
    public class DataProviderManager
    {
        private Lazy<List<string>> _weaponClasses = new Lazy<List<string>>(() => new WeaponClassProvider().GetData().ToList() );
        public List<string> WeaponClasses { get {  return _weaponClasses.Value; } }

        // Subclass
        private Lazy<List<string>> _weaponSubclasses = new Lazy<List<string>>(() => new WeaponSubclassProvider().GetData().ToList());
        public List<string> WeaponSubclasses { get { return _weaponSubclasses.Value; } }
        
        // Others
        private Lazy<List<string>> _gripTypes = new Lazy<List<string>>(() => new GripTypesProvider().GetData().ToList());
        public List<string> GripTypes { get { return _gripTypes.Value; } }
        
        // Fire types? But this will be populated from the beginning.
        // Maybe the user will be able to add their own firemodes who knows
        private Lazy<List<string>> _fireModes = new Lazy<List<string>>(() => new FiremodesDataProvider().GetData().ToList());
        public List<string> FireModes { get { return _fireModes.Value; } }

        // Tags provider?
        private Lazy<List<string>> _categories = new Lazy<List<string>>(() => new CategoriesTagsProvider().GetData().ToList());
        public List<string> Categories { get { return _categories.Value; } }

        // Some grenades
        private Lazy<List<string>> _grenades = new Lazy<List<string>>(() => new GrenadesProvider().GetData().ToList());
        public List<string> Grenades { get { return _grenades.Value; } }

        // Factions
        private Lazy<List<string>> _factions = new Lazy<List<string>>(() => new FactionsProvider().GetData().ToList());
        public List<string> Factions { get { return _factions.Value; } }
        // Chips in-game
        private Lazy<List<string>> _chips = new Lazy<List<string>>(() => new ChipsProvider().GetData().ToList());
        public List<string> Chips { get { return _chips.Value; } }
    }
}
