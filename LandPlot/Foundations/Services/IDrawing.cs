using LandPlot.Models;

using System.Collections.Generic;
using System.Windows.Controls;

namespace LandPlot.Foundations.Services;

internal interface IDrawing
{
    Image Draw(IEnumerable<Contour> contours);
}