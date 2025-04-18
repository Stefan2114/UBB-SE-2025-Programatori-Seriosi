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
using Team3.ModelViews;
using System.Diagnostics;

namespace Team3.Views
{
    public sealed partial class MessageView : Page
    {
        public MessageModelView ViewModel { get; } = new MessageModelView();
        public int UserId { get; set; }
        public int ChatId { get; set; }



        public MessageView()
        {
            this.InitializeComponent();
            this.chatMessages.DataContext = ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is (int userId, int chatId))
            {
                UserId = userId;
                ChatId = chatId;
                ViewModel.LoadMessages(userId);
                // In a real app, you might filter notifications by user
                Debug.WriteLine($"Loading notifications for user: ID={userId}");
            }
        }

        private
        void BackClicked(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(typeof(ChatView), (UserId, ChatId));
        }

        private void sendButtonClicked(object sender, RoutedEventArgs e)
        {
            string message = messageBar.Text;
            ViewModel.SendButtonHandler(UserId, ChatId, message);
            messageBar.PlaceholderText = "Type a message...";
        }

    }
}
