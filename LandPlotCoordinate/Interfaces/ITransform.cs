using LandPlotCoordinate.Models;

using System.Collections.Generic;

namespace LandPlotCoordinate.Interfaces;

public interface ITransform
{
    IEnumerable<string> Systems { get; }

    Coordinate Transform(Coordinate coordinate, string systemKey);
}