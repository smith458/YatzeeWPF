using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Yatzee
{
  public class ViewModel : ObservableObject
  {
    private Die[] _dice = new Die[5];
    private Random _rand;
    private int _roll;

    public Die[] Dice
    {
      get => _dice;
      set
      {
        _dice = value;
        OnPropertyChanged(nameof(Dice));
      }
    }

    public int Roll
    {
      get => _roll;
      set
      {
        _roll = value;
        OnPropertyChanged(nameof(Roll));
      }
    }

    public ViewModel()
    {
      Dice = Dice.Select((d, i) => new Die(new ParameterCommand<int>(HoldDie, i))).ToArray();
      _rand = new Random();
      Roll = 0;
    }

    private int randDieVal()
    {
      return _rand.Next(1, 7);
    }

    public void RollDice()
    {
      Dice = Dice.Select(RollIfNotHeld).ToArray();
      Roll += 1;
    }

    public Die RollIfNotHeld(Die d)
    {
      d.Value = d.Hold ? d.Value : randDieVal();
      return d;
    }

    public string someName(bool hold)
    {
      return hold ? "Hold" : "Unhold";
    }

    public void HoldDie(int index)
    {
      Dice[index].Hold = !Dice[index].Hold;
    }

    public ICommand RollCommand => new DelegateCommand(RollDice);
  }
}
