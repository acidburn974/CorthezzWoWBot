using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BotTemplate.Interact;
using BotTemplate.Helper;
using BotTemplate.Constants;
using System.Windows.Forms;
using System.IO;

namespace BotTemplate.Engines.Create
{
    internal static class CreateEngine
    {
        internal static Create engine;
        internal static string name = "Creater";
    }

    internal class Create
    {
        internal Create(string[] parChars)
        {
            characters = parChars;
        }

        private Thread thrWorker;
        internal bool isRunning
        {
            get
            {
                return Running;
            }
        }
        private bool Running;

        string[] characters;
        internal void StartEngine(string name)
        {
            Running = true;
            thrWorker = new Thread(Run) { IsBackground = true };
            thrWorker.Start();
            Exchange.CurrentEngine = name;
            Exchange.IsEngineRunning = true;
        }

        private void Run()
        {
            if (ObjectManager.LoginState == "charselect")
            {
                foreach (string x in characters)
                {
                    if (x.Trim() != "")
                    {
                        Calls.DoString("CharSelectCreateCharacterButton:Click() CharacterCreateRaceButton1:Click() CharacterCreateNameEdit:SetText('" + x.Trim() + "'); CharCreateOkayButton:Click()");
                        Thread.Sleep(1000);
                    }
                }
            }
            Exchange.IsEngineRunning = false;
            Running = false;
            Exchange.CurrentEngine = "None";
            CreateEngine.engine = null;
        }

    }
}
