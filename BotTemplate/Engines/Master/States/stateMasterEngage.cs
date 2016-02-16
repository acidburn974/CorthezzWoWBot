using BotTemplate.Helper;
using BotTemplate.Interact;
using BotTemplate.Engines.CustomClass;
using BotTemplate.Helper.SpellSystem;

namespace BotTemplate.Engines.Master.States
{
    public class stateGrindEngage : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                if (!ObjectManager.TargetObject.isPlayerPet)
                {
                    if (ObjectManager.targetGuid != 0 && ObjectManager.AggroMobs().Count == 0 && ObjectManager.TargetObject.healthPercent == 100
                        && Data.Faction.Contains(ObjectManager.TargetObject.factionId))
                    {
                        if (!ObjectManager.TargetObject.isTapped)
                        {
                            if (!MasterContainer.blacklistGuid.Contains(ObjectManager.TargetObject.guid))
                            {
                                if (MasterContainer.resetEngage)
                                {
                                    blacklistCount = 0;
                                    BlacklistTimer.Reset();
                                    MasterContainer.resetEngage = false;
                                }

                                MasterContainer.StuckTimer.Reset();
                                return true;
                            }
                        }
                        else
                        {
                            Calls.SetTarget(0);
                        }
                    }
                }
                return false;
            }
        }

        public override string Name
        {
            get
            {
                return "Engaging";
            }
        }

        // higher number = higher priority
        public override int Priority
        {
            get
            {
                return 30;
            }
        }

        int blacklistCount = 0;
        cTimer BlacklistTimer = new cTimer(10000);
        cTimer OnRightClickTimer = new cTimer(500);
        public override void Run()
        {
            if (MasterContainer.engageGuid != ObjectManager.TargetObject.guid)
            {
                blacklistCount = 0;
                MasterContainer.engageGuid = ObjectManager.TargetObject.guid;
                BlacklistTimer.Reset();
            }
            if (BlacklistTimer.IsReady())
            {
                blacklistCount = blacklistCount + 1;
                if (blacklistCount == 1)
                {
                    MasterContainer.IsStuck = true;
                }
            }
            if (blacklistCount >= 2)
            {
                MasterContainer.blacklistGuid.Add(ObjectManager.TargetObject.guid);
                blacklistCount = 0;
                BlacklistTimer.Reset();
                Calls.SetTarget(0);
            }

            MasterFightMovement.Handle();
            SpellManager.CheckSpells();
            CCManager.FightPulse();
            Ingame.CastFinal();
            MasterContainer.AfterFight = true;
        }
    }
}
