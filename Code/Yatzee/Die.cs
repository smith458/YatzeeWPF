using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Yatzee
{
  public class Die
  {
    private int _value;

    public Die( int value = 1, bool hold = false)
    {
      Value = value;
      Hold = hold;
    }

    public int Value
    {
      get => _value;
      set => _value = value > 0 && value <= 6 ? value : _value;
    }

    public bool Hold { get; set; }
  }
}
