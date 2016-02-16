using BotTemplate.Interact;
using BotTemplate.Helper;
using BotTemplate.Constants;
using BotTemplate.Engines.Assist;
using BotTemplate.Engines.Networking;
using BotTemplate.Forms;

namespace BotTemplate.Engines.Assist.States
{
    public class stateAssistWalk : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                return true;
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

        float tmp;
        bool bool1 = false;
        cTimer diff = new cTimer(5000);
        public override void Run()
        {
            if (AssistContainer.AfterFight)
            {
                diff.Reset();
                bool1 = false;
                Ingame.PetFollow();
                AssistContainer.AfterFight = false;
            }

            if (AssistContainer.leader.baseAdd != 0)
            {
                tmp = AssistContainer.leader.Pos.differenceToPlayer();
                if (tmp < 3)
                {
                    clientConnect.requestResume();
                    Calls.StopRunning();
                    diff.Reset();
                    bool1 = false;
                }
                else
                {
                    if (ObjectManager.leader.movementState == 0x80000000)
                    {
                        if (!bool1)
                        {
                            diff.Reset();
                            bool1 = true;
                        }
                        else
                        {
                            if (diff.IsReady())
                            {
                                AssistContainer.forceTele = true;
                            }
                        }
                    }
                    else
                    {
                        diff.Reset();
                    }
                    Ingame.moveForward();
                }
                if (!Calls.IsFacing(AssistContainer.leader.Pos))
                {
                    Calls.TurnCharacter(AssistContainer.leader.Pos);
                }
            }
        }
    }
}
