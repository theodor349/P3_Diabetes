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
        spNotificationSetting_Create,

        spAccount_Get,
        spAccount_GetByPhoneNumber,
        spAccount_CreateAccount,
        spAccount_DeleteAccount,
        spAccount_UpdateAccount,
        spAccount_UpdateNightScoutLink,
        spAccount_UpdateUnitOfMeasure,
        spAccount_GetByEmail,
        spAccount_GetName,

        spPermission_Get,
        spPermission_GetByTargetId,
        spPermission_GetByWatcherId,
        spPermission_UpdatePermissionModel,
        spPermission_Delete,
        spPermission_Create,
        spPermission_DeleteByUserId,
        spPermission_GetPendingPermissions,
        spPermission_AcceptPermissionRequest,
    }
}
