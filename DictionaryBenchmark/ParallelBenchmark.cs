using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DictionaryBenchmark
{
    class ParallelBenchmark
    {
        static void Populate(ConcurrentDictionary<int, int> dict, int dictSize) => Parallel.For(0, dictSize, (i) => {
                                                                                                                                                                                        dict.TryAdd(i, 1);
                                                                                                                                                                                        Worker.DoSomething();
                                                                                                                                                                                    });
         
        static int Enumerate(ConcurrentDictionary<int, int> dict)
        {
            int expectedTotal = dict.Count;

            int total = 0;

            Parallel.ForEach(dict, KeyValuePair => { 
                Interlocked.Add(ref total, KeyValuePair.Value);
                Worker.DoSomething();
            });
            return total;
        }

        static int Lookup(ConcurrentDictionary<int, int> dict)
        {
            int total = 0;
            Parallel.For(0, dict.Count, (int i) =>
            {
                Interlocked.Add(ref total, dict[i]);
                Worker.DoSomething();
            });
            return total;
        }
        public static void Benchmark(ConcurrentDictionary<int, int> dict, int dictSize)
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
            if (total != dictSize)
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
