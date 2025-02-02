using QM_WeaponImporter.Templates;
using System.Globalization;
using System.Windows.Data;

namespace QM_ItemCreatorTool.Converters;

[ValueConversion(typeof(ConfigTableRecordTemplate), typeof(string))]
public class ConfigTableToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // Transform string to float?
        var configItem = value as ConfigTableRecordTemplate;
        if (configItem != null)
            return configItem.id;
        else return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}