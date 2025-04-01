using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using System.Diagnostics;
using Team3.ModelViews;
using Team3.Entities;

namespace Team3.Views
{
    /// <summary>
    /// A page that displays a list of chats with a search bar and a back button.
    /// </summary>
    public sealed partial class ChatView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ChatViewModel ViewModel { get; set; }

        public User SelectedUser { get; set; }

        private ObservableCollection<Chat> _filteredChats = new ObservableCollection<Chat>();
        public ObservableCollection<Chat> FilteredChats
        {
            get => _filteredChats;
            set
            {
                _filteredChats = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilteredChats)));
            }
        }

        public ChatView()
        {
            this.InitializeComponent();
            ViewModel = new ChatViewModel();
            this.DataContext = ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is User user)
            {
                SelectedUser = user;
                LoadChats();
            }
        }

        private void LoadChats()
        {
            ViewModel.LoadChats(SelectedUser);
            //FilterChats(SelectedUser.Id.ToString());
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            FilterChats(SearchBox.Text);
        }

        private void FilterChats(string query)
        {
            var chats = ViewModel.GetChatsByName(query);
            FilteredChats = new ObservableCollection<Chat>(chats);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.BackButtonHandler();
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void ChatsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Chat selectedChat)
            {
                Debug.WriteLine($"Selected Chat: ID={selectedChat.ChatID}, User1={selectedChat.user1}, User2={selectedChat.user2}");
                var messagePage = new MessageView();
                //messagePage.SelectedChat = selectedChat;
                Frame.Navigate(typeof(MessageView), selectedChat);
            }
        }
    }
}
