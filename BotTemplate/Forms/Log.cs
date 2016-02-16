using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotTemplate.Forms
{
    internal static class Log
    {
        private static bool clear = false;
        private static string messages = "";

        internal static void Add(string parMessage)
        {
            if (clear)
            {
                messages = "";
                clear = false;
            }
            messages += DateTime.Now.ToString("HH:mm:ss") + " -> " + parMessage + Environment.NewLine;

        }

        internal static string get
        {
            get
            {

                if (!clear)
                {
                    clear = true;
                    return messages;
                }
                return "";
            }
        }
    }
}
