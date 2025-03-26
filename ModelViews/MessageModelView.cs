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
    class MessageModelView
    {
        private readonly MessageModel _messageModel;
        private int chat_id;
        private int _userId;
        public ObservableCollection<Message> Messages { get; set; }

        public MessageModelView()
        {
            _messageModel = MessageModel.Instance;
            Messages = new ObservableCollection<Message>();
            loadMessages();
        }

        public Dictionary<string, Message> getMessagesByChatId(int chat_id)
        {
            List<Message> messages = _messageModel.getMessagesByChatId(chat_id);
            Dictionary<string, Message> messagesDict = new Dictionary<string, Message>();
            foreach (Message message in messages)
            {
                messagesDict.Add(message.id.ToString(), message);
            }
            return messagesDict;
        }

        public void addMessage(int user_id, int chat_id, string content)
        {
            Message message = new Message(0, content, user_id, chat_id);
            _messageModel.addMessage(message);
        }

        public void loadMessages()
        {
            try
            {
                Dictionary<string, Message> messages = getMessagesByChatId(chat_id);
                foreach (KeyValuePair<string, Message> message in messages)
                {
                    Messages.Add(message.Value);
                    Debug.WriteLine("Message loaded: " + message.Value.content);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error while loading messages: " + e.Message);
            }
        }
    }
}
