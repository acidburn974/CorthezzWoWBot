using BotTemplate.Helper;
using BotTemplate.Interact;
using BotTemplate.Engines.CustomClass;
using System.Threading;
using BotTemplate.Helper.SpellSystem;
using BotTemplate.Engines.Networking;

namespace BotTemplate.Engines.Master.States
{
    public class stateMasterVendor : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                return (Data.VendorItems && Data.gotVendor && !MasterContainer.StopVendor && (ObjectManager.FreeBagSlots <= Data.LeaveFreeSlots || IsVendoring));
            }
        }

        public override string Name
        {
            get
            {
                return "Vendoring";
            }
        }

        // higher number = higher priority
        public override int Priority
        {
            get
            {
                return 45;
            }
        }

        Objects.Location curPoint = new Objects.Location();
        bool locationSaved = false;
        bool IsVendoring = false;
        int failCounter = 0;
        int failCounter2 = 0;

        internal void GoBack()
        {
            IsVendoring = false;
            locationSaved = false;
            Ingame.Tele(curPoint, 60, false);
            MasterContainer.StuckTimer.Reset();
            MasterContainer.IsStuck = false;
            slaveStates.returnCoords = true;
        }

        public override void Run()
        {
            slaveStates.returnCoords = false;
            MasterContainer.StuckTimer.Reset();
            MasterContainer.IsStuck = false;
            MasterContainer.resetEngage = true;
            Calls.StopRunning();
            Thread.CurrentThread.Join(1000);
            if (Calls.MovementIsOnly(0x0))
            {
                if (!locationSaved)
                {
                    MasterContainer.StuckTimer.Reset();
                    curPoint = new Objects.Location(ObjectManager.PlayerObject.Pos.x, ObjectManager.PlayerObject.Pos.y, ObjectManager.PlayerObject.Pos.z);
                    locationSaved = true;
                    IsVendoring = true;
                    failCounter = 0;
                    failCounter2 = 0;
                }
                else
                {
                    if (Data.VendorLocation.differenceToPlayer() > 2)
                    {
                        if (ObjectManager.playerClass == (uint)Constants.Offsets.classIds.Warlock || ObjectManager.playerClass == (uint)Constants.Offsets.classIds.Hunter)
                        {
                            if (Ingame.GotPet())
                            {
                                Ingame.DismissPet();
                            }
                            else
                            {
                                Ingame.Tele(Data.VendorLocation, 60, false);
                            }
                        }
                        else
                        {
                            Ingame.Tele(Data.VendorLocation, 60, false);
                        }
                    }
                    else
                    {
                        if (ObjectManager.playerClass == (byte)Constants.Offsets.classIds.Druid)
                        {
                            SpellManager.CheckSpells();
                            if (Ingame.druidIsBear()) Ingame.Cast("Bear Form", false);
                            if (Ingame.druidIsCat()) Ingame.Cast("Cat Form", false);
                            Ingame.CastFinal();
                        }

                        if (Ingame.IsVendorFrameOpen())
                        {
                            Ingame.SellAllBut(Data.ProtectedItems);

                            cTimer waitTimer = new cTimer(1000);
                            while (!waitTimer.IsReady()) Thread.CurrentThread.Join(100);
                            failCounter2 = failCounter2 + 1;

                            if (Data.gotVendor && ObjectManager.FreeBagSlots <= Data.LeaveFreeSlots)
                            {
                                if (failCounter2 >= 20)
                                {
                                    MasterContainer.StopVendor = true;
                                    if (Data.StopOnVendorFail)
                                    {
                                        Master.engine.StopEngine();
                                        Master.Dispose();
                                    }
                                    else
                                    {
                                        GoBack();
                                    }
                                }
                            }
                            else
                            {
                                GoBack();
                            }
                        }
                        else
                        {
                            Objects.UnitObject tmpObj = ObjectManager.GetUnitByName(Data.VendorName);
                            if (tmpObj.baseAdd != 0)
                            {
                                Calls.OnRightClickUnit(tmpObj.baseAdd, 1);
                            }
                            else
                            {
                                failCounter = failCounter + 1;
                            }

                            cTimer waitTimer = new cTimer(1000);
                            while (!waitTimer.IsReady()) Thread.CurrentThread.Join(100);

                            if (failCounter >= 6)
                            {
                                MasterContainer.StopVendor = true;
                                GoBack();
                            }
                        }
                    }
                }
            }
            else
            {
                Calls.StopRunning();
            }

        }
    }
}
