using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Yatzee
{
  class DelegateCommand : ICommand
  {
    private readonly Action _action;

    public DelegateCommand(Action action)
    {
      _action = action;
    }

    public void Execute(object parameter)
    {
      _action?.Invoke();
    }

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public event EventHandler CanExecuteChanged;
  }
}
