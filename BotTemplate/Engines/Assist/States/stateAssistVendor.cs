using BotTemplate.Helper;
using BotTemplate.Interact;
using BotTemplate.Engines.CustomClass;
using System.Threading;
using BotTemplate.Helper.SpellSystem;

namespace BotTemplate.Engines.Assist.States
{
    public class stateAssistVendor : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                return false;
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
        }

        public override void Run()
        {
            if (Calls.MovementIsOnly(0x0))
            {
                if (!locationSaved)
                {
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
                                    GoBack();
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
