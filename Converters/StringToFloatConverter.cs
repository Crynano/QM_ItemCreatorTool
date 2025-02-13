using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace QM_ItemCreatorTool.Converters;
internal class StringToFloatConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // Transform string to float?
        var cleanString = value as string;
        if (!string.IsNullOrEmpty(cleanString))
        {
            Regex rgx = new Regex("[^a-zA-Z -]");
            cleanString = rgx.Replace(cleanString, "");
            _ = int.TryParse(cleanString, out var floatValue);
            return floatValue;
        }
        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var cleanString = value as string;
        if (!string.IsNullOrEmpty(cleanString))
        {
            Regex rgx = new Regex("[^a-zA-Z -]");
            cleanString = rgx.Replace(cleanString, "");
            _ = int.TryParse(cleanString, out var floatValue);
            return floatValue;
        }
        return 0;
    }
}
