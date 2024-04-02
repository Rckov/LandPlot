using LandPlot.Foundations.Commands;
using LandPlot.Models;
using LandPlot.ViewModels.Base;

using LandPlotCoordinate;
using LandPlotCoordinate.Interfaces;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace LandPlot.ViewModels;

internal class MainViewModel : BaseViewModel
{
    private readonly ITransform _transform = CoordinateTransformer.Instance;

    private string _status = "";
    private string _selectedSystem;

    private Image _image;
    private Contour _selectedContour;

    private ObservableCollection<Contour> _contours = new();

    public MainViewModel()
    {
        DrawCommand = new DrawCommand(_transform, this);

        ImportCommand = new ImportCommand(this);
        ExportCommand = new ExportCommand(this);
        ScreenCommand = new ScreenCommand(this);
    }

    public ICommand DrawCommand { get; }
    public ICommand ImportCommand { get; }
    public ICommand ExportCommand { get; }
    public ICommand ScreenCommand { get; }

    public Contour SelectedContour
    {
        get => _selectedContour;
        set => Set(ref _selectedContour, value);
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

    public async Task SetDelayStatus(string status, int delay = 3000)
    {
        Status = status;
        await Task.Delay(delay);

        Status = string.Empty;
    }
}