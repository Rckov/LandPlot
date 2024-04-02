using LandPlot.Models;

using System.Collections.Generic;
using System.Windows.Controls;

namespace LandPlot.Foundations.Interfaces;

internal interface IDrawing
{
    Image Draw(IEnumerable<Contour> contours);
}