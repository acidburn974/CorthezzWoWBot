using System;
using System.Collections.Generic;
using BotTemplate.Interact;
using BotTemplate.Helper;

namespace BotTemplate.Engines.Grindbot
{
    internal static class GrindbotContainer
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
