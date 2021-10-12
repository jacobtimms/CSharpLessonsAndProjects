using System;
using System.Collections.Generic;
using System.Text;

namespace CsvPnl
{
    public class Capital
    {
        public Capital(DateTime newDate, decimal newAmount, StrategyPnl strat)
        {
            Date = newDate;
            Amount = newAmount;
            Strategy = strat;
        }
        public StrategyPnl Strategy { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public override string ToString()
        {
            return "Date: " + Date.ToShortDateString() + " Capital: " + Amount;
        }
    }
}
