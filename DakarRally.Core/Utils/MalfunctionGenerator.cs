using DakarRally.Core.Models;
using System;

namespace DakarRally.Core.Utils
{
    class MalfunctionGenerator
    {
        public Malfunction Generate(int lightMalfunctionProb, int heavyMalfunctionProb, DateTime time)
        {
            RandomNumberGenerator randomNumbergenerator = new RandomNumberGenerator();
            int random = randomNumbergenerator.Generate();

            if (random <= lightMalfunctionProb)
            {
                return new Malfunction(MalfunctionType.Light, time);
            }
            else if (random <= lightMalfunctionProb + heavyMalfunctionProb)
            {
                return new Malfunction(MalfunctionType.Heavy, time);
            }
            return null;
        }
    }
}