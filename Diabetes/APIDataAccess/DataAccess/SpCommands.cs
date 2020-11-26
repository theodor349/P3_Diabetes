using System;
using System.Collections.Generic;
using System.Text;

namespace APIDataAccess.DataAccess
{
    public enum SpCommands
    {
        spNotificationSetting_GetByUser,
        spNotificationSetting_Update,
        spNotificationSetting_DeleteByUserId,

        spAccount_Get,

        spPermission_Get,
        spPermission_GetByTargetId,
        spPermission_GetByWatcherId,
        spPermission_UpdatePermissionModel,
        spNotificationSetting_Create,
        spPermission_Delete,
        spPermission_Create,
        spPermission_DeleteByUserId,
        spPermission_GetPendingPermissions,
        spPermission_GetPermissionAttributes,
        spPermission_AcceptPermissionRequest,
        spPermission_DenyPermissionRequest,
    }
}
