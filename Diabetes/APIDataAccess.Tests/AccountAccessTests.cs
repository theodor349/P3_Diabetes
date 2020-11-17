using APIDataAccess.DataAccess;
using APIDataAccess.Internal.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using APIDataAccess.Models.Account;

namespace APIDataAccess.Tests
{
    [TestClass]
    public class AccountAccessTests
    {
        #region Get
        // Exists
        [DataRow("name")]
        [DataRow("name2")]
        [TestMethod]
        public void Get_Exists(string expected)
        {
            var userStore = Substitute.For<IUserStore<IdentityUser>>();
            var userManager = Substitute.For<UserManager<IdentityUser>>(userStore);
            var sql = Substitute.For<ISqlDataAccess>();
            var input = "exists";
            // Imposible (Cannot overwrite methods if they are not Abstract or Virtual)
            //userManager.GetEmail<IdentityUser, string>(input).Returns(expected);
            //userManager.GetPhoneNumber(input).Returns(expected);
            sql.LoadData<AccountDBModel, dynamic>("spAccount_Get", Arg.Any<object>(), "DDB").
                Returns(new List<AccountDBModel>() { new AccountDBModel(){ FirstName = expected } });
            
            var data = new AccountAccess(sql, userManager);
            var res = data.Get(input);

            Assert.AreEqual(expected, res.FirstName);
        }

        // Does not exist 
        #endregion
    }
}
