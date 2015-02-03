using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem.Common
{
    public static class RandomGenerator
    {
        private static Random _rand;
        private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const string LettersWithSpaces = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz       ";

        static RandomGenerator()
        {
            _rand = new Random();
        }

        public static string GetRandomString(int min = 0,int max = 30)
        {
            StringBuilder result = new StringBuilder();
            int length = _rand.Next(min, max + 1);
            for (int i = 0; i < length; i++)
            {
                result.Append(Letters[_rand.Next(Letters.Length - 1)]);
            }
            return result.ToString();
        }

        public static int GetRandomNumber(int min = 0, int max = 100)
        {
            return _rand.Next(min, max + 1);
        }

        public static string GetRandomText(int min = 0, int max = 300)
        {
            StringBuilder result = new StringBuilder();
            int length = _rand.Next(min, max + 1);
            for (int i = 0; i < length; i++)
            {
                result.Append(LettersWithSpaces[_rand.Next(LettersWithSpaces.Length - 1)]);
            }
            return result.ToString().Trim();
        }
    }
}
