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
using System.IO;
using System.Collections.Generic;


namespace PCGSharp.Tests {
  
  public static class RandomHelpers {
    public static List<uint> ReadPcgOutput(int seed) {
      var dir = Path.GetDirectoryName(typeof(RandomHelpers).Assembly.Location);
      var fileName = Path.Combine(dir, Path.Combine("Data", string.Format("pcg32_seed_{0}.txt", seed)));
      return ReadPcgValuesFile(fileName);
    }    
    
    public static List<uint> ReadPcgExtendedOutput(int seed, int tablePow2, int advancePow2) {
      var dir = Path.GetDirectoryName(typeof(RandomHelpers).Assembly.Location);
      var dataFile = string.Format("pcg32_k_table_pow2_{0}_advance_pow2_{1}_seed_{2}.txt", tablePow2, advancePow2, seed);
      var fileName = Path.Combine(dir, Path.Combine("Data", dataFile));
      return ReadPcgValuesFile(fileName);
    }
    
    static List<uint> ReadPcgValuesFile(string fileName) {
      var list = new List<uint>();
      try {        
        using(TextReader reader = File.OpenText(fileName)) {
          do {
            var uintString = reader.ReadLine();
            if(string.IsNullOrEmpty(uintString))
              break;
            var x = uint.Parse(uintString);
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

