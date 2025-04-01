using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Team3.Entities;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Team3.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OptionsPage : Page
    {
        public User SelectedUser { get; set; }

        public OptionsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is User user)
            {
                SelectedUser = user;
                // Now you can use the selected user in this page
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.Navigate(typeof(UserView));

            }
        }

        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to ChatPage and pass the selected user
            Frame.Navigate(typeof(ChatView), SelectedUser);
        }

        private void NotificationsButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to NotificationsPage and pass the selected user
            Frame.Navigate(typeof(NotificationView), SelectedUser.Id);
        }

    }
}
