using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace Yatzee
{
  public class ViewModel : ObservableObject
  {
    private Die[] _dice = new Die[5];
    private Random _rand;
    private int _roll;
    private ObservableCollection<ScoreItem> _scoreCard = new ObservableCollection<ScoreItem>();

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

    public ObservableCollection<ScoreItem> ScoreCard
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
      get => Scoring.ScoreOptions.Keys.Except(ScoreCard.Select(x => x.Name).ToArray());
    }

    public ViewModel()
    {
      Dice = Dice.Select((d, i) => new Die()).ToArray();
      _rand = new Random();
      Roll = 0;
      _scoreCard.CollectionChanged += notifyOptionsAvailable;
    }

    private int randDieVal()
    {
      return _rand.Next(1, 7);
    }

    public ICommand RollCommand => new DelegateCommand(RollDice);

    public void RollDice()
    {
      Array.ForEach(Dice, RollIfNotHeld);
      Roll += 1;
    }

    public void RollIfNotHeld(Die d)
    {
      d.Value = d.Hold ? d.Value : randDieVal();
    }

    public ICommand ScoreCommand => new ParameterCommand<string>(ScoreDice);

    public void ScoreDice(string option)
    {
      int[] dieValues = Dice.Select(x => x.Value).ToArray();
      int score = Scoring.ScoreOptions[option](dieValues);
      ScoreCard.Add(new ScoreItem(option, score, 1));
    }

    public void notifyOptionsAvailable(object sender, NotifyCollectionChangedEventArgs e) => OnPropertyChanged(nameof(OptionsAvailable));

  }
}
