using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using BotTemplate.Interact;
using BotTemplate.Helper;

namespace BotTemplate.Engines
{
    internal class Engine
    {
        private Thread thrWorker;

        internal List<State> States { get; private set; }
        
        public Engine()
        {
            States = new List<State>();
            States.Sort();
        }

        public virtual void Pulse()
        {
            foreach (State state in States)
            {
                if (state.NeedToRun)
                {
                    Exchange.CurrentState = state.Name;
                    state.Run();
                    break;
                }
            }
        }

        public bool Running { get; private set; }

        public void StartEngine(string name)
        {
            States.Sort();
            Running = true;
            thrWorker = new Thread(Run) { IsBackground = true };
            thrWorker.Start();
            Exchange.CurrentEngine = name;
            Exchange.IsEngineRunning = true;
            BmWrapper.memory.WriteUInt(0x00C7B2A4, 0);
            ObjectManager.ExecuteOnce = true;
        }

        Stopwatch tickTimer = new Stopwatch();
        cTimer SetTickCount = new cTimer(1000);
        private void Run()
        {
            while (Running)
            {
                //try
                //{
                tickTimer.Start();
                Pulse();
                if (SetTickCount.IsReady())
                {
                    Calls.AntiAfk();
                }
                Exchange.tickRate = tickTimer.Elapsed.TotalMilliseconds;
                tickTimer.Reset();

                Thread.Sleep(150);
                //}
                //catch (Exception ex)
                //{
                //    Logging.OnNewLog("[FSM]: " + ex.Message);
                //}
                //Thread.Sleep((int)sleepTime);
            }
            BmWrapper.memory.WriteUInt(0x00C7B2A4, 0x0F110B73);
        }

        public void StopEngine()
        {
            if (!Running)
            {
                return;
            }
            Running = false;
            Exchange.IsEngineRunning = false;
            Exchange.CurrentEngine = "None";
            BmWrapper.memory.WriteUInt(0x00C7B2A4, 0x0F110B73);
        }
    }
}
