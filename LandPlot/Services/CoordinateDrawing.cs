using LandPlot.Interfaces;

namespace LandPlot.Services;

internal class CoordinateDrawing : ICoordinateDrawing
{
    private ICoordinateTransformer _transform;

    public CoordinateDrawing(ICoordinateTransformer transform)
    {
        _transform = transform;
    }
}