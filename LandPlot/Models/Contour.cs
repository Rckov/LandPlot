using LandPlotCoordinate.Models;

using System.Collections.Generic;

namespace LandPlot.Models;

internal class Contour
{
    public string Name { get; set; }
    public List<Coordinate> Coordinates { get; set; } = new();
}