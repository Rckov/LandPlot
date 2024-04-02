using LandPlot.Foundations.Interfaces;
using LandPlot.Models;

using LandPlotCoordinate.Models;

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LandPlot.Foundations.Utils;

internal class LoadService : ILoader
{
    private const char CoordinateSeparator = ';';

    public void Save(IEnumerable<Contour> contours, string filePath)
    {
        using var writer = new StreamWriter(filePath);

        foreach (var contour in contours)
        {
            foreach (var coordinate in contour.Coordinates)
            {
                writer.WriteLine($"{coordinate.X}{CoordinateSeparator}{coordinate.Y}");
            }
            writer.WriteLine();
        }
    }

    public IEnumerable<Contour> Load(string filePath)
    {
        var contours = new List<Contour>();
        var currentContour = new Contour();

        int contourIndex = 0;

        foreach (var line in File.ReadLines(filePath))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                AddContourIfNotEmpty(contours, currentContour, contourIndex);
                currentContour = new Contour();
                contourIndex++;
                continue;
            }

            if (TryParseCoordinate(line, out var coordinate))
            {
                currentContour.Coordinates.Add(coordinate);
            }
        }

        AddContourIfNotEmpty(contours, currentContour, contourIndex);

        return contours;
    }

    private static void AddContourIfNotEmpty(List<Contour> contours, Contour currentContour, int contourIndex)
    {
        if (currentContour.Coordinates.Any())
        {
            currentContour.Name = $"Контур {contourIndex + 1}";
            contours.Add(currentContour);
        }
    }

    private static bool TryParseCoordinate(string source, out Coordinate coordinate)
    {
        coordinate = default;

        if (Coordinate.TryParsePoint(source, CoordinateSeparator, out var parsedCoordinate))
        {
            coordinate = parsedCoordinate;
            return true;
        }

        return false;
    }
}