using System.Threading;
using BotTemplate.Interact;
using System;
using System.Collections.Generic;

namespace BotTemplate.Engines.Fishbot.States
{
    public class stateFisherWaitForBite : State
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
                return "Waiting for Bite";
            }
        }

        // higher number = higher priority
        public override int Priority
        {
            get
            {
                return 80;
            }
        }

        bool foundBobber = false;
        Objects.GameObject bobber = new Objects.GameObject(0, 0, 0);
        public override void Run()
        {
            if (!foundBobber)
            {
                int count = 0;
                List<Objects.GameObject> tmpList = ObjectManager.GetGameObjectsCreatedByPlayer();
                foreach (Objects.GameObject x in tmpList)
                {
                    if (x.objectId == 35591)
                    {
                        count = count + 1;
                        bobber = x;
                    }
                }
                if (count == 1)
                {
                    foundBobber = true;
                }
            }
            else
            {
                if (bobber.isBusy == 1)
                {
                    Calls.OnRightClickObject(bobber.baseAdd, 1);
                    foundBobber = false;
                }
            }
            
        }
    }
}
