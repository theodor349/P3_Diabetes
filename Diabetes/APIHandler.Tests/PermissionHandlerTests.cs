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
    public class PermissionHandlerTests {
        #region GetPermissionAttributes

        [TestMethod]
        public void GetPermissionAttributes_SinglePermission() {
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<UpdatePermissionDBModel, dynamic>(SpCommands.spPermission_GetPermissionAttributes.ToString(), Arg.Any<object>(), "DDB").
                Returns(new List<UpdatePermissionDBModel>() { new UpdatePermissionDBModel() });
            var access = new PermissionAccess(sql);
            var handler = new PermissionHandler(sql, access);

            UpdatePermissionDBModel perm1 = new UpdatePermissionDBModel {
                TargetID = "2",
                startTime = DateTime.Now.TimeOfDay.Ticks,
                endTime = DateTime.Now.AddMinutes(1).TimeOfDay.Ticks,
                Attributes = 1
            };

            UpdatePermissionDBModel[] perms = { perm1 };
            var res = handler.GetPermissionAttributes(perms);

            Assert.IsTrue(res.Count > 0);
        }

        [TestMethod]
        public void GetPermissionAttributes_MultiplePermissions() {
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<UpdatePermissionDBModel, dynamic>(SpCommands.spPermission_GetPermissionAttributes.ToString(), Arg.Any<object>(), "DDB").
                Returns(new List<UpdatePermissionDBModel>() { new UpdatePermissionDBModel() });
            var access = new PermissionAccess(sql);
            var handler = new PermissionHandler(sql, access);

            UpdatePermissionDBModel perm1 = new UpdatePermissionDBModel {
                TargetID = "2",
                startTime = DateTime.Now.TimeOfDay.Ticks,
                endTime = DateTime.Now.AddMinutes(1).TimeOfDay.Ticks,
                Attributes = 1
            };

            UpdatePermissionDBModel perm2 = new UpdatePermissionDBModel {
                TargetID = "2",
                startTime = DateTime.Now.TimeOfDay.Ticks,
                endTime = DateTime.Now.AddMinutes(1).TimeOfDay.Ticks,
                Attributes = 12
            };

            UpdatePermissionDBModel[] perms = { perm1, perm2 };
            var res = handler.GetPermissionAttributes(perms);

            Assert.AreEqual(13, res["2"]);
        }

        [TestMethod]
        public void GetPermissionAttributes_NotExists() {
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<UpdatePermissionDBModel, dynamic>(SpCommands.spPermission_GetPendingPermissions.ToString(), Arg.Any<object>(), "DDB").
                Returns(new List<UpdatePermissionDBModel>());

            var access = new PermissionAccess(sql);
            var handler = new PermissionHandler(sql, access);

            UpdatePermissionDBModel perm = new UpdatePermissionDBModel();
            perm.TargetID = "2";

            UpdatePermissionDBModel[] perms = { perm };
            Dictionary<string, int> res = handler.GetPermissionAttributes(perms);

            Assert.AreEqual(res.Count, 0);
        }

        #endregion
    }
}
