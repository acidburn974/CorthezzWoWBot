using System.Threading;

namespace BotTemplate.Engines.Assist.States
{
    public class stateTeleToMaster : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                return AssistContainer.teleToMaster();
            }
        }

        public override string Name
        {
            get
            {
                return "Tele to master";
            }
        }

        // higher number = higher priority
        public override int Priority
        {
            get
            {
                return 75;
            }
        }

        public override void Run()
        {
            Thread.Sleep(1);
        }
    }
}
