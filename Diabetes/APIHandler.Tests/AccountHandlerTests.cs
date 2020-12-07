using APIDataAccess.DataAccess;
using APIDataAccess.Models.Account;
using APIHandler.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHandler.Tests
{
    [TestClass]
    public class AccountHandlerTests
    {
        //[DataRow("expected", true)]
        //[DataRow("expected2", false)]
        //[TestMethod]
        //public void RegisterAccount_CallsCreate(string exStr, bool exBool)
        //{
        //    var aa = Substitute.For<IAccountAccess>();
        //    var nh = Substitute.For<INotificationSettingHandler>();
        //    var ph = Substitute.For<IPermissionHandler>();
        //    var input = new CreateAccountModel(exStr, exStr, exStr, exStr, exStr, exStr, exBool);

        //    var data = new AccountHandler(aa, nh, ph);
        //    data.RegisterAccount(input);

        //    aa.Received(1).CreateAccount(Arg.Is<AccountDBModel>(x =>
        //        x.FirstName.Equals(input.FirstName) &&
        //        x.LastName.Equals(input.LastName) &&
        //        x.NSLink.Equals(input.NSLink) &&
        //        x.IsEUMeasure.Equals(input.IsEUMeasure)
        //    ));
        //}
        #region Get

        [TestMethod]
        public void Get_Exists()
        {
            var aa = Substitute.For<IAccountAccess>();
            var nh = Substitute.For<INotificationSettingHandler>();
            var ph = Substitute.For<IPermissionHandler>();
            AccountHandler accountHandler = new AccountHandler(aa,nh, ph);

            aa.Get("id").Returns(new AccountDBModel() {Id = "id"});

            AccountDBModel accountDBModel = accountHandler.Get("id");
            Assert.AreEqual("id", accountDBModel.Id);
        }

        [TestMethod]
        public void Get_NotExists()
        {
            var aa = Substitute.For<IAccountAccess>();
            var nh = Substitute.For<INotificationSettingHandler>();
            var ph = Substitute.For<IPermissionHandler>();
            AccountHandler accountHandler = new AccountHandler(aa, nh, ph);

            AccountDBModel accountDBModel = accountHandler.Get("id");
            Assert.AreEqual(null, accountDBModel);
        }

        #endregion

        #region GetByPhoneNumber

        [TestMethod]
        public void GetByPhoneNumber_Exists()
        {
            var aa = Substitute.For<IAccountAccess>();
            var nh = Substitute.For<INotificationSettingHandler>();
            var ph = Substitute.For<IPermissionHandler>();
            AccountHandler accountHandler = new AccountHandler(aa, nh, ph);

            aa.GetByPhoneNumber("phoneNumber").Returns(new AccountDBModel() { PhoneNumber = "phoneNumber" });

            AccountDBModel accountDBModel = accountHandler.GetByPhoneNumber("phoneNumber");
            Assert.AreEqual("phoneNumber", accountDBModel.PhoneNumber);
        }

        [TestMethod]
        public void GetByPhoneNumber_NotExists()
        {
            var aa = Substitute.For<IAccountAccess>();
            var nh = Substitute.For<INotificationSettingHandler>();
            var ph = Substitute.For<IPermissionHandler>();
            AccountHandler accountHandler = new AccountHandler(aa, nh, ph);

            AccountDBModel accountDBModel = accountHandler.GetByPhoneNumber("phoneNumber");
            Assert.AreEqual(null, accountDBModel);
        }

        #endregion

        #region RegisterAccount

        #endregion

        #region UnregisterAccount

        #endregion

        #region EmailExists

        #endregion

        #region PhoneNumberExists

        #endregion

        #region GetUnitOfMeasurement

        #endregion

        #region UpdateUnitOfMeasurement

        #endregion

        #region CreateAccount

        #endregion

        #region DeleteAccount

        #endregion
    }

}
