using LandPlot.Commands.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandPlot.Commands;
internal class RelayCommand : BaseCommand
{
    private readonly Action<object> _execute;
    private readonly Func<object, bool> _canExecute;

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public override void Execute(object parameter) => _execute(parameter);

    public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
}
