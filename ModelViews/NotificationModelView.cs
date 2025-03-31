using System;
using System.Collections.Generic;
using Team3.Entities;

namespace Team3.ModelViews
{
    public class NotificationModelView
    {
        public List<Notification> Notifications { get; private set; }

        public NotificationModelView()
        {
            LoadNotifications();
        }

        private void LoadNotifications()
        {
            // In a real application, you would fetch the notifications from a database or service
            // For now, let's create some sample data
            Notifications = new List<Notification>
            {
                new Notification(1, DateTime.Now.AddDays(-1), "Welcome to the application! We're glad to have you here."),
                new Notification(2, DateTime.Now.AddHours(-5), "Your account settings have been updated."),
                new Notification(3, DateTime.Now.AddHours(-2), "You have a new message from the administrator."),
                new Notification(4, DateTime.Now.AddMinutes(-30), "System maintenance scheduled for tonight at 2 AM.")
            };
        }

        public bool DeleteNotification(int notificationId)
        {
            // In a real application, you would delete from a database or service
            try
            {
                var notificationToRemove = Notifications.Find(n => n.Id == notificationId);

                if (notificationToRemove != null)
                {
                    Notifications.Remove(notificationToRemove);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}