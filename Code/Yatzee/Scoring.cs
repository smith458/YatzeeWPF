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
      {"Ones", SumDie },
      {"Twos", SumDie },
      {"Threes", SumDie },
      {"Fours", SumDie },
      {"Fives", SumDie },
      {"Sixs", SumDie },
      {"Three of a Kind", SumDie },
      {"Four of a Kind", SumDie },
      {"Small Straight", SumDie },
      {"Large Straight", SumDie },
      {"Full House", SumDie },
      {"Yatzee", ScoreYatzee },
      {"Chance", SumDie },

    };

    public delegate int ScoringFunc(int[] dieValues);

    public static int SumDie(int[] dieValues)
    {
      return dieValues.Aggregate(0, (x, y) => x + y);
    }

    public static int ScoreYatzee(int[] dieValues)
    {
      return new HashSet<int>(dieValues).Count == 1 ? 50 : 0;
    }
  }
}
