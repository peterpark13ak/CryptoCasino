using System;
using System.Collections.Generic;
using System.Text;

namespace WebCasino.Service.Utility.RandomGeneration
{
    public interface IRandomGenerator
    {
        IEnumerable<int> GenerateNumber(int min, int max, int amount);
    }
}
