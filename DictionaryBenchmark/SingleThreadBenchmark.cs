using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryBenchmark
{
    class SingleThreadBenchmark
    {

        // IDictionary<key, value> encapsulates both Dictionary and ConcurentDictionary
        static void Populate(IDictionary<int, int> dict, int dictSize)
        {


            for (int i = 0; i < dictSize; i++)
            {
                dict.Add(i, 1);
                Worker.DoSomething();
            }
        }

        static int Enumerate(IDictionary<int, int> dict)
        {
            int total = 0;
            foreach (var keyValPair in dict)
            {
                total += keyValPair.Value;
                Worker.DoSomething();
            }

            return total;
        }

        static int Lookup(IDictionary<int, int> dict)
        {
            int total = 0;
           // int count = dict.Count();

            for (int i = 0; i < dict.Count; i++)
            {
                total  += dict[i];
                Worker.DoSomething();
            }

            return total;
        }

        public static void Benchmark(IDictionary<int, int> dict, int dictSize)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            Populate(dict, dictSize);
            stopwatch.Stop();
            Console.WriteLine($"Build:     {stopwatch.ElapsedMilliseconds} ms");

            stopwatch.Restart();
            int total = Enumerate(dict);
            stopwatch.Stop();
            Console.WriteLine($"Enumerate:     {stopwatch.ElapsedMilliseconds} ms");
            if(total != dictSize)
                Console.WriteLine($"Error:  Total was {total}, expected {dictSize}");

            stopwatch.Restart();
            int total2 = Lookup(dict);
            stopwatch.Stop();
            Console.WriteLine($"Lookup:     {stopwatch.ElapsedMilliseconds} ms");
            if (total != dictSize)
                Console.WriteLine($"Error:  Total was {total2}, expected {dictSize}");

        }
    }
}
