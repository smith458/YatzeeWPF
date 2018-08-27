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
    private Dictionary<string, int> _scoreCard = new Dictionary<string, int>();

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

    public Dictionary<string, int> ScoreCard
    {
      get => _scoreCard;
      set
      {
        _scoreCard = value;
        OnPropertyChanged(nameof(ScoreCard));
        OnPropertyChanged(nameof(OptionsAvailable));
      }
    }

    public IEnumerable<string> OptionsAvailable
    {
      get => Scoring.ScoreOptions.Keys.Except(ScoreCard.Keys);
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

    public void HoldDie(int index)
    {
      Dice[index].Hold = !Dice[index].Hold;
    }

    public ICommand RollCommand => new DelegateCommand(RollDice);
  }
}
