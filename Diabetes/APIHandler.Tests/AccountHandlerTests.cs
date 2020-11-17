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
        [DataRow("expected", true)]
        [DataRow("expected2", false)]
        [TestMethod]
        public void RegisterAccount_CallsCreate(string exStr, bool exBool)
        {
            var aa = Substitute.For<IAccountAccess>();
            var nh = Substitute.For<INotificationSettingHandler>();
            var ph = Substitute.For<IPermissionHandler>();
            var input = new CreateAccountModel(exStr, exStr, exStr, exStr, exStr, exStr, exBool);

            var data = new AccountHandler(aa, nh, ph);
            data.RegisterAccount(input);

            aa.Received(1).CreateAccount(Arg.Is<AccountDBModel>(x =>
                x.FirstName.Equals(input.FirstName) &&
                x.LastName.Equals(input.LastName) &&
                x.NSLink.Equals(input.NSLink) &&
                x.IsEUMeasure.Equals(input.IsEUMeasure)
            ));
        }
    }
}
