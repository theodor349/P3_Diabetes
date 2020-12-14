using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using APIHandler.Handlers;
using API.Controllers;
using APIDataAccess.Models.NotificationSetting;
using API.Models;

namespace API.Tests
{
    [TestClass]
    public class NotificationSettingControllerTests
    {
        #region Get

        [TestMethod]
        public void Get_Calls()
        {
            var notificationSettingHandler = Substitute.For<INotificationSettingHandler>();
            NotificationSettingController notificationSettingController = new NotificationSettingController(notificationSettingHandler);

            StringValue userID = new StringValue {
                Value = "userID"
            };

            notificationSettingController.Get(userID);

            notificationSettingHandler.Received(1).Get(Arg.Any<string>());
        }

        #endregion

        #region Update

        [TestMethod]
        public void Update_Pass()
        {
            var notificationSettingHandler = Substitute.For<INotificationSettingHandler>();
            NotificationSettingController notificationSettingController = new NotificationSettingController(notificationSettingHandler);
            UpdateNotificationSettingModel updateNotificationSettingModel = new UpdateNotificationSettingModel("testNote");

            notificationSettingController.Update(updateNotificationSettingModel);

            notificationSettingHandler.Received(1).Update(Arg.Is<UpdateNotificationSettingModel>(x => x.Note.Equals("testNote")));
        }

        [TestMethod]
        public void Update_NotPass()
        {
            var notificationSettingHandler = Substitute.For<INotificationSettingHandler>();
            NotificationSettingController notificationSettingController = new NotificationSettingController(notificationSettingHandler);
            UpdateNotificationSettingModel updateNotificationSettingModel = new UpdateNotificationSettingModel();

            notificationSettingController.Update(updateNotificationSettingModel);

            notificationSettingHandler.Received(0).Update(Arg.Any<UpdateNotificationSettingModel>());
        }

        #endregion
    }
}
