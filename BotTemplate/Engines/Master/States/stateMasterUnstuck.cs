using System;
using BotTemplate.Interact;
using BotTemplate.Helper;
using BotTemplate.Constants;

namespace BotTemplate.Engines.Master.States
{
    public class stateMasterUnstuck : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                return MasterContainer.IsStuck && (!ObjectManager.isDeath && !ObjectManager.isGhost);
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
            MasterContainer.firstBool = false;
            MasterContainer.secondBool = false;
            MasterContainer.thirdBool = false;
            MasterContainer.fourthBool = false;
            MasterContainer.fifthBool = false;
            MasterContainer.someBool = false;
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
            if (MasterContainer.firstBool == false)
            {
                Calls.StopRunning();
                MasterContainer.firstBool = true;
                MasterContainer.someBool = true;
            }
            else
            {
                if (Calls.MovementIsOnly((uint)Offsets.movementFlags.None) && MasterContainer.someBool == true)
                {
                    MasterContainer.secondBool = true;
                    MasterContainer.someBool = false;
                }

                if (MasterContainer.secondBool == true)
                {
                    Ingame.moveBackwards();
                    moveBackTimer.Reset();
                    MasterContainer.thirdBool = true;
                    MasterContainer.secondBool = false;
                }

                if (moveBackTimer.IsReady() && MasterContainer.thirdBool == true)
                {
                    Ingame.moveBackwards();
                    MasterContainer.thirdBool = false;
                    MasterContainer.fourthBool = true;
                    ChooseRandom(out strafeStart, out strafeStop);
                    Calls.DoString(strafeStart);
                    strafeTimer.Reset();
                }

                if (MasterContainer.fourthBool == true && strafeTimer.IsReady())
                {
                    Calls.DoString(strafeStop);
                    MasterContainer.fifthBool = true;
                    MasterContainer.fourthBool = false;
                }
            }

            if (MasterContainer.fifthBool == true)
            {
                MasterContainer.IsStuck = false;
                Calls.StopRunning();
                ResetBools();
            }

            //if (jumpTimer.IsReady())
            //{
            //    Ingame.Jump();
            //}
        }
    }
}
