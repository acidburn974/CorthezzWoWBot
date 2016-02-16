using System;
using BotTemplate.Helper;
using BotTemplate.Interact;
using BotTemplate.Engines.CustomClass;
using BotTemplate.Helper.SpellSystem;
using BotTemplate.Engines.Networking;

namespace BotTemplate.Engines.Assist.States
{
    public class stateAssistFight : State
    {
        // needs the state to get executed?
        UInt64 leaderTarget;
        Objects.UnitObject attTarget;
        public override bool NeedToRun
        {
            get
            {
                if (AssistContainer.leader.baseAdd != 0)
                {
                    leaderTarget = AssistContainer.leader.targetGuid;
                    if (leaderTarget != 0)
                    {
                        attTarget = ObjectManager.GetUnitByGuid(leaderTarget);
                        if (attTarget.isUnit && !attTarget.isPlayerPet && ((!attTarget.isTapped && attTarget.healthPercent == 100) || ObjectManager.IsUnitOnGroup(attTarget.targetGuid, attTarget.isTapped, attTarget.healthPercent)))
                        {
                            if (ObjectManager.targetGuid != attTarget.guid)
                            {
                                Calls.SetTarget(attTarget.guid);
                            }
                            return true;
                        }
                    }
                }
                else if ((AssistContainer.leader.health == 0 || AssistContainer.leader.health == 1) && ObjectManager.targetGuid != 0)
                {
                    attTarget = ObjectManager.GetUnitByGuid(ObjectManager.targetGuid);
                    if (attTarget.isUnit && !attTarget.isPlayerPet && ObjectManager.IsUnitOnGroup(attTarget.targetGuid, attTarget.isTapped, attTarget.healthPercent))
                    {
                        return true;
                    }
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

        public override void Run()
        {
            {
                if (ObjectManager.IsUnitOnGroup(attTarget.targetGuid, attTarget.isTapped, attTarget.healthPercent))
                {
                    clientConnect.requestWait();
                }
                FightMovement.Handle();
                SpellManager.CheckSpells();
                CCManager.FightPulse();
                Ingame.CastFinal();
                AssistContainer.AfterFight = true;
            }
        }
    }
}
