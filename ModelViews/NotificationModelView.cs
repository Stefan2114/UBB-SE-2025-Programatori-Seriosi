using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Team3.Entities;
using Team3.Models;

namespace Team3.ModelViews
{


    public class NotificationModelView
    {

        private readonly NotificationModel _notificationModel;

        public List<Notification> Notifications { get; private set; }

        public NotificationModelView()
        {
            _notificationModel = NotificationModel.Instance;
            Notifications = new List<Notification>();
        }

        public void LoadNotifications(int userId)
        {

            DateTime currentDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Config.ROMANIA_TIMEZONE);

            List<Notification> notifications = _notificationModel.GetUserNotifications(userId);
            notifications = notifications
                .Where(n => n.DeliveryDateTime < currentDateTime)
                .OrderByDescending(n => n.DeliveryDateTime)
                .ToList();
            foreach(Notification notification in notifications)
            {
                Notifications.Add(notification);
                Debug.WriteLine(notification.ToString());
            }
        }

        public void DeleteNotification(int notificationId)
        {
            _notificationModel.deleteNotification(notificationId);
        }
    }
}