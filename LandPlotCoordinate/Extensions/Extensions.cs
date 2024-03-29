using LandPlotCoordinate.Models;

using System;
using System.Globalization;

namespace LandPlotCoordinate.Extensions;

public static class Extensions
{
    public static bool TryParseToDouble(this string source, out double value)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return double.TryParse(source, NumberStyles.Any, CultureInfo.InvariantCulture, out value);
    }

    public static bool TryParsePoint(this string source, char separator, out Coordinate point)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        point = default;

        var array = source.Split(separator);

        if (array.Length < 2)
        {
            return false;
        }

        if (TryParseToDouble(array[0], out var x) && TryParseToDouble(array[1], out var y))
        {
            point = new Coordinate(x, y);
            return true;
        }

        return false;
    }
}