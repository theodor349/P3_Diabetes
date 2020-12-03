using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using APIHandler.Handlers;
using API.Controllers;
using Microsoft.AspNetCore.Identity;
using Moq;
using Microsoft.AspNetCore.Http;
using APIDataAccess.Models.Account;
using APIDataAccess.DataAccess;

namespace API.Tests
{
    [TestClass]
    public class AccountControllerTests
    {
        #region Get

        [TestMethod]
        public void Get_Calls()
        {
            var accountHandler = Substitute.For<IAccountHandler>();
            var store = new Mock<IUserStore<IdentityUser>>();
            var userManager = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipal = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            var signInManager = new SignInManager<IdentityUser>(userManager, contextAccessor.Object, userPrincipal.Object, null, null, null, null);

            AccountController accountController = new AccountController(userManager, signInManager, accountHandler);

            accountController.Get("UserId");

            accountHandler.Received(1).Get(Arg.Any<string>());
        }
        
        #endregion

        #region Register

        #endregion

        #region Update

        [TestMethod]
        public void Update_Pass()
        {
            var accountHandler = Substitute.For<IAccountHandler>();
            var store = new Mock<IUserStore<IdentityUser>>();
            var userManager = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipal = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            var signInManager = new SignInManager<IdentityUser>(userManager, contextAccessor.Object, userPrincipal.Object, null, null, null, null);

            AccountController accountController = new AccountController(userManager, signInManager, accountHandler);

            UpdateAccountDBModel updateAccountDBModel = new UpdateAccountDBModel("test1", "test2", "test3", "test4");

            accountController.Update(updateAccountDBModel);

            accountHandler.Received(1).UpdateAccount(Arg.Any<UpdateAccountDBModel>());
        }

        [TestMethod]
        public void Update_NotPass()
        {
            var accountHandler = Substitute.For<IAccountHandler>();
            var store = new Mock<IUserStore<IdentityUser>>();
            var userManager = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipal = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            var signInManager = new SignInManager<IdentityUser>(userManager, contextAccessor.Object, userPrincipal.Object, null, null, null, null);

            AccountController accountController = new AccountController(userManager, signInManager, accountHandler);

            UpdateAccountDBModel updateAccountDBModel = new UpdateAccountDBModel();

            accountController.Update(updateAccountDBModel);

            accountHandler.Received(0).UpdateAccount(Arg.Any<UpdateAccountDBModel>());
        }

        #endregion

        #region Unregister

        //[TestMethod]
        //public void Unregistre_Calls()
        //{
        //    var accountHandler = Substitute.For<IAccountHandler>();
        //    var store = new Mock<IUserStore<IdentityUser>>();
        //    var userManager = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);
        //    var contextAccessor = new Mock<IHttpContextAccessor>();
        //    var userPrincipal = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
        //    var signInManager = new SignInManager<IdentityUser>(userManager, contextAccessor.Object, userPrincipal.Object, null, null, null, null);

        //    AccountController accountController = new AccountController(userManager, signInManager, accountHandler);

        //    accountController.UnRegistre("UserId");

        //    accountHandler.Received(1).UnregisterAccount(Arg.Any<string>());
        //}

        #endregion

        #region GetByPhoneNumber

        [TestMethod]
        public void GetByPhoneNumber_Call()
        {
            var accountHandler = Substitute.For<IAccountHandler>();
            var store = new Mock<IUserStore<IdentityUser>>();
            var userManager = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipal = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            var signInManager = new SignInManager<IdentityUser>(userManager, contextAccessor.Object, userPrincipal.Object, null, null, null, null);

            AccountController accountController = new AccountController(userManager, signInManager, accountHandler);

            accountController.GetByPhoneNumber("phoneNumber");

            accountHandler.Received(1).GetByPhoneNumber(Arg.Any<string>());
        }

        #endregion

        #region EmailExists

        [DataRow(true)]
        [DataRow(false)]
        [TestMethod]
        public void EmailExists_Test(bool output)
        {
            var accountHandler = Substitute.For<IAccountHandler>();
            var store = new Mock<IUserStore<IdentityUser>>();
            var userManager = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipal = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            var signInManager = new SignInManager<IdentityUser>(userManager, contextAccessor.Object, userPrincipal.Object, null, null, null, null);

            accountHandler.EmailExists("Email").Returns(output);

            AccountController accountController = new AccountController(userManager, signInManager, accountHandler);

            bool expected = output;
            bool res = accountController.EmailExists("Email");

            accountHandler.Received(1).EmailExists(Arg.Any<string>());
            Assert.AreEqual(expected, res);
        }

        #endregion

        #region PhoneNumberExists

        #endregion

        #region GetNightScoutLink

        #endregion

        #region GetUnitOfMeasurement

        #endregion

        #region UpdateUnitOfMeasurement

        #endregion
    }
}
