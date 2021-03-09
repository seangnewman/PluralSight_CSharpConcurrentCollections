using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace EnumerateDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            var stock = new ConcurrentDictionary<string, int>();

            stock.TryAdd("kcdc", 0);
            stock.TryAdd("PluralSight Live 2018", 0);
            stock.TryAdd("docker", 0);

            foreach (var shirt in stock.ToArray())
            {
                stock["kcdc"]++;   
                Console.WriteLine(shirt.Key + ":" + shirt.Value);
            }
        }
    }
}
