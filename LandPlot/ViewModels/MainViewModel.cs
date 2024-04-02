using LandPlot.Foundations.Commands;
using LandPlot.Models;
using LandPlot.ViewModels.Base;

using LandPlotCoordinate;
using LandPlotCoordinate.Interfaces;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LandPlot.ViewModels;

internal class MainViewModel : BaseViewModel
{
    private readonly ITransform _transform = CoordinateTransformer.Instance;

    private string _status;
    private string _selectedSystem;

    private Image _image;
    private Contour _selectedContour;

    private ObservableCollection<Contour> _contours = new();
    private ObservableCollection<UIElement> _canvasChildren = new();

    public MainViewModel()
    {
        DrawCommand = new DrawCommand(_transform, this);
        ImportCommand = new ImportCommand(this);
        ExportCommand = new ExportCommand(this);
    }

    public ICommand DrawCommand { get; }
    public ICommand ImportCommand { get; }
    public ICommand ExportCommand { get; }

    public Contour SelectedContour
    {
        get => _selectedContour;
        set => Set(ref _selectedContour, value);
    }

    public ObservableCollection<UIElement> CanvasChildren
    {
        get => _canvasChildren;
        set => Set(ref _canvasChildren, value);
    }

    public ObservableCollection<Contour> Contours
    {
        get => _contours;
        set => Set(ref _contours, value);
    }

    public Image Image
    {
        get => _image;
        set => Set(ref _image, value);
    }

    public string Status
    {
        get => _status;
        set => Set(ref _status, value);
    }

    public string SelectedSystem
    {
        get => _selectedSystem;
        set => Set(ref _selectedSystem, value);
    }

    public IEnumerable<string> CoordinateSystems => _transform.Systems;
}