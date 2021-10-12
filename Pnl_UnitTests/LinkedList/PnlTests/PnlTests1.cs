using NUnit.Framework;
using System;
using CsvPnl;
using Moq;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace PnlTests
{
    [TestFixture]
    public class PnlTests1
    {
        private readonly Mock<StrategyReader> _strategyReaderMock = new();

        [Test]
        public void ReadCsvFileTest()
        {
            //ARRANGE
            string[] expectedOutput = { "StratName,Region","Strategy1,AP","Strategy2,EU","Strategy3,EU","Strategy4,US","Strategy5,US","Strategy6,AP","Strategy7,US","Strategy8,AP","Strategy9,AP","Strategy10,AP","Strategy11,US","Strategy12,EU","Strategy13,EU", "Strategy14,US", "Strategy15,AP" };
            string RegionDataFile = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Data/properties.csv";

            //ACT
            _strategyReaderMock.CallBase = true;
            var output = _strategyReaderMock.Object.ReadCsvFile(RegionDataFile);

            //ASSERT
            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void ReadCsvTest_PnlData()
        {
        //ARRANGE
            string[] mockPnlRows = { "Date,Strategy1,Strategy2,Strategy3,Strategy4,Strategy5,Strategy6,Strategy7,Strategy8,Strategy9,Strategy10,Strategy11,Strategy12,Strategy13,Strategy14,Strategy15", "2010-01-01,95045,501273,429834,-352913,-69905,-188487,-179959,242415,269510,587689,-283773,-144819,125358,236732,-26555" };

            var expectedOutput = new List<string[]>()
            {
                new string[]
                {
                    "Date","Strategy1","Strategy2","Strategy3","Strategy4","Strategy5","Strategy6","Strategy7","Strategy8","Strategy9","Strategy10","Strategy11","Strategy12","Strategy13","Strategy14","Strategy15"
                },
                new string[]
                {
                    "2010-01-01","95045","501273","429834","-352913","-69905","-188487","-179959","242415","269510","587689","-283773","-144819","125358","236732","-26555"
                }
            };

            _strategyReaderMock.CallBase = true;
            _strategyReaderMock.Setup(x => x.ReadCsvFile(It.IsAny<string>()))
                .Returns(mockPnlRows);

            //ACT
            var output = _strategyReaderMock.Object.ReadCsv("test");

            //ASSERT
            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void ReadCsvTest_CapitalData()
        {
            //ARRANGE
            string[] mockCapitalRows = { "Date,Strategy1,Strategy2,Strategy3,Strategy4,Strategy5,Strategy6,Strategy7,Strategy8,Strategy9,Strategy10,Strategy11,Strategy12,Strategy13,Strategy14,Strategy15", "2010-01-01,120500000,118000000,98000000,129500000,55000000,129000000,55500000,77000000,99500000,51000000,113000000,55000000,95500000,73500000,53500000" };

            var expectedOutput = new List<string[]>()
            {
                new string[]
                {
                    "Date","Strategy1","Strategy2","Strategy3","Strategy4","Strategy5","Strategy6","Strategy7","Strategy8","Strategy9","Strategy10","Strategy11","Strategy12","Strategy13","Strategy14","Strategy15"
                },
                new string[]
                {
                    "2010-01-01","120500000","118000000","98000000","129500000","55000000","129000000","55500000","77000000","99500000","51000000","113000000","55000000","95500000","73500000","53500000"
                }
            };

            _strategyReaderMock.CallBase = true;
            _strategyReaderMock.Setup(x => x.ReadCsvFile(It.IsAny<string>()))
                .Returns(mockCapitalRows);

            //ACT
            var output = _strategyReaderMock.Object.ReadCsv("test");

            //ASSERT
            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void ReadCsvTest_RegionProperitesData()
        {
            //ARRANGE
            string[] mockPropertiesRows = { "StratName,Region", "Strategy1,AP" };

            var expectedOutput = new List<string[]>()
            {
                new string[]
                {
                    "StratName","Region"
                },
                new string[]
                {
                    "Strategy1","AP"
                }
            };

            _strategyReaderMock.CallBase = true;
            _strategyReaderMock.Setup(x => x.ReadCsvFile(It.IsAny<string>()))
                .Returns(mockPropertiesRows);

            //ACT
            var output = _strategyReaderMock.Object.ReadCsv("test");

            //ASSERT
            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void ReadCapitalTest_CheckExpectedOutput()
        {
            //ARRANGE
            var csvCapitalData = new List<string[]>()
            {
                new string[]
                {
                    "Date","Strategy1"
                },
                new string[]
                {
                    "2010-01-01","120500000"
                }
            };

            var strategyPnlList = new List<StrategyPnl>()
            {
                new StrategyPnl("Strategy1"),
            };

            var expectedOutput = new List<StrategyPnl>()
            {
                new StrategyPnl("Strategy1")
                {
                    Capitals = new List<Capital>
                    {
                        new Capital(DateTime.Parse("2010-01-01"), 120500000m, strategyPnlList[0])
                    }
                },
            };

            //ACT
            _strategyReaderMock.CallBase = true;
            var output = _strategyReaderMock.Object.ReadCapital(csvCapitalData, strategyPnlList);

            //ASSERT
            Assert.AreEqual(expectedOutput[0].Strategy, output[0].Strategy);
            Assert.AreEqual(expectedOutput[0].Capitals[0].Amount, output[0].Capitals[0].Amount);
            Assert.AreEqual(expectedOutput[0].Capitals[0].Date, output[0].Capitals[0].Date);
            Assert.AreEqual(expectedOutput[0].Capitals[0].Strategy, output[0].Capitals[0].Strategy);
        }

        [Test]
        public void ReadRegionTest_CheckExpectedOutput()
        {
            //ARRANGE
            var csvRegionData = new List<string[]>()
            {
                new string[]
                {
                    "StratName","Region"
                },
                new string[]
                {
                    "Strategy1","AP"
                }
            };

            var strategyPnlList = new List<StrategyPnl>()
            {
                new StrategyPnl("Strategy1")
                {
                    Strategy = "Strategy1",
                },
            };

            var expectedOutput = new List<StrategyPnl>()
            {
                new StrategyPnl("Strategy1")
                {
                    Strategy = "Strategy1",
                    Region = "AP",
                },
            };

            //ACT
            _strategyReaderMock.CallBase = true;
            var output = _strategyReaderMock.Object.ReadRegions(csvRegionData, strategyPnlList);

            //ASSERT
            Assert.AreEqual(expectedOutput[0].Strategy, output[0].Strategy);
            Assert.AreEqual(expectedOutput[0].Region, output[0].Region);
        }

        [Test]
        public void ReadPnlsTest_CheckExpectedOutput()
        {
            //ARRANGE
            var csvPnlData = new List<string[]>()
            {
                new string[]
                {
                    "Date","Strategy1"
                },
                new string[]
                {
                    "2010-01-01","95045"
                }
            };

            var strategyPnlList = new List<StrategyPnl>()
            {
                new StrategyPnl("Strategy1")
                {
                    Strategy = "Strategy1",
                },
            };

            var expectedOutput = new List<StrategyPnl>()
            {
                new StrategyPnl("Strategy1")
                {
                    Strategy = "Strategy1",
                    Pnls = new List<Pnl>
                    {
                        new Pnl(DateTime.Parse("2010-01-01"), 95045m)
                    }
                },
            };

            //ACT
            _strategyReaderMock.CallBase = true;
            var output = _strategyReaderMock.Object.ReadPnls(csvPnlData, strategyPnlList);

            //ASSERT
            Assert.AreEqual(expectedOutput[0].Strategy, output[0].Strategy);
            Assert.AreEqual(expectedOutput[0].Pnls[0].Date, output[0].Pnls[0].Date);
            Assert.AreEqual(expectedOutput[0].Pnls[0].Amount, output[0].Pnls[0].Amount);
        }

        [Test]
        public void ReadCapitalTest_CheckAmountOfCapitals()
        {
            //ARRANGE
            var csvCapitalData = new List<string[]>()
            {
                new string[]
                {
                    "Date","Strategy1"
                },
                new string[]
                {
                    "2010-01-01","120500000"
                }
            };

            var strategyPnlList = new List<StrategyPnl>()
            {
                new StrategyPnl("Strategy1"),
            };

            //ACT
            _strategyReaderMock.CallBase = true;
            var output = _strategyReaderMock.Object.ReadCapital(csvCapitalData, strategyPnlList);

            //ASSERT
            Assert.That(output[0].Capitals.Count, Is.Not.GreaterThan(1));
        }

        [Test]
        public void ReadPnlsTest_CheckAmountOfPnls()
        {
            //ARRANGE
            var csvPnlData = new List<string[]>()
            {
                new string[]
                {
                    "Date","Strategy1"
                },
                new string[]
                {
                    "2010-01-01","95045"
                }
            };

            var strategyPnlList = new List<StrategyPnl>()
            {
                new StrategyPnl("Strategy1")
                {
                    Strategy = "Strategy1",
                },
            };

            //ACT
            _strategyReaderMock.CallBase = true;
            var output = _strategyReaderMock.Object.ReadPnls(csvPnlData, strategyPnlList);

            //ASSERT
            Assert.That(output[0].Pnls.Count, Is.Not.GreaterThan(1));
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
        //    catch { }

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