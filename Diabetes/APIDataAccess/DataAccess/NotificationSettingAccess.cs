using APIDataAccess.Models.NotificationSetting;
using System;
using System.Collections.Generic;
using System.Text;
using APIDataAccess.Internal.DataAccess;

namespace APIDataAccess.DataAccess
{
    public class NotificationSettingAccess : INotificationSettingAccess
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public NotificationSettingAccess(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }


        public void Create(CreateNotificationSettingModel model)
        {
            _sqlDataAccess.SaveData(SpCommands.spNotificationSetting_Create.ToString(), model, "DDB");
        }

        public List<NotificationSettingModel> Get(string userId)
        {
            var data = _sqlDataAccess.LoadData<NotificationSettingModel, dynamic>(SpCommands.spNotificationSetting_GetByUser.ToString(), new { UserId = userId }, "DDB");
            return data;
        }

        public void Update(UpdateNotificationSettingModel model)
        {
            throw new NotImplementedException();
        }

        public void DeleteByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
