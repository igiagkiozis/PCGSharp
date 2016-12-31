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
using System.Collections.Generic;
using System.IO;


namespace PCGSharp.Tests {

  public static class RandomHelpers {

    public static List<uint> ReadPCGOutput(int seed) {
      var dir = Path.GetDirectoryName(typeof(RandomHelpers).Assembly.Location);
      var fileName = string.Format("{0}/Data/pcg32_seed_{1}.txt", dir, seed);
      return ReadPCGValuesFile(fileName);
    }

    public static List<uint> ReadPCGExtendedOutput(int seed, int tablePow2, int advancePow2) {
      var dir = Path.GetDirectoryName(typeof(RandomHelpers).Assembly.Location);
      var fileName = string.Format("{0}/Data/pcg32_k_table_pow2_{1}_advance_pow2_{2}_seed_{3}.txt", 
        dir, tablePow2, advancePow2, seed);
      return ReadPCGValuesFile(fileName);
    }

    static List<uint> ReadPCGValuesFile(string fileName) {
      var list = new List<uint>();
      try {        
        using(TextReader reader = File.OpenText(fileName)) {
          do {
            string uintString = reader.ReadLine();
            if(string.IsNullOrEmpty(uintString))
              break;
            uint x = uint.Parse(uintString);
            list.Add(x);

          } while(true);
        }
      } catch {
        Console.WriteLine("File {0}, does not exist!", fileName);
      }
      return list;
    }

  }
}

