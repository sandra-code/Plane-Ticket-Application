using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Eksam
{
    [TestFixture]

    class TestingMachine
    {
        EconomyTicketMachine etm;
        BusinessTicketMachine btm;

        [Test]
        public void TestGetFirstCity_Kuressaare()
        {
            etm = new EconomyTicketMachine("EAKRSTYO2", "2018-02-02");
            string actual = etm.GetFirstCity();
            string expectedResult = "Kuressaare";
            Assert.AreEqual(expectedResult, actual);
        }

        [Test]
        public void TestGetSecondCity_Berlin()
        {
            etm = new EconomyTicketMachine("EAKRSBER2", "2018-02-02");
            string actual = etm.GetSecondCity();
            string expectedResult = "Berlin";
            Assert.AreEqual(expectedResult, actual);
        }

        [Test]
        public void TestSetFlightData()
        {
            etm = new EconomyTicketMachine("EAKRSBER2", "2018-05-02");
            DateTime actual = etm.SetFlightDate();
            DateTime expectedResult = new DateTime(2018, 05, 02);
            Assert.AreEqual(expectedResult, actual);
        }

        [Test]
        public void TestApplyWeekdayRate_Friday()
        {
            etm = new EconomyTicketMachine("EATLLTYO2", "2018-02-02");
            etm.SetFlightDate();
            double actual=etm.ApplyWeekdayRate(100);
            double expectedResult = 100 + 100 * 0.15;
            Assert.AreEqual(expectedResult, actual);
        }

        [Test]
        public void TestApplyTimeRate_MoreThan6months()
        {
            etm = new EconomyTicketMachine("EATLLTYO2", "2018-10-01");
            etm.SetFlightDate();
            DateTime dt = new DateTime(2018, 02, 01);
            double actual = etm.ApplyTimeRateForTestingOnly(100, dt);
            double expectedResult=100;
            Assert.AreEqual(expectedResult, actual);
        }

        [Test]
        public void TestApplyTimeRate_TwoMonthsPrior()
        {
            etm = new EconomyTicketMachine("EATLLTYO2", "2018-04-01");
            etm.SetFlightDate();
            DateTime dt = new DateTime(2018, 02, 01);
            double actual = etm.ApplyTimeRateForTestingOnly(100, dt);
            double expectedResult = 100+100*((6-2)*0.1);
            Assert.AreEqual(expectedResult, actual);
        }

        [Test]
        public void TestApplyOccupancyRate_Economy_10percentFull()
        {
            etm = new EconomyTicketMachine("EATLLTYO2", "2018-04-01");
            etm.SetPriceData(100, 10);
            etm.SellTicket("Peter Pie");
            double actual = etm.ApplyOccupancyRate(100);
            double expectedResult = 100;
            Assert.AreEqual(expectedResult, actual);
        }

        [Test]
        public void TestApplyOccupancyRate_Economy_100percentFull()
        {
            etm = new EconomyTicketMachine("EATLLTYO2", "2018-10-01");
            etm.SetPriceData(100, 3);
            etm.SellTicket("Peter Pie");
            etm.SellTicket("Thomas Pie");
            etm.SellTicket("Mary Pie");
            double actual = etm.ApplyOccupancyRate(100);
            double expectedResult = 100 + 100 * 0.17;
            Assert.AreEqual(expectedResult, actual);
        }

        [Test]
        public void TestApplyOccupancyRate_Business_100percentFull()
        {
            btm = new BusinessTicketMachine("EATLLTYO2b", "2018-10-01");
            btm.SetPriceData(100, 3);
            btm.SellTicket("Peter Pie");
            btm.SellTicket("Thomas Pie");
            btm.SellTicket("Mary Pie");
            double actual = btm.ApplyOccupancyRate(100);
            double expectedResult = 100 + 100 * 0.34;
            Assert.AreEqual(expectedResult, actual);
        }


    }

}
