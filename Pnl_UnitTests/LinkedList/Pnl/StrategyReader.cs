using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CsvPnl
{
    public class StrategyReader : IStrategyReader
    {
        public string CapitalDataFile = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Data/capital.csv";
        public string PnlDataFile = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Data/pnl.csv";
        public string RegionDataFile = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Data/properties.csv";
        public IEnumerable<StrategyPnl> InitStrategyList(List<string[]> data)
        {
            // gets strategies
            // string[] columnHeaders = data.First().Split(",");
            foreach (string column in data.First().Skip(1))
            {
                yield return new StrategyPnl(column);
            }
        }
        public virtual string[] ReadCsvFile(string file)
        {
            string[] csvRows = System.IO.File.ReadAllLines(file).ToArray();
            return csvRows;
        }

        public virtual List<string[]> ReadCsv(string file)
        {
            // return data of csv
            string[] csvRows = ReadCsvFile(file);
            var rowsToReturn = new List<string[]>();
            foreach (var row in csvRows)
            {
                rowsToReturn.Add(row.Split(","));
            }
            return rowsToReturn;
        }
        public List<StrategyPnl> ReadCapital(List<string[]> data, List<StrategyPnl> list)
        {
            // returns list of capitals to fill in
            foreach (var row in data.Skip(1))
            {
                DateTime currentDate = DateTime.Parse(row[0]);
                for (int x = 1; x < row.Length; x++)
                {
                    Capital newCap = new Capital(currentDate, decimal.Parse(row[x]), list[x - 1]);
                    list[x - 1].Capitals.Add(newCap);
                }
            }
            return list;
        }
        public List<StrategyPnl> ReadRegions(List<string[]> data, List<StrategyPnl> list)
        {
            foreach (var row in data.Skip(1))
            {
                var strat = row[0];
                var newRegion = row[1];
                list.First(x => x.Strategy.Equals(strat)).Region = newRegion;
            }
            return list;
        }
        public List<StrategyPnl> ReadPnls(List<string[]> data, List<StrategyPnl> list)
        {
            // populate strategy's pnls
            foreach (var row in data.Skip(1))
            {
                DateTime currentDate = DateTime.Parse(row[0]);
                for (int x = 1; x < row.Length; x++)
                {
                    Pnl newPnl = new Pnl(currentDate, decimal.Parse(row[x]));
                    list[x - 1].Pnls.Add(newPnl);
                }
            }
            return list;
        }
    }
}
