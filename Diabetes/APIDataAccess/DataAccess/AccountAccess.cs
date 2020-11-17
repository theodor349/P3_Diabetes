using APIDataAccess.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIDataAccess.DataAccess
{
    public class AccountAccess
    {
        public AccountModel Get(string id)
        {
            throw new NotImplementedException();
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
