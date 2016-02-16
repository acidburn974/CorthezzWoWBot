using System.Threading;
using BotTemplate.Interact;
using System;

namespace BotTemplate.Engines.Fishbot.States
{
    public class stateFisherCastFishing : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                return !ObjectManager.PlayerObject.isFishing;
            }
        }

        public override string Name
        {
            get
            {
                return "Cast Fishing";
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

        int lastCast = 0;
        public override void Run()
        {
            if (Environment.TickCount - lastCast >= 1500)
            {
                Calls.DoString("CastSpellByName('Fishing')");
                lastCast = Environment.TickCount;
            }
        }
    }
}
