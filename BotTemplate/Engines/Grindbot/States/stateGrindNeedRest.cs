using BotTemplate.Helper;
using BotTemplate.Interact;
using BotTemplate.Engines.CustomClass;
using BotTemplate.Constants;

namespace BotTemplate.Engines.Grindbot.States
{
    public class stateGrindNeedRest : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                if (!Calls.MovementContainsFlag((uint)Offsets.movementFlags.Swimming))
                {
                    if (Data.needHealth || Data.needMana)
                    {
                        GrindbotContainer.StuckTimer.Reset();
                        GrindbotContainer.BlackListTimer.Reset();
                        return true;
                    }

                    if (IsWaitingForHealth || IsWaitingForMana)
                    {
                        GrindbotContainer.StuckTimer.Reset();
                        GrindbotContainer.BlackListTimer.Reset();
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
                return "Need rest";
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

        bool IsWaitingForMana = false;
        bool IsWaitingForHealth = false;
        cTimer UseRestItemTimer = new cTimer(500);
        public override void Run()
        {
            if (!Calls.MovementIsOnly((uint)Offsets.movementFlags.None))
            {
                Calls.StopRunning();
            }

            if (Data.needHealth)
            {
                IsWaitingForHealth = true;
            }

            if (Data.needMana)
            {
                IsWaitingForMana = true;
            }

            if (UseRestItemTimer.IsReady())
            {
                if (IsWaitingForHealth == true)
                {
                    if (ObjectManager.PlayerHealthPercent > 90)
                    {
                        IsWaitingForHealth = false;
                    }
                    else
                    {
                        if (Data.UseCcRest)
                        {
                            CCManager.RestHealth(IsWaitingForMana);
                        }
                        else
                        {
                            Ingame.UseFood();
                        }
                    }
                }

                if (IsWaitingForMana == true)
                {
                    if (ObjectManager.PlayerObject.manaPercent > 95)
                    {
                        IsWaitingForMana = false;
                    }
                    else
                    {
                        if (Data.UseCcRest)
                        {
                            CCManager.RestMana(IsWaitingForHealth);
                        }
                        else
                        {
                            Ingame.UseDrink();
                        }
                    }
                }
            }
        }
    }
}
