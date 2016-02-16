using System.Threading;
using BotTemplate.Interact;
using System;
using System.Collections.Generic;
using BotTemplate.Helper;

namespace BotTemplate.Engines.Stockades.States
{
    public class stateStockOutside : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                return StockadesContainer.isOutside && ObjectManager.playerPtr != 0;
            }
        }

        public override string Name
        {
            get
            {
                return "Outside";
            }
        }

        // higher number = higher priority
        public override int Priority
        {
            get
            {
                return 70;
            }
        }

        Objects.UnitObject spiritHealer;
        Objects.Location spiritHealerPos = new Objects.Location(-9340.824f, 165.019f, 61.55899f);
        Objects.UnitObject vendor;
        public override void Run()
        {
            if (ObjectManager.FreeBagSlots > 3)
            {
                StockadesContainer.done = false;
                Calls.DoString("ResetInstances();");
                StockadesContainer.doOncePerRun = true;
                Ingame.Tele(StockadesContainer.posZoneIn1, 60, true);
                Ingame.setCoords(StockadesContainer.posZoneIn2);
                while (StockadesContainer.isOutside && ObjectManager.FreeBagSlots > 3 && !Ingame.IsDc()) Thread.CurrentThread.Join(10);
            }
            else
            {
                if (ObjectManager.isGhost)
                {
                    Ingame.Tele(spiritHealerPos, 5, false);
                    spiritHealer = ObjectManager.GetUnitByName("Spirit Healer");
                    if (spiritHealer.baseAdd != 0)
                    {
                        Calls.OnRightClickUnit(spiritHealer.baseAdd, 0);
                        Thread.CurrentThread.Join(250);
                        while (ObjectManager.isGhost && !Ingame.IsDc())
                        {
                            Calls.OnRightClickUnit(spiritHealer.baseAdd, 0);
                            Thread.CurrentThread.Join(500);
                            Calls.DoString("AcceptXPLoss();");
                            Thread.CurrentThread.Join(500);
                        }
                    }
                }
                else
                {
                    Ingame.Tele(StockadesContainer.posVendor, 60, false);
                    vendor = ObjectManager.GetUnitByName(StockadesContainer.nameVendor);
                    if (vendor.baseAdd != 0)
                    {
                        Calls.OnRightClickUnit(vendor.baseAdd, 1);
                        if (Ingame.IsVendorFrameOpen()) Ingame.SellAll();
                    }

                }
            }
        }
    }
}
