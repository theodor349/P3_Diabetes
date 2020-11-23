using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using APIDataAccess.Internal.DataAccess;
using APIDataAccess.DataAccess;
using System;
using System.Collections.Generic;
using APIDataAccess.Models.NotificationSetting;

namespace APIDataAccess.Tests
{
    [TestClass]
    public class NotificationSettingControllerTests
    {

        #region create
        [TestMethod]
        public void Create_Call()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            var input = new CreateNotificationSettingModel()
            {
                OwnerID = "user",
                ThresHold = 32
            };

            var notificationSettingAccess = new NotificationSettingAccess(sql);
            notificationSettingAccess.Create(input);

            sql.Received(1).SaveData(SpCommands.spNotificationSetting_Create.ToString(), Arg.Is<CreateNotificationSettingModel>((x)=>
                x.ThresHold == 32 &&
                x.OwnerID.Equals("user")
            ), "DDB");
        }

        #endregion

        #region get

        [TestMethod]
        public void Get_Exists()
        {
            int expected = 1;

            var sql = Substitute.For<ISqlDataAccess>();

            sql.LoadData<NotificationSettingModel, dynamic>(SpCommands.spNotificationSetting_GetByUser.ToString(), Arg.Any<object>(), "DDB").
                Returns(new List<NotificationSettingModel>() { new NotificationSettingModel()});

            var data = new NotificationSettingAccess(sql);
            var res = data.Get("").Count;

            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        public void Get_TwoModels()
        {
            int expected = 2;

            var sql = Substitute.For<ISqlDataAccess>();

            sql.LoadData<NotificationSettingModel, dynamic>(SpCommands.spNotificationSetting_GetByUser.ToString(), Arg.Any<object>(), "DDB").
                Returns(new List<NotificationSettingModel>() { new NotificationSettingModel() , new NotificationSettingModel() });

            var data = new NotificationSettingAccess(sql);
            var res = data.Get("").Count;

            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        public void Get_NotExists()
        {
            int expected = 0;

            var sql = Substitute.For<ISqlDataAccess>();

            sql.LoadData<NotificationSettingModel, dynamic>("spNotificationSetting_GetByUser", Arg.Any<object>(), "DDB").
                Returns(new List<NotificationSettingModel>());

            var data = new NotificationSettingAccess(sql);
            var res = data.Get("").Count;

            Assert.AreEqual(expected, res);
        }
        #endregion 

        #region update

        [TestMethod]
        public void Update_Calls()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            var updateNotificationSettingModel = new UpdateNotificationSettingModel(){
                ID = 42,
                Note = "test"
            };

            var notificationSettingAccess = new NotificationSettingAccess(sql);
            notificationSettingAccess.Update(updateNotificationSettingModel);


            sql.Received(1).SaveData(SpCommands.spNotificationSetting_Update.ToString(), Arg.Is<UpdateNotificationSettingModel>((x) =>
            x.ID == 42 && x.Note.Equals("test")
            ), "DDB");
        }

        #endregion

        #region deleteByUserId

        [TestMethod]
        public void DeleteByUserId_Calls()
        {
            var sql = Substitute.For<ISqlDataAccess>();

            var notificationSettingAccess = new NotificationSettingAccess(sql);
            notificationSettingAccess.DeleteByUserId("user");

            sql.Received(1).DeleteData(SpCommands.spNotificationSetting_DeleteByUserId.ToString(), Arg.Any<string>(), "DDB");
            
        }

        #endregion



    }
}
