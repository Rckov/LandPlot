using LandPlot.Foundations.Commands.Base;
using LandPlot.ViewModels;

using System;

namespace LandPlot.Foundations.Commands;

internal class ImportCommand : BaseCommand
{
    private MainViewModel _mainViewModel;

    public ImportCommand(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public override void Execute(object parameter)
    {
        throw new NotImplementedException();
    }

    public override bool CanExecute(object parameter) => true;
}