using System;
using BotTemplate.Interact;
using BotTemplate.Helper;
using BotTemplate.Constants;
using System.Threading;

namespace BotTemplate.Engines.Master.States
{
    public class stateMasterLoot : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                if (ObjectManager.LootableMobsCount != 0)
                {
                    MasterContainer.StuckTimer.Reset();
                    MasterContainer.BlackListTimer.Reset();
                    return !(ObjectManager.FreeBagSlots <= Data.LeaveFreeSlots);
                }
                return false;
            }
        }

        public override string Name
        {
            get
            {
                return "Looting";
            }
        }

        // higher number = higher priority
        public override int Priority
        {
            get
            {
                return 40;
            }
        }

        UInt64 CurLootMob = 0x0;
        UInt64 OldLootMob = 0x0;
        int LootTryOuts = 0;
        cTimer LootClickTimer = new cTimer(1000);
        public override void Run()
        {
            Objects.UnitObject tmpMob = ObjectManager.GetNextLoot();

            if (tmpMob.baseAdd != 0)
            {
                CurLootMob = tmpMob.guid;
                if (CurLootMob != OldLootMob)
                {
                    OldLootMob = CurLootMob;
                    LootTryOuts = 0;
                }

                if (LootTryOuts < 10)
                {
                    if (ObjectManager.PlayerObject.targetGuid != tmpMob.guid)
                    {
                        Calls.SetTarget(tmpMob.guid);
                    }
                    else
                    {
                        float diff = ObjectManager.PlayerObject.Pos.differenceTo(tmpMob.Pos);
                        if (diff > 2)
                        {
                            Calls.TurnCharacter(tmpMob.Pos);
                            Ingame.moveForward();

                        }
                        else
                        {
                            Calls.DoString("DoEmote('stand')");
                            Calls.StopRunning();
                        }

                        if (LootClickTimer.IsReady())
                        {
                            if (diff < 4)
                            {
                                Calls.OnRightClickUnit(tmpMob.baseAdd, 1);
                            }
                            LootTryOuts = LootTryOuts + 1;
                        }
                    }
                }
                else
                {
                    if (!ObjectManager.BlacklistedLoot.Contains(tmpMob.guid))
                    {
                        ObjectManager.BlacklistedLoot.Add(tmpMob.guid);
                    }
                }
            }
        }
    }
}
