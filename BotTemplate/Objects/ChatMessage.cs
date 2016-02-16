using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotTemplate.Objects
{
    internal class ChatMessage
    {
        internal ChatMessage()
        {
            this.Type = "";
            this.Channel = "";
            this.playerName = "";
            this.Text = "";
            this.Time = "";
        }

        internal string Type;
        internal string Channel;
        internal string playerName;
        internal string Text;
        internal string Time;

    }
}
