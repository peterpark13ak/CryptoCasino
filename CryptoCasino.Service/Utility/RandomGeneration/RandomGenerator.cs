using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WebCasino.Service.Utility.RandomGeneration
{
    public class RandomGenerator : IRandomGenerator
    {
        public IEnumerable<int> GenerateNumber(int min, int max, int amount)
        {
            if(amount < 1)
            {
                throw new ArgumentOutOfRangeException("", "Amount of numbers cannot be negative");
            }

            if(min > max)
            {
                throw new ArgumentOutOfRangeException("", "Min border cannot be greater than max border");
            }
            if(min == max)
            {
                return Enumerable.Repeat(min, amount).ToList();
            }

            using(var rng = new RNGCryptoServiceProvider())
            {
                var numbers = new List<int>();
                var data = new byte[16];
                for (int i = 0; i < amount; i++)
                {
                    rng.GetBytes(data);

                    var generatedNumber = Math.Abs(BitConverter.ToUInt16(data, startIndex: 0));

                    int diff = max - min;
                    int mod = generatedNumber % diff;   
                    int normalizedValue = min + mod;
                    numbers.Add(normalizedValue);
                }


                return numbers;
            }
        }
    }
}
