using APIDataAccess.DataAccess;
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
        private readonly IAccountAccess _aa;
        private readonly INotificationSettingHandler _nh;
        private readonly IPermissionHandler _ph;

        public AccountHandler(IAccountAccess aa, INotificationSettingHandler nh, IPermissionHandler ph)
        {
            _aa = aa;
            _nh = nh;
            _ph = ph;
        }

        public AccountDBModel Get(string id)
        {
            return _aa.Get(id);
        }

        public AccountDBModel GetByPhoneNumber(string phoneNumber)
        {
            return _aa.GetByPhoneNumber(phoneNumber);
        }

        public void RegisterAccount(CreateAccountDBModel model)
        {
            throw new NotImplementedException();
        }

        public void UnregisterAccount(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAccount(UpdateAccountDBModel model)
        {
            _aa.UpdateAccount(model);
        }

        public void UpdateNightScoutLink(UpdateNightScoutLinkModel model)
        {
            _aa.UpdateNighScoutLink(model);
        }

        public bool EmailExists(string email)
        {
            return _aa.EmailExists(email);
        }

        public bool PhoneNumberExists(string phone)
        {
            return _aa.PhoneNumberExists(phone);
        }

        public bool GetUnitOfMeasurement(string id)
        {
            return _aa.GetUnitOfMeasure(id);
        }

        public void UpdateUnitOfMeasurement(UpdateUnitOfMesureModel model)
        {
            _aa.UpdateUnitOfMeasure(model);
        }

        private void CreateAccount(CreateAccountDBModel model)
        {
            throw new NotImplementedException();
        }

        private void DeleteAccount(string id)
        {
            throw new NotImplementedException();
        }

        public AccountNameModel GetName(string id)
        {
            return _aa.GetName(id);
        }

        public string GetNightscoutLink(string id) {
            return _aa.GetNightscoutLink(id);
        }
    }
}
