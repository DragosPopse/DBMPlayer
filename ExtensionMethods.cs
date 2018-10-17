using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notifications.Wpf;


namespace DBMPlayer
{
    public static class ExtensionMethods
    {
        public static void ShowInfo(this NotificationManager manager, string message)
        {
            manager.Show(new NotificationContent
            {
                Title = "Info",
                Type = NotificationType.Information,
                Message = message
            });
        }
    }
}
