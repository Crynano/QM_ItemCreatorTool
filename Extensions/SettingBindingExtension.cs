using System.Windows.Data;

namespace QM_ItemCreatorTool.Extensions
{
    public class SettingBindingExtension : Binding
    {
        public SettingBindingExtension()
        {
            Initialize();
        }

        public SettingBindingExtension(string path) : base(path)
        {
            Initialize();
        }

        private void Initialize()
        {
            this.Source = QM_ItemCreatorTool.Properties.Settings.Default;
            this.Mode = BindingMode.TwoWay;
        }
    }
}
