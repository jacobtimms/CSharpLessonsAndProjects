using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvPnl
{
    public class Program
    {
        static void Main(string[] args)
        {
            StrategyList stratList = new StrategyList();
            StrategyReader _reader = stratList._reader;

            stratList.InitialiseStrategyList();
            //stratList.PopulateStrategyListCapital(stratList.CapitalDataFile);
            Console.WriteLine("COMMANDS: capital, cumulative-pnl");
            Console.WriteLine("ENTER COMMAND:");
            string[] commandArray = Console.ReadLine().Split(" ");
            switch (commandArray[0])
            {
                case "capital":
                    {
                        string[] outputArray;
                        outputArray = stratList.PrintStrategyCapitals(commandArray[1], stratList).ToArray();
                        Console.WriteLine(String.Join("\n", outputArray));
                    }
                    break;
                case "cumulative-pnl":
                    {
                        string[] outputArray;
                        outputArray = stratList.PrintRegionCumulativePnl(commandArray[1], stratList).ToArray();
                        Console.WriteLine(String.Join("\n", outputArray));
                    }
                    break;
            }
        }
    }
}
