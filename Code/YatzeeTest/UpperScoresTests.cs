using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Yatzee;

namespace YatzeeTest
{
  [TestFixture]
  public class UpperScoresTests
  {
    [Test]
    public void OnesZero()
    {
      int[] dieValues = new int[] {2, 3, 5, 6, 3};
      Assert.AreEqual(0, Scoring.SumDieValue(1)(dieValues));
    }

    [Test]
    public void OnesOne()
    {
      int[] dieValues = new int[] { 5, 1, 3, 2, 5 };
      Assert.AreEqual(1, Scoring.SumDieValue(1)(dieValues));
    }

    [Test]
    public void OnesFive()
    {
      int[] dieValues = new int[] { 1, 1, 1, 1, 1 };
      Assert.AreEqual(5, Scoring.SumDieValue(1)(dieValues));
    }

    [Test]
    public void TwosZero()
    {
      int[] dieValues = new int[] { 1, 3, 5, 6, 3 };
      Assert.AreEqual(0, Scoring.SumDieValue(2)(dieValues));
    }

    [Test]
    public void TwosOne()
    {
      int[] dieValues = new int[] { 5, 1, 3, 2, 5 };
      Assert.AreEqual(2, Scoring.SumDieValue(2)(dieValues));
    }

    [Test]
    public void TwosFive()
    {
      int[] dieValues = new int[] { 2, 2, 2, 2, 2 };
      Assert.AreEqual(10, Scoring.SumDieValue(2)(dieValues));
    }

    [Test]
    public void ThreesZero()
    {
      int[] dieValues = new int[] { 1, 2, 5, 6, 2 };
      Assert.AreEqual(0, Scoring.SumDieValue(3)(dieValues));
    }

    [Test]
    public void ThreesOne()
    {
      int[] dieValues = new int[] { 5, 1, 3, 2, 5 };
      Assert.AreEqual(3, Scoring.SumDieValue(3)(dieValues));
    }

    [Test]
    public void ThreesFive()
    {
      int[] dieValues = new int[] { 3, 3, 3, 3, 3 };
      Assert.AreEqual(15, Scoring.SumDieValue(3)(dieValues));
    }

    [Test]
    public void FoursZero()
    {
      int[] dieValues = new int[] { 1, 2, 5, 6, 2 };
      Assert.AreEqual(0, Scoring.SumDieValue(4)(dieValues));
    }

    [Test]
    public void FoursOne()
    {
      int[] dieValues = new int[] { 5, 1, 4, 2, 5 };
      Assert.AreEqual(4, Scoring.SumDieValue(4)(dieValues));
    }

    [Test]
    public void FoursFive()
    {
      int[] dieValues = new int[] { 4, 4, 4, 4, 4 };
      Assert.AreEqual(20, Scoring.SumDieValue(4)(dieValues));
    }

    [Test]
    public void FivesZero()
    {
      int[] dieValues = new int[] { 1, 2, 4, 6, 2 };
      Assert.AreEqual(0, Scoring.SumDieValue(5)(dieValues));
    }

    [Test]
    public void FiveesOne()
    {
      int[] dieValues = new int[] { 5, 1, 3, 2, 3 };
      Assert.AreEqual(5, Scoring.SumDieValue(5)(dieValues));
    }

    [Test]
    public void FiveesFive()
    {
      int[] dieValues = new int[] { 5, 5, 5, 5, 5 };
      Assert.AreEqual(25, Scoring.SumDieValue(5)(dieValues));
    }

    [Test]
    public void SixesZero()
    {
      int[] dieValues = new int[] { 1, 2, 5, 1, 2 };
      Assert.AreEqual(0, Scoring.SumDieValue(6)(dieValues));
    }

    [Test]
    public void SixesOne()
    {
      int[] dieValues = new int[] { 5, 1, 3, 6, 5 };
      Assert.AreEqual(6, Scoring.SumDieValue(6)(dieValues));
    }

    [Test]
    public void SixesFive()
    {
      int[] dieValues = new int[] { 6, 6, 6, 6, 6 };
      Assert.AreEqual(30, Scoring.SumDieValue(6)(dieValues));
    }
  }
}
