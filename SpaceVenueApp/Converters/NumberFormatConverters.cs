using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SpaceVenueApp.Converters;

public class CurrencyDisplayConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Length < 2 || values[0] is null || values[1] is null)
        {
            return string.Empty;
        }

        var isRtl = values[1] is bool enabled && enabled;
        if (values[0] is not IFormattable formattable)
        {
            return values[0]?.ToString() ?? string.Empty;
        }

        var formatted = formattable.ToString("C", CultureInfo.CurrentCulture);
        return isRtl ? ArabicNumerals.ToArabicNumerals(formatted) : formatted;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

public class TimeDisplayConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Length < 2 || values[0] is null || values[1] is null)
        {
            return string.Empty;
        }

        var isRtl = values[1] is bool enabled && enabled;
        var formatted = values[0]?.ToString() ?? string.Empty;
        return isRtl ? ArabicNumerals.ToArabicNumerals(formatted) : formatted;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

public static class ArabicNumerals
{
    private static readonly char[] ArabicDigits = { '٠', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩' };

    public static string ToArabicNumerals(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return input;
        }

        return new string(input.Select(ch => char.IsDigit(ch) ? ArabicDigits[ch - '0'] : ch).ToArray());
    }
}
