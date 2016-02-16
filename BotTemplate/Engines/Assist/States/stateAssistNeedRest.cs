using BotTemplate.Helper;
using BotTemplate.Interact;
using BotTemplate.Engines.CustomClass;
using BotTemplate.Constants;
using System.IO;
using BotTemplate.Engines.Networking;

namespace BotTemplate.Engines.Assist.States
{
    public class stateAssistNeedRest : State
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
                        clientConnect.requestWait();
                        return true;
                    }

                    if (IsWaitingForHealth || IsWaitingForMana)
                    {
                        clientConnect.requestWait();
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
