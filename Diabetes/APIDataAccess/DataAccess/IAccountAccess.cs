using APIDataAccess.Models.Account;
using System.Threading.Tasks;

namespace APIDataAccess.DataAccess
{
    public interface IAccountAccess
    {
        void CreateAccount(AccountModel model);
        void DeleteAccount(string id);
        bool EmailExists(string email);
        Task<AccountModel> Get(string id);
        AccountModel GetByPhoneNumber(string phoneNumber);
        bool GetUnitOfMeasure(string id);
        bool PhoneNumberExists(string phoneNumber);
        void UpdateAccount(UpdateAccountModel model);
        void UpdateNighScoutLink(string id, string nsLink);
        void UpdateUnitOfMeasure(string id, bool value);
    }
}