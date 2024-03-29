using LandPlotCoordinate.Interfaces;

namespace LandPlot.Foundations.Services;

internal class DrawingService : IDrawing
{
    private ITransform _transform;

    public DrawingService(ITransform transform)
    {
        _transform = transform;
    }
}