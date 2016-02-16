using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotTemplate.Interact;

namespace BotTemplate.Helper.SpellSystem
{
    internal static class SpellManager
    {
        private static List<Spell> Spells = new List<Spell>();
        internal static void Add(Spell spell)
        {
            if (!SpellContains(spell.ToString()))
            {
                Spells.Add(spell);
                LastAdded = spell.ToString();
            }
        }

        internal static string LastAdded
        {
            private set;
            get;
        }

        internal static void CheckSpells()
        {
            if (ObjectManager.PlayerObject.isChanneling == 0 && !ObjectManager.IsCasting)
            {
                for (int i = 0; i < Spells.Count; i++)
                {
                    if (Spells[i].IsSpellReady())
                    {
                        Spells.RemoveAt(i);
                    }
                }
            }
            CooldownManager.checkCds();
            BuffManager.checkBuffs();
            PetCooldownManager.checkCds();
        }

        internal static bool SpellContains(string name)
        {
            foreach (Spell x in Spells)
            {
                if (x.ToString() == name)
                {
                    return true;
                }
            }
            return false;
        }
    }

    internal class Spell
    {
        internal string Name;
        private cTimer wait;

        public override string ToString()
        {
            return Name;
        }

        internal bool IsSpellReady()
        {
            return wait.IsReady();
        }

        internal Spell(string name, int sleepTime)
        {
            wait = new cTimer(sleepTime);
            Name = name;
        }
    }
}
