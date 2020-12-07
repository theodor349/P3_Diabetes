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
        #region Get
        // Exists
        [DataRow("name")]
        [DataRow("name2")]
        [TestMethod]
        public void Get_Exists(string expected)
        {
            var input = "exists";
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<AccountDBModel, dynamic>(SpCommands.spAccount_Get.ToString(), Arg.Any<object>(), "DDB").
                Returns(new List<AccountDBModel>() { new AccountDBModel() { FirstName = expected, Email = expected, PhoneNumber = expected } });

            var data = new AccountAccess(sql);
            var res = data.Get(input);

            Assert.AreEqual(expected, res.FirstName);
            Assert.AreEqual(expected, res.Email);
            Assert.AreEqual(expected, res.PhoneNumber);
        }

        // Does not exist 
        [TestMethod]
        public void Get_NotExisting()
        {
            var input = "exists";
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<AccountDBModel, dynamic>(SpCommands.spAccount_Get.ToString(), Arg.Any<object>(), "DDB").
                Returns(new List<AccountDBModel>() { new AccountDBModel() { FirstName = null } });

            var data = new AccountAccess(sql);
            var res = data.Get(input);

            Assert.AreEqual(null, res);

        }
        #endregion

        #region GetByPhoneNumber

        [TestMethod]
        public void GetByPhoneNumber_Exists()
        {
            var input = "exists";
            var expected = "Name";
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<AccountDBModel, dynamic>(SpCommands.spAccount_GetByPhoneNumber.ToString(), Arg.Any<object>(), "DDB").
                Returns(new List<AccountDBModel>() { new AccountDBModel() { FirstName = expected, Email = expected, PhoneNumber = expected } });

            var data = new AccountAccess(sql);
            var res = data.GetByPhoneNumber(input);

            Assert.AreEqual(expected, res.FirstName);
            Assert.AreEqual(expected, res.Email);
            Assert.AreEqual(expected, res.PhoneNumber);
        }

        [TestMethod]
        public void GetByPhoneNumber_NotExisting()
        {
            var input = "exists";
            var sql = Substitute.For<ISqlDataAccess>();
            sql.LoadData<AccountDBModel, dynamic>(SpCommands.spAccount_GetByPhoneNumber.ToString(), Arg.Any<object>(), "DDB").
                Returns(new List<AccountDBModel>() { new AccountDBModel() { FirstName = null } });

            var data = new AccountAccess(sql);
            var res = data.GetByPhoneNumber(input);

            Assert.AreEqual(null, res);
        }

        #endregion

        #region CreateAccount

        [TestMethod]
        public void CreateAccount_Call()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            var data = new AccountAccess(sql);
            var input = new CreateAccountDBModel()
            {
                FirstName = "FirstName",
                LastName = "LastName"
            };

            data.CreateAccountOnDB(input);
            sql.Received(1).SaveData(SpCommands.spAccount_CreateAccount.ToString(), Arg.Is<CreateAccountDBModel>((x) =>
            x.FirstName.Equals("FirstName") &&
            x.LastName.Equals("LastName")
            ), "DDB");
        }

        #endregion

        #region DeleteAccount

        [TestMethod]
        public void DeleteAccount_Call()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            var data = new AccountAccess(sql);
            data.DeleteAccount("id");

            sql.Received(1).DeleteData(SpCommands.spAccount_DeleteAccount.ToString(), Arg.Any<string>(), "DDB");
        }

        #endregion

        #region UpdateAccount

        [TestMethod]
        public void UpdateAccount_Call()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            var data = new AccountAccess(sql);
            var input = new UpdateAccountDBModel()
            {
                Id = "idstring",
                FirstName = "FirstName",
                LastName = "LastName"
            };

            data.UpdateAccount(input);

            sql.Received(1).SaveData(SpCommands.spAccount_UpdateAccount.ToString(), Arg.Is<UpdateAccountDBModel>((x) =>
            x.Id.Equals("idstring") &&
            x.FirstName.Equals("FirstName") &&
            x.LastName.Equals("LastName")
            ), "DDB");
        }

        #endregion
    }
}
