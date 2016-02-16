using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotTemplate.Helper.SpellSystem
{
    internal static class CooldownManager
    {
        internal static void Add(string spell, double remaining)
        {
            if (!spellName.Contains(spell))
            {
                spellName.Add(spell);
                cdDura.Add(remaining);
            }
        }

        internal static bool Contains(string spell)
        {
            return spellName.Contains(spell);
        }

        private static List<string> spellName = new List<string>();
        private static List<double> cdDura = new List<double>();
        internal static void checkCds()
        {
            for (int i = 0; i < spellName.Count; i++)
            {
                if (cdDura[i] < Environment.TickCount)
                {
                    spellName.RemoveAt(i);
                    cdDura.RemoveAt(i);
                }
            }
        }
    }
}
