using APIDataAccess.Models.Account;

namespace APIHandler.Handlers
{
    public interface IAccountHandler
    {
        bool EmailExists(string email);
        AccountModel Get(string id);
        AccountModel GetByPhoneNumber(string phoneNumber);
        bool GetUnitOfMeasurement(string id);
        bool PhoneNumberExists(string phone);
        void RegisterAccount(CreateAccountModel model);
        void UnregisterAccount(string id);
        void UpdateAccount(CreateAccountModel model);
        void UpdateNightScoutLink(string id, string link);
        void UpdateUnitOfMeasurement(string id, bool input);
    }
}