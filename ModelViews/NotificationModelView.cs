using System;


namespace Team3.ModelViews
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Team3.Models;
    class NotificationModelView
    {
        private readonly NotificationModel _notificaitonModel;
        public ObservableCollection<Notification> notifications { get; private set; }

        public NotificationModelView()
        {
            _notificaitonModel = NotificationModel.Instance;
            notifications = new ObservableCollection<Notification>();
            LoadNotificaitons();
        }

        private void LoadNotificaitons()
        {
            try
            {
                notifications.Clear();
                var notifList = _notificaitonModel.GetNotifications();

                if (notifList != null && notifList.Any())
                {
                    foreach (var notification in notifications)
                    {
                        notifications.Add(notification);
                        Debug.WriteLine(notification.ToString());
                    }
                }
                else
                {
                    Debug.WriteLine("No notifications\n");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }


}