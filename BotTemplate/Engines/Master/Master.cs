using BotTemplate.Engines.Master.States;
using BotTemplate.Engines.CustomClass;
using BotTemplate.Interact;
using System.Windows.Forms;
using BotTemplate.Helper;
namespace BotTemplate.Engines.Master
{
    internal class Master
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

                MasterContainer.Reset();
                Calls.DoString("ConsoleExec('Autointeract 0')");
                Calls.DoString("CameraZoomIn(50)");
                Data.curWp = MasterFunctions.GetClosestWaypoint(0);
                engine = new Engine();
                engine.States.Add(new stateMasterIdle());
                engine.States.Add(new stateMasterWalk());
                engine.States.Add(new stateMasterGetTarget());
                engine.States.Add(new stateGrindEngage());
                engine.States.Add(new stateMasterNeedRest());
                engine.States.Add(new stateMasterLoot());
                engine.States.Add(new stateMasterFight());
                engine.States.Add(new stateMasterUnstuck());
                engine.States.Add(new stateMasterDc());
                engine.States.Add(new stateMasterBuff());
                engine.States.Add(new stateMasterDeath());
                engine.States.Add(new stateMasterVendor());
                engine.States.Add(new stateMasterWaitForSlaves());
                return true;
            }
            else
            {
                MessageBox.Show("No CC found");
            }
            return false;
        }

        internal static string name = "Master";

        internal static void Dispose()
        {
            engine.StopEngine();
            engine = null;
        }
    }
}
