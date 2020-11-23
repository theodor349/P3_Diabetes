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

        #endregion

        #region get

        [TestMethod]
        public void Get_Exists()
        {
            int expected = 1;

            var sql = Substitute.For<ISqlDataAccess>();

            sql.LoadData<NotificationSettingModel, dynamic>("spNotificationSetting_GetByUser", Arg.Any<object>(), "DDB").
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

            sql.LoadData<NotificationSettingModel, dynamic>("spNotificationSetting_GetByUser", Arg.Any<object>(), "DDB").
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

        #endregion

        #region deleteByUserId

        #endregion



    }
}
