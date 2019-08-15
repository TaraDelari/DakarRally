using System;

namespace DakarRally.Core.Utils
{
    class RandomNumberGenerator
    {
        private static readonly Random _random = new Random();

        public int Generate()
        {
            return _random.Next(0, 100);
        }
    }
}