using BotTemplate.Engines.Grindbot.States;
using BotTemplate.Engines.CustomClass;
using BotTemplate.Interact;
using System.Windows.Forms;
using BotTemplate.Helper;
namespace BotTemplate.Engines.Grindbot
{
    internal class Grindbot
    {
        internal static Engine engine;

        internal static bool Init()
        {
            bool ccLoaded = CCManager.ChooseCustomClassByWowClass(ObjectManager.playerClass);
            bool profileLoaded = Data.getProfile();

            if (ccLoaded && profileLoaded)
            {
                string txt = "Port to first waypoint?";
                DialogResult dialogResult = MessageBox.Show(txt, "Port", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Ingame.Tele(Data.Profile[0], 60, false);
                }
                GrindbotContainer.Reset();
                Calls.DoString("ConsoleExec('Autointeract 0')");
                Data.curWp = GrindbotFunctions.GetClosestWaypoint(0);
                engine = new Engine();
                engine.States.Add(new stateGrindIdle());
                engine.States.Add(new stateGrindWalk());
                engine.States.Add(new stateGrindGetTarget());
                engine.States.Add(new stateGrindEngage());
                engine.States.Add(new stateGrindNeedRest());
                engine.States.Add(new stateGrindLoot());
                engine.States.Add(new stateGrindFight());
                engine.States.Add(new stateGrindUnstuck());
                engine.States.Add(new stateGrindDc());
                engine.States.Add(new stateGrindBuff());
                engine.States.Add(new stateGrindDeath());
                engine.States.Add(new stateGrindVendor());
                return true;
            }
            else
            {
                MessageBox.Show("No CC found");
            }
            return false;
        }

        internal static string name = "Grindbot";

        internal static void Dispose()
        {
            engine.StopEngine();
            engine = null;
        }
    }
}
