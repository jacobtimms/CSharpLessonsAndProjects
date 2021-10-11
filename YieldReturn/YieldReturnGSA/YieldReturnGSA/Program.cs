using System;
using System.Collections.Generic;

namespace YieldReturnGSA
{
    class Program
    {
        static void Main(string[] args)
        {
            //Specify location of csv file
            string csvData = "/Users/JacobTimms/WEB DEV/C# Lessons/YieldReturn/pnl.csv";
            string csvCapital = "/Users/JacobTimms/WEB DEV/C# Lessons/YieldReturn/capital.csv";
            string csvProperties = "/Users/JacobTimms/WEB DEV/C# Lessons/YieldReturn/properties.csv";

            //Crete new 'StrategyPnl' by passing in CSV location data & strategy number
            StrategyPnl Strat1 = new StrategyPnl(csvData, 1);
            Console.WriteLine(Strat1.ToString());
        }
    }
}
