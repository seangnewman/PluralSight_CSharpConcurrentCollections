using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            int dictSize = 200000;

            Console.WriteLine("Dictionary, Single Thread");
            var dict = new Dictionary<int, int>();
            SingleThreadBenchmark.Benchmark(dict, dictSize);

            Console.WriteLine("\r\nConcurrentDictionary, Single Thread");
            var dict2 = new ConcurrentDictionary<int, int>();
            SingleThreadBenchmark.Benchmark(dict2, dictSize);

            Console.WriteLine("\r\nConcurrentDictionary, Multiple Threads");
            var dict3 = new ConcurrentDictionary<int, int>();
            ParallelBenchmark.Benchmark(dict3, dictSize);

            


        }
    }
}
