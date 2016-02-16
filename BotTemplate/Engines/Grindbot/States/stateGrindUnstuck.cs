using System;
using BotTemplate.Interact;
using BotTemplate.Helper;
using BotTemplate.Constants;

namespace BotTemplate.Engines.Grindbot.States
{
    public class stateGrindUnstuck : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                return GrindbotContainer.IsStuck && (!ObjectManager.isDeath && !ObjectManager.isGhost);
            }
        }

        public override string Name
        {
            get
            {
                return "Unstucking";
            }
        }

        // higher number = higher priority
        public override int Priority
        {
            get
            {
                return 90;
            }
        }

        internal void ResetBools()
        {
            GrindbotContainer.firstBool = false;
            GrindbotContainer.secondBool = false;
            GrindbotContainer.thirdBool = false;
            GrindbotContainer.fourthBool = false;
            GrindbotContainer.fifthBool = false;
            GrindbotContainer.someBool = false;
        }

        cTimer moveBackTimer = new cTimer(2000);
        cTimer strafeTimer = new cTimer(2000);

        void ChooseRandom(out string start, out string stop)
        {
            Random rnd = new Random();
            int rndInt = rnd.Next(1, 3);

            if (rndInt == 1)
            {
                start = "StrafeLeftStart()";
                stop = "StrafeLeftStop()";
            }
            else
            {
                start = "StrafeRightStart()";
                stop = "StrafeRightStop()";
            }
        }

        string strafeStart = "";
        string strafeStop = "";

        cTimer jumpTimer = new cTimer(500);
        public override void Run()
        {
            if (GrindbotContainer.firstBool == false)
            {
                Calls.StopRunning();
                GrindbotContainer.firstBool = true;
                GrindbotContainer.someBool = true;
            }
            else
            {
                if (Calls.MovementIsOnly((uint)Offsets.movementFlags.None) && GrindbotContainer.someBool == true)
                {
                    GrindbotContainer.secondBool = true;
                    GrindbotContainer.someBool = false;
                }

                if (GrindbotContainer.secondBool == true)
                {
                    Ingame.moveBackwards();
                    moveBackTimer.Reset();
                    GrindbotContainer.thirdBool = true;
                    GrindbotContainer.secondBool = false;
                }

                if (moveBackTimer.IsReady() && GrindbotContainer.thirdBool == true)
                {
                    Ingame.moveBackwards();
                    GrindbotContainer.thirdBool = false;
                    GrindbotContainer.fourthBool = true;
                    ChooseRandom(out strafeStart, out strafeStop);
                    Calls.DoString(strafeStart);
                    strafeTimer.Reset();
                }

                if (GrindbotContainer.fourthBool == true && strafeTimer.IsReady())
                {
                    Calls.DoString(strafeStop);
                    GrindbotContainer.fifthBool = true;
                    GrindbotContainer.fourthBool = false;
                }
            }

            if (GrindbotContainer.fifthBool == true)
            {
                GrindbotContainer.IsStuck = false;
                ResetBools();
            }

            if (jumpTimer.IsReady())
            {
                Ingame.Jump();
            }

        }
    }
}
