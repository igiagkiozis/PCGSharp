[![license](https://img.shields.io/badge/license-MIT-brightgreen.svg?style=flat)](https://github.com/ThelDoctor/PCGSharp/blob/master/LICENSE)
[![Build Status](https://travis-ci.org/ThelDoctor/PCGSharp.svg?branch=master)](https://travis-ci.org/ThelDoctor/PCGSharp)

# PCGSharp - PCG Random Generator for C# .NET >= 2.0 and Unity3D

PCG (Permuted Congruential Generator) Extended is a C# port from C++ of a part of the extended PCG family of
generators presented in "PCG: A Family of Simple Fast Space-Efficient Statistically Good
Algorithms for Random Number Generation" by Melissa E. O'Neill. The code follows closely the one 
made available by O'Neill at her [site](http://www.pcg-random.org/using-pcg-cpp.html)
To understand how exactly this generator works read [this paper](http://www.pcg-random.org/pdf/toms-oneill-pcg-family-v1.02.pdf) its a fun read, enjoy!

## Fun Fact About PCG Extended

The period of PCG Extended for one of its parametrisations (~16k of state) is 2^(524352) which is more than 10^(157846)!

To put this in context, the number particles in the known Universe is about 10^(80) if we also count photons
that number goes up to approximately 10^(90). There are estimates that suggest that 
visible matter (counting photons, energy-matter same thing...) accounts for 5% of the matter in the
visible Universe. If these estimates are correct the total visible+invisible particles
in the visible Universe would approximately be 10^(91). That means that this generator has a period
that is **157755 orders of magnitude** larger than the number of **all particles in the visible Universe...** 
Also, if the universe had an equal number of particles (to the period of PCG 14), that would mean that
its mean density (at its current volume) would be so high that the entire universe would collapse unto
its weight into a black hole.

## Getting Started
PCGSharp implements two flavors of PCG, see [PCG](PCGSharp/Source/PCG.cs) and [PCGExtended](PCGSharp/Source/PCGExtended.cs). The first version ([PCG](PCGSharp/Source/PCG.cs)) is simple, fast and has much better statistical properties compared with System.Random, and [other well known(!) generators](http://www.pcg-random.org/pdf/toms-oneill-pcg-family-v1.02.pdf). It also supports multiple streams and has a period of 2^64. All in all its a good general purpose (pseudo-)random number generator, so if in doubt use this one. [PCG](PCGSharp/Source/PCG.cs) can be used in two ways: 

**Instanced**
Assuming you have added the appropriate files (or dll) to your project you can create a PCG instance in the same way you would for System.Random
```csharp
var rnd = new PCG(); 
```
That's it. 

**Static**
Both [PCG](PCGSharp/Source/PCG.cs) and [PCGExtended](PCGSharp/Source/PCGExtended.cs) have a static invocation
```csharp
var rnd = PCG.Default;
```
or 
```csharp
var rnd = PCGExtended.Default;
```
When used like that a singleton will be created per thread, i.e. each thread will have its own version of PCG. 

[PCGExtended](PCGSharp/Source/PCGExtended.cs) is more configurable compared with [PCG](PCGSharp/Source/PCG.cs) and most importantly can create generators with almost any dimensional equidistribution. Note, a 2-dimensionally equidistributed generator can produce random tuples of length 2 that are equidistributed in two dimensions (uniform random distribution in 2d space). That property essentially minimizes the likelihood of correlations between tuples (or vectors) of dimension 2. A 1024-dimensional equidistribution generator has the same property for vectors with dimension upto 1024, etc. [PCGExtended](PCGSharp/Source/PCGExtended.cs) with 1024-dimensional equidistribution is still faster than System.Random, at least on the machines I have access to.

You can create a PCG with 1024-dimensional distribution as follows
```csharp
ulong seed = 42; // This can also be an int
ulong sequence = 0; // This can also be an int
// This directly controls the dimensional distribution of the generator, the period, and
// the amount of state. For k being the dimensional distribution, k = 2^(tablePow2), so 
// for tablePow2 = 10 we get a generator with 2^(10) = 1024 dimensional equi-distribution.
// The state memory requirements for such a generator are (approximately) 4096 bytes. 
int tablePow2 = 10;
// I think this will eventually be removed from the public API, but if you're curious as to
// what this does, see http://www.pcg-random.org/using-pcg-cpp.html
int advancePow2 = 16;
var rnd = new PCGExtended(seed, sequence, tablePow2, advancePow2);
```

## API

The random number generation API for both versions of PCG is identical and is identical to System.Random for 
the methods that are shared. As reference these are public methods in [PCG](PCGSharp/Source/PCG.cs) and [PCGExtended](PCGSharp/Source/PCGExtended.cs).

```csharp
public int Next();

public int Next(int maxExclusive);

public int Next(int minInclusive, int maxExclusive);

public int[] NextInts(int count)

public int[] NextInts(int count, int maxExclusive)

public int[] NextInts(int count, int minInclusive, int maxExclusive)

public uint NextUInt()

public uint NextUInt(uint maxExclusive);

public uint NextUInt(uint minInclusive, uint maxExclusive);

public uint[] NextUInts(int count);

public uint[] NextUInts(int count, uint maxExclusive);

public uint[] NextUInts(int count, uint minInclusive, uint maxExclusive);

public float NextFloat();

public float NextFloat(float maxInclusive);

public float NextFloat(float minInclusive, float maxInclusive);

public float[] NextFloats(int count);

public float[] NextFloats(int count, float maxInclusive);

public float[] NextFloats(int count, float minInclusive, float maxInclusive);

public double NextDouble();

public double NextDouble(double maxInclusive);

public double NextDouble(double minInclusive, double maxInclusive);

public double[] NextDoubles(int count);

public double[] NextDoubles(int count, double maxInclusive);

public double[] NextDoubles(int count, double minInclusive, double maxInclusive);

public byte NextByte();

public byte[] NextBytes(int count);
      
public bool NextBool();

public bool[] NextBools(int count);
```

## Running the tests


## License

MIT License

Copyright (c) 2016 Bismur Studios Ltd.
Copyright (c) 2016 Ioannis Giagkiozis

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

