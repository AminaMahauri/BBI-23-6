using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Lerok
{
    interface IHarvestCalculator
    {
        public double GetYield();
        public double GetYieldPerDay();
        public double GetAvgYield();
        public double GetAllYield();
    }
}
