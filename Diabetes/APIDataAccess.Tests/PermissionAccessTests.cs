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
            sql.LoadData<UpdatePermissionDBModel, dynamic>("spPermission_Get", Arg.Any<object>(), "DDB").
                Returns(new List<UpdatePermissionDBModel>() { new UpdatePermissionDBModel() });
            var data = new PermissionAccess(sql);

            var res = data.Get(2);

            Assert.IsTrue(res != null);
        }

        [TestMethod]
        public void Get_NotExists()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<UpdatePermissionDBModel, dynamic>("spPermission_Get", Arg.Any<object>(), "DDB").
                Returns(new List<UpdatePermissionDBModel>());

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
            sql.LoadData<UpdatePermissionDBModel, dynamic>("spPermission_GetByWatcherId", Arg.Any<object>(), "DDB").
                Returns(new List<UpdatePermissionDBModel>());

            var data = new PermissionAccess(sql);

            var res = data.GetByWatcherId("2");

            Assert.IsTrue(res.Count == 0);
        }

        [TestMethod]
        public void GetByWatcherId_Exists()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<UpdatePermissionDBModel, dynamic>("spPermission_GetByWatcherId", Arg.Any<object>(), "DDB").
                Returns(new List<UpdatePermissionDBModel>() {new UpdatePermissionDBModel()});

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
            sql.LoadData<UpdatePermissionDBModel, dynamic>("spPermission_GetByTargetId", Arg.Any<object>(), "DDB").
                Returns(new List<UpdatePermissionDBModel>());

            var data = new PermissionAccess(sql);

            var res = data.GetByTargetId("2");
            Assert.AreEqual(0, res.Count);

        }

        [TestMethod]
        public void GetByTargetId_Exists()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<UpdatePermissionDBModel, dynamic>("spPermission_GetByTargetId", Arg.Any<object>(), "DDB").
                Returns(new List<UpdatePermissionDBModel>() { new UpdatePermissionDBModel() });

            var data = new PermissionAccess(sql);

            var res = data.GetByTargetId("2");

            Assert.AreEqual(1, res.Count);
        }

        #endregion

        #region UpdatePermissionModel

        [TestMethod]
        public void Update_CorrectCall()
        {
            var sql = Substitute.For<ISqlDataAccess>();

            DateTime testTime = DateTime.Now;

            UpdatePermissionDBModel updatedPermission = new UpdatePermissionDBModel()
            {
                Id = 3,
                StartDate = testTime,
                ExpireDate = testTime,
                WeeksActive = 2,
                WeeksDeactive = 1,
                Attributes = 4,
                Accepted = true
            };

            var data = new PermissionAccess(sql);

            data.Update(updatedPermission);

            sql.Received(1).SaveData(SpCommands.spPermission_UpdatePermissionModel.ToString(), Arg.Is<UpdatePermissionDBModel>((x) =>

                x.Id == 3 &&
                x.StartDate == testTime &&
                x.ExpireDate == testTime &&
                x.WeeksActive == 2 &&
                x.WeeksDeactive == 1 &&
                x.Attributes == 4 &&
                x.Accepted == true

            ), "DDB");
        }

        #endregion

        #region DeletePermission

        [TestMethod]
        public void Delete_CorrectCall()
        {
            var sql = Substitute.For<ISqlDataAccess>();

            var data = new PermissionAccess(sql);
            data.Delete(1);
            
            sql.Received(1).DeleteData(SpCommands.spPermission_Delete.ToString(), Arg.Any<object>(), "DDB");
        }

        #endregion

        #region CreatePermission

        [TestMethod]
        public void Create_CorrectCall()
        {
            var sql = Substitute.For<ISqlDataAccess>();

            DateTime testTime = DateTime.Now;

            var data = new PermissionAccess(sql);
            RequestPermissionDBModel newPermission = new RequestPermissionDBModel()
            {
                WatcherID = "1",
                TargetID = "2",
                StartDate = testTime,
                ExpireDate = testTime,
                Days = 5,
                WeeksActive = 2,
                WeeksDeactive = 1,
                Attributes = 4,
                
            };

            data.Create(newPermission);

            sql.Received(1).SaveData(SpCommands.spPermission_Create.ToString(), Arg.Any<object>(), "DDB");
        }

        #endregion

        #region DeleteByUserId

        [TestMethod]
        public void DeleteByUserId_CorrectCall()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            var data = new PermissionAccess(sql);

            data.DeleteByUserId("23");

            sql.Received(1).DeleteData(SpCommands.spPermission_DeleteByUserId.ToString(), Arg.Any<object>(), "DDB");
        }

        #endregion

        #region 


    }
}
