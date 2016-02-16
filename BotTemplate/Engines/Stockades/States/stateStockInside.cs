using System.Threading;
using BotTemplate.Interact;
using System;
using System.Collections.Generic;
using BotTemplate.Helper;

namespace BotTemplate.Engines.Stockades.States
{
    public class stateStockInside : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                return StockadesContainer.isInside && ObjectManager.playerPtr != 0;
            }
        }

        public override string Name
        {
            get
            {
                return "Inside";
            }
        }

        // higher number = higher priority
        public override int Priority
        {
            get
            {
                return 60;
            }
        }

        private static int waitTollerance = 0;
        private static List<UInt64> lootedChests = new List<UInt64>();
        public override void Run()
        {
            if (!StockadesContainer.done && !ObjectManager.isDeath)
            {
                if (StockadesContainer.doOncePerRun)
                {
                    waitTollerance = 0;
                    Calls.DoString("ResetInstances()");
                }
                Thread.Sleep(250);
                List<Objects.GameObject> chestList = ObjectManager.GetGameObjectsByName("Large Solid Chest");
                int i = 0;
                while (i < chestList.Count)
                {
                    if (Ingame.IsDc()) return;
                    if (ObjectManager.FreeBagSlots >= 3)
                    {
                        if (chestList[i].baseAdd != 0)
                        {
                            if (chestList[i].Pos.differenceToPlayer() > 2)
                            {
                                Objects.Location tmpPos = new Objects.Location();
                                tmpPos.x = chestList[i].Pos.x;
                                tmpPos.y = chestList[i].Pos.y;
                                tmpPos.z = chestList[i].Pos.z - 4;
                                Ingame.Tele(tmpPos, 60, false);
                                waitTollerance = 0;
                            }
                            else
                            {
                                try
                                {
                                    int tmp = Environment.TickCount;
                                    Calls.OnRightClickObject(chestList[i].baseAdd, 1);
                                    Thread.CurrentThread.Join(1000);
                                    while (ObjectManager.PlayerObject.IsCasting != 0 && Environment.TickCount - tmp <= 8000) Thread.CurrentThread.Join(10);
                                    tmp = Environment.TickCount;
                                    while (Calls.IsLooting() == 1 && Environment.TickCount - tmp <= 1000) Thread.CurrentThread.Join(10); Console.WriteLine("WAITING");
                                    i++;
                                }
                                catch
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            waitTollerance += 1;
                            if (waitTollerance == 3)
                            {
                                StockadesContainer.done = true;
                                i = int.MaxValue;
                                break;
                            }
                        }
                    }
                    else
                    {
                        StockadesContainer.done = true;
                        i = int.MaxValue;
                        break;
                    }
                }
                StockadesContainer.done = true;
            }
            
            if (!ObjectManager.isDeath)
            {
                Ingame.KillPlayer();
                while (StockadesContainer.isInside && !Ingame.IsDc()) Thread.CurrentThread.Join(10);
            }
            else
            {
                Calls.DoString("RepopMe();");
            }
        }
    }
}
