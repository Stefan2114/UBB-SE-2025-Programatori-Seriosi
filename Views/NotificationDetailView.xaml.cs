using System;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Team3.Entities;
using Team3.ModelViews;

namespace Team3.Views
{
    public sealed partial class NotificationDetailView : Page
    {
        public Notification SelectedNotification { get; set; }
        private NotificationModelView ViewModel { get; } = new NotificationModelView();

        public NotificationDetailView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is Notification notification)
            {
                SelectedNotification = notification;
                Debug.WriteLine($"Viewing notification detail: ID={SelectedNotification.Id}, Message={SelectedNotification.Message}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog confirmDialog = new ContentDialog
            {
                Title = "Delete Notification",
                Content = "Are you sure you want to delete this notification?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = this.XamlRoot
            };

            ContentDialogResult result = await confirmDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                // Delete the notification
                bool deleted = ViewModel.DeleteNotification(SelectedNotification.Id);

                if (deleted)
                {
                    Debug.WriteLine($"Deleted notification: ID={SelectedNotification.Id}");
                    // Go back to the notifications list
                    if (Frame.CanGoBack)
                    {
                        Frame.GoBack();
                    }
                }
                else
                {
                    // Show error
                    ContentDialog errorDialog = new ContentDialog
                    {
                        Title = "Error",
                        Content = "Failed to delete the notification. Please try again.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };

                    await errorDialog.ShowAsync();
                }
            }
        }
    }
}