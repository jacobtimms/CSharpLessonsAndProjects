using System;
using System.Collections.Generic;
using System.Text;

namespace CsvPnl
{
    public class Pnl
    {
        public Pnl(DateTime newDate, decimal newAmount)
        {
            Date = newDate;
            Amount = newAmount;
        }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public override string ToString()
        {
            return "Date: " + Date.ToShortDateString() + " Value: " + Amount;
        }
    }
}
