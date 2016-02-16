using BotTemplate.Interact;
using BotTemplate.Helper;
using BotTemplate.Constants;

namespace BotTemplate.Engines.Grindbot.States
{
    public class stateGrindWalk : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                if (GrindbotContainer.AfterFight == true)
                {
                    if (ObjectManager.playerClass == (uint)Offsets.classIds.Warlock
                        || ObjectManager.playerClass == (uint)Offsets.classIds.Hunter)
                    {
                        Ingame.PetFollow();
                    }

                    Data.curWp = GrindbotFunctions.GetClosestWaypoint(Data.curWp);
                    StuckCounter = 0;
                    StuckTimer.Reset();
                    GrindbotContainer.BlackListTimer.Reset();
                    GrindbotContainer.AfterFight = false;
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
                return GrindbotContainer.StuckTimer;
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
                        GrindbotContainer.IsStuck = true;
                        StuckCounter = 0;
                    }
                }
            }
            Ingame.moveForward();
            Calls.TurnCharacter(Data.Profile[Data.curWp]);
        }
    }
}
