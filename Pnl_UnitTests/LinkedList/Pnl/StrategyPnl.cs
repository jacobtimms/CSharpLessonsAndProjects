using System;
using System.Collections.Generic;
using System.Text;

namespace CsvPnl
{
    // (TRADING) STRATEGY
    public class StrategyPnl
    {
        public StrategyPnl(string newStrategy)
        {
            Strategy = newStrategy;
            Pnls = new List<Pnl>();
            Capitals = new List<Capital>();
        }
        public string Strategy { get; set; }
        public List<Pnl> Pnls { get; set; }
        public List<Capital> Capitals { get; set; }
        public string Region { get; set; }
    }
}
