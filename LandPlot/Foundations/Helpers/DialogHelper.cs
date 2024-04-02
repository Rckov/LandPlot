using Microsoft.Win32;

namespace LandPlot.Foundations.Helpers;

internal static class DialogHelper
{
    public static string OpenDialog(string filter)
    {
        return ShowDialog(new OpenFileDialog() { Filter = filter });
    }

    public static string SaveDialog(string filter)
    {
        return ShowDialog(new SaveFileDialog() { Filter = filter });
    }

    private static string ShowDialog(FileDialog fileDialog)
    {
        return fileDialog.ShowDialog() == true ? fileDialog.FileName : null;
    }
}