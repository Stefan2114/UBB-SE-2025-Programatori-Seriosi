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
        private readonly MessageModel messageModel;
        private int chatId;
        private int userId;
        public ObservableCollection<String> Messages { get; set; }

        public MessageModelView()
        {
            Debug.WriteLine("MessageModelView created");
            messageModel = MessageModel.Instance;
            Messages = new ObservableCollection<String>();
        }

        //public Dictionary<string, Message> getMessagesByChatId(int chatId)
        //{
        //    List<Message> messages = messageModel.GetMessagesByChatId(chatId);
        //    Debug.WriteLine(messages.Count + "Messages loaded");
        //    Dictionary<Message, string> messagesDict = new Dictionary<string, Message>();
        //    foreach (Message message in messages)
        //    {
        //        messagesDict.Add(message.id.ToString(), message);
        //        Debug.WriteLine("Message loaded: " + message.content);
        //    }
        //    return messagesDict;
        //}


        public void setUserIdAndChat(int userId, int chatId)
        {
            userId = userId;
            chatId = chatId;
        }

        public void loadMessages()
        {
            //Debug.WriteLine("Loading messages");
            //try
            //{
            //    List<Message> messages = messageModel.GetMessagesByChatId(chatId);

            //    foreach (Message message in messages)
            //    {
            //        messagesDict.Add(message.id.ToString(), message);
            //        Debug.WriteLine("Message loaded: " + message.content);
            //    }
            //    Debug.WriteLine(messages.Count);
            //    foreach (KeyValuePair<string, Message> message in messages)
            //    {
            //        Messages.Add(message.Value);
            //        Debug.WriteLine("Message loaded: " + message.Value.content);
            //    }
            //    Debug.WriteLine(Messages.Count + " messages loaded");
            //}
            //catch (Exception e)
            //{
            //    throw new Exception("Error while loading messages: " + e.Message);
            //}
        }


        public void BackButtonHandler()
        {
            Debug.WriteLine("Back button clicked");
        }

        public void sendButtonHandler()
        {
            Debug.WriteLine("Send button clicked");
            string message = ""; // get it from a text field
            //Messages.Add(new Message(messageID, message, _userId, chat_id));
        }
    }
}
