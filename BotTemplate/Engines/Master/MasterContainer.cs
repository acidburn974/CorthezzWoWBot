using System;
using System.Collections.Generic;
using BotTemplate.Interact;
using BotTemplate.Helper;
using BotTemplate.Engines.Networking;
using BotTemplate.Constants;

namespace BotTemplate.Engines.Master
{
    internal static class MasterContainer
    {
    
        internal static bool AfterFight = true;
        internal static bool IsStuck = false;
        internal static cTimer StuckTimer = new cTimer(10000);
        internal static UInt64 engageGuid = 0x0;
        internal static List<UInt64> blacklistGuid = new List<UInt64>();
        internal static bool StopVendor = false;
        internal static cTimer fightWait = new cTimer(250);
        internal static cTimer BlackListTimer = new cTimer(10000);
    

        internal static bool resetEngage = false;

        internal static void Reset()
        {
            AfterFight = true;
            IsStuck = false;
            firstBool = false;
            secondBool = false;
            thirdBool = false;
            fourthBool = false;
            fifthBool = false;
            someBool = false;
            engageGuid = 0x0;
            StopVendor = false;
        }

        private static cTimer lagWait = new cTimer(2000);
        private static uint lastNumber = 0;
        internal static bool isLagSpike()
        {
            if (Calls.MovementContains(ObjectManager.party1.movementState, (uint)Offsets.movementFlags.Back)
                || Calls.MovementContains(ObjectManager.party1.movementState, (uint)Offsets.movementFlags.Forward))
            {
                if (lastNumber != ObjectManager.lastMovemmentPacketParty1)
                {
                    lagWait.autoReset = false;
                    lagWait.Reset();
                    lastNumber = ObjectManager.lastMovemmentPacketParty1;
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

        #region for unstuck
        internal static bool firstBool = false;
        internal static bool secondBool = false;
        internal static bool thirdBool = false;
        internal static bool fourthBool = false;
        internal static bool fifthBool = false;
        internal static bool someBool = false;
        #endregion
    }
}
