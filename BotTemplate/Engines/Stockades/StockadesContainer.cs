using System;
using System.Collections.Generic;
using BotTemplate.Interact;
using BotTemplate.Helper;
using BotTemplate.Constants;
using BotTemplate.Engines.Networking;
using System.Threading;
using BotTemplate.Forms;

namespace BotTemplate.Engines.Stockades
{
    internal static class StockadesContainer
    {
        internal static bool isInside
        {
            get
            {
                return ObjectManager.MapId == 34;
            }
        }

        internal static bool isOutside
        {
            get
            {
                return ObjectManager.MapId == 0;
            }
        }

        internal static readonly Objects.Location posZoneOut = new Objects.Location(45.69411f, 0.445177f, -15.15261f);
        internal static readonly Objects.Location posZoneIn1 = new Objects.Location(-8764.83f, 846.075f, 300f);
        internal static readonly Objects.Location posZoneIn2 = new Objects.Location(-8762.974f, 846.876f, 86.7542f);
        internal static readonly Objects.Location posVendor = new Objects.Location(-8856f, 805.48f, 93.33456f);
        internal static readonly string nameVendor = "Jessara Cordell";
        internal static bool done = false;
        internal static bool doOncePerRun = false;

        
        internal static void reset()
        {
            
        }
    }
}
