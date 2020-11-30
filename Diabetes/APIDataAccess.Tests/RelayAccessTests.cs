using APIDataAccess.DataAccess;
using APIDataAccess.Internal.DataAccess;
using APIDataAccess.Models.Permission;
using APIHandler.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace APIDataAccess.Tests
{
    [TestClass]
    public class RelayAccessTests
    {
        RelayAccess relayAccess = new RelayAccess();

        #region GetBloodGlucose
        [TestMethod]
        public void GetBloodGlucose_Exists()
        {
            float res = 9999;
            res = relayAccess.GetBloodGlucose("https://thomascgm.herokuapp.com");

            Assert.AreNotEqual(9999, res);
        }

        #endregion

        #region GetBatteryStatus

        [TestMethod]
        public void GetBatteryStatus_Exists()
        {
            float res = 9999;
            res = relayAccess.GetBatteryStatus("https://thomascgm.herokuapp.com");

            Assert.AreNotEqual(9999, res);
        }

        #endregion

        #region GetInsulinStatus

        [TestMethod]
        public void GetInsulinStatus_Exists()
        {
            string insulinStatus = "sdsd";
            string res = relayAccess.GetInsulinStatus("https://thomascgm.herokuapp.com");

            Assert.AreNotEqual(insulinStatus, res);
        }

        #endregion

        #region GetLastReceived

        [TestMethod]
        public void GetLastReceived_Exists()
        {
            DateTime lastWeek = new DateTime(DateTime.Now.AddDays(-7).Ticks);
            DateTime res = relayAccess.GetLastReceived("https://thomascgm.herokuapp.com");

            Assert.IsTrue(lastWeek < res);
        }

        #endregion

        #region GetStatus

        [TestMethod]
        public void GetStatus_Exists()
        {
            string res = relayAccess.GetStatus("https://thomascgm.herokuapp.com");

            Assert.AreNotEqual(null, res); 
        }

        #endregion
    }
}
