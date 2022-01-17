using System;

namespace PnlGSA
{
    class Program
    {
        static void Main(string[] args)
        {
            //Specify location of csv file
            string csvData = "/Users/JacobTimms/REPOS/C# Lessons/PnlGSA/pnl.csv";

            //Crete new 'StrategyPnl' by passing in CSV location data & strategy number
            StrategyPnl Strat1 = new StrategyPnl(csvData, 1);
            Console.WriteLine(Strat1.ToString());
        }
    }
}