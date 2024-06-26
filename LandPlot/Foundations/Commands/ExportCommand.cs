﻿using LandPlot.Foundations.Commands.Base;
using LandPlot.Foundations.Helpers;
using LandPlot.Foundations.Utils;
using LandPlot.ViewModels;

using System.Linq;

namespace LandPlot.Foundations.Commands;

internal class ExportCommand : BaseCommand
{
    private readonly MainViewModel _viewModel;

    public ExportCommand(MainViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override async void Execute(object parameter)
    {
        var filePath = DialogHelper.SaveDialog("Файл с координатами|*.txt");

        if (string.IsNullOrEmpty(filePath))
        {
            return;
        }

        var load = new LoadService();
        load.Save(_viewModel.Contours, filePath);

        await _viewModel.SetDelayStatus($"Файл сохранен по пути {filePath}");
    }

    public override bool CanExecute(object parameter)
    {
        return _viewModel.Contours.Any();
    }
}