using System;
using System.Collections.Generic;
using System.Text;

namespace APIDataAccess.DataAccess
{
    public enum SpCommands
    {
        spNotificationSetting_GetByUser,
        spPermission_Get,
        spAccount_Get,
        spPermission_GetByTargetId,
        spPermission_GetByWatcherId,
        spPermission_UpdatePermissionModel,
        spNotificationSetting_Create,
        spPermission_Delete,
        spPermission_Create,
        spPermission_DeleteByUserId,
    }
}
