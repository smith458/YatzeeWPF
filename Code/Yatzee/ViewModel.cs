using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Yatzee.Annotations;

namespace Yatzee
{
  public class ViewModel : ObservableObject
  {
    private Die[] _dice = new Die[5];
    private Random _rand;
    private int _roll;
    private ObservableCollection<ScoreItem> _scoreCard = new ObservableCollection<ScoreItem>();
    private int _score;

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
        OnPropertyChanged(nameof(CategoriesAvailable));
      }
    }

    public int Score
    {
      get => _score;
      set
      {
        _score = value;
        OnPropertyChanged(nameof(Score));
      }
    }

    public IEnumerable<string> CategoriesAvailable =>
      Scoring.ScoreCategories.Select(x => x.Name).Except(ScoreCard.Select(x => x.Name).ToArray());

    public ViewModel()
    {
      Dice = Dice.Select((d, i) => new Die(0)).ToArray();
      _rand = new Random();
      Roll = 0;
      Score = 0;
      _scoreCard.CollectionChanged += notifyOptionsAvailable;
    }

    private int randDieVal()
    {
      return _rand.Next(1, 7);
    }

    public ICommand RollCommand => new DelegateCommand(RollDice);

    public void RollDice()
    {
      if (Roll < 3)
      {
        Array.ForEach(Dice, RollIfNotHeld);
        Roll += 1;
      }
      else
      {
        MessageBox.Show("You must score your hand of dice after the third roll.");
      }
    }

    public void ResetDice(Die[] dice)
    {
      Array.ForEach(dice, die =>
      {
        die.Value = 0;
        die.Hold = false;
      });
    }

    public void RollIfNotHeld(Die d)
    {
      d.Value = d.Hold ? d.Value : randDieVal();
    }

    public ICommand ScoreCommand => new ParameterCommand<string>(ScoreDice);

    public void ScoreDice(string category)
    {
      int[] dieValues = Dice.Select(x => x.Value).ToArray();
      var categoryItem = Scoring.ScoreCategories.First(x => x.Name == category);
      int score = categoryItem.ScoreFunc(dieValues);
      int rank = categoryItem.Rank;
      ScoreCard.Add(new ScoreItem(category, score, rank));
      ResetDice(this.Dice);
      Roll = 0;
      Score += score;
    }

    public void notifyOptionsAvailable(object sender, NotifyCollectionChangedEventArgs e) => OnPropertyChanged(nameof(CategoriesAvailable));

  }
}
