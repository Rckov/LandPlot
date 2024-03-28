using LandPlot.Models;

using System.Collections.Generic;

namespace LandPlot.Interfaces;

internal interface ICoordinateTransformer
{
    IEnumerable<string> GetCoordinateSystems();

    Contour TransformContour(Contour contour, string systemKey);

    IEnumerable<Contour> TransformContours(IEnumerable<Contour> contours, string systemKey);
}