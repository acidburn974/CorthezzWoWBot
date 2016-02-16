using BotTemplate.Helper;
using BotTemplate.Interact;
using BotTemplate.Engines.CustomClass;
using System.Threading;
using BotTemplate.Engines.Networking;

namespace BotTemplate.Engines.Master.States
{
    public class stateMasterWaitForSlaves : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                return (
                        (ObjectManager.party1Guid != 0 && (ObjectManager.party1.baseAdd == 0 || ObjectManager.party1.Pos.differenceToPlayer() > 18 || ObjectManager.party1.health == 0 || ObjectManager.party1.health == 1))
                        || (ObjectManager.party2Guid != 0 && (ObjectManager.party2.baseAdd == 0 || ObjectManager.party1.Pos.differenceToPlayer() > 18 || ObjectManager.party2.health == 0 || ObjectManager.party2.health == 1))
                        || (ObjectManager.party3Guid != 0 && (ObjectManager.party3.baseAdd == 0 || ObjectManager.party1.Pos.differenceToPlayer() > 18 || ObjectManager.party3.health == 0 || ObjectManager.party3.health == 1))
                        || (ObjectManager.party4Guid != 0 && (ObjectManager.party4.baseAdd == 0 || ObjectManager.party1.Pos.differenceToPlayer() > 18 || ObjectManager.party4.health == 0 || ObjectManager.party4.health == 1))
                        ||
                !slaveStates.slavesReady
                || MasterContainer.isLagSpike());
            }
        }

        public override string Name
        {
            get
            {
                return "Waiting for Slaves";
            }
        }

        // higher number = higher priority
        public override int Priority
        {
            get
            {
                return 42;
            }
        }

        public override void Run()
        {
            Calls.StopRunning();
            MasterContainer.BlackListTimer.Reset();
            MasterContainer.StuckTimer.Reset();
            MasterContainer.resetEngage = true;
            Thread.CurrentThread.Join(100);
        }
    }
}
