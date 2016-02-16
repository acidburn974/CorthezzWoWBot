using System;
using BotTemplate.Interact;
using BotTemplate.Helper;

namespace BotTemplate.Engines.Master.States
{
    public class stateMasterGetTarget : State
    {
        // needs the state to get executed?
        cTimer TargetSearchTimer = new cTimer(1000);
        public override bool NeedToRun
        {
            get
            {
                if (TargetSearchTimer.IsReady())
                {
                    if (!Data.needHealth || !Data.needMana)
                    {
                        return (Data.Profile[Data.curWp].differenceToPlayer() < Data.roamAway);
                    }
                }
                return false;
            }
        }

        public override string Name
        {
            get
            {
                return "Get Target";
            }
        }

        // higher number = higher priority
        public override int Priority
        {
            get
            {
                return 20;
            }
        }

        public override void Run()
        {
            bool foundTarget;
            UInt64 NextTargetGuid = MasterFunctions.GetNextTarget(out foundTarget);
            if (foundTarget == true)
            {
                Calls.SetTarget(NextTargetGuid);
            }
        }
    }
}
