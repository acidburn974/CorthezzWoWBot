using System.Threading;
using BotTemplate.Interact;
using System;
using System.Collections.Generic;

namespace BotTemplate.Engines.Stockades.States
{
    public class stateStockIdle : State
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
                return "Idle";
            }
        }

        // higher number = higher priority
        public override int Priority
        {
            get
            {
                return 1;
            }
        }

        public override void Run()
        {
            Thread.Sleep(1);    
        }
    }
}
