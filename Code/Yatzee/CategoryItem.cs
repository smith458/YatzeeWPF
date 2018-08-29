using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzee
{
  public class CategoryItem
  {
    public string Name { get; }
    public ScoringFunc ScoreFunc { get; }
    public int Rank { get; }

    public CategoryItem(string name, ScoringFunc scoreFunc, int rank)
    {
      Name = name;
      ScoreFunc = scoreFunc;
      Rank = rank;
    }


  }
}
