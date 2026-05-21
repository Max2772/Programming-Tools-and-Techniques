using System.Globalization;
using LB78.UI.Services;

namespace LB78.UI.ValueConvertors;

class IdToImageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int id && id > 0)
        {
            var path = ImageStorageService.GetImagePath(id);
            if (File.Exists(path))
                return path;
        }

        return "dotnet_bot.png";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
