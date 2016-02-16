using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotTemplate.Helper.SpellSystem
{
    internal static class BuffManager
    {
        internal static void Add(string name)
        {
            if (!buffName.Contains(name))
            {
                buffName.Add(name);
                buffDura.Add(Environment.TickCount + 1000);
            }
        }

        internal static bool Contains(string name)
        {
            return buffName.Contains(name);
        }

        private static List<string> buffName = new List<string>();
        private static List<int> buffDura = new List<int>();
        internal static void checkBuffs()
        {
            for (int i = 0; i < buffName.Count; i++)
            {
                if (buffDura[i] < Environment.TickCount)
                {
                    buffName.RemoveAt(i);
                    buffDura.RemoveAt(i);
                }
            }
        }
    }
}
