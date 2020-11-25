using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHandler.Utils
{
    public class NotificationsSettingsUtils : INotificationsSettingsUtils
    {
        private readonly IConfiguration configuration;

        public NotificationsSettingsUtils(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetDefaultValue(string type, string value)
        {
            return configuration.GetSection("NotificationSettingsDefault").GetSection(type).GetSection(value).Value;
        }
    }
}
