using LandPlot.Foundations.Commands.Base;
using LandPlot.Foundations.Helpers;
using LandPlot.ViewModels;

namespace LandPlot.Foundations.Commands;

internal class ScreenCommand : BaseCommand
{
    private readonly MainViewModel _viewModel;

    public ScreenCommand(MainViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override async void Execute(object parameter)
    {
        var filePath = DialogHelper.SaveDialog("Чертеж |*.png");

        if (string.IsNullOrEmpty(filePath))
        {
            return;
        }

        ImageHelper.SaveImageToFile(_viewModel.Image, filePath);//TODO Можно наверно сделать какой-нибудь local storage, т.е. еще один сервис который будет сохранять файла. А как он это делает уже не важно, в файл или в базу. Хэлперы мне не особо нравятся, чаще всего в них скидывают то, что никуда больше не пришлось. При таком подходе они слишком быстро разрастаются.

        await _viewModel.SetDelayStatus($"файл сохранен по пути {filePath}");
    }

    public override bool CanExecute(object parameter)
    {
        return _viewModel.Image != null && _viewModel.Image.Source != null;
    }
}