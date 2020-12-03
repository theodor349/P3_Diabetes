using APIDataAccess.Models.NotificationSetting;
using System;
using System.Collections.Generic;
using System.Text;
using APIDataAccess.Internal.DataAccess;

namespace APIDataAccess.DataAccess
{
    public class NotificationSettingAccess : INotificationSettingAccess
    {
        private readonly ISqlDataAccess _sql;

        public NotificationSettingAccess(ISqlDataAccess sqlDataAccess)
        {
            _sql = sqlDataAccess;
        }


        public void Create(CreateNotificationSettingModel model)
        {
            _sql.SaveData(SpCommands.spNotificationSetting_Create.ToString(), model, "DDB");
        }

        public List<NotificationSettingModel> Get(string userId)
        {
            var data = _sql.LoadData<NotificationSettingModel, dynamic>(SpCommands.spNotificationSetting_GetByUser.ToString(), new { UserId = userId }, "DDB");
            return data;
        }

        public void Update(UpdateNotificationSettingModel model)
        {
            _sql.SaveData(SpCommands.spNotificationSetting_Update.ToString(), model, "DDB");
        }

        public void DeleteByUserId(string userId)
        {
            _sql.DeleteData(SpCommands.spNotificationSetting_DeleteByUserId.ToString(), userId, "DDB");
        }
    }
}
