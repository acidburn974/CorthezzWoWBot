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
    internal static class ZgFarmEngine
    {
        internal static ZgFarm engine;
        internal static string name = "ZG Farm";
    }

    internal class ZgFarm
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
            string[] lines = File.ReadAllLines("./zg.txt");
            groupGuy = lines[0].Trim();
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
                Calls.DoString("GetNumRaidMembers() InviteByName('" + groupGuy + "')");
                while (Running && ObjectManager.party1Guid == 0)
                {
                    Thread.Sleep(100);
                }
                //Calls.DoString("for bag = 0,4,1 do for slot = 1, GetContainerNumSlots(bag), 1 do link = GetContainerItemLink(bag,slot) if link then name = gsub(link,'^.*%[(.*)%].*$','%1') if '1' == '1' and string.find(name, 'Bijou') == nil then PickupContainerItem(bag,slot) if (CursorHasItem()) then DeleteCursorItem(); end end end end end");
                Thread.Sleep(1000);
                Calls.DoString("ConvertToRaid()");
                while (Running && Calls.GetText("troll1 = GetNumRaidMembers()", "troll1", 10).Trim() == "0")
                {
                    Calls.DoString("ConvertToRaid()");
                    Thread.Sleep(100);
                }
                Calls.DoString("SetLootMethod('freeforall');");
                Thread.Sleep(250);
                if (Running)
                {
                    Ingame.TeleNoZFake(Entrance, 50, true);
                    Ingame.setCoords(ZoneIn);
                }

                while (Running && ObjectManager.MapId == 0)
                {
                    Thread.Sleep(100);
                }
                if (Running)
                {
                    Thread.Sleep(250);
                    //Ingame.TeleNoZFake(PullSpot3, 50, true);
                    //Thread.Sleep(500);
                    //Calls.SetTarget(pullMobGuid3);
                    //Thread.Sleep(10);
                    //Calls.TurnCharacter(ObjectManager.TargetObject.Pos);
                    //Thread.Sleep(250);
                    //Calls.DoString("CastSpellByName('Frostbolt(rank 1)')");
                    //Thread.Sleep(10);
                    //while (Running && ObjectManager.IsCasting)
                    //{
                    //    Thread.Sleep(100);
                    //}

                    //Ingame.TeleNoZFake(PullSpot2, 50, true);
                    //Thread.Sleep(3000);
                    //Calls.SetTarget(pullMobGuid2);
                    //Thread.Sleep(10);
                    //Calls.TurnCharacter(ObjectManager.TargetObject.Pos);
                    //Thread.Sleep(250);
                    //Calls.DoString("CastSpellByName('Frostbolt(rank 1)')");
                    //Thread.Sleep(10);
                    //while (Running && ObjectManager.IsCasting)
                    //{
                    //    Thread.Sleep(100);
                    //}

                    Ingame.TeleNoZFake(PullSpot1, 50, true);
                    Thread.Sleep(500);
                    Calls.SetTarget(pullMobGuid1);
                    Thread.Sleep(10);
                    Calls.TurnCharacter(ObjectManager.TargetObject.Pos);
                    Thread.Sleep(250);
                    Calls.DoString("CastSpellByName('Frostbolt(rank 1)')");
                    Thread.Sleep(10);
                    while (Running && ObjectManager.IsCasting)
                    {
                        Thread.Sleep(100);
                    }
                }
                if (Running)
                {
                    Ingame.TeleNoZFake(SafeSpot, 50, true);
                }

                while (Running && ObjectManager.TargetObject.Pos.differenceToPlayer() >= 15)
                {
                    Thread.Sleep(250);
                }
                if (Running)
                {
                    Calls.TurnCharacter(ObjectManager.TargetObject.Pos);
                }

                while (Running && ObjectManager.aggroCountPlain() != 0)
                {
                    if (ObjectManager.PlayerObject.isChanneling == 0)
                    {
                        Calls.CastAoe("Blizzard", AoePos);
                    }
                    Thread.Sleep(500);
                }
                Thread.Sleep(500);
                List<Objects.UnitObject> lootMobs = ObjectManager.AllLootableMobs();
                if (Running)
                {
                    Ingame.TeleNoZFake(lootMobs[0].Pos, 50, true);
                    Thread.Sleep(250);
                }
                foreach (Objects.UnitObject u in lootMobs)
                {
                    if (Running)
                    {
                        Calls.DoString("for bag = 0,4,1 do for slot = 1, GetContainerNumSlots(bag), 1 do link = GetContainerItemLink(bag,slot) if link then name = gsub(link,'^.*%[(.*)%].*$','%1') if string.find(name, 'Coin') ~= nil or string.find(name, 'Totem') ~= nil or string.find(name, 'Libram') ~= nil or string.find(name, 'Idol') ~= nil or string.find(name, 'Blood Scythe') ~= nil or string.find(name, 'Voodoo') ~= nil or string.find(name, 'Sceptre') ~= nil then PickupContainerItem(bag,slot) if (CursorHasItem()) then DeleteCursorItem(); end end end end end");
                        Calls.OnRightClickUnit(u.baseAdd, 0);
                        while (Running && Calls.IsLooting() == 0) ;
                        Thread.Sleep(100);
                        while (Running && Calls.IsLooting() == 1)
                        {
                            Calls.AutoLoot();
                            Thread.Sleep(250);
                        }
                        Thread.Sleep(100);
                    }
                }
                Thread.Sleep(750);
                if (Running)
                {
                    Ingame.TeleNoZFake(Exit, 50, true);
                    Ingame.setCoords(ZoneOut);
                }
                while (Running && ObjectManager.MapId == 309)
                {
                    Thread.Sleep(100);
                }
                if (Running)
                {
                    Thread.Sleep(250);
                    Calls.DoString("LeaveParty()");
                }

                if (ObjectManager.FreeBagSlots < 10)
                {
                    Ingame.TeleHb(new Objects.Location(-14377.8f, 411.6882f, 6.626376f), 60, true);

                    Objects.UnitObject vendorObject = new Objects.UnitObject(0, 0, 0);
                    while (vendorObject.baseAdd == 0 && Running)
                        vendorObject = ObjectManager.GetUnitByName("Zarena Cromwind");

                    Calls.OnRightClickUnit(vendorObject.baseAdd, 0);

                    while (!(Calls.GetText("troll1 = 'false' if MerchantFrame:IsVisible() then troll1 = 'true' end;", "troll1", 10).Trim() == "true") && Running)
                    {
                        Thread.Sleep(100);
                    }

                    string[] items = new string[] { "Bloodvine", "Bijou", "Major Mana Potion", "Zulian Ceremonial Staff", "Zulian Hacker of the Tiger", "Traveler\\'s Backpack" };
                    Ingame.SellAllBut(items);
                }

            }

            Exchange.IsEngineRunning = false;
            Exchange.CurrentEngine = "None";
            DupeEngine.engine = null;
        }
    }
}
