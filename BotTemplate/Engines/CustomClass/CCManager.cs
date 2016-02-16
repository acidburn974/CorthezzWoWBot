using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using BotTemplate.Interact;
using BotTemplate.Constants;
using BotTemplate.Helper;
using BotTemplate.Helper.SpellSystem;

namespace BotTemplate.Engines.CustomClass
{
    public static class CCManager
    {
        internal static List<CustomClass> ccs;
        internal static int toUse;

        internal static void Initialisate()
        {
            ccs = new List<CustomClass>();
            toUse = -1;
        }

        internal static void GetCustomClasses()
        {
            //ccs.Clear();
            if (!Directory.Exists(".\\CustomClasses"))
            {
                Directory.CreateDirectory(".\\CustomClasses");
                Directory.CreateDirectory(".\\CustomClasses\\Compiled");
                return;
            }
            string[] files = Directory.GetFiles(".\\CustomClasses");

            foreach (string file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string pathToAssembly = "CustomClasses\\Compiled\\" + fileName + ".dll";
                if (CodeCompiler.CreateAssemblyFromFile(file, pathToAssembly))
                {
                    pathToAssembly = Path.GetFullPath(pathToAssembly);
                    Assembly ass = Assembly.LoadFile(pathToAssembly);
                    foreach (Type t in ass.GetTypes())
                    {
                        if (t.BaseType.Name == "CustomClass")
                        {
                            CustomClass cc = (CustomClass)Activator.CreateInstance(t, null);
                            ccs.Add(cc);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Chooses the CustomClass for the specific class you entered
        /// </summary>
        /// <param name="wowClass"></param>
        /// <returns>If there is more than one CC for a Class then it returns false</returns>
        internal static bool ChooseCustomClassByWowClass(byte wowClass)
        {
            int counter = 0;
            for (int i = 0; i < ccs.Count; i++)
            {
                if (ccs[i].DesignedForClass == wowClass)
                {
                    counter++;
                    toUse = i;
                }
            }
            if (toUse != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static void FightPulse()
        {
            ccs[toUse].Fight();
        }

        internal static bool IsBuffed()
        {
            SpellManager.CheckSpells();
            if (!ccs[toUse].BuffRoutine())
            {
                Ingame.Stand();
                if (!Calls.MovementIsOnly(0) && !Calls.MovementIsOnly((uint)Offsets.movementFlags.Swimming))
                {
                    Calls.StopRunning();
                }
                else
                {
                    Ingame.CastFinal();
                }
                return false;
            }
            return true;
        }

        internal static void RestHealth(bool firstBool)
        {
            SpellManager.CheckSpells();
            if (ObjectManager.PlayerObject.isChanneling == 0 && !ObjectManager.IsCasting)
            {
                ccs[toUse].RestHealth(firstBool);
                Ingame.CastFinal();
            }
        }

        internal static void RestMana(bool firstBool)
        {
            if (ObjectManager.PlayerObject.isChanneling == 0 && !ObjectManager.IsCasting)
            {
                SpellManager.CheckSpells();
                ccs[toUse].RestMana(firstBool);
                Ingame.CastFinal();
            }
        }
    }
}
