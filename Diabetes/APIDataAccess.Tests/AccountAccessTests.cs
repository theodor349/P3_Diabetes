using APIDataAccess.DataAccess;
using APIDataAccess.Internal.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using APIDataAccess.Models.Account;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Moq;
using System.Threading;

namespace APIDataAccess.Tests
{
    [TestClass]
    public class AccountAccessTests
    {
        private UserManager<IdentityUser> GetUserManager(string input, string expected)
        {
            var store = new Mock<IUserStore<IdentityUser>>();
            store.Setup(x => x.FindByIdAsync(input, CancellationToken.None))
            .ReturnsAsync(new IdentityUser()
            {
                UserName = expected,
                Id = expected,
                Email = expected,
                PhoneNumber = expected
            });
            var userManager = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);
            return userManager;
        }
        private UserManager<IdentityUser> GetEmptyUserManager()
        {
            var store = new Mock<IUserStore<IdentityUser>>();
            var userManager = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);
            return userManager;
        }

        #region Get
        // Exists
        [DataRow("name")]
        [DataRow("name2")]
        [TestMethod]
        public async Task Get_Exists(string expected)
        {
            var input = "exists";
            var userManager = GetUserManager(input, expected);
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<AccountDBModel, dynamic>("spAccount_Get", Arg.Any<object>(), "DDB").
                Returns(new List<AccountDBModel>() { new AccountDBModel(){ FirstName = expected } });

            var data = new AccountAccess(sql, userManager);
            var res = await data.Get(input);

            Assert.AreEqual(expected, res.FirstName);
            Assert.AreEqual(expected, res.Email);
            Assert.AreEqual(expected, res.PhoneNumber);
        }

        // Does not exist 
        [TestMethod]
        public async Task Get_NotExisting()
        {
            var input = "doesnotexist";
            var userManager = GetEmptyUserManager();
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<AccountDBModel, dynamic>("spAccount_Get", Arg.Any<object>(), "DDB").
                Returns(new List<AccountDBModel>());

            var data = new AccountAccess(sql, userManager);
            var res = await data.Get(input);

            Assert.AreEqual(null, res);
        }
        #endregion
    }
}
