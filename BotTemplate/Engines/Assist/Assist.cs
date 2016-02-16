using BotTemplate.Engines.CustomClass;
using BotTemplate.Interact;
using BotTemplate.Engines.Assist.States;
using System.Windows.Forms;
namespace BotTemplate.Engines.Assist
{
    internal class Assist
    {
        internal static Engine engine;

        internal static bool Init()
        {
            bool ccLoaded = CCManager.ChooseCustomClassByWowClass(ObjectManager.playerClass);
            if (ccLoaded)
            {
                Calls.DoString("ConsoleExec('Autointeract 0')");
                Calls.DoString("CameraZoomIn(50)");
                engine = new Engine();
                engine.States.Add(new stateAssistIdle());
                engine.States.Add(new stateAssistWalk());
                engine.States.Add(new stateAssistVendor());
                engine.States.Add(new stateAssistNeedRest());
                engine.States.Add(new stateAssistFight());
                engine.States.Add(new stateAssistDeath());
                engine.States.Add(new stateAssistDc());
                engine.States.Add(new stateAssistBuff());
                engine.States.Add(new stateAssistWait());
                engine.States.Add(new stateTeleToMaster());
                return true;
            }
            else
            {
                MessageBox.Show("No CC found");
            }
            return false;
        }

        internal static string name = "Assist";

        internal static void Dispose()
        {
            engine.StopEngine();
            engine = null;
        }
    }
}
