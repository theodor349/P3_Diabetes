﻿using APIDataAccess.Internal.DataAccess;
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
            if (model.FirstName == null)
            {
                return null;
            }
            return GenerateAccountModel(model);
        }

        public AccountDBModel GetByPhoneNumber(string phoneNumber)
        {
            var model = _sql.LoadData<AccountDBModel, dynamic>(SpCommands.spAccount_GetByPhoneNumber.ToString(), new { phoneNumber }, "DDB").FirstOrDefault();
            if (model.FirstName == null)
            {
                return null;
            }
            return GenerateAccountModel(model);
        }

        private AccountDBModel GenerateAccountModel(AccountDBModel model)
        {
            return new AccountDBModel()
            {
                ID = model.ID,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                NSLink = model.NSLink,
                IsEUMeasure = model.IsEUMeasure
            };
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
