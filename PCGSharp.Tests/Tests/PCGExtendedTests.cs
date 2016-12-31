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


namespace PCGSharp.Tests {

  public class PcgExtendedTests {

    const int N = 10000;

    // The comments to the right explain what is being tested. Note that comments about 
    // speed are simply indicative of expected speed, not the result of extensive testing!
    public static object[] PCGExtendedTestCases = new object[] {
      new object[] { 42,  1, 16 }, // 2-dimensionally equidistributed generator, period 2^(2*32 + 64)
      new object[] { 42,  1, 32 }, // 2-dimensionally equidistributed generator, period 2^(2*32 + 64)
      new object[] { 42,  2, 16 }, // 4-dimensionally equidistributed generator, period 2^(4*32 + 64)
      new object[] { 42,  2, 32 }, // 4-dimensionally equidistributed generator, period 2^(4*32 + 64)
      new object[] { 42,  3, 16 }, // 8-dimensionally equidistributed generator, period 2^(8*32 + 64)
      new object[] { 42,  3, 32 }, // 8-dimensionally equidistributed generator, period 2^(8*32 + 64)
      new object[] { 42,  4, 16 }, // 16-dimensionally equidistributed generator, period 2^(16*32 + 64)
      new object[] { 42,  4, 32 }, // 16-dimensionally equidistributed generator, period 2^(16*32 + 64)
      new object[] { 42,  5, 16 }, // 32-dimensionally equidistributed generator, period 2^(32*32 + 64)
      new object[] { 42,  5, 32 }, // 32-dimensionally equidistributed generator, period 2^(32*32 + 64)
      new object[] { 42,  6, 16 }, // 64-dimensionally equidistributed generator, period 2^(64*32 + 64)
      new object[] { 42,  6, 32 }, // 64-dimensionally equidistributed generator, period 2^(64*32 + 64)
      new object[] { 42,  7, 16 }, // 128-dimensionally equidistributed generator, period 2^(128*32 + 64)
      new object[] { 42,  7, 32 }, // 128-dimensionally equidistributed generator, period 2^(128*32 + 64)
      new object[] { 42,  8, 16 }, // 256-dimensionally equidistributed generator, period 2^(256*32 + 64)
      new object[] { 42,  8, 32 }, // 256-dimensionally equidistributed generator, period 2^(256*32 + 64)
      new object[] { 42,  9, 16 }, // 512-dimensionally equidistributed generator, period 2^(512*32 + 64)
      new object[] { 42,  9, 32 }, // 512-dimensionally equidistributed generator, period 2^(512*32 + 64)
      new object[] { 42, 10, 16 }, // 1024-dimensionally equidistributed generator, period 2^(1024*32 + 64)     Up to here generators perform more  
      new object[] { 42, 10, 32 }, // 1024-dimensionally equidistributed generator, period 2^(1024*32 + 64) <-- or less equally fast (within a 10% tolerance)
      new object[] { 42, 11, 16 }, // 2048-dimensionally equidistributed generator, period 2^(2048*32 + 64) <-- After this point generators start
      new object[] { 42, 11, 32 }, // 2048-dimensionally equidistributed generator, period 2^(2048*32 + 64)     exhibiting noticeable slow downs.
      new object[] { 42, 12, 16 }, // 4096-dimensionally equidistributed generator, period 2^(4096*32 + 64)     
      new object[] { 42, 12, 32 }, // 4096-dimensionally equidistributed generator, period 2^(4096*32 + 64)
      new object[] { 42, 13, 16 }, // 8192-dimensionally equidistributed generator, period 2^(8192*32 + 64)
      new object[] { 42, 13, 32 }, // 8192-dimensionally equidistributed generator, period 2^(8192*32 + 64)
      new object[] { 42, 14, 16 }, // 16384-dimensionally equidistributed generator, period 2^(16384*32 + 64) <-- Approximately 60% as fast as generators
      new object[] { 42, 14, 32 }  // 16384-dimensionally equidistributed generator, period 2^(16384*32 + 64)     1024 and below. Still, not bad!
    };

    [Test, TestCaseSource("PCGExtendedTestCases")]
    public void CorrectnessTests(int seed, int tablePow2, int advancePow2) {
      var list = RandomHelpers.ReadPcgExtendedOutput(seed, tablePow2, advancePow2);
      Assert.AreEqual(10000, list.Count);
      var pcg = new PcgExtended((ulong)seed, 721347520444481703, tablePow2, advancePow2);
      for(int i = 0; i < 10000; i++) {
        var aVal = pcg.NextUInt();
        var cVal = list[i];
        Assert.That(aVal, Is.EqualTo(cVal));
      }
    }

    [Test]
    public void ReproducibilityTest() {
      var r1 = new PcgExtended(11,1);
      var r1v = r1.NextInts(N);
      var r2 = new PcgExtended(11,1);
      var r2v = r2.NextInts(N);
      for(int i = 0; i < N; i++) {
        Assert.That(r1v[i], Is.EqualTo(r2v[i]));
      }
    }

    [Test]
    public void NextFloatBoundsTest() {
      var pcg = new PcgExtended(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextFloat();
        Assert.That(aVal <= 1.0f);
        Assert.That(aVal >= 0.0f);
      }
    }

    [Test]
    public void NextFloatUpperBoundTest() {
      var maxV = 2.5f;
      var pcg = new PcgExtended(42);
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
      var pcg = new PcgExtended(42);
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
      var pcg = new PcgExtended(42);
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
      var pcg = new PcgExtended(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextDouble();
        Assert.That(aVal <= 1.0);
        Assert.That(aVal >= 0.0);
      }
    }

    [Test]
    public void NextDoubleUpperBoundTest() {
      var maxV = 2.5;
      var pcg = new PcgExtended(42);
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
      var pcg = new PcgExtended(42);
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
      var pcg = new PcgExtended(10);
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
      var pcg = new PcgExtended(42);
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
      var pcg = new PcgExtended(42);
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

