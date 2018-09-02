using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using Yatzee.Annotations;
using MahApps.Metro.Controls.Dialogs;

namespace Yatzee
{
  public class ViewModel : ObservableObject
  {
    private const int NUM_ROLLS = 3;

    private Die[] _dice = new Die[5];
    private readonly Random _rand;
    private int _roll;
    private ObservableCollection<ScoreItem> _scoreCard = new ObservableCollection<ScoreItem>();
    private int _score;
    private string _errorText;

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

    public string ErrorText
    {
      get => _errorText;
      set
      {
        _errorText = value;
        OnPropertyChanged(nameof(ErrorText));
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
      _scoreCard.CollectionChanged += updateCategoriesAvailable;
    }

    public ICommand RollCommand => new DelegateCommand(RollDice);

    public void RollDice()
    {
      if (Roll < NUM_ROLLS)
      {
        Array.ForEach(Dice, RollIfNotHeld);
        Roll += 1;
        ErrorText = "";
      }
      else
      {
        ErrorText = "You must score your hand of dice after the third roll.";
      }
    }

    public void RollIfNotHeld(Die d)
    {
      d.Value = d.Hold ? d.Value : randDieVal();
    }

    private int randDieVal()
    {
      return _rand.Next(1, 7);
    }

    public ICommand ScoreCommand => new ParameterCommand<string>(ScoreDice);

    public void ScoreDice(string category)
    {
      if (Roll == 0)
      {
        ErrorText = "You cannot score before rolling!";
        return;
      }

      ErrorText = "";
      int[] dieValues = Dice.Select(x => x.Value).ToArray();
      var categoryItem = Scoring.ScoreCategories.First(x => x.Name == category);
      int score = categoryItem.ScoreFunc(dieValues);
      int rank = categoryItem.Rank;

      // Handle extra yatzee
      if (category == "Yatzee" && score == 50)
      {
        if (ScoreCard.Any(x => x.Name == "Yatzee 1"))
        {
          score = 100;
        }
        else
        {
          category = "Yatzee 1";
        }
      }

      ScoreCard.Add(new ScoreItem(category, score, rank));
      ResetDice(this.Dice);
      Roll = 0;

      if (!CategoriesAvailable.Any(x => Scoring.UpperCategories.Contains(x)) && !ScoreCard.Any(x => x.Name == "Upper Bonus"))
      {
        int upperScore = ScoreCard.Where(x => Scoring.UpperCategories.Contains(x.Name))
                                  .Select(x => x.Score).Sum();
        if (upperScore >= Scoring.UPPER_SCORE_FOR_BONUS)
        {
          ScoreCard.Add(new ScoreItem("Upper Bonus", Scoring.UPPER_BONUS, 13));
        }
      }

      Score = ScoreCard.Select(x => x.Score).Sum();

      if (!CategoriesAvailable.Any())
      {
        EndGameDialog();
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

    private async Task EndGameDialog()
    {
      var dc = DialogCoordinator.Instance;
      var window = Application.Current.MainWindow as MetroWindow;
      var result = await window.ShowMessageAsync("Game Over", $"Your Score: {Score}\nWould you like to play another game?", MessageDialogStyle.AffirmativeAndNegative);
      if (result == MessageDialogResult.Affirmative)
      {
        ResetDice(Dice);
        ScoreCard.Clear();
      }
      else
      {
        Application.Current.Shutdown();
      }
    }

    public void updateCategoriesAvailable(object sender, NotifyCollectionChangedEventArgs e) => OnPropertyChanged(nameof(CategoriesAvailable));

  }
}
