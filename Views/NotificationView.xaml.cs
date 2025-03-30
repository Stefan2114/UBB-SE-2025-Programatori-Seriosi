using System;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Team3.Entities;
using Team3.ModelViews;

namespace Team3.Views
{
    public sealed partial class NotificationView : Page
    {
        public User SelectedUser { get; set; }
        public NotificationModelView ViewModel { get; } = new NotificationModelView();

        public NotificationView()
        {
            this.InitializeComponent();
            this.NotificationsListView.DataContext = ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is User user)
            {
                SelectedUser = user;
                // In a real app, you might filter notifications by user
                Debug.WriteLine($"Loading notifications for user: ID={SelectedUser.Id}, Name={SelectedUser.Name}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void NotificationsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Notification selectedNotification)
            {
                Debug.WriteLine($"Selected Notification: ID={selectedNotification.Id}, Date={selectedNotification.DeliveryDateTime}");
                // Navigate to the notification detail page passing the notification
                Frame.Navigate(typeof(NotificationDetailView), selectedNotification);
            }
        }
    }
}