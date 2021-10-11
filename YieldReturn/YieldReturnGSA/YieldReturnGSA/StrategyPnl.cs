using System;
using System.Collections.Generic;
namespace YieldReturnGSA
{
    public class StrategyPnl
    {
        public string Strategy { get; set; }
        List<Pnl> Pnls { get; set; }
        decimal PnlTotal { get; set; }

        public StrategyPnl(string csvData, string csvCapital, string csvProperties, int stratNum)
        {
            //Read CSV Data
            //Check for errors, throw exceptions if needed
            string[] csvRows;

            if (System.IO.File.Exists(csvData))
            {
                csvRows = System.IO.File.ReadAllLines(csvData);
            }
            if (System.IO.File.Exists(csvCapital))
            {
                csvRows = System.IO.File.ReadAllLines(csvCapital);
            }
            if (System.IO.File.Exists(csvProperties))
            {
                csvRows = System.IO.File.ReadAllLines(csvProperties);
            }

            else
            {
                throw new ArgumentException("File directory not found");
            }

            if (stratNum == 0)
            {
                throw new ArgumentException("Strategy number must be labelled 1 or higher");
            }

            //Split header line and match strat number to name
            ListPnLs(stratNum, csvRows);
        }

        private string[] ListPnLs(int stratNum, string[] csvRows)
        {
            string[] stratNames = csvRows[0].Split(',');
            if (stratNum > stratNames.Length)
            {
                throw new ArgumentException("This strategy does not exist");
            }
            Strategy = stratNames[stratNum];

            Pnls = new List<Pnl>();
            PnlTotal = 0;

            //Loop through CSV data and create PnL objects for each line / date
            for (int i = 1; i < csvRows.Length; i++)
            {
                string[] csvColumn = csvRows[i].Split(',');
                Pnl StratPnl = new Pnl(csvColumn, stratNum);

                Pnls.Add(StratPnl);
                PnlTotal += StratPnl.Amount;
            }
            return stratNames;
        }

        //Override 'ToString()' for StrategyPnl object
        public override string ToString()
        {
            string pnls = "";
            foreach (var pnl in Pnls)
            {
                pnls += pnl.ToString() + Environment.NewLine;
            }

            string str = $"{Strategy}:{Environment.NewLine}{pnls}" +
                $"{Environment.NewLine}Strategy Pnl Total: {PnlTotal}";
            return str;
        }
    }

    public class Pnl
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public Pnl(string[] csvColumnData, int stratNum)
        {
            //Convert inputted rowData to DateTime & Amount variables
            Date = DateTime.ParseExact(csvColumnData[0], "yyyy-MM-dd", null);
            Amount = Convert.ToDecimal(csvColumnData[stratNum]);
        }

        //Override 'ToString()' for Pnl object
        public override string ToString()
        {
            string str = $"Date: {Date}, PnL Amount: {Amount}";
            return str;
        }
    }
}
 