using APIDataAccess.DataAccess;
using APIDataAccess.Internal.DataAccess;
using APIDataAccess.Models.Permission;
using APIHandler.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;

namespace APIHandler.Tests {

    [TestClass]
    public class PermissionHandlerTests
    {
        #region GetPermissionAttributes

        [TestMethod]
        public void GetPermissionAttributes_SinglePermission()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<PermissionDBModel, dynamic>(SpCommands.spPermission_GetPermissionAttributes.ToString(), Arg.Any<object>(), "DDB").
                Returns(new List<PermissionDBModel>() { new PermissionDBModel() });
            var access = new PermissionAccess(sql);
            var handler = new PermissionHandler(access);

            PermissionDBModel perm1 = new PermissionDBModel
            {
                TargetID = "2",
                StartDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddMinutes(1),
                Accepted = true,
                Attributes = 1
            };

            PermissionDBModel[] perms = { perm1 };
            var res = handler.GetPermissionAttributes(perms);

            Assert.IsTrue(res.Count > 0);
        }

        [TestMethod]
        public void GetPermissionAttributes_MultiplePermissions()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<PermissionDBModel, dynamic>(SpCommands.spPermission_GetPermissionAttributes.ToString(), Arg.Any<object>(), "DDB").
                Returns(new List<PermissionDBModel>() { new PermissionDBModel() });
            var access = new PermissionAccess(sql);
            var handler = new PermissionHandler(access);

            PermissionDBModel perm1 = new PermissionDBModel
            {
                TargetID = "2",
                StartDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddMinutes(1),
                Accepted = true,
                Attributes = 1
            };

            PermissionDBModel perm2 = new PermissionDBModel
            {
                TargetID = "2",
                StartDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddMinutes(1),
                Accepted = true,
                Attributes = 12
            };

            PermissionDBModel[] perms = { perm1, perm2 };
            var res = handler.GetPermissionAttributes(perms);

            Assert.AreEqual(13, res["2"]);
        }

        [TestMethod]
        public void GetPermissionAttributes_NotExists()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<PermissionDBModel, dynamic>(SpCommands.spPermission_GetPendingPermissions.ToString(), Arg.Any<object>(), "DDB").
                Returns(new List<PermissionDBModel>());

            var access = new PermissionAccess(sql);
            var handler = new PermissionHandler(access);

            PermissionDBModel perm = new PermissionDBModel();
            perm.TargetID = "2";

            PermissionDBModel[] perms = { perm };
            Dictionary<string, int> res = handler.GetPermissionAttributes(perms);

            Assert.AreEqual(res.Count, 0);
        }

        #endregion

        #region IsActive
        [TestMethod]
        public void IsActive_ActiveTime()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            var access = new PermissionAccess(sql);

            var startDate = new DateTime(2020, 10, 20, 10, 0, 0);   // 10:00
            var expireDate = new DateTime(2020, 10, 20, 10, 15, 0); // 10:15
            var testDate = new DateTime(2020, 10, 20, 10, 10, 0);   // 10:10
            var expected = true;

            var p = new PermissionDBModel
            {
                TargetID = "2",
                StartDate = startDate, 
                ExpireDate = expireDate, 
                Accepted = true,
                Attributes = 1
            };

            var res = p.IsActive(testDate);

            Assert.AreEqual(expected, res);
        }
        [TestMethod]
        public void IsActive_InactiveTime()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            var access = new PermissionAccess(sql);

            var startDate =     new DateTime(2020, 10, 20, 10, 0, 0);   // 10:00
            var expireDate =    new DateTime(2020, 10, 20, 10, 15, 0);  // 10:15
            var testDate =      new DateTime(2020, 10, 20, 10, 20, 0);  // 10:10
            var expected = false;

            var p = new PermissionDBModel
            {
                TargetID = "2",
                StartDate = startDate,
                ExpireDate = expireDate,
                Accepted = true,
                Attributes = 1
            };

            var res = p.IsActive(testDate);

            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        public void IsActive_ActiveDays()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            var access = new PermissionAccess(sql);

            var startDate =     new DateTime(2020, 10, 20, 10, 0, 0);   // 20/10
            var expireDate =    new DateTime(2020, 10, 22, 10, 0, 0);   // 22/10
            var testDate =      new DateTime(2020, 10, 21, 10, 0, 0);   // 21/10
            var expected = true;

            var p = new PermissionDBModel
            {
                TargetID = "2",
                StartDate = startDate,
                ExpireDate = expireDate,
                Accepted = true,
                Attributes = 1
            };

            var res = p.IsActive(testDate);

            Assert.AreEqual(expected, res);
        }
        [TestMethod]
        public void IsActive_InactiveDays()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            var access = new PermissionAccess(sql);

            var startDate =     new DateTime(2020, 10, 20, 10, 0, 0);   // 20/10
            var expireDate =    new DateTime(2020, 10, 22, 10, 0, 0);   // 22/10
            var testDate =      new DateTime(2020, 10, 23, 10, 0, 0);   // 23/10
            var expected = false;

            var p = new PermissionDBModel
            {
                TargetID = "2",
                StartDate = startDate,
                ExpireDate = expireDate,
                Accepted = true,
                Attributes = 1
            };

            var res = p.IsActive(testDate);

            Assert.AreEqual(expected, res);
        }

        [DataRow(18, 12, false)]
        [DataRow(17, 12, true)] // End
        [DataRow(12, 12, true)]  
        [DataRow(7, 12, true)]  // Start
        [DataRow(6, 12, false)] 
        [DataRow(23, 11, false)] 
        [DataRow(22, 11, true)] // End
        [DataRow(16, 11, true)]  
        [DataRow(9, 11, true)]  // Start
        [DataRow(5, 11, false)]
        [TestMethod]
        public void IsActive_Recurring2x2(int day, int month, bool expected)
        {
            var sql = Substitute.For<ISqlDataAccess>();
            var access = new PermissionAccess(sql);

            var startDate_ = new DateTime(2020, 11, 9, 1, 0, 0);
            var expireDate = new DateTime(2020, 12, 17, 23, 0, 0);
            var testDate__ = new DateTime(2020, month, day, 10, 0, 0); 
            int weeksActive = 2;
            int weeksDeactive = 2;
            int days = 127;
            
            var p = new PermissionDBModel
            {
                StartDate = startDate_,
                ExpireDate = expireDate,
                WeeksActive = weeksActive,
                WeeksDeactive = weeksDeactive,
                Accepted = true,
                Days = days
            };

            var res = p.IsActive(testDate__);

            Assert.AreEqual(expected, res);
        }
        [DataRow(14, 1, true)] // Monday
        [DataRow(15, 1, false)]
        [DataRow(15, 2, true)] // Tuesday
        [DataRow(16, 2, false)]
        [DataRow(16, 4, true)] // Wedensday
        [DataRow(17, 4, false)]
        [DataRow(17, 8, true)]  // Thursday
        [DataRow(18, 8, false)]
        [DataRow(18, 16, true)]  // Friday
        [DataRow(19, 16, false)]
        [DataRow(19, 32, true)]  // Saturday
        [DataRow(20, 32, false)]
        [DataRow(13, 64, true)]  // Sunday
        [DataRow(14, 64, false)]
        [DataRow(13, 127, true)]  // All
        [TestMethod]
        public void IsActive_RecurringDays(int day, int days, bool expected)
        {
            var sql = Substitute.For<ISqlDataAccess>();
            var access = new PermissionAccess(sql);

            var startDate_ = new DateTime(2020, 11, 9, 1, 0, 0);
            var expireDate = new DateTime(2020, 12, 30, 23, 0, 0);
            var testDate__ = new DateTime(2020, 12, day, 10, 0, 0);
            int weeksActive = 2;
            int weeksDeactive = 2;

            var p = new PermissionDBModel
            {
                StartDate = startDate_,
                ExpireDate = expireDate,
                WeeksActive = weeksActive,
                WeeksDeactive = weeksDeactive,
                Accepted = true,
                Days = days
            };

            var res = p.IsActive(testDate__);

            Assert.AreEqual(expected, res);
        }

        [DataRow(true)]
        [DataRow(false)]
        [TestMethod]
        public void IsActive_Accepted(bool accepted)
        {
            var sql = Substitute.For<ISqlDataAccess>();
            var access = new PermissionAccess(sql);

            var startDate = new DateTime(2020, 10, 20, 10, 0, 0);   // 10:00
            var expireDate = new DateTime(2020, 10, 20, 10, 15, 0); // 10:15
            var testDate = new DateTime(2020, 10, 20, 10, 10, 0);   // 10:10
            var expected = accepted;

            var p = new PermissionDBModel
            {
                TargetID = "2",
                StartDate = startDate,
                ExpireDate = expireDate,
                Attributes = 1,
                Accepted = accepted
            };

            var res = p.IsActive(testDate);

            Assert.AreEqual(expected, res);
        }
        #endregion
    }
}
