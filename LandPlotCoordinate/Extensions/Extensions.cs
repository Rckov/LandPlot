using System;
using System.Globalization;

namespace LandPlotCoordinate.Extensions;

internal static class Extensions
{
    public static bool TryParseToDouble(this string source, out double value)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return double.TryParse(source, NumberStyles.Any, CultureInfo.InvariantCulture, out value);
    }
}