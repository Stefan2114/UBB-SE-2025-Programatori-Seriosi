using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Team3.Entities;
using Team3.Models;

namespace Team3.ModelViews
{
    internal class ChatViewModel
    {
        public int userID { get; set; }
        public ObservableCollection<Chat> Chats { get; private set; }
        private readonly UserModelView UserMV;

        public ChatViewModel()
        {
            Chats = new ObservableCollection<Chat>();
            UserMV = new UserModelView();
        }

        public void LoadChats(User selectedUser)
        {
            List<Chat> chats = ChatModel.Instance.getChats(selectedUser.Id);
            Debug.WriteLine(chats.Count + "chats loaded in VM");
            foreach (Chat chat in chats)
            {
                Chats.Add(chat);
            }

        }

        public Dictionary<Chat, string> GetChats(int id)
        {
            List<Chat> chats = ChatModel.Instance.getChats(id);
            Dictionary<Chat, string> chatDict = new Dictionary<Chat, string>();
            foreach (Chat chat in chats)
            {
                chatDict.Add(chat, chat.ChatID.ToString());
            }
            return chatDict;
        }

        public void AddChat(Chat chat)
        {
            ChatModel.Instance.addChat(chat.user1, chat.user2);
            Chats.Add(chat);
        }

        public void setUserId(int id)
        {
            userID = id;
        }

        public List<Chat> GetChatsByName(string name)
        {
            List<Chat> chats = ChatModel.Instance.getChats(userID);
            return chats.Where(chat => chat.user1.ToString().Contains(name) || chat.user2.ToString().Contains(name)).ToList();
        }

        public void BackButtonHandler()
        {
            Debug.WriteLine("Back button clicked in Chats");
        }

        public void SearchButtonHandler(string SearchQuery)
        {
            Debug.WriteLine("Search button clicked in Chats");
            GetChatsByName(SearchQuery);
        }
    }
}
