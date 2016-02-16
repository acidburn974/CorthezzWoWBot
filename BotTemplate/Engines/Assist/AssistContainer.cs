using System;
using System.Collections.Generic;
using BotTemplate.Interact;
using BotTemplate.Helper;
using BotTemplate.Constants;
using BotTemplate.Engines.Networking;
using System.Threading;
using BotTemplate.Forms;

namespace BotTemplate.Engines.Assist
{
    internal static class AssistContainer
    {
        internal static bool forceTele = false;

        internal static bool AfterFight = true;
        internal static cTimer fightWait = new cTimer(250);
        
        internal static Objects.UnitObject leader;
        internal static bool teleToMaster()
        {
            try
            {
                leader = ObjectManager.leader;
                if (leader.Pos.differenceToPlayer() > 30 || forceTele)
                {
                    Objects.Location tmp = clientConnect.requestCoords();
                    if (tmp.x != 0 && tmp.y != 0 && tmp.z != 0)
                    {
                        Calls.StopRunning();
                        Ingame.DismissPet();
                        Thread.CurrentThread.Join(2000);
                        Ingame.Tele(tmp, 60, false);
                        forceTele = false;
                        return true;
                    }
                }
            }
            catch
            {
            }
            forceTele = false;
            return false;
        }

        private static cTimer lagWait = new cTimer(2000);
        private static uint lastNumber = 0;
        internal static bool isLagSpike()
        {
            if (Calls.MovementContains(ObjectManager.leader.movementState, (uint)Offsets.movementFlags.Back)
                || Calls.MovementContains(ObjectManager.leader.movementState, (uint)Offsets.movementFlags.Forward))
            {
                if (lastNumber != ObjectManager.lastMovemmentPacket)
                {
                    lagWait.autoReset = false;
                    lagWait.Reset();
                    lastNumber = ObjectManager.lastMovemmentPacket;
                    return false;
                }
                else
                {
                    if (lagWait.IsReady())
                    {
                        return true;
                    }
                    return false;
                }
            }
            lagWait.autoReset = false;
            lagWait.ResetOnly();
            return false;
        }

        internal static void reset()
        {
            
        }
    }
}
