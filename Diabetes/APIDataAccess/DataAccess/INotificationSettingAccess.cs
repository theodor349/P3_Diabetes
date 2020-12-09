using APIDataAccess.Models.NotificationSetting;
using System.Collections.Generic;

namespace APIDataAccess.DataAccess
{
    public interface INotificationSettingAccess
    {
        void Create(CreateNotificationSettingModel model);
        void DeleteByUserId(string userId);
        List<NotificationSettingModel> Get(string userId);
        void Update(UpdateNotificationSettingModel model);
        NotificationSettingModel Get(int id);
    }
}