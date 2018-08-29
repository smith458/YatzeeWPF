using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatzee;
using NUnit.Framework;

namespace YatzeeTest
{
  [TestFixture]
  public class LowerScoresTests
  {
    [Test]
    public void ThreeOfAKind_TwoMatch()
    {
      int[] dieValues = new int[] { 2, 3, 5, 6, 3 };
      Assert.AreEqual(0, Scoring.ScoreOfAKind(3)(dieValues));
    }

    [Test]
    public void ThreeOfAKind_ThreeMatch()
    {
      int[] dieValues = new int[] { 3, 3, 5, 6, 3 };
      Assert.AreEqual(20, Scoring.ScoreOfAKind(3)(dieValues));
    }

    [Test]
    public void ThreeOfAKind_FiveMatch()
    {
      int[] dieValues = new int[] { 3, 3, 3, 3, 3 };
      Assert.AreEqual(15, Scoring.ScoreOfAKind(3)(dieValues));
    }

    [Test]
    public void FourOfAKind_ThreeMatch()
    {
      int[] dieValues = new int[] { 2, 4, 5, 4, 4 };
      Assert.AreEqual(0, Scoring.ScoreOfAKind(4)(dieValues));
    }

    [Test]
    public void FourOfAKind_FourMatch()
    {
      int[] dieValues = new int[] { 4, 4, 4, 6, 4 };
      Assert.AreEqual(22, Scoring.ScoreOfAKind(4)(dieValues));
    }

    [Test]
    public void FourOfAKind_FiveMatch()
    {
      int[] dieValues = new int[] { 4, 4, 4, 4, 4 };
      Assert.AreEqual(20, Scoring.ScoreOfAKind(4)(dieValues));
    }

    [Test]
    public void FullHouse_No()
    {
      int[] dieValues = new int[] { 4, 4, 1, 5, 5 };
      Assert.AreEqual(0, Scoring.ScoreFullHouse(dieValues));
    }

    [Test]
    public void FullHouse_Yes()
    {
      int[] dieValues = new int[] { 4, 4, 4, 5, 5 };
      Assert.AreEqual(25, Scoring.ScoreFullHouse(dieValues));
    }

    [Test]
    public void SmallStraight_No()
    {
      int[] dieValues = new int[] { 4, 2, 1, 5, 6 };
      Assert.AreEqual(0, Scoring.ScoreSmallStraight(dieValues));
    }

    [Test]
    public void SmallStraightEasy_Yes()
    {
      int[] dieValues = new int[] { 1, 2, 3, 4, 4 };
      Assert.AreEqual(30, Scoring.ScoreSmallStraight(dieValues));
    }

    [Test]
    public void SmallStraightLow_Yes()
    {
      int[] dieValues = new int[] { 1, 2, 3, 4, 6 };
      Assert.AreEqual(30, Scoring.ScoreSmallStraight(dieValues));
    }

    [Test]
    public void SmallStraightHigh_Yes()
    {
      int[] dieValues = new int[] { 1, 3, 4, 5, 6 };
      Assert.AreEqual(30, Scoring.ScoreSmallStraight(dieValues));
    }

    [Test]
    public void SmallStraightScrambled_Yes()
    {
      int[] dieValues = new int[] { 1, 6, 4, 5, 3 };
      Assert.AreEqual(30, Scoring.ScoreSmallStraight(dieValues));
    }

    [Test]
    public void LargeStraight_No()
    {
      int[] dieValues = new int[] { 4, 2, 1, 5, 6 };
      Assert.AreEqual(0, Scoring.ScoreLargeStraight(dieValues));
    }

    [Test]
    public void LargeStraightLow_Yes()
    {
      int[] dieValues = new int[] { 1, 2, 3, 4, 5 };
      Assert.AreEqual(40, Scoring.ScoreLargeStraight(dieValues));
    }

    [Test]
    public void LargeStraightHigh_Yes()
    {
      int[] dieValues = new int[] { 2, 3, 4, 5, 6 };
      Assert.AreEqual(40, Scoring.ScoreLargeStraight(dieValues));
    }

    [Test]
    public void LargeStraightHighScrambled_Yes()
    {
      int[] dieValues = new int[] { 2, 5, 4, 3, 6 };
      Assert.AreEqual(40, Scoring.ScoreLargeStraight(dieValues));
    }

    [Test]
    public void Chance1()
    {
      int[] dieValues = new int[] { 2, 5, 4, 3, 6 };
      Assert.AreEqual(20, Scoring.SumDie(dieValues));
    }

    [Test]
    public void Chance2()
    {
      int[] dieValues = new int[] { 1, 2, 1, 2, 1 };
      Assert.AreEqual(7, Scoring.SumDie(dieValues));
    }

    [Test]
    public void Yatzee_No()
    {
      int[] dieValues = new int[] { 1, 2, 1, 2, 1 };
      Assert.AreEqual(0, Scoring.ScoreYatzee(dieValues));
    }

    [Test]
    public void Yatzee_Yes()
    {
      int[] dieValues = new int[] { 1, 1, 1, 1, 1 };
      Assert.AreEqual(50, Scoring.ScoreYatzee(dieValues));
    }
  }
}
