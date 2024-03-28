using System.Collections.Generic;

namespace LandPlot.Models;

internal class Contour
{
    public string Name { get; set; }

    public List<Coordinate> Coordinates { get; set; } = new();

    public void Add(Coordinate coordinate)
    {
        if (coordinate == null)
        {
            return;
        }

        Coordinates.Add(coordinate);
    }
}