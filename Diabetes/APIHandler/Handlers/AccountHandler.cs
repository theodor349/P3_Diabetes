using APIDataAccess.DataAccess;
using APIDataAccess.Models.Account;
using APIHandler.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountHandler(IAccountAccess aa, INotificationSettingHandler nh, IPermissionHandler ph, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _aa = aa;
            _nh = nh;
            _ph = ph;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public AccountDBModel Get(string id)
        {
            return _aa.Get(id);
        }

        public AccountDBModel GetByPhoneNumber(string phoneNumber)
        {
            return _aa.GetByPhoneNumber(phoneNumber);
        }

        public async Task<bool> RegisterAccount(InputCreateAccountModel model)
        {
            var userId = await CreateAuthUser(model);
            if (userId == null)
                return false;

            var user = new CreateAccountDBModel()
            {
                Id = userId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                NSLink = model.NSLink,
                IsEUMeasure = model.IsEUMeasure,
            };
            CreateAccount(user);
            _ph.CreatePermanent(userId);
            _nh.CreateStandardSettings(userId);
            return true;
        }

        private async Task<string> CreateAuthUser(InputCreateAccountModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return await _userManager.GetUserIdAsync(user);
            }
            return null;
        }

        public void UnregisterAccount(string id)
        {
            throw new NotImplementedException();
        }

        public int UpdateAccount(UpdateAccountDBModel model)
        {
            return _aa.UpdateAccount(model);
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
            _aa.CreateAccountOnDB(model);
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

        public float GetMaxReservoir(string id) {
            return _aa.GetMaxReservoir(id);
        }
    }
}
