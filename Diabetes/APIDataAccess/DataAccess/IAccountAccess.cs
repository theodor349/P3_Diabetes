﻿using APIDataAccess.Models.Account;
using System.Threading.Tasks;

namespace APIDataAccess.DataAccess
{
    public interface IAccountAccess
    {
        void CreateAccountOnDB(CreateAccountDBModel model);
        void DeleteAccount(string id);
        bool EmailExists(string email);
        AccountDBModel Get(string id);
        AccountDBModel GetByPhoneNumber(string phoneNumber);
        bool GetUnitOfMeasure(string id);
        bool PhoneNumberExists(string phoneNumber);
        void UpdateAccount(UpdateAccountDBModel model);
        void UpdateNighScoutLink(string id, string nsLink);
        void UpdateUnitOfMeasure(string id, bool value);
    }
}