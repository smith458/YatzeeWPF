using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Yatzee
{
  public static class Scoring
  {
    public static readonly Dictionary<string, ScoringFunc> ScoreOptions = new Dictionary<string, ScoringFunc>()
    {
      {"Ones", SumDieValue(1) },
      {"Twos", SumDieValue(2) },
      {"Threes", SumDieValue(3) },
      {"Fours", SumDieValue(4) },
      {"Fives", SumDieValue(5) },
      {"Sixs", SumDieValue(6) },
      {"Three of a Kind", ScoreOfAKind(3) },
      {"Four of a Kind", ScoreOfAKind(4) },
      {"Small Straight", ScoreSmallStraight },
      {"Large Straight", ScoreLargeStraight },
      {"Full House", ScoreFullHouse },
      {"Yatzee", ScoreYatzee },
      {"Chance", SumDie },

    };

    public delegate int ScoringFunc(int[] dieValues);

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
      HashSet<int> set =new HashSet<int>(dieValues);
      int[] uniqueValues = set.ToArray();
      if (set.Count == 4)
      {
        for (int x = 1; x < dieValues.Length; x++)
        {
          straight = straight && (uniqueValues[x] - uniqueValues[x - 1] == 1);
        }
      }
      else if (set.Count == 5)
      {
        bool straightLow = true;
        for (int x = 1; x < dieValues.Length - 1; x++)
        {
          straightLow = straightLow && (dieValues[x] - dieValues[x - 1] == 1);
        }

        bool straightHigh = true;
        for (int x = 2; x < dieValues.Length; x++)
        {
          straightHigh = straightHigh && (dieValues[x] - dieValues[x - 1] == 1);
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
      return new HashSet<int>(dieValues).Count == 1 ? 50 : 0;
    }
  }
}
