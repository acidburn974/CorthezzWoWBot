using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotTemplate.Interact;
using BotTemplate.Engines;
using BotTemplate.Helper;

namespace BotTemplate.Engines.Master
{
    internal static class MasterFunctions
    {
        #region get next target
        internal static UInt64 GetNextTarget(out bool gotTarget)
        {
            gotTarget = false;
            int nearestMobIndex = 0;
            float nearestDiff = float.MaxValue;
            List<Objects.UnitObject> tmpUnits = ObjectManager.UnitObjectList;
            for (int i = 0; i < tmpUnits.Count; i = i + 1)
            {
                if (tmpUnits[i].isUnit && !tmpUnits[i].isPlayerPet && Data.Faction.Contains(tmpUnits[i].factionId))
                {
                    if (tmpUnits[i].healthPercent == 100 && !tmpUnits[i].isTapped)
                    {
                        float DiffToMob = tmpUnits[i].Pos.differenceToPlayer();

                        if (DiffToMob < Data.searchRange)
                        {
                            if (DiffToMob < nearestDiff)
                            {
                                if (!MasterContainer.blacklistGuid.Contains(tmpUnits[i].guid))
                                {
                                    nearestDiff = DiffToMob;
                                    nearestMobIndex = i;
                                    gotTarget = true;
                                }

                            }
                        }
                    }
                }
            }
            return tmpUnits[nearestMobIndex].guid;
        }
        #endregion

        #region get closest wp
        internal static int GetClosestWaypoint(int curWp)
        {
            int smallestIndex = 0;
            float smallestDistance = float.MaxValue;
            for (int i = curWp; i < Data.Profile.Count; i++)
            {
                float DiffToPlayer = Data.Profile[i].differenceToPlayer();
                if (DiffToPlayer < smallestDistance)
                {
                    smallestDistance = DiffToPlayer;
                    smallestIndex = i;
                }
            }
            return smallestIndex;
        }
        #endregion
    }
}
