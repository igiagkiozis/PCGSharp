[![Build Status](https://travis-ci.org/ThelDoctor/PCGSharp.svg)](https://travis-ci.org/ThelDoctor/PCGSharp)

https://travis-ci.org/ThelDoctor/PCGSharp/builds

# PCGSharp

Permuted Congruential Generator (PCG) for C# for .NET 2.0 and above

PCG (Permuted Congruential Generator) Extended is a C# port from C++ of a part of the extended PCG family of
generators presented in "PCG: A Family of Simple Fast Space-Efficient Statistically Good
Algorithms for Random Number Generation" by Melissa E. O'Neill. The code follows closely the one 
made available by O'Neill at her site: http://www.pcg-random.org/using-pcg-cpp.html 
To understand how exactly this generator works read this:
http://www.pcg-random.org/pdf/toms-oneill-pcg-family-v1.02.pdf its a fun read, enjoy!

The most important difference between the extended version and PCG is that the extended generators 
can have extremely larger periods (extremely!) and have configurable k-dimensional equidistribution.
All this at a very small speed penalty! (Upto 1024-dimensionally equidistributed generators perform 
faster than System.Random, at least on the machines I have access to).

Fun fact, the period of PCG Extended for _table_mask = 14 is 2^(524352) which is more than 10^(157846). To put this 
in context, the number particles in the known Universe is about 10^(80 to 81ish) if we also count photons
that number goes up (with a lot of effort) to 10^(90ish). Now, there are estimates that say that 
visible matter (counting photons, energy-matter same thing...) accounts for 5% of the matter in the
visible Universe, so if these estimates are correct the total visible+invisible particles/(energy particles)
in the visible Universe would approximately be 10^(91). That means that this generator has a period
that is 157755 orders of magnitude larger than the number of all particles in the visible Universe... 
Also, if the universe had an equal number of particles (to the period of PCG 14), that would mean that
its mean density (at its current volume) would be so high that the entire universe would collapse unto
its weight into a black hole.


