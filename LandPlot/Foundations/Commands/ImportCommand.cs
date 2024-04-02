using LandPlot.Foundations.Commands.Base;
using LandPlot.ViewModels;

using System;

namespace LandPlot.Foundations.Commands;

internal class ImportCommand : BaseCommand
{
    private MainViewModel _viewModel;

    public ImportCommand(MainViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override void Execute(object parameter)
    {
        throw new NotImplementedException();
    }

    public override bool CanExecute(object parameter) => true;
}