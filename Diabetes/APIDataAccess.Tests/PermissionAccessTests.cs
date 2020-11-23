using APIDataAccess.DataAccess;
using APIDataAccess.Internal.DataAccess;
using APIDataAccess.Models.Permission;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIDataAccess.Tests
{
    [TestClass]
    public class PermissionAccessTests

    #region Get
    {
        [TestMethod]
        public void Get_Exists()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<PermissionDBModel, dynamic>("spPermission_Get", Arg.Any<object>(), "DDB").
                Returns(new List<PermissionDBModel>() { new PermissionDBModel() });
            var data = new PermissionAccess(sql);

            var res = data.Get(2);

            Assert.IsTrue(res != null);
        }

        [TestMethod]
        public void Get_NotExists()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<PermissionDBModel, dynamic>("spPermission_Get", Arg.Any<object>(), "DDB").
                Returns(new List<PermissionDBModel>());

            var data = new PermissionAccess(sql);

            var res = data.Get(2);

            Assert.AreEqual(null, res);
        }

        #endregion

        #region GetByWatcherId

        [TestMethod]
        public void GetByWatcherId_NotExists()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<PermissionDBModel, dynamic>("spPermission_GetByWatcherId", Arg.Any<object>(), "DDB").
                Returns(new List<PermissionDBModel>());

            var data = new PermissionAccess(sql);

            var res = data.GetByWatcherId("2");

            Assert.IsTrue(res.Count == 0);
        }

        [TestMethod]
        public void GetByWatcherId_Exists()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<PermissionDBModel, dynamic>("spPermission_GetByWatcherId", Arg.Any<object>(), "DDB").
                Returns(new List<PermissionDBModel>() {new PermissionDBModel()});

            var data = new PermissionAccess(sql);

            var res = data.GetByWatcherId("2");
            Assert.AreEqual(1, res.Count);
        }

        #endregion

        #region GetByTargetId

        [TestMethod]
        public void GetByTargetId_NotExists()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<PermissionDBModel, dynamic>("spPermission_GetByTargetId", Arg.Any<object>(), "DDB").
                Returns(new List<PermissionDBModel>());

            var data = new PermissionAccess(sql);

            var res = data.GetByTargetId("2");
            Assert.AreEqual(0, res.Count);

        }

        [TestMethod]
        public void GetByTargetId_Exists()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<PermissionDBModel, dynamic>("spPermission_GetByTargetId", Arg.Any<object>(), "DDB").
                Returns(new List<PermissionDBModel>() { new PermissionDBModel() });

            var data = new PermissionAccess(sql);

            var res = data.GetByTargetId("2");

            Assert.AreEqual(1, res.Count);
        }

        #endregion

    }
}
