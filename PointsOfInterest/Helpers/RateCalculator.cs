using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointsOfInterest.Helpers
{
    public static class RateCalculator
    {
        public static decimal  Calculcate(int fiveCount, int fourCount, int threeCount, int doubleCount,int oneCount)
        {
            return (5 * fiveCount + 4 * fourCount + 3 * threeCount + 2 * doubleCount + 1 * oneCount)
                / (fiveCount + fourCount + threeCount + doubleCount + oneCount);
        }
    }
}
