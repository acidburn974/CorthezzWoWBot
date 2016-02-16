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
using System.Runtime.InteropServices;
using System.Globalization;
using System.Diagnostics;

namespace BotTemplate.Engines.Dupe
{
    internal static class SpawnerEngine
    {
        internal static Spawner engine;
        internal static string name = "Spawner";
    }

    internal class Spawner
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        uint WM_KEYUP = 0x0101;
        uint WM_KEYDOWN = 0x0100;

        private Thread thrWorker;
        internal bool isRunning
        {
            get
            {
                return Running;
            }
        }
        private bool Running;
        private bool doVendor;

        internal void StartEngine(string name)
        {
            Running = true;
            thrWorker = new Thread(Run) { IsBackground = true };
            thrWorker.Start();
            Exchange.CurrentEngine = name;
            Exchange.IsEngineRunning = true;
        }

        internal void StopEngine()
        {
            Running = false;
        }

        Objects.Location spawnObject = new Objects.Location(1914.97f, -1638.881f, 56.3973f);
        int spawnObjectId = 175926;
        string gossipOpenLua = "troll1 = 'false' if GossipFrame:IsVisible() then troll1 = 'true' end;";
        
        bool afterDc = false;

        private void Run()
        {
            Ingame.Tele(spawnObject, 50, true);
            while (Running)
            {
                Objects.GameObject spawner = new Objects.GameObject(0, 0, 0);
                while (spawner.baseAdd == 0 && Running)
                    spawner = ObjectManager.GetGameObjectById(spawnObjectId);

                Calls.OnRightClickObject(spawner.baseAdd, 0);

                while (!(Calls.GetText(gossipOpenLua, "troll1", 10).Trim() == "true") && Running)
                {
                    Thread.Sleep(100);
                }
                Thread.Sleep(250);

                while ((Calls.GetText(gossipOpenLua, "troll1", 10).Trim() == "true") && Running)
                {
                    Calls.DoString("GossipTitleButton1:Click() QuestFrameCompleteQuestButton:Click()");
                }

            }
            
            Exchange.IsEngineRunning = false;
            Exchange.CurrentEngine = "None";
            DupeEngine.engine = null;
        }
    }
}
