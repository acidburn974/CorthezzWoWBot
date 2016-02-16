using System;
using BotTemplate.Helper;
using BotTemplate.Interact;
using BotTemplate.Engines.CustomClass;
using BotTemplate.Helper.SpellSystem;

namespace BotTemplate.Engines.Master.States
{
    public class stateMasterFight : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                if (ObjectManager.AggroMobs().Count != 0)
                {
                    MasterContainer.StuckTimer.Reset();
                    return true;
                }
                return false;
            }
        }

        public override string Name
        {
            get
            {
                return "Fight";
            }
        }

        // higher number = higher priority
        public override int Priority
        {
            get
            {
                return 70;
            }
        }

        int curHealthPercent;
        int BlackListCounter;
        cTimer BlackListTimer
        {
            get
            {
               return MasterContainer.BlackListTimer;
            }
        }
        public override void Run()
        {
            if (curHealthPercent == (int)ObjectManager.TargetObject.healthPercent)
            {
                if (BlackListTimer.IsReady())
                {
                    BlackListCounter = BlackListCounter + 1;
                    if (BlackListCounter == 2)
                    {
                        MasterContainer.blacklistGuid.Add(ObjectManager.TargetObject.guid);
                        BlackListCounter = 0;
                        BlackListTimer.Reset();
                        Calls.SetTarget(0);
                    }
                    else
                    {
                        MasterContainer.IsStuck = true;
                    }
                }
            }
            else
            {
                curHealthPercent = (int)ObjectManager.TargetObject.healthPercent;
                BlackListTimer.Reset();
                BlackListCounter = 0;
            }

            if (ObjectManager.TargetObject.healthPercent == 100)
            {
                int curHealth = (int)ObjectManager.TargetObject.healthPercent;
            }

            MasterFightMovement.Handle();
            if (ObjectManager.IsTargetOnMe())
            {
                SpellManager.CheckSpells();
                CCManager.FightPulse();
                Ingame.CastFinal();
                MasterContainer.AfterFight = true;
            }
            else
            {
                UInt64 guid = 0x0;
                try
                {
                    guid = ObjectManager.AggroMobs()[0];
                }
                catch { }
                Calls.SetTarget(guid);
            }
        }
    }
}
