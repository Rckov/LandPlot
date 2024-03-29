using LandPlotCoordinate.Extensions;

using System;

namespace LandPlotCoordinate.Models;

public class Coordinate
{
    public Coordinate(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; set; }
    public double Y { get; set; }

    public static bool TryParsePoint(string source, char separator, out Coordinate point)
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

        if (array[0].TryParseToDouble(out var x) && array[1].TryParseToDouble(out var y))
        {
            point = new Coordinate(x, y);
            return true;
        }

        return false;
    }
}