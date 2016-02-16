using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BotTemplate.Interact;
using BotTemplate.Helper;

namespace BotTemplate.Engines.Explorer
{
    internal static class ExplorerEngine
    {
        internal static Explorer engine;
        internal static string name = "Explorer";
    }

    internal class Explorer
    {
        private Thread thrWorker;
        internal bool isRunning
        {
            get
            {
                return Running;
            }
        }
        private bool Running;

        internal void StopRunning()
        {
            Running = false;
            Calls.CancelTele();
        }

        internal void StartEngine(string name)
        {
            Running = true;
            thrWorker = new Thread(Run) { IsBackground = true };
            thrWorker.Start();
            Exchange.CurrentEngine = name;
            Exchange.IsEngineRunning = true;
        }

        private void Run()
        {
            if (Running)
            {

                float x = 0;
                float y = 0;
                float z = 0;
                float xStepDown = 0;
                float yStep = 0;

                if (ObjectManager.MapId == 1)
                {
                    x = 10808.122f;
                    y = 3186.583f;
                    z = 1327.806f;

                    xStepDown = 20308f;
                    yStep = 289f;
                }

                if (ObjectManager.MapId == 0)
                {
                    x = 3079.523f;
                    y = 1776.130f;
                    z = 73.872f;

                    xStepDown = 17000f;
                    yStep = 250f;
                }

                bool increase = false;
                Ingame.Tele(new Objects.Location(x, y, z), 5, true);
                int i = 0;
                int k = 0;
                while (k < 30 && ObjectManager.playerPtr != 0 && Running)
                {
                    if (i % 2 == 0)
                    {
                        if (!increase)
                        {
                            Calls.AntiAfk();
                            x = x - xStepDown;
                            increase = true;
                            Ingame.Tele(new Objects.Location(x, y, z), 5, true);
                        }
                        else
                        {
                            x = x + xStepDown;
                            increase = false;
                            Ingame.Tele(new Objects.Location(x, y, z), 5, true);
                        }
                    }
                    else
                    {
                        y = y - yStep;
                        Ingame.Tele(new Objects.Location(x, y, z), 5, true);
                        k = k + 1;
                    }
                    i = i + 1;
                }
            }
            Exchange.IsEngineRunning = false;
            Exchange.CurrentEngine = "None";
            ExplorerEngine.engine = null;
        }

    }
}
