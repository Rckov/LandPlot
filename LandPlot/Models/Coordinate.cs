using System.Globalization;

namespace LandPlot.Models;

internal class Coordinate
{
    public double X { get; set; }
    public double Y { get; set; }

    public static Coordinate Parse(string line)
    {
        if (string.IsNullOrEmpty(line))
        {
            return null;
        }

        var array = line.Split(';');

        if (array.Length < 2)
        {
            return null;
        }

        if (TryToDouble(array[0], out var x) && TryToDouble(array[1], out var y))
        {
            return new Coordinate()
            {
                X = x,
                Y = y
            };
        }

        return null;
    }

    private static bool TryToDouble(string input, out double value)
    {
        return double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out value);
    }
}