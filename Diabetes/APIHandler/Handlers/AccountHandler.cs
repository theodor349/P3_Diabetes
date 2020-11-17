using APIDataAccess.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// The handler will only recive valid data
/// It is the Function layer
/// </summary>
namespace APIHandler.Handlers
{
    public class AccountHandler : IAccountHandler
    {
        public AccountModel Get(string id)
        {
            throw new NotImplementedException();
        }

        public AccountModel GetByPhoneNumber(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public void RegisterAccount(CreateAccountModel model)
        {
            throw new NotImplementedException();
        }

        public void UnregisterAccount(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAccount(CreateAccountModel model)
        {
            throw new NotImplementedException();
        }

        public void UpdateNightScoutLink(string id, string link)
        {
            throw new NotImplementedException();
        }

        public bool EmailExists(string email)
        {
            throw new NotImplementedException();
        }

        public bool PhoneNumberExists(string phone)
        {
            throw new NotImplementedException();
        }

        public bool GetUnitOfMeasurement(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateUnitOfMeasurement(string id, bool input)
        {
            throw new NotImplementedException();
        }

        private void CreateAccount(CreateAccountModel model)
        {
            throw new NotImplementedException();
        }

        private void DeleteAccount(string id)
        {
            throw new NotImplementedException();
        }
    }
}
