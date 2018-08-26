using System;
using System.Windows.Input;

namespace Yatzee
{
  public class ParameterCommand<T> : ICommand
  {
    private readonly Action<T> _action;
    private readonly T _index;

    public ParameterCommand(Action<T> action, T index)
    {
      _action = action;
      _index = index;
    }

    public void Execute(object parameter)
    {
      _action?.Invoke(_index);
    }

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public event EventHandler CanExecuteChanged;
  }
}
