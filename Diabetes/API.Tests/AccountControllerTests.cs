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
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

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
            var relayHandler = Substitute.For<IRelayHandler>();
            var store = new Mock<IUserStore<IdentityUser>>();
            var userManager = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipal = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            var signInManager = new SignInManager<IdentityUser>(userManager, contextAccessor.Object, userPrincipal.Object, null, null, null, null);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, "example name"),
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim("custom-claim", "example claim value"),
            }, "mock"));

            AccountController accountController = new AccountController(userManager, signInManager, accountHandler, relayHandler);
            accountController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };


            accountController.Get();

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
            var relayHandler = Substitute.For<IRelayHandler>();
            var store = new Mock<IUserStore<IdentityUser>>();
            var userManager = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipal = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            var signInManager = new SignInManager<IdentityUser>(userManager, contextAccessor.Object, userPrincipal.Object, null, null, null, null);

            AccountController accountController = new AccountController(userManager, signInManager, accountHandler, relayHandler);

            UpdateAccountDBModel updateAccountDBModel = new UpdateAccountDBModel("test1", "test2", "test3", "test4");
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, "example name"),
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim("custom-claim", "example claim value"),
            }, "mock"));

            accountController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            accountController.Update(updateAccountDBModel);

            accountHandler.Received(1).UpdateAccount(Arg.Any<UpdateAccountDBModel>());
        }

        [TestMethod]
        public void Update_NotPass()
        {
            var accountHandler = Substitute.For<IAccountHandler>();
            var relayHandler = Substitute.For<IRelayHandler>();
            var store = new Mock<IUserStore<IdentityUser>>();
            var userManager = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipal = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            var signInManager = new SignInManager<IdentityUser>(userManager, contextAccessor.Object, userPrincipal.Object, null, null, null, null);

            AccountController accountController = new AccountController(userManager, signInManager, accountHandler, relayHandler);

            UpdateAccountDBModel updateAccountDBModel = new UpdateAccountDBModel();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, "example name"),
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim("custom-claim", "example claim value"),
            }, "mock"));

            accountController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            accountController.Update(updateAccountDBModel);

            accountHandler.Received(0).UpdateAccount(Arg.Is<UpdateAccountDBModel>(x => (x.Id.Equals("testId"))));
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
            var relayHandler = Substitute.For<IRelayHandler>();
            var store = new Mock<IUserStore<IdentityUser>>();
            var userManager = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipal = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            var signInManager = new SignInManager<IdentityUser>(userManager, contextAccessor.Object, userPrincipal.Object, null, null, null, null);

            AccountController accountController = new AccountController(userManager, signInManager, accountHandler, relayHandler);

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
            var relayHandler = Substitute.For<IRelayHandler>();
            var store = new Mock<IUserStore<IdentityUser>>();
            var userManager = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipal = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            var signInManager = new SignInManager<IdentityUser>(userManager, contextAccessor.Object, userPrincipal.Object, null, null, null, null);

            accountHandler.EmailExists("Email").Returns(output);

            AccountController accountController = new AccountController(userManager, signInManager, accountHandler, relayHandler);

            bool expected = output;
            bool res = accountController.EmailExists(new Models.StringValue() { Value = "Email" });

            accountHandler.Received(1).EmailExists(Arg.Any<string>());
            Assert.AreEqual(expected, res);
        }

        #endregion

        #region UpdateNightScoutLink

        [TestMethod]
        public void UpdateNightScoutLink_Valid()
        {
            var accountHandler = Substitute.For<IAccountHandler>();
            var relayHandler = Substitute.For<IRelayHandler>();
            var store = new Mock<IUserStore<IdentityUser>>();
            var userManager = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipal = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            var signInManager = new SignInManager<IdentityUser>(userManager, contextAccessor.Object, userPrincipal.Object, null, null, null, null);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, "example name"),
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim("custom-claim", "example claim value"),
            }, "mock"));

            AccountController accountController = new AccountController(userManager, signInManager, accountHandler, relayHandler);
            accountController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            relayHandler.ConnectionOk("url").Returns(true);

            var res = accountController.UpdateNightScoutLink(new Models.StringValue() { Value = "url" });

            Assert.AreEqual(typeof(OkResult), res.GetType());
        }

        [TestMethod]
        public void UpdateNightScoutLink_Invalid()
        {
            var accountHandler = Substitute.For<IAccountHandler>();
            var relayHandler = Substitute.For<IRelayHandler>();
            var store = new Mock<IUserStore<IdentityUser>>();
            var userManager = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipal = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            var signInManager = new SignInManager<IdentityUser>(userManager, contextAccessor.Object, userPrincipal.Object, null, null, null, null);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, "example name"),
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim("custom-claim", "example claim value"),
            }, "mock"));

            AccountController accountController = new AccountController(userManager, signInManager, accountHandler, relayHandler);
            accountController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            relayHandler.ConnectionOk("url").Returns(false);

            var res = accountController.UpdateNightScoutLink(new Models.StringValue() { Value = "url" });

            Assert.AreEqual(typeof(BadRequestResult), res.GetType());
        }


        #endregion
    }
}
