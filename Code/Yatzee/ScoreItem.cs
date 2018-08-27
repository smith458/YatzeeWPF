using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzee
{
  public class ScoreItem
  {
    public string Name { get; }
    public int Score { get; }
    public int Rank { get; }

    public ScoreItem(string name, int score, int rank)
    {
      Name = name;
      Score = score;
      Rank = rank;
    }
  }
}
