using LandPlot.Foundations.Commands.Base;
using LandPlot.Foundations.Services;
using LandPlot.Models;
using LandPlot.ViewModels;

using LandPlotCoordinate.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace LandPlot.Foundations.Commands;

internal class DrawCommand : BaseCommand
{
    private readonly IDrawing _drawing;
    private readonly ITransform _transform;

    private readonly MainViewModel _viewModel;

    public DrawCommand(ITransform transform, MainViewModel viewModel)
    {
        _drawing = new DrawingService();

        _transform = transform;
        _viewModel = viewModel;
    }

    public override void Execute(object parameter)
    {
        try
        {
            var system = _viewModel.SelectedSystem;
            var contours = TransformContours(_viewModel.Contours, system);

            _viewModel.Image = _drawing.Draw(contours);
            _viewModel.Status = $"Отрисовано {contours.Count()} контуров, система координат {system}";
        }
        catch (Exception ex)
        {
            _viewModel.Status = ex.Message.Replace(Environment.NewLine, " ");
        }
    }

    public override bool CanExecute(object parameter)
    {
        return _viewModel.CanvasChildren.Any();
    }

    private IEnumerable<Contour> TransformContours(IEnumerable<Contour> contours, string system)
    {
        foreach (var contour in contours)
        {
            var transformedContour = new Contour
            {
                Name = contour.Name,
                Coordinates = contour.Coordinates.Select(point => _transform.Transform(point, system)).ToList()
            };
            yield return transformedContour;
        }
    }
}