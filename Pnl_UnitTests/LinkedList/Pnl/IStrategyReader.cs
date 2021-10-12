using System;
using System.Collections.Generic;

namespace CsvPnl
{
    public interface IStrategyReader
    {
        public string[] ReadCsvFile(string file);
        public List<string[]> ReadCsv(string file);
    }
}
