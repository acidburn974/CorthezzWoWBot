using BotTemplate.Interact;
using BotTemplate.Helper;
using System.Threading;

namespace BotTemplate.Engines.Master.States
{
    public class stateMasterDc : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                return (Ingame.IsDc() || gotDc == true);
            }
        }
            
        

        public override string Name
        {
            get
            {
                return "Disconnect";
            }
        }

        // higher number = higher priority
        public override int Priority
        {
            get
            {
                return 100;
            }
        }

        bool gotDc = false;
        bool firstBool = true;
        int failTrys = 0;
        cTimer DcTimer = new cTimer(2000);
        cTimer EnterWorldTimer = new cTimer(20000);
        cTimer tmpTimer;
        public override void Run()
        {
            if (firstBool)
            {
                tmpTimer = new cTimer(10000);
                while (!tmpTimer.IsReady() && ObjectManager.playerPtr == 0) Thread.CurrentThread.Join(100);
                
                Ingame.BackToLogin();
                tmpTimer = new cTimer(10000);
                while (!tmpTimer.IsReady() && ObjectManager.playerPtr == 0) Thread.CurrentThread.Join(100);
                firstBool = false;
                failTrys = 0;
            }

            if (Ingame.IsDc())
            {
                if (ObjectManager.LoginState == "login")
                {
                    Calls.DoString("DefaultServerLogin('" + Data.AccName + "', '" + Data.AccPw + "');");

                    tmpTimer = new cTimer(600000);
                    while (!tmpTimer.IsReady() && ObjectManager.playerPtr == 0 && ObjectManager.LoginState == "login") Thread.CurrentThread.Join(100);
                    failTrys = failTrys + 1;

                    if (failTrys >= 2)
                    {
                        firstBool = true;
                    }
                }

                if (ObjectManager.LoginState == "charselect")
                {
                    Calls.DoString("EnterWorld();");
                    EnterWorldTimer.Reset();
                    while (!EnterWorldTimer.IsReady() && ObjectManager.playerPtr == 0) Thread.CurrentThread.Join(100);
                }
                gotDc = true;
            }
            else
            {
                if (ObjectManager.playerPtr != 0)
                {
                    Thread.CurrentThread.Join(3000);
                    Calls.SetMovementFlags(0);
                    gotDc = false;
                    firstBool = true;
                    MasterContainer.StuckTimer.Reset();
                    MasterContainer.IsStuck = false;
                    MasterContainer.resetEngage = true;
                    ObjectManager.ExecuteOnce = true;
                    ChatReader.ClearChat = true;
                    Calls.DoString("SetLootMethod('freeforall');");
                }
            }
        }
    }
}
