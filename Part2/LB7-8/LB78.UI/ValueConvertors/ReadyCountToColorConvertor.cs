using System.Globalization;

namespace LB78.UI.ValueConvertors;

class ReadyCountToColorConvertor : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int readyCount && readyCount < 5)
            return Colors.Red;
        return Colors.DimGray;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
