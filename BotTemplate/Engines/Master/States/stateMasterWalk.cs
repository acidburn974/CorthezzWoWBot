using BotTemplate.Interact;
using BotTemplate.Helper;
using BotTemplate.Constants;

namespace BotTemplate.Engines.Master.States
{
    public class stateMasterWalk : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                if (MasterContainer.AfterFight == true)
                {
                    if (ObjectManager.playerClass == (uint)Offsets.classIds.Warlock
                        || ObjectManager.playerClass == (uint)Offsets.classIds.Hunter)
                    {
                        Ingame.PetFollow();
                    }
                    Data.curWp = MasterFunctions.GetClosestWaypoint(Data.curWp);
                    StuckCounter = 0;
                    StuckTimer.Reset();
                    MasterContainer.BlackListTimer.Reset();
                    MasterContainer.AfterFight = false;
                    return true;
                }

                if (Data.Profile[Data.curWp].differenceToPlayer() < 5)
                {
                    if (Data.curWp == Data.wpCount - 1)
                    {
                        Data.Profile.Reverse();
                        Data.curWp = 0;
                    }
                    else
                    {
                        Data.curWp = Data.curWp + 1;
                    }

                    StuckCounter = 0;
                    StuckTimer.Reset();
                    return true;
                }

                if (!Calls.MovementContainsFlag((uint)Offsets.movementFlags.Forward) || !Calls.IsFacing(Data.Profile[Data.curWp]))
                {
                    return true;
                }

                return false;
            }
        
        }

        public override string Name
        {
            get
            {
                return "Walk";
            }
        }

        // higher number = higher priority
        public override int Priority
        {
            get
            {
                return 10;
            }
        }

        int StuckCounter = 0;
        cTimer StuckTimer
        {
            get
            {
                return MasterContainer.StuckTimer;
            }
        }
        int CurWp = 0;
        public override void Run()
        {
            if (CurWp != Data.curWp)
            {
                CurWp = Data.curWp;
                StuckCounter = 0;
                StuckTimer.Reset();
            }
            else
            {
                if (StuckTimer.IsReady())
                {
                    StuckCounter = StuckCounter + 1;
                }

                if (StuckCounter >= 1)
                {
                    if (StuckCounter >= 2)
                    {
                        MasterContainer.IsStuck = true;
                        StuckCounter = 0;
                    }
                }
            }
            Ingame.moveForward();
            if (!Calls.IsFacing(Data.Profile[Data.curWp]))
            {
                Calls.TurnCharacter(Data.Profile[Data.curWp]);
            }
        }
    }
}
