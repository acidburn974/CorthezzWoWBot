using BotTemplate.Engines.Stockades.States;
using BotTemplate.Engines.CustomClass;
using BotTemplate.Interact;
using System.Windows.Forms;

namespace BotTemplate.Engines.Stockades
{
    internal class Stockades
    {
        internal static Engine engine;

        internal static bool Init()
        {
            Calls.DoString("ConsoleExec('Autointeract 0')");
            Calls.DoString("CameraZoomIn(50)");
            engine = new Engine();
            engine.States.Add(new stateStockIdle());
            engine.States.Add(new stateStockInside());
            engine.States.Add(new stateStockOutside());
            engine.States.Add(new stateStockDc());
            return true;

        }

        internal static string name = "Stockades";

        internal static void Dispose()
        {
            engine.StopEngine();
            engine = null;
        }
    }
}
