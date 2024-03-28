using LandPlot.Models;
using LandPlot.Services;
using LandPlot.ViewModels.Base;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace LandPlot.ViewModels;

internal class MainViewModel : BaseViewModel
{
    private Contour _selectedContour;

    public Contour SelectedContour
    {
        get => _selectedContour;
        set => Set(ref _selectedContour, value);
    }

    private ObservableCollection<UIElement> _canvasChildren;

    public ObservableCollection<UIElement> CanvasChildren
    {
        get => _canvasChildren;
        set => Set(ref _canvasChildren, value);
    }

    private ObservableCollection<Contour> _contours = new();

    public ObservableCollection<Contour> Contours
    {
        get => _contours;
        set => Set(ref _contours, value);
    }
}