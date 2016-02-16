using System.Threading;

namespace BotTemplate.Engines.Master.States
{
    public class stateMasterIdle : State
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
