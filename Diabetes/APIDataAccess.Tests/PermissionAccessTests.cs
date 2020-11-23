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
    {
        [TestMethod]
        public void Get_Exists()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<PermissionModel, dynamic>("spPermission_Get", Arg.Any<object>(), "DDB").
                Returns(new List<PermissionModel>() { new PermissionModel() });
            var data = new PermissionAccess(sql);

            var res = data.Get(2);

            Assert.IsTrue(res != null);
        }

        [TestMethod]
        public void Get_NotExists()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<PermissionModel, dynamic>("spPermission_Get", Arg.Any<object>(), "DDB").
                Returns(new List<PermissionModel>());

            var data = new PermissionAccess(sql);

            var res = data.Get(2);

            Assert.AreEqual(null, res);
        }

    }
}
