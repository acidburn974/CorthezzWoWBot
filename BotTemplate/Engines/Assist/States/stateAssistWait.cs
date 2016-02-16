using BotTemplate.Interact;
using BotTemplate.Helper;
using BotTemplate.Constants;
using BotTemplate.Engines.Assist;
using BotTemplate.Engines.Networking;
using System.Threading;

namespace BotTemplate.Engines.Assist.States
{
    public class stateAssistWait : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                return AssistContainer.leader.health == 0 || AssistContainer.leader.health == 1 || AssistContainer.leader.baseAdd == 0 || AssistContainer.isLagSpike();
            }
        
        }

        public override string Name
        {
            get
            {
                return "Waiting";
            }
        }

        // higher number = higher priority
        public override int Priority
        {
            get
            {
                return 15;
            }
        }

        public override void Run()
        {
            Thread.Sleep(1);
        }
    }
}
