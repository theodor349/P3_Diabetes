using APIDataAccess.Internal.DataAccess;
using APIDataAccess.Models.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using APIDataAccess.DataAccess;

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

        public async Task<AccountModel> Get(string id)
        {
            var model = _sql.LoadData<AccountDBModel, dynamic>(SpCommands.spAccount_Get.ToString(), new { id }, "DDB").FirstOrDefault();
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return null;
            else
                return new AccountModel()
                {
                    ID = model.ID,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    NSLink = model.NSLink,
                    IsEUMeasure = model.IsEUMeasure
                };
        }

        public async Task<AccountModel> GetByPhoneNumber(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public void CreateAccount(AccountDBModel model)
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
