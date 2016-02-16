using System.Threading;

namespace BotTemplate.Engines.Assist.States
{
    public class stateAssistIdle : State
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
