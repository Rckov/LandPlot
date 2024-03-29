using LandPlot.Interfaces;

using LandPlotCoordinate.Interfaces;

namespace LandPlot.Services;

internal class CoordinateDrawing : ICoordinateDrawing
{
    private ITransform _transform;

    public CoordinateDrawing(ITransform transform)
    {
        _transform = transform;
    }
}