// MIT License
// 
// Copyright (c) 2016 Bismur Studios Ltd.
// Copyright (c) 2016 Ioannis Giagkiozis
//
// This file is part of PCGSharp.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
// and associated documentation files (the "Software"), to deal in the Software without restriction, 
// including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, 
// subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all copies or substantial 
//  portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT 
//  LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN 
//  NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
//  WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
//  SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Web;
using NUnit.Framework;


namespace PCGSharp.Tests {

  [TestFixture]
  public class PcgTests {

    const int N = 10000;
    private Pcg Rng;

    [TestFixtureSetUp]
    public void Initialize() {
      Rng = Pcg.Default;
    }

    [Test]
    public void DefaultConstructorTest() {
      var rng = new Pcg();
      Assert.IsNotNull(rng);
    }

    [Test]
    public void StaticConstructorTest() {
      var rng = Pcg.Default;
      Assert.IsNotNull(rng);
    }

    [Test]
    public void SingleParameterIntConstructorTest() {
      var rng = new Pcg(11);
      Assert.IsNotNull(rng);
    }

    [Test]
    public void SingleParameterULongConstructorTest() {
      var rng = new Pcg(11ul);
      Assert.IsNotNull(rng);
    }

    [Test]
    public void TwoParameterIntConstructorTest() {
      var rng = new Pcg(42, 54);
      Assert.IsNotNull(rng);
    }

    [Test]
    public void TwoParameterULongConstructorTest() {
      var rng = new Pcg(42ul, 54ul);
      Assert.IsNotNull(rng);
    }

    [Test]
    public void CorrectnessTest() {
      int seed = 42;
      int sequence = 54;
      var list = RandomHelpers.ReadPcgOutput(42);
      Assert.AreEqual(10000, list.Count);
      var pcg = new Pcg(seed, sequence);
      for(int i = 0; i < 10000; i++) {
        var aVal = pcg.NextUInt();
        var cVal = list[i];
        Assert.That(aVal, Is.EqualTo(cVal));
      }
    }

    [Test]
    public void ReproducibilityTest() {
      var r1 = new PcgExtended(11, 1);
      var r1V = r1.NextInts(N);
      var r2 = new PcgExtended(11, 1);
      var r2V = r2.NextInts(N);
      for(int i = 0; i < N; i++) {
        Assert.That(r1V[i], Is.EqualTo(r2V[i]));
      }
    }

    [Test]
    public void NextTest() {
      for(int i = 0; i < N; i++) {
        var aVal = Rng.Next();
        Assert.That(aVal >= 0);
      }
    }

    [Test]
    public void NextIntsTest() {
      var vals = Rng.NextInts(N);
      foreach(var v in vals) {
        Assert.That(v >= 0);
      }
    }

    [Test]
    public void NextIntsUpperBoundTest() {
      var vals = Rng.NextInts(N, 10);
      foreach(var v in vals) {
        Assert.That(v >= 0);
        Assert.That(v < 10);
      }
    }

    [Test]
    public void NextFloatTest() {
      for(int i = 0; i < N; i++) {
        var v = Rng.NextFloat();
        Assert.That(v >= 0f);
        Assert.That(v <= 1.0f);
      }
    }

    [Test]
    public void NextDoubleTest() {
      for(int i = 0; i < N; i++) {
        var v = Rng.NextDouble();
        Assert.That(v >= 0.0);
        Assert.That(v <= 1.0);
      }
    }

    [Test]
    public void PeriodPow2Test() {
      Assert.AreEqual(64, Rng.PeriodPow2());
    }

    [Test]
    public void SetStreamTest() {
      Rng.SetStream(0);
      Console.WriteLine(Rng.CurrentStream());
      Assert.That(Rng.CurrentStream() == 0);
      Rng.SetStream(1);
      Assert.That(Rng.CurrentStream() == 1);
    }

    [Test]
    public void NextByteTest() {
      for(int i = 0; i < N; i++) {
        var val = Convert.ToInt32(Rng.NextByte());
        Assert.That(val >= 0);
        Assert.That(val <= 255);
      }
    }

    [Test]
    public void NextBytesTest() {
      Assert.That(Rng.NextBytes(N).Length == N);
    }

    [Test]
    public void NextBoolTest() {
      for(int i = 0; i < N; i++) {
        var val = Convert.ToInt32(Rng.NextBool());
        Assert.That(val == 0 || val == 1);
      }
    }

    [Test]
    public void NextBoolsTest() {
      Assert.That(Rng.NextBools(N).Length == N);
    }

    [Test]
    public void NextBoolsThrowsWhenZeroCountTest() {
      Assert.Throws<ArgumentException>(() => Rng.NextBools(0));
    }
  

    [Test]
    public void NextIntsZeroCountThrowsTest() {
      var rng = Pcg.Default;
      Assert.Throws<ArgumentException>(() => { rng.NextInts(0); });
    }

    [Test]
    public void NextUIntUpperBoundTest() {
      for(int i = 0; i < N; i++) {
        var aVal = Rng.NextUInt(10);
        Assert.That(aVal < 10);
      }
    }

    [Test]
    public void NextIntsIntervalTest() {
      var list = Rng.NextInts(N, -4, 6);
      for(int i = 0; i < N; i++) {
        Assert.That(list[i] >= -4);
        Assert.That(list[i] < 6);
      }
    }

    [Test]
    public void NextUIntTest() {
      for(int i = 0; i < N; i++) {
        var aVal = Rng.NextUInt(10, 15);
        Assert.That(aVal >= 10);
        Assert.That(aVal < 15);
      }
    }

    [Test]
    public void NextUIntsZeroCountThrowsTest() {
      Assert.Throws<ArgumentException>(() => Rng.NextUInts(0));
    }

    [Test]
    public void NextFloatsZeroCountThrowsTest() {
      Assert.Throws<ArgumentException>(() => Rng.NextFloats(0));
    }

    [Test]
    public void NextFloatsUpperBoundTest() {
      var list = Rng.NextFloats(N, 3.3f);
      foreach(var v in list) {
        Assert.That(v >= 0f);
        Assert.That(v <= 3.3f);
      }
    }

    [Test]
    public void NextDoublesZeroCountThrowsTest() {
      Assert.Throws<ArgumentException>(() => Rng.NextDoubles(0));
    }

    [Test]
    public void NextDoublesUpperBoundTest() {
      var list = Rng.NextDoubles(N, 4.4);
      foreach(var v in list) {
        Assert.That(v >= 0.0);
        Assert.That(v <= 4.4);
      }
    }

    [Test]
    public void NextDoublesIntervalTest() {
      var list = Rng.NextDoubles(N, -1.1, 1.1);
      foreach(var v in list) {
        Assert.That(v >= -1.1);
        Assert.That(v <= 1.1);
      }
    }

    [Test]
    public void NextFloatBoundsTest() {
      var pcg = new Pcg(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextFloat();
        Assert.That(aVal <= 1.0f);
        Assert.That(aVal >= 0.0f);
      }
    }

    [Test]
    public void NextFloatUpperBoundTest() {
      var maxV = 2.5f;
      var pcg = new Pcg(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextFloat(maxV);
        Assert.That(aVal <= maxV);
        Assert.That(aVal >= 0.0f);
      }
    }

    [Test]
    public void NextFloatIntervalTest() {
      var minV = -10f;
      var maxV = 2.5f;
      var pcg = new Pcg(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextFloat(minV, maxV);
        Assert.That(aVal <= maxV);
        Assert.That(aVal >= minV);
      }
    }

    [Test]
    public void NextUIntsUpperBoundTest() {
      var list = Rng.NextUInts(N, 4);
      foreach(var v in list) {
        Assert.That(v < 4);
      }
    }

    [Test]
    public void NextUIntsIntervalTest() {
      var list = Rng.NextUInts(N, 44, 55);
      foreach(var v in list) {
        Assert.That(v >= 44);
        Assert.That(v < 55);
      }
    }

    [Test]
    public void NextFloatsIntervalTest() {
      var list = Rng.NextFloats(N, -4.4f, 8.8f);
      foreach(var v in list) {
        Assert.That(v >= -4.4f);
        Assert.That(v <= 8.8f);
      }
    }
    
    static object[] _nextFloatMeanTestCases = new object[] { 1f, 2f, 4.3f, 10f, 1000f, 10000000f };
    [Test, TestCaseSource("_nextFloatMeanTestCases")]
    public void NextFloatMeanTest(float val) {
      var minV = -val;
      var maxV = val;
      var pcg = new Pcg(42);
      var rsum = 0.0f;
      var lTol = 0.1f * (maxV - minV);
      for(int i = 0; i < N; i++) {
        rsum += pcg.NextFloat(minV, maxV);
      }
      var mean = rsum / N;
      Assert.That(mean, Is.EqualTo(0.0f).Within(lTol));
    }

    [Test]
    public void NextDoubleBoundsTest() {
      var pcg = new Pcg(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextDouble();
        Assert.That(aVal <= 1.0);
        Assert.That(aVal >= 0.0);
      }
    }

    [Test]
    public void NextDoubleUpperBoundTest() {
      var maxV = 2.5;
      var pcg = new Pcg(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextDouble(maxV);
        Assert.That(aVal <= maxV);
        Assert.That(aVal >= 0.0f);
      }
    }

    [Test]
    public void NextDoubleIntervalTest() {
      var minV = -10;
      var maxV = 2.5;
      var pcg = new Pcg(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextDouble(minV, maxV);
        Assert.That(aVal <= maxV);
        Assert.That(aVal >= minV);
      }
    }

    static object[] _nextDoubleMeanTestCases = new object[] { 1f, 2f, 4.3f, 10f, 1000f, 10000000f };
    [Test, TestCaseSource("_nextDoubleMeanTestCases")]
    public void NextDoubleMeanTest(float val) {
      var minV = -val;
      var maxV = val;
      var pcg = new Pcg(10);
      var rsum = 0.0;
      var lTol = 0.1 * (maxV - minV);
      for(int i = 0; i < N; i++) {
        rsum += pcg.NextDouble(minV, maxV);
      }
      var mean = rsum / (float)N;
      Assert.That(mean, Is.EqualTo(0.0f).Within(lTol));
    }

    [Test]
    public void NextIntUpperBoundTest() {
      var maxV = 10;
      var pcg = new Pcg(42);
      var lbCount = 0;
      var ubCount = 0;
      for(int i = 0; i < N; i++) {
        var aVal = pcg.Next(maxV);
        Assert.That(aVal >= 0);
        Assert.That(aVal < maxV);
        if(aVal == 0)
          lbCount++;
        if(aVal == (maxV - 1))
          ubCount++;
      }
      Assert.That(lbCount > 0);
      Assert.That(ubCount > 0);
    }

    [Test]
    public void NextIntIntervalTest() {
      var minV = -20;
      var maxV = 10;
      var pcg = new Pcg(42);
      var lbCount = 0;
      var ubCount = 0;
      for(int i = 0; i < N; i++) {
        var aVal = pcg.Next(minV, maxV);
        Assert.That(aVal >= minV);
        Assert.That(aVal < maxV);
        if(aVal == minV)
          lbCount++;
        if(aVal == (maxV - 1))
          ubCount++;
      }
      Assert.That(lbCount > 0);
      Assert.That(ubCount > 0);
    }

  }
}

