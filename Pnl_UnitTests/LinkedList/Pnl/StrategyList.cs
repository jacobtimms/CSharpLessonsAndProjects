using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvPnl
{
    public class StrategyList : List<StrategyPnl>
    {
        public StrategyReader _reader = new StrategyReader();
        public List<StrategyPnl> _list;

        public void InitialiseStrategyList()
        {
            _list = _reader.InitStrategyList(_reader.ReadCsv(_reader.CapitalDataFile)).ToList();
            _list = _reader.ReadRegions(_reader.ReadCsv(_reader.RegionDataFile), _list);
            _list = _reader.ReadCapital(_reader.ReadCsv(_reader.CapitalDataFile), _list);
            _list = _reader.ReadPnls(_reader.ReadCsv(_reader.PnlDataFile), _list);
        }

        public IEnumerable<string> PrintRegionCumulativePnl(string region, StrategyList list)
        {
            var stratsByRegion = list._list.Where(x => x.Region.Equals(region)).ToList();
            int amountOfPnls = stratsByRegion.First().Pnls.Count();
            string toReturn = "";
            for (int x = 0; x < amountOfPnls; x++)
            {
                // every date (pnl)
                decimal totalPnlOnDate = 0;
                foreach (StrategyPnl strat in stratsByRegion)
                {
                    // every strategy
                    totalPnlOnDate += strat.Pnls[x].Amount;
                }
                toReturn = "Date: " + stratsByRegion.First().Pnls[x].Date + ", cumulative Pnl: " + totalPnlOnDate;
                yield return toReturn;
            }
        }
        public IEnumerable<string> PrintStrategyPnls(int strategyNumber, List<StrategyPnl> list)
        {
            if (strategyNumber > list.Count)
            {
                throw new Exception("Out of bounds");
            }
            foreach (Pnl pnl in list[strategyNumber - 1].Pnls)
            {
                Console.WriteLine(pnl.ToString());
                yield return pnl.ToString();
            }
        }
        public IEnumerable<string> PrintStrategyCapitals(string strategies, StrategyList list)
        {
            string[] stratNameArray = strategies.Split(",");
            foreach (string stratName in stratNameArray)
            {
                // input
                foreach (StrategyPnl strategy in list._list)
                {
                    // strats

                    if (stratName.Equals(strategy.Strategy))
                    {
                        foreach (Capital cap in strategy.Capitals)
                        {
                            // get capitals
                            yield return strategy.Strategy + ": " + cap.ToString();
                        }
                    }
                }
            }
        }
    }
}