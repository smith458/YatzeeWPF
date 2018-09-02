using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Yatzee.Annotations;

namespace Yatzee
{
  public delegate int ScoringFunc(int[] dieValues);

  public static class Scoring
  {
    public const int UPPER_SCORE_FOR_BONUS = 63;
    public const int UPPER_BONUS = 35;

    public static readonly CategoryItem[] ScoreCategories = new CategoryItem[]
    {
      new CategoryItem("Ones", SumDieValue(1), 0),
      new CategoryItem("Twos", SumDieValue(2), 1 ),
      new CategoryItem("Threes", SumDieValue(3), 2 ),
      new CategoryItem("Fours", SumDieValue(4), 3 ),
      new CategoryItem("Fives", SumDieValue(5), 4 ),
      new CategoryItem("Sixes", SumDieValue(6), 5 ),
      new CategoryItem("Three of a Kind", ScoreOfAKind(3), 6 ),
      new CategoryItem("Four of a Kind", ScoreOfAKind(4), 7 ),
      new CategoryItem("Small Straight", ScoreSmallStraight, 8 ),
      new CategoryItem("Large Straight", ScoreLargeStraight, 9 ),
      new CategoryItem("Full House", ScoreFullHouse, 10 ),
      new CategoryItem("Yatzee", ScoreYatzee, 11 ),
      new CategoryItem("Chance", SumDie, 12 ),

    };

    public static readonly string[] UpperCategories = new string[]
    {
      "Ones",
      "Twos",
      "Threes",
      "Fours",
      "Fives",
      "Sixes"
    };

    public static ScoringFunc SumDieValue(int val)
    {
      return (int[] dieValues) => dieValues.Where(x => x == val).Sum();
    }

    public static ScoringFunc ScoreOfAKind(int num)
    {
      return (int[] dieValues) =>
      {
        int maxCount = dieValues.GroupBy(x => x).Max(x => x.Count());
        bool meets = maxCount >= num;
        return meets ? dieValues.Sum() : 0;
      };
    }

    public static int ScoreFullHouse(int[] dieValues)
    {
      var groups = dieValues.GroupBy(x => x);
      int maxCount = groups.Max(x => x.Count());
      int minCount = groups.Min(x => x.Count());
      return (maxCount == 3 && minCount == 2) ? 25 : 0;
    }

    public static int ScoreSmallStraight(int[] dieValues)
    {
      bool straight = true;
      int[] uniqueValues = dieValues.Distinct().ToArray();
      Array.Sort(uniqueValues);
      if (uniqueValues.Length == 4)
      {
        for (int x = 1; x < uniqueValues.Length; x++)
        {
          straight = straight && (uniqueValues[x] - uniqueValues[x - 1] == 1);
        }
      }
      else if (uniqueValues.Length == 5)
      {
        bool straightLow = true;
        for (int x = 1; x < uniqueValues.Length - 1; x++)
        {
          straightLow = straightLow && (uniqueValues[x] - uniqueValues[x - 1] == 1);
        }

        bool straightHigh = true;
        for (int x = 2; x < uniqueValues.Length; x++)
        {
          straightHigh = straightHigh && (uniqueValues[x] - uniqueValues[x - 1] == 1);
        }

        straight = (straightHigh || straightLow);
      }
      else
      {
        straight = false;
      }
      
      return straight ? 30 : 0;
    }


    public static int ScoreLargeStraight(int[] dieValues)
    {
      Array.Sort(dieValues);
      bool straight = true;
      for (int x = 1; x < dieValues.Length; x++)
      {
        straight = straight && (dieValues[x] - dieValues[x - 1] == 1);
      }
      return straight ? 40 : 0;
    }

    public static int SumDie(int[] dieValues)
    {
      return dieValues.Sum();
    }

    public static int ScoreYatzee(int[] dieValues)
    {
      return dieValues.Distinct().Count() == 1 ? 50 : 0;
    }
  }
}
