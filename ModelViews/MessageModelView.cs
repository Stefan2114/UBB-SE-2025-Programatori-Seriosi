using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3.Entities;
using Team3.Models;
using Windows.Services.Maps;

namespace Team3.ModelViews
{
    public class MessageModelView
    {
        private readonly MessageModel _messageModel;
        private int chat_id;
        private int _userId;
        public ObservableCollection<Message> Messages { get; set; }

        public MessageModelView()
        {
            Debug.WriteLine("MessageModelView created");
            _messageModel = MessageModel.Instance;
            Messages = new ObservableCollection<Message>();
            loadMessages();
        }

        public Dictionary<string, Message> getMessagesByChatId(int chat_id)
        {
            List<Message> messages = _messageModel.GetMessagesByChatId(chat_id);
            Debug.WriteLine(messages.Count + "Messages loadedsasd");
            Dictionary<string, Message> messagesDict = new Dictionary<string, Message>();
            foreach (Message message in messages)
            {
                messagesDict.Add(message.id.ToString(), message);
                Debug.WriteLine("Message loaded: " + message.content);
            }
            return messagesDict;
        }

        public void addMessage(int user_id, int chat_id, string content)
        {
            _messageModel.addMessage(user_id, chat_id, content);
        }

        public void setUserIdAndChat(int userId, int chatId)
        {
            _userId = userId;
            chat_id = chatId;
        }

        public void loadMessages()
        {
            Debug.WriteLine("Loading messages");
            try
            {
                Dictionary<string, Message> messages = getMessagesByChatId(0);
                Debug.WriteLine(messages.Count);
                foreach (KeyValuePair<string, Message> message in messages)
                {
                    Messages.Add(message.Value);
                    Debug.WriteLine("Message loaded: " + message.Value.content);
                }
                Debug.WriteLine(Messages.Count + " messages loaded");
            }
            catch (Exception e)
            {
                throw new Exception("Error while loading messages: " + e.Message);
            }
        }


        public void BackButtonHandler()
        {
            Debug.WriteLine("Back button clicked");
        }

        public void sendButtonHandler(string message)
        {
            Debug.WriteLine("Send button clicked");
            int messageID = _messageModel.addMessage(_userId, chat_id, message);
            Messages.Add(new Message(messageID, message, _userId, chat_id));
        }
    }
}
