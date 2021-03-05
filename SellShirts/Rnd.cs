using System;

namespace SellShirts
{
    public class Rnd
    {
        private static readonly Random _generator = new Random();
        public static int NextInt(int max) => _generator.Next(max);

        public static bool TrueWithProb(double probOfTrue) => _generator.NextDouble() < probOfTrue;
    }
}