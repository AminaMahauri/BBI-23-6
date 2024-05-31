using System;

namespace Lab_Lerok
{
    public interface IHarvestCalculator
    {
        double GetYield();
        double GetYieldPerDay();
        double GetAvgYield();
        double GetAllYield();
    }
}
