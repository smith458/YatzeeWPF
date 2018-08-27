using System;
using System.Windows.Input;

namespace Yatzee
{
  public class ParameterCommand<T> : ICommand
  {
    private readonly Action<T> _action;

    public ParameterCommand(Action<T> action)
    {
      _action = action;
    }

    public void Execute(object parameter)
    {
      _action?.Invoke((T) parameter);
    }

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public event EventHandler CanExecuteChanged;
  }
}
