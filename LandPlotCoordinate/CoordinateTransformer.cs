using LandPlotCoordinate.Interfaces;
using LandPlotCoordinate.Models;
using LandPlotCoordinate.Properties;

using Proj4Net;

using System.Collections.Generic;
using System.Xml.Linq;

namespace LandPlotCoordinate;

public class CoordinateTransformer : ITransform//TODO Для этого прям отдельная библиотека нужна? Не помню, мб ты хотел в другом проекте ее еще использовать.
{
    private const string TargetCoordinateSystemKey = "EPSG:4326";
    private const string TargetCoordinateSystemParameters = "+proj=longlat +ellps=WGS84 +datum=WGS84 +units=degrees";

    private readonly Dictionary<string, XElement> _coordinateSystems = new();

    public CoordinateTransformer()
    {
        Load();
    }

    public IEnumerable<string> Systems => _coordinateSystems.Keys;

    public static CoordinateTransformer Instance { get; } = new CoordinateTransformer();

    public void Load()
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

    public Coordinate Transform(Coordinate coordinate, string systemKey)
    {
        var system = _coordinateSystems[systemKey];

        var crsFactory = new CoordinateReferenceSystemFactory();

        var crsSource = crsFactory.CreateFromParameters(system.Attribute("key").Value, system.Value);
        var crsTarget = crsFactory.CreateFromParameters(TargetCoordinateSystemKey, TargetCoordinateSystemParameters);

        var coordinateTransform = new BasicCoordinateTransform(crsSource, crsTarget);

        var sourcePoint = new ProjCoordinate(coordinate.Y, coordinate.X);
        var targetPoint = new ProjCoordinate();

        coordinateTransform.Transform(sourcePoint, targetPoint);

        return new Coordinate(targetPoint.X, targetPoint.Y);
    }
}