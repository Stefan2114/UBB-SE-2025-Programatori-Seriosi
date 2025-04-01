using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Team3.Entities;
using Team3.Models;
using Team3.DTOs;

namespace Team3.ModelViews
{
    internal class ChatViewModel
    {
        public int userID { get; set; }
        public ObservableCollection<ChatDTO> Chats { get; private set; }
        private readonly UserModelView UserMV;

        public ChatViewModel()
        {
            Chats = new ObservableCollection<ChatDTO>();
            UserMV = new UserModelView();
        }

        public void LoadChats(User selectedUser)
        {
            List<Chat> chats = ChatModel.Instance.getChats(selectedUser.Id);
            Debug.WriteLine(chats.Count + "chats loaded in VM");
            foreach (Chat chat in chats)
            {
                User user1 = UserMV.GetUser(chat.user1);
                User user2 = UserMV.GetUser(chat.user2);
                Chats.Add(new ChatDTO(chat.ChatID, chat.user1, chat.user2, user1.Name, user2.Name));
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
