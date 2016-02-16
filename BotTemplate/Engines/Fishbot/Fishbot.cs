using BotTemplate.Engines.Fishbot.States;
using BotTemplate.Engines.CustomClass;
using BotTemplate.Interact;
using System.Windows.Forms;

namespace BotTemplate.Engines.Fishbot
{
    internal class Fishbot
    {
        internal static Engine engine;

        internal static bool Init()
        {
            Calls.DoString("ConsoleExec('Autointeract 0')");
            engine = new Engine();
            engine.States.Add(new stateFisherCastFishing());
            engine.States.Add(new stateFisherWaitForBite());
            return true;

        }

        internal static string name = "Fishbot";

        internal static void Dispose()
        {
            engine.StopEngine();
            engine = null;
        }
    }
}
