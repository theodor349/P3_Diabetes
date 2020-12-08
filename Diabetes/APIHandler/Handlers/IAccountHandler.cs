using APIDataAccess.Models.Account;
using APIHandler.Models;
using System.Threading.Tasks;

namespace APIHandler.Handlers
{
    public interface IAccountHandler
    {
        bool EmailExists(string email);
        AccountDBModel Get(string id);
        AccountDBModel GetByPhoneNumber(string phoneNumber);
        bool GetUnitOfMeasurement(string id);
        bool PhoneNumberExists(string phone);
        Task<bool> RegisterAccount(InputCreateAccountModel model);
        void UnregisterAccount(string id);
        void UpdateAccount(UpdateAccountDBModel model);
        void UpdateNightScoutLink(UpdateNightScoutLinkModel model);
        void UpdateUnitOfMeasurement(UpdateUnitOfMesureModel model);
        AccountNameModel GetName(string id);
        string GetNightscoutLink(string id);
        float GetMaxReservoir(string id);
    }
}