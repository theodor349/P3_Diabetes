using APIDataAccess.Internal.DataAccess;
using APIDataAccess.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APIDataAccess.DataAccess
{
    public class AccountAccess : IAccountAccess
    {
        private readonly ISqlDataAccess _sql;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountAccess(ISqlDataAccess sql, UserManager<IdentityUser> userManager)
        {
            _sql = sql;
            _userManager = userManager;
        }

        public AccountModel Get(string id)
        {
            var model = _sql.LoadData<AccountDBModel, dynamic>("spAccount_Get", new { id }, "DDB").FirstOrDefault();
            var email = _userManager.GetEmail(id);
            var phone = _userManager.GetPhoneNumber(id);

            return new AccountModel()
            {
                ID = model.ID,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = email,
                PhoneNumber = phone,
                NSLink = model.NSLink,
                IsEUMeasure = model.IsEUMeasure
            };
        }

        public AccountModel GetByPhoneNumber(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public void CreateAccount(AccountModel model)
        {
            throw new NotImplementedException();
        }

        public void DeleteAccount(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAccount(UpdateAccountModel model)
        {
            throw new NotImplementedException();
        }

        public void UpdateNighScoutLink(string id, string nsLink)
        {
            throw new NotImplementedException();
        }

        public bool PhoneNumberExists(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public bool EmailExists(string email)
        {
            throw new NotImplementedException();
        }

        public bool GetUnitOfMeasure(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateUnitOfMeasure(string id, bool value)
        {
            throw new NotImplementedException();
        }
    }
}
