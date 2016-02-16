using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BotTemplate.Helper;

namespace BotTemplate.Interact
{
    internal static class ChatReader
    {
        private static readonly object chatObjects_Lock = new object();
        private static List<Objects.ChatMessage> chatObjects = new List<Objects.ChatMessage>();
        internal static List<Objects.ChatMessage> ChatMessageList
        {
            get
            {
                lock (chatObjects_Lock)
                {
                    return chatObjects;
                }
            }

            set
            {
                lock (chatObjects_Lock)
                {
                    chatObjects = value;
                }
            }
        }

        internal static bool runThread = false;
        internal static Thread readChatThread = new Thread(readChat);
        private static byte[] emptyMessage = Enumerable.Repeat((byte)0x20, 150).ToArray();
        
        internal static bool ClearChat = true;
        internal static bool doNotify = false;
        internal static bool Notify = false;

        internal static void readChat()
        {
            while (runThread)
            {
                if (ObjectManager.playerPtr != 0)
                {
                    if (ClearChat)
                    {
                        ClearChat = false;
                        // clearing chat line by line
                        for (int i = 0; i < 60; i = i + 1)
                        {
                            BmWrapper.memory.WriteBytes(0xB50580 + (uint)(0x800 * i), emptyMessage);
                        }
                    }

                    // reading all chat messages (60)
                    for (int i = 0; i < 60; i = i + 1)
                    {
                        // Is message empty?
                        if (BmWrapper.memory.ReadASCIIString(0xB50580 + (uint)(0x800 * i), 1) != " ")
                        {
                            // split into type, channel, playername and the body
                            string[] tmpMsgSplitted = BmWrapper.memory.ReadASCIIString(0xB50580 + (uint)(0x800 * i), 150).Trim().Split(',');
                            if (tmpMsgSplitted.Length == 4)
                            {
                                // make an object of the message
                                Objects.ChatMessage tmpChat = new Objects.ChatMessage();
                                tmpChat.Time = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
                                tmpChat.Type = tmpMsgSplitted[0].Split(new char[] { '[', ']' })[1].Trim();
                                tmpChat.Channel = tmpMsgSplitted[1].Split(new char[] { '[', ']' })[1].Trim();
                                tmpChat.playerName = tmpMsgSplitted[2].Split(new char[] { '[', ']' })[1].Trim();
                                tmpChat.Text = tmpMsgSplitted[3].Split(new char[] { '[', ']' })[1].Trim();

                                // Notify on whisper (6) or say (0)
                                if (tmpChat.Type == "6" || tmpChat.Type == "0"
                                        || tmpChat.Text.ToLower().Contains(ObjectManager.playerName.ToLower()))
                                {
                                    // Let the gui timer now that it needs to make a noise
                                    if (doNotify)
                                    {
                                        Notify = true;
                                    }
                                }
                                // add the message object to the list of messages
                                ChatMessageList.Add(tmpChat);
                                // empty the memory where the cur message is stored so it dont get read again
                                BmWrapper.memory.WriteBytes(0xB50580 + (uint)(0x800 * i), emptyMessage);
                            }
                        }
                    }
                }
                Thread.Sleep(30000);
            }
        }
    }
}
