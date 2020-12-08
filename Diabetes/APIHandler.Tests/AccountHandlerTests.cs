using APIDataAccess.DataAccess;
using APIDataAccess.Models.Account;
using APIHandler.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Identity;
using Moq;
using NSubstitute;
using Microsoft.AspNetCore.Http;
using APIHandler.Models;
using System.Threading.Tasks;

namespace APIHandler.Tests
{
    [TestClass]
    public class AccountHandlerTests
    {
        #region Register
        //[TestMethod]
        //public async Task Register_Calls()
        //{
        //    var userId = "userId";

        //    var aa = Substitute.For<IAccountAccess>();
        //    var nh = Substitute.For<INotificationSettingHandler>();
        //    var ph = Substitute.For<IPermissionHandler>();
        //    var store = new Mock<IUserStore<IdentityUser>>();
        //    var userManager = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);
        //    var contextAccessor = new Mock<IHttpContextAccessor>();
        //    var userPrincipal = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
        //    var signInManager = new SignInManager<IdentityUser>(userManager, contextAccessor.Object, userPrincipal.Object, null, null, null);

        //    AccountHandler data = new AccountHandler(aa, nh, ph, userManager, signInManager);
        //    var input = new InputCreateAccountModel()
        //    {
        //        FirstName = "Test",
        //        LastName = "Test",
        //        Email = "Test@test.com",
        //        Password = "test12345",
        //        NSLink = "test.com",
        //        IsEUMeasure = true,
        //        PhoneNumber = "12345678",
        //    };

        //    var res = await data.RegisterAccount(input);

        //    Assert.AreEqual(true, res);
        //    nh.Received(1).CreateStandardSettings(userId);
        //    ph.Received(1).CreatePermanent(userId);
        //}
        #endregion


        #region Get
        [TestMethod]
        public void Get_Exists()
        {
            var aa = Substitute.For<IAccountAccess>();
            var nh = Substitute.For<INotificationSettingHandler>();
            var ph = Substitute.For<IPermissionHandler>();
            AccountHandler accountHandler = new AccountHandler(aa,nh, ph, null, null);

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
            AccountHandler accountHandler = new AccountHandler(aa, nh, ph, null, null);

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
            AccountHandler accountHandler = new AccountHandler(aa, nh, ph, null, null);

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
            AccountHandler accountHandler = new AccountHandler(aa, nh, ph, null, null);

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
