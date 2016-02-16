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
    internal static class ZgAssistEngine
    {
        internal static ZgAssist engine;
        internal static string name = "ZG Assist";
    }

    internal class ZgAssist
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

        Objects.Location Entrance = new Objects.Location(-11922.73f, -1219.869f, 92.42826f);
        Objects.Location Entrance2 = new Objects.Location(-11915.69f, -1226.053f, 92.28723f - 100);
        Objects.Location ZoneIn = new Objects.Location(-11920.93f, -1231.383f, 89.48486f);
        Objects.Location PullSpot1 = new Objects.Location(-11632.71f, -1298.558f, 90.51556f);
        Objects.Location PullSpot2 = new Objects.Location(-11565.04f, -1333.042f, 100.3579f);
        Objects.Location PullSpot3 = new Objects.Location(-11535.87f, -1226.897f, 95.02663f);
        Objects.Location SafeSpot = new Objects.Location(-11645.65f, -1340.844f, 81.02175f);
        Objects.Location AoePos = new Objects.Location(-11638.22f, -1340.717f, 77.57141f);
        Objects.Location Exit = new Objects.Location(-11916.14f, -1233.267f, 92.5334f);
        Objects.Location ZoneOut = new Objects.Location(-11916.14f, -1229.531f, 92.5334f);
        int outsideId = 0;
        int insideId = 309;
        UInt64 pullMobGuid1 = 17379391211701329315;
        UInt64 pullMobGuid2 = 17379391210745027999;
        UInt64 pullMobGuid3 = 17379391211701329261;
        string groupGuy;

        private void Run()
        {
            while (Running)
            {
                while (Running && Calls.GetText("troll1 = GetNumRaidMembers()", "troll1", 10).Trim() == "0")
                {
                    Thread.Sleep(100);
                }
                Thread.Sleep(1000);
                if (Running)
                {
                    Ingame.TeleHb(Entrance, 50, true);
                    Ingame.setCoords(ZoneIn);
                }

                while (Running && ObjectManager.MapId == 0)
                {
                    Thread.Sleep(100);
                }
                if (Running)
                {
                    Thread.Sleep(1000);

                    Ingame.TeleHb(SafeSpot, 50, true);
                    Thread.Sleep(500);
                }
                while (ObjectManager.GetPlayerByGuid(ObjectManager.party1Guid).Pos.differenceToPlayer() > 3 && Running)
                {
                    Thread.Sleep(500);
                }
                Thread.Sleep(5000);
                while (Running && ObjectManager.aggroCountOnGuid(ObjectManager.party1Guid) != 0)
                {
                    Thread.Sleep(100);
                }
                Thread.Sleep(500);

                if (Running)
                {
                    Ingame.TeleHb(Exit, 50, true);
                    Ingame.setCoords(ZoneOut);
                }
                while (Running && ObjectManager.MapId == 309)
                {
                    Thread.Sleep(100);
                }
                if (Running)
                {
                    Thread.Sleep(500);
                    Ingame.TeleHb(Entrance2, 50, true);
                }

                while (Running && Calls.GetText("troll1 = GetNumRaidMembers()", "troll1", 10).Trim() != "0")
                {
                    Thread.Sleep(100);
                }

                //while (Running && ObjectManager.aggroCountPlain() != 0)
                //{
                //    if (ObjectManager.PlayerObject.isChanneling == 0)
                //    {
                //        Calls.CastAoe("Blizzard", AoePos);
                //    }
                //    Thread.Sleep(500);
                //}
                //Thread.Sleep(500);
                //List<Objects.UnitObject> lootMobs = ObjectManager.AllLootableMobs();
                //if (Running)
                //{
                //    Ingame.TeleHb(lootMobs[0].Pos, 50, true);
                //    Thread.Sleep(250);
                //}
                //foreach (Objects.UnitObject u in lootMobs)
                //{
                //    if (Running)
                //    {
                //        Calls.OnRightClickUnit(u.baseAdd, 0);
                //        while (Running && Calls.IsLooting() == 0) ;
                //        Thread.Sleep(250);
                //        while (Running && Calls.IsLooting() == 1)
                //        {
                //            Calls.AutoLoot();
                //            Thread.Sleep(250);
                //        }
                //        Thread.Sleep(250);
                //    }
                //}
                //Thread.Sleep(750);
                //if (Running)
                //{
                //    Ingame.TeleHb(Exit, 50, true);
                //    Ingame.setCoords(ZoneOut);
                //}
                //while (Running && ObjectManager.MapId == 309)
                //{
                //    Thread.Sleep(100);
                //}
                //if (Running)
                //{
                //    Thread.Sleep(250);
                //    Calls.DoString("LeaveParty()");
                //}

            }

            Exchange.IsEngineRunning = false;
            Exchange.CurrentEngine = "None";
            DupeEngine.engine = null;
        }
    }
}
