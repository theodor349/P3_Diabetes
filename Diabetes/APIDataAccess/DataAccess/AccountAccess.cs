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

        public AccountAccess(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public AccountDBModel Get(string id)
        {
            var model = _sql.LoadData<AccountDBModel, dynamic>(SpCommands.spAccount_Get.ToString(), new { id }, "DDB").FirstOrDefault();
            return model;
        }

        public AccountDBModel GetByPhoneNumber(string phoneNumber)
        {
            var model = _sql.LoadData<AccountDBModel, dynamic>(SpCommands.spAccount_GetByPhoneNumber.ToString(), new { PhoneNumber = phoneNumber }, "DDB").FirstOrDefault();
            return model;
        }

        public void CreateAccountOnDB(CreateAccountDBModel model)
        {
            _sql.SaveData(SpCommands.spAccount_CreateAccount.ToString(), model, "DDB");
        }

        public void DeleteAccount(string id)
        {
            _sql.DeleteData(SpCommands.spAccount_DeleteAccount.ToString(), id, "DDB");
        }

        public void UpdateAccount(UpdateAccountDBModel model)
        {
            _sql.SaveData(SpCommands.spAccount_UpdateAccount.ToString(), model, "DDB");
        }

        public void UpdateNighScoutLink(UpdateNightScoutLinkModel model)
        {
            _sql.SaveData(SpCommands.spAccount_UpdateNightScoutLink.ToString(), model, "DDB");
        }

        public bool PhoneNumberExists(string phoneNumber)
        {
            var model = _sql.LoadData<AccountDBModel, dynamic>(SpCommands.spAccount_GetByPhoneNumber.ToString(), new { phoneNumber }, "DDB").FirstOrDefault();
            if (model == null)
                return false;
            else
                return true;
        }

        public bool EmailExists(string email)
        {
            var model = _sql.LoadData<AccountDBModel, dynamic>(SpCommands.spAccount_GetByEmail.ToString(), new { Email = email }, "DDB").FirstOrDefault();
            if (model == null)
                return false;
            else
                return true;
        }

        public bool GetUnitOfMeasure(string id)
        {
            var model = Get(id);
            return model.IsEUMeasure;
        }

        public void UpdateUnitOfMeasure(UpdateUnitOfMesureModel model)
        {
            _sql.SaveData(SpCommands.spAccount_UpdateUnitOfMeasure.ToString(), model, "DDB");
        }

        public AccountNameModel GetName(string id)
        {
            return _sql.LoadData<AccountNameModel, dynamic>(SpCommands.spAccount_GetName.ToString(), new { Id = id }, "DDB").FirstOrDefault();
        }

        public string GetNightscoutLink(string id) {
            return _sql.LoadData<AccountNameModel, dynamic>(SpCommands.spAccount_GetName.ToString(), new { Id = id }, "DDB").FirstOrDefault();
        }
    }
}
