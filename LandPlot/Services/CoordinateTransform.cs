using LandPlot.Models;
using LandPlot.Properties;

using Proj4Net;

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace LandPlot.Services;

internal class CoordinateTransform
{
    private const string TargetCoordinateSystemKey = "EPSG:4326";
    private const string TargetCoordinateSystemParameters = "+proj=longlat +ellps=WGS84 +datum=WGS84 +units=degrees";

    private readonly Dictionary<string, XElement> _coordinateSystems;

    public CoordinateTransform()
    {
        _coordinateSystems = new Dictionary<string, XElement>();
        LoadSystems();
    }

    private void LoadSystems()
    {
        var root = XElement.Parse(Resources.CoordinateSystems);

        foreach (var system in root.Elements())
        {
            var key = system.Attribute("name").Value;

            if (!_coordinateSystems.ContainsKey(key))
            {
                _coordinateSystems.Add(key, system);
            }
        }
    }

    public IEnumerable<string> GetCoordinateSystems()
    {
        return _coordinateSystems.Keys;
    }

    public IEnumerable<Contour> TransformContours(IEnumerable<Contour> contours, string systemKey)
    {
        foreach (var contour in contours)
        {
            yield return TransformContour(contour, systemKey);
        }
    }

    public Contour TransformContour(Contour contour, string systemKey)
    {
        var transformedCoordinates = contour.Coordinates.Select(c => TransformCoordinate(c, systemKey)).ToList();
        return new Contour { Name = contour.Name, Coordinates = transformedCoordinates };
    }

    public Coordinate TransformCoordinate(Coordinate coordinate, string systemKey)
    {
        var system = _coordinateSystems[systemKey];

        var crsFactory = new CoordinateReferenceSystemFactory();

        var crsSource = crsFactory.CreateFromParameters(system.Attribute("key").Value, system.Value);
        var crsTarget = crsFactory.CreateFromParameters(TargetCoordinateSystemKey, TargetCoordinateSystemParameters);

        var coordinateTransform = new BasicCoordinateTransform(crsSource, crsTarget);

        var sourcePoint = new ProjCoordinate(coordinate.Y, coordinate.X);
        var targetPoint = new ProjCoordinate();

        coordinateTransform.Transform(sourcePoint, targetPoint);

        return new Coordinate 
        { 
            X = targetPoint.X, 
            Y = targetPoint.Y 
        };
    }
}