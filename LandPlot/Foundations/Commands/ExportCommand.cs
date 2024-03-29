using LandPlot.Foundations.Commands.Base;
using LandPlot.ViewModels;

using System;
using System.Linq;

namespace LandPlot.Foundations.Commands;

internal class ExportCommand : BaseCommand
{
    private MainViewModel _mainViewModel;

    public ExportCommand(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public override void Execute(object parameter)
    {
        throw new NotImplementedException();
    }

    public override bool CanExecute(object parameter)
    {
        return _mainViewModel.Contours.Any();
    }
}