using LandPlot.Models;

using System.Collections.Generic;

namespace LandPlot.Foundations.Interfaces;

internal interface ILoader
{
    void Save(IEnumerable<Contour> contours, string filePath);

    IEnumerable<Contour> Load(string filePath);
}