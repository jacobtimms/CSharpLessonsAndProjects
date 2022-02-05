using NUnit.Framework;
using System;
using CsvPnl;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace PnlTests
{
    [TestFixture]
    public class PnlTests1
    {
        [Test]
        public void PnlClassWorks()
        {
            Pnl testPnl = new Pnl(DateTime.Now, Convert.ToDecimal(123.12));
            string expected = "Date: " + DateTime.Now.ToShortDateString() + " Value: 123.12";

            Assert.AreEqual(expected, testPnl.ToString());
        }







        //[Test]
        //public void PnlClassWorks()
        //{
        //    Pnl testPnl = new Pnl(DateTime.Now, Convert.ToDecimal(123.12));
        //    string expected = "Date: " + DateTime.Now.ToShortDateString() + " Value: 123.12";

        //    Assert.AreEqual(expected, testPnl.ToString());
        //}
        //[Test]
        //public void StrategyListCanInitialiseUsingCsv()
        //{
        //    StrategyList stratList = new StrategyList();
        //    Assert.AreEqual(stratList._list[0].Strategy, "Strategy1");
        //}
        //[Test]
        //public void StrategyListCanPopulatePnlsUsingCsv()
        //{
        //    StrategyList stratList = new StrategyList();

        //    stratList.PopulateStrategyListPnls(stratList.PnlDataFile);

        //    Assert.IsNotNull(stratList._list[1].Pnls);
        //    Assert.AreEqual(stratList._list[0].Pnls[0].Amount, 95045m);
        //}
        //[Test]
        //public void StrategyListCanPrintPnlsForStrategy()
        //{
        //    StrategyService service = new StrategyService();
        //    StrategyList stratList = new StrategyList();
        //    stratList.PopulateStrategyListPnls(stratList.PnlDataFile);

        //    string actualString = service.PrintStrategyPnls(1, stratList._list).ToList()[1];
        //    string expectedString = "Date: 04/01/2010 Value: -140135";
        //    Assert.AreEqual(actualString, expectedString);
        //}
        //[Test]
        //public void StrategyListDoesNotPrintOutOfBounds()
        //{
        //    StrategyService service = new StrategyService();
        //    StrategyList stratList = new StrategyList();
        //    stratList.PopulateStrategyListPnls(stratList.PnlDataFile);

        //    try
        //    {
        //        List<string> s = service.PrintStrategyPnls(200, stratList._list).ToList();
        //        Assert.Fail();
        //    }
        //    catch {}

        //}
        //[Test]
        //public void StrategyListCanPopulateRegion()
        //{
        //    StrategyList stratList = new StrategyList();

        //    stratList.PopulateStrategyListPnls(stratList.PnlDataFile);
        //    stratList.PopulateStrategyListRegions(stratList.RegionDataFile);

        //    Assert.AreEqual(stratList._list[0].Region, "AP");
        //}
        //[Test]
        //public void StrategyListCanPopulateCapitals()
        //{
        //    StrategyList stratList = new StrategyList();

        //    stratList.PopulateStrategyListPnls(stratList.PnlDataFile);
        //    stratList.PopulateStrategyListCapital(stratList.CapitalDataFile);

        //    Assert.AreEqual(stratList._list[0].Capitals[0].Amount, 120500000m);
        //}
        //[Test]
        //public void ServiceCanReturnCapitalsOfStrategy()
        //{
        //    StrategyService service = new StrategyService();
        //    StrategyList stratList = new StrategyList();

        //    stratList.PopulateStrategyListPnls(stratList.PnlDataFile);
        //    stratList.PopulateStrategyListCapital(stratList.CapitalDataFile);
        //    string[] actual = service.PrintStrategyCapitals("Strategy1,Strategy2", stratList).ToArray();

        //    Assert.AreEqual(actual[0], "Strategy1: Date: 01/01/2010 Capital: 120500000");
        //}
        //[Test]
        //public void ServiceCanReturnCumulativePnlsByRegion()
        //{
        //    StrategyService service = new StrategyService();
        //    StrategyList stratList = new StrategyList();

        //    stratList.PopulateStrategyListPnls(stratList.PnlDataFile);
        //    stratList.PopulateStrategyListRegions(stratList.RegionDataFile);
        //    stratList.PopulateStrategyListCapital(stratList.CapitalDataFile);
        //    string actual = service.PrintRegionCumulativePnl("AP", stratList).ToList()[3];

        //    Assert.AreEqual(actual, "Date: 06/01/2010 00:00:00, cumulative Pnl: -280599");
        //}
    }
}