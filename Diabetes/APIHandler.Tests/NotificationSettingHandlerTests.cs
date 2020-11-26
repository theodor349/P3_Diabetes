using APIDataAccess.DataAccess;
using APIHandler.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using APIHandler.Utils;
using APIDataAccess.Models.NotificationSetting;


namespace APIHandler.Tests
{
    [TestClass]
    public class NotificationSettingHandlerTests
    {
        #region CreateStandardSettings

        [TestMethod]
        public void CreateStandard_Calls()
        {
            var config = Substitute.For<IConfiguration>();
            var  notificationSettingDataAccesser = Substitute.For<INotificationSettingAccess>();
            var util = Substitute.For<NotificationsSettingsUtils>(config);
            util.GetDefaultValue("High", "Threshold").Returns("8");
            util.GetDefaultValue("High", "ThresholdType").Returns("High");
            util.GetDefaultValue("High", "NotificationType").Returns("Warning");
            util.GetDefaultValue("High", "Note").Returns("If blood sugar is high, ask patient wether he has taken some insulin. If blood sugar continues to be high, then help patient measure ketones.");
            util.GetDefaultValue("Low", "Threshold").Returns("4");
            util.GetDefaultValue("Low", "ThresholdType").Returns("Low");
            util.GetDefaultValue("Low", "NotificationType").Returns("Warning");
            util.GetDefaultValue("Low", "Note").Returns("If blood sugar is low, ask patient if they has eaten or drinken something with fast carbohydrates. If blood sugar gets(Around 2.2 or lower) keep a good eye on patient.");

            NotificationSettingHandler notificationSettingHandler = new NotificationSettingHandler(notificationSettingDataAccesser, util);

            notificationSettingHandler.CreateStandardSettings("");

            notificationSettingDataAccesser.Received(2).Create(Arg.Any<CreateNotificationSettingModel>());
        }
        #endregion

        #region DeleteByUserId

        [TestMethod]
        public void DeleteByUserId_Calls()
        {
            var config = Substitute.For<IConfiguration>();
            var util = Substitute.For<NotificationsSettingsUtils>(config);
            var notificationSettingDataAccesser = Substitute.For<INotificationSettingAccess>();
            NotificationSettingHandler notificationSettingHandler = new NotificationSettingHandler(notificationSettingDataAccesser, util);

            notificationSettingHandler.DeleteByUserId("userId");

            notificationSettingDataAccesser.Received(1).DeleteByUserId(Arg.Any<string>());
        }

        #endregion

        #region Get

        [TestMethod]
        public void Get_Calls()
        {
            var config = Substitute.For<IConfiguration>();
            var util = Substitute.For<NotificationsSettingsUtils>(config);
            var notificationSettingDataAccesser = Substitute.For<INotificationSettingAccess>();
            NotificationSettingHandler notificationSettingHandler = new NotificationSettingHandler(notificationSettingDataAccesser, util);

            notificationSettingHandler.Get("userId");

            notificationSettingDataAccesser.Received(1).Get(Arg.Any<string>());
        }

        #endregion

        #region Update
        [TestMethod]
        public void Update_Calls()
        {
            var config = Substitute.For<IConfiguration>();
            var util = Substitute.For<NotificationsSettingsUtils>(config);
            var notificationSettingDataAccesser = Substitute.For<INotificationSettingAccess>();
            NotificationSettingHandler notificationSettingHandler = new NotificationSettingHandler(notificationSettingDataAccesser, util);
            UpdateNotificationSettingModel updateNotificationSettingModel = new UpdateNotificationSettingModel();

            notificationSettingHandler.Update(updateNotificationSettingModel);

            notificationSettingDataAccesser.Received(1).Update(Arg.Any<UpdateNotificationSettingModel>());
        }

        #endregion

    }
}
