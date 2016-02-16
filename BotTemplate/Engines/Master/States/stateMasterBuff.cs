using BotTemplate.Helper;
using BotTemplate.Interact;
using BotTemplate.Engines.CustomClass;

namespace BotTemplate.Engines.Master.States
{
    public class stateMasterBuff : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                if (!CCManager.IsBuffed() || ObjectManager.IsCasting || ObjectManager.PlayerObject.isChanneling != 0)
                {
                    MasterContainer.BlackListTimer.Reset();
                    return true;
                }
                return false;
            }
        }

        public override string Name
        {
            get
            {
                return "Buffing";
            }
        }

        // higher number = higher priority
        public override int Priority
        {
            get
            {
                return 50;
            }
        }

        public override void Run()
        {
        }
    }
}
