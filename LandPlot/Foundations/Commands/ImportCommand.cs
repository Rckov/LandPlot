using LandPlot.Foundations.Commands.Base;
using LandPlot.Foundations.Helpers;
using LandPlot.Foundations.Utils;
using LandPlot.ViewModels;

using System.Linq;

namespace LandPlot.Foundations.Commands;

internal class ImportCommand : BaseCommand
{
    private readonly MainViewModel _viewModel;

    public ImportCommand(MainViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override async void Execute(object parameter)
    {
        var filePath = DialogHelper.OpenDialog("Файл с координатами|*.txt");

        if (string.IsNullOrEmpty(filePath))
        {
            return;
        }

        var load = new LoadService();
        var contours = load.Load(filePath);

        if (contours.Any())
        {
            foreach (var contour in contours)
            {
                _viewModel.Contours.Add(contour);
            }

            await _viewModel.SetDelayStatus($"Файл загружен, получено {contours.Count()} контуров");
        }
    }

    public override bool CanExecute(object parameter) => true;
}