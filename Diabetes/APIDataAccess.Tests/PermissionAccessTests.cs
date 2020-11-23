using APIDataAccess.DataAccess;
using APIDataAccess.Internal.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIDataAccess.Tests
{
    [TestClass]
    class PermissionAccessTests
    {
        [TestMethod]
        public void Get_Exists()
        {
            var sql = Substitute.For<ISqlDataAccess>();
            var data = new PermissionAccess(sql);
        }

    }
}
