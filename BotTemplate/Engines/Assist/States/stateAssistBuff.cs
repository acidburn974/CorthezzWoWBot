using BotTemplate.Helper;
using BotTemplate.Interact;
using BotTemplate.Engines.CustomClass;
using BotTemplate.Engines.Networking;

namespace BotTemplate.Engines.Assist.States
{
    public class stateAssistBuff : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                if (!CCManager.IsBuffed() || ObjectManager.IsCasting || ObjectManager.PlayerObject.isChanneling != 0)
                {
                    AssistContainer.AfterFight = true;
                    clientConnect.requestWait();
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
