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
using NUnit.Framework;
using System.Collections.Generic;


namespace PCGSharp.Tests {

  [TestFixture]
  public class PCGTests {

    const int N = 10000;

    [Test]
    public void ConstructorTest() {
      var rng = new PCG();
      Assert.IsNotNull(rng);
    }

    [Test]
    public void CorrectnessTest() {
      int seed = 42;
      int sequence = 54;
      var list = RandomHelpers.ReadPCGOutput(42);
      Assert.AreEqual(10000, list.Count);
      var pcg = new PCG(seed, sequence);
      for(int i = 0; i < 10000; i++) {
        var aVal = pcg.NextUInt();
        var cVal = list[i];
        Assert.That(aVal, Is.EqualTo(cVal));
      }
    }

    [Test]
    public void ReproducibilityTest() {
      var r1 = new PCGExtended(11,1);
      var r1v = r1.NextInts(N);
      var r2 = new PCGExtended(11,1);
      var r2v = r2.NextInts(N);
      for(int i = 0; i < N; i++) {
        Assert.That(r1v[i], Is.EqualTo(r2v[i]));
      }
    }

    [Test]
    public void NextFloatBoundsTest() {
      var pcg = new PCG(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextFloat();
        Assert.That(aVal <= 1.0f);
        Assert.That(aVal >= 0.0f);
      }
    }

    [Test]
    public void NextFloatUpperBoundTest() {
      var maxV = 2.5f;
      var pcg = new PCG(42);
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
      var pcg = new PCG(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextFloat(minV, maxV);
        Assert.That(aVal <= maxV);
        Assert.That(aVal >= minV);
      }
    }

    static object[] NextFloatMeanTestCases = new object[] { 1f, 2f, 4.3f, 10f, 1000f, 10000000f };
    [Test, TestCaseSource("NextFloatMeanTestCases")]
    public void NextFloatMeanTest(float val) {
      var minV = -val;
      var maxV = val;
      var pcg = new PCG(42);
      var rsum = 0.0f;
      var lTol = 0.1f * (maxV - minV);
      for(int i = 0; i < N; i++) {
        rsum += pcg.NextFloat(minV, maxV);
      }
      var mean = rsum / (float)N;
      Assert.That(mean, Is.EqualTo(0.0f).Within(lTol));
    }

    [Test]
    public void NextDoubleBoundsTest() {
      var pcg = new PCG(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextDouble();
        Assert.That(aVal <= 1.0);
        Assert.That(aVal >= 0.0);
      }
    }

    [Test]
    public void NextDoubleUpperBoundTest() {
      var maxV = 2.5;
      var pcg = new PCG(42);
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
      var pcg = new PCG(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextDouble(minV, maxV);
        Assert.That(aVal <= maxV);
        Assert.That(aVal >= minV);
      }
    }

    static object[] NextDoubleMeanTestCases = new object[] { 1f, 2f, 4.3f, 10f, 1000f, 10000000f };
    [Test, TestCaseSource("NextDoubleMeanTestCases")]
    public void NextDoubleMeanTest(float val) {
      var minV = -val;
      var maxV = val;
      var pcg = new PCG(10);
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
      var pcg = new PCG(42);
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
      var pcg = new PCG(42);
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

