using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yatzee.Annotations;

namespace Yatzee
{
  public class Die : INotifyPropertyChanged
  {
    private int _value;
    private bool _hold;

    public Die( int value = 1, bool hold = false)
    {
      Value = value;
      Hold = hold;
    }

    public int Value
    {
      get => _value;
      set
      {
        _value = value > 0 && value <= 6 ? value : _value;
        OnPropertyChanged();
      } 
    }

    public bool Hold
    {
      get => _hold;
      set
      {
        if (value == _hold) return;
        _hold = value;
        OnPropertyChanged();
      }
    }
    
    public ICommand HoldCommand => new DelegateCommand(() => this.Hold = !this.Hold);

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
