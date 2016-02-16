using System;
using System.Collections.Generic;
using BotTemplate.Helper;
using BotTemplate.Constants;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using BotTemplate.Engines;
using System.Runtime.InteropServices;
using System.Text;

namespace BotTemplate.Interact
{
    internal static class ObjectManager
    {


        private enum ObjTypes : byte
        {
            OT_NONE = 0,
            OT_ITEM = 1,
            OT_CONTAINER = 2,
            OT_UNIT = 3,
            OT_PLAYER = 4,
            OT_GAMEOBJ = 5,
            OT_DYNOBJ = 6,
            OT_CORPSE = 7,
        }

        #region Different variables read out of game
        internal static int characterCount()
        {
            try
            {
                return BmWrapper.memory.ReadInt((uint)Offsets.player.CharacterCount);
            }
            catch
            {
                return 0;
            }
        }
        
        internal static bool isFollowing(UInt64 parGuid)
        {
            try
            {
                if (parGuid == 0)
                {
                    if (BmWrapper.memory.ReadUInt(0x00C4D888) == 3)
                    {
                        if (BmWrapper.memory.ReadUInt64(0x00C4D980) == parGuid) return true;
                    }
                }
            }
            catch
            {

            }
            return false;
        }

        internal static int MapId
        {
            get
            {
                try
                {
                    return BmWrapper.memory.ReadInt(Offsets.baseAddress + (uint)Offsets.misc.MapId);
                }
                catch { return 0; }
            }
        }

        internal static Objects.Location CorpseLocation = new Objects.Location(0, 0, 3);

        internal static int TotalBags
        {
            get
            {
                int total = 0;
                if (Bag1.baseAdd != 0) total = total + 1;
                if (Bag2.baseAdd != 0) total = total + 1;
                if (Bag3.baseAdd != 0) total = total + 1;
                if (Bag4.baseAdd != 0) total = total + 1;
                return total;

            }
        }

        internal static int GetBagSlotsById(int bagid)
        {
            switch (bagid)
            {
                case 0:
                    return 16;

                case 1:
                    return Bag1.TotalSlots;

                case 2:
                    return Bag2.TotalSlots;

                case 3:
                    return Bag3.TotalSlots;

                case 4:
                    return Bag4.TotalSlots;

                default:
                    return 0;
            }
        }

        internal static int TotalBagSlots
        {
            get
            {
                return Bag1.TotalSlots + Bag2.TotalSlots + Bag3.TotalSlots + Bag4.TotalSlots + 16;
            }
        }

        private static int baseBagFreeSlots;
        internal static int FreeBagSlots
        {
            get
            {
                return Bag1.FreeSlots + Bag2.FreeSlots + Bag3.FreeSlots + Bag4.FreeSlots + baseBagFreeSlots;
            }
        }

        internal static UInt64 targetGuid
        {
            get
            {
                try
                {
                    return BmWrapper.memory.ReadUInt64((uint)Offsets.player.TargetGuid + Offsets.baseAddress);
                }
                catch { return 0; }
            }
        }

        internal static UInt64 leaderGuid
        {
            get
            {
                try
                {
                    return BmWrapper.memory.ReadUInt64((uint)Offsets.partyStuff.leaderGuid);
                }
                catch { return 0; }
            }
        }

        internal static Objects.UnitObject leader
        {
            get
            {
                return ObjectManager.GetPlayerByGuid(ObjectManager.leaderGuid);
            }
        }

        internal static UInt64 party1Guid
        {
            get
            {
                try
                {
                    return BmWrapper.memory.ReadUInt64((uint)Offsets.partyStuff.party1Guid);
                }
                catch { return 0; }
            }
        }

        internal static UInt64 party2Guid
        {
            get
            {
                try
                {
                    return BmWrapper.memory.ReadUInt64((uint)Offsets.partyStuff.party2Guid);
                }
                catch { return 0; }
            }
        }

        internal static UInt64 party3Guid
        {
            get
            {
                try
                {
                    return BmWrapper.memory.ReadUInt64((uint)Offsets.partyStuff.party3Guid);
                }
                catch { return 0; }
            }
        }

        internal static UInt64 party4Guid
        {
            get
            {
                try
                {
                    return BmWrapper.memory.ReadUInt64((uint)Offsets.partyStuff.party4Guid);
                }
                catch { return 0; }
            }
        }

        internal static float PlayerHealthPercent
        {
            get
            {
                try
                {
                    return ObjectManager.PlayerObject.healthPercent;
                }
                catch
                {
                    return 0;
                }
            }
        }

        internal static string playerName
        {
            get
            {
                try
                {
                    return BmWrapper.memory.ReadASCIIString((uint)Offsets.player.Name + Offsets.baseAddress, 10).Trim();
                }
                catch { return "-"; }
            }
        }

        internal static float Facing
        {
            get
            {
                try
                {
                    return BmWrapper.memory.ReadFloat(PlayerObject.baseAdd + 0x9C4);
                }
                catch { return 0; }
            }
        }

        private static UInt64 cpGuid = 0x0;
        internal static int GetComboPoints
        {
            get
            {
                if (playerClass == (byte)Offsets.classIds.Rogue || playerClass == (byte)Offsets.classIds.Warrior || playerClass == (byte)Offsets.classIds.Druid)
                {
                    uint ptr = playerPtr;
                    if (ptr != 0)
                    {
                        if (targetGuid == 0)
                        {
                            BmWrapper.memory.WriteByte(BmWrapper.memory.ReadUInt(ptr + (uint)Offsets.player.ComboPoints1) + (uint)Offsets.player.ComboPoints2, 0);
                            return 0;
                        }
                        else
                        {
                            int points = BmWrapper.memory.ReadByte(BmWrapper.memory.ReadUInt(ptr + (uint)Offsets.player.ComboPoints1) + (uint)Offsets.player.ComboPoints2);
                            if (points == 0)
                            {
                                cpGuid = targetGuid;
                                return points;
                            }
                            else
                            {
                                if (cpGuid != targetGuid)
                                {
                                    BmWrapper.memory.WriteByte(BmWrapper.memory.ReadUInt(ptr + (uint)Offsets.player.ComboPoints1) + (uint)Offsets.player.ComboPoints2, 0);
                                    return 0;
                                }
                                else
                                {
                                    return points;
                                }
                            }
                        }
                    }
                    return 0;
                }
                return 0;
            }
        }

        // #################################################
        // ############### IsCasting #######################

        internal static int[] WarriorHS = new int[] { 78, 284, 285, 1608, 11564, 11565, 11566, 11567, 25286 };
        internal static bool arrContains(int[] arr, int id)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == id)
                {
                    return true;
                }
            }
            return false;
        }

        internal static bool IsCasting
        {
            get
            {
                try
                {
                    int curCastId = BmWrapper.memory.ReadInt((uint)Offsets.player.IsCasting);
                    if (curCastId != 0)
                    {
                        if (playerClass == (byte)Offsets.classIds.Warrior)
                        {
                            return !arrContains(WarriorHS, curCastId);
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch { return false; }
            }
        }

        // #################################################
        // ############### IsCasting #######################

        internal static byte playerClass
        {
            get
            {
                try
                {
                    return BmWrapper.memory.ReadByte((uint)Offsets.player.Class + Offsets.baseAddress);
                }
                catch { return 0; }
            }
        }

        internal static bool isDeath
        {
            get
            {
                try
                {
                    return ObjectManager.PlayerObject.health == 0;
                }
                catch { return false; }
            }
        }

        internal static bool isGhost
        {
            get
            {
                try
                {
                    return BmWrapper.memory.ReadByte(Offsets.baseAddress + (uint)Offsets.player.IsGhost) == 1;
                }
                catch { return false; }
            }
        }

        internal static bool IsTargetOnMe()
        {
            for (int i = 0; i < tmpAggroMobs.Count; i = i + 1)
            {
                if (tmpAggroMobs[i] == targetGuid) return true;
            }
            return false;
        }

        private static List<UInt64> tmpAggroMobs;
        private static int lastCheck = 0;

        internal static int aggroCountPlain()
        {
            int count = 0;
            foreach (Objects.UnitObject u in UnitObjectList)
            {
                if (u.targetGuid == playerGuid) count++;
            }
            return count;
        }

        internal static int aggroCountOnGuid(UInt64 parGuid)
        {
            int count = 0;
            foreach (Objects.UnitObject u in UnitObjectList)
            {
                if (u.targetGuid == parGuid) count++;
            }
            return count;
        }

        internal static List<UInt64> AggroMobs()
        {

            lastCheck = Environment.TickCount;
            tmpAggroMobs = new List<UInt64>();
            if (IsIngame == 1)
            {
                foreach (Objects.UnitObject u in UnitObjectList)
                {
                    if (u.isUnit && !u.isPlayerPet)
                    {
                        if (
                                ((u.targetGuid != 0) &&
                                (
                                    (u.targetGuid == playerGuid || (PetObject.guid != 0 && u.targetGuid == PetObject.guid))
                                    ||
                                    (
                                        (party1Guid != 0) && ((u.targetGuid == party1Guid) || (party1Pet.guid != 0 && u.targetGuid == party1Pet.guid)) ||

                                        (party2Guid != 0) && ((u.targetGuid == party2Guid) || (party2Pet.guid != 0 && u.targetGuid == party2Pet.guid)) ||

                                        (party3Guid != 0) && ((u.targetGuid == party3Guid) || (party3Pet.guid != 0 && u.targetGuid == party3Pet.guid)) ||

                                        (party4Guid != 0) && ((u.targetGuid == party4Guid) || (party4Pet.guid != 0 && u.targetGuid == party4Pet.guid))
                                    )
                                ))

                                ||

                                (u.guid == targetGuid && (u.targetGuid == 0 && !u.isTapped)
                                && Ingame.IsTargetInCombat())
                        )
                        {
                            if (!tmpAggroMobs.Contains(u.guid))
                                tmpAggroMobs.Add(u.guid);
                        }

                    }
                }
                       
            }
            return tmpAggroMobs;
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll")]
        public static extern uint
        GetWindowThreadProcessId(IntPtr hwnd, out uint lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        
        internal static bool IsWowCrashed()
        {
            StringBuilder className = new StringBuilder(256);
            int ret = GetClassName(Process.GetProcessById(BmWrapper.memory.ProcessId).MainWindowHandle, className, className.Capacity);
            if (ret != 0)
            {
                bool ret2 = className.ToString() != "GxWindowClassD3d";
                if (ret2)
                {
                    return true;
                }
            }

            return false;
        }

        internal static bool isProcessOpen
        {
            get
            {
                try
                {
                    Process.GetProcessById(BmWrapper.memory.ProcessId);
                    //return (Process.GetProcessById(BmWrapper.memory.ProcessId).HandleCount == 1);
                }
                catch (ArgumentException)
                {
                    return false;
                }

                return true;
            }
        }

        private static byte IsIngame
        {
            get
            {
                if (isProcessOpen)
                {
                    try
                    {
                        return BmWrapper.memory.ReadByte((int)Offsets.player.IsIngame);
                    }
                    catch { return 0; }
                }
                return 0;
            }
        }

        internal static uint playerPtr
        {
            get
            {
                if (IsIngame == 1)
                {
                    try
                    {
                        return BmWrapper.memory.ReadUInt(BmWrapper.memory.ReadUInt(BmWrapper.memory.ReadUInt(0xC7BCD4) + 0x88) + 0x28);
                    }
                    catch { return 0; }
                }
                return 0;
            }
        }

        internal static uint objectManager
        {
            get
            {
                if (IsIngame == 1)
                {
                    try
                    {
                        return BmWrapper.memory.ReadUInt((uint)Offsets.objectManager.ObjectManager);
                    }
                    catch { return 0; }
                }
                return 0;
            }
        }

        internal static UInt64 playerGuid
        {
            get
            {
                try
                {
                    return BmWrapper.memory.ReadUInt64((uint)Offsets.objectManager.PlayerGuid + objectManager);
                }
                catch { return 0; }
            }
        }

        internal static string LoginState
        {
            get
            {
                return BmWrapper.memory.ReadASCIIString((uint)Offsets.misc.LoginState, 10);
            }
        }
        #endregion

        #region Access to the saved objects
        private static readonly object gObjects_Lock = new object();
        private static List<Objects.GameObject> gObjects = new List<Objects.GameObject>();
        internal static List<Objects.GameObject> GameObjectList
        {
            get
            {
                lock (gObjects_Lock)
                {
                    return gObjects;
                }
            }

            set
            {
                lock (gObjects_Lock)
                {
                    gObjects = value;
                }
            }
        }
        
        private static readonly object pObject_Lock = new object();
        private static Objects.UnitObject pObject = new Objects.UnitObject(0, 0, 0);
        internal static Objects.UnitObject PlayerObject
        {
            get
            {
                lock (pObject_Lock)
                {
                    return pObject;
                }
            }

            set
            {
                lock (pObject_Lock)
                {
                    pObject = value;
                }
            }
        }

        private static readonly object b1Object_Lock = new object();
        private static Objects.ContainerObject b1Object = new Objects.ContainerObject();
        internal static Objects.ContainerObject Bag1
        {
            get
            {
                lock (b1Object_Lock)
                {
                    return b1Object;
                }
            }

            set
            {
                lock (b1Object_Lock)
                {
                    b1Object = value;
                }
            }
        }

        private static readonly object b2Object_Lock = new object();
        private static Objects.ContainerObject b2Object = new Objects.ContainerObject();
        internal static Objects.ContainerObject Bag2
        {
            get
            {
                lock (b2Object_Lock)
                {
                    return b2Object;
                }
            }

            set
            {
                lock (b2Object_Lock)
                {
                    b2Object = value;
                }
            }
        }

        private static readonly object b3Object_Lock = new object();
        private static Objects.ContainerObject b3Object = new Objects.ContainerObject();
        internal static Objects.ContainerObject Bag3
        {
            get
            {
                lock (b3Object_Lock)
                {
                    return b3Object;
                }
            }

            set
            {
                lock (b3Object_Lock)
                {
                    b3Object = value;
                }
            }
        }

        private static readonly object b4Object_Lock = new object();
        private static Objects.ContainerObject b4Object = new Objects.ContainerObject();
        internal static Objects.ContainerObject Bag4
        {
            get
            {
                lock (b4Object_Lock)
                {
                    return b4Object;
                }
            }

            set
            {
                lock (b4Object_Lock)
                {
                    b4Object = value;
                }
            }
        }

        private static readonly object UnitObject_Lock = new object();
        private static List<Objects.UnitObject> uObjectList = new List<Objects.UnitObject>();
        internal static List<Objects.UnitObject> UnitObjectList
        {
            get
            {
                lock (UnitObject_Lock)
                {
                    return uObjectList;
                }
            }

            set
            {
                lock (UnitObject_Lock)
                {
                    uObjectList = value;
                }
            }
        }

        internal static List<UInt64> BlacklistedLoot = new List<UInt64>();
        internal static int LootableMobsCount
        {
            get
            {
                int count = 0;
                List<Objects.UnitObject> tmpLootableMobs = new List<Objects.UnitObject>();
                List<Objects.UnitObject> tmpUnitList = ObjectManager.UnitObjectList;
                for (int i = 0; i < tmpUnitList.Count; i = i + 1)
                {
                    if (tmpUnitList[i].isLootable && !BlacklistedLoot.Contains(tmpUnitList[i].guid))
                    {
                        count = count + 1;
                    }
                }
                return count;
            }
        }

        internal static List<Objects.UnitObject> AllLootableMobs()
        {
            List<Objects.UnitObject> lootable = new List<Objects.UnitObject>();
            List<Objects.UnitObject> tmpUnitList = ObjectManager.UnitObjectList;
            for (int i = 0; i < tmpUnitList.Count; i = i + 1)
            {
                if (tmpUnitList[i].isLootable)
                {
                    lootable.Add(tmpUnitList[i]);
                }
            }
            return lootable;
        }

        internal static Objects.UnitObject GetNextLoot()
        {
            float smallestDis = float.MaxValue;
            Objects.UnitObject tmpMob = new Objects.UnitObject(0, 0, 0);

            List<Objects.UnitObject> tmpUnitList = ObjectManager.UnitObjectList;
            for (int i = 0; i < tmpUnitList.Count; i = i + 1)
            {
                if (tmpUnitList[i].isLootable && !BlacklistedLoot.Contains(tmpUnitList[i].guid))
                {
                    float dif = tmpUnitList[i].Pos.differenceTo(PlayerObject.Pos);
                    if (dif < smallestDis)
                    {
                        tmpMob = tmpUnitList[i];
                        smallestDis = dif;
                    }
                }
            }
            return tmpMob;
        }

        private static readonly object TargetObject_Lock = new object();
        private static Objects.UnitObject tObject = new Objects.UnitObject(0, 0, 0);
        internal static Objects.UnitObject TargetObject
        {
            get
            {
                lock (TargetObject_Lock)
                {
                    return GetTargetByGuid();
                }
            }
        }

        private static readonly object PetObject_Lock = new object();
        internal static Objects.UnitObject PetObject
        {
            get
            {
                lock (PetObject_Lock)
                {
                    return GetPetByPlayerGuid(playerGuid);
                }
            }
        }
        #endregion

        #region Objectmanager thread
        internal static bool runThread = false;
        internal static Thread getObjThread = new Thread(getObj);
        internal static double tickRate = 0;
        private static Stopwatch tickTimer = new Stopwatch();
        internal static void getObj()
        {
            while (runThread)
            {
                tickTimer.Start();
                getObjPulse();
                tickRate = tickTimer.Elapsed.TotalMilliseconds;
                tickTimer.Reset();
                Thread.Sleep(100);
            }
        }
        #endregion

        #region other
        internal static void UpdateObjects()
        {
            getObjPulse();
        }

        internal static bool IsUnitOnGroup(UInt64 parUnitTargetguid, bool tagged, float hpPercent)
        {
            if (parUnitTargetguid != 0)
            {
                if (party1Pet.guid != 0 && parUnitTargetguid == party1Pet.guid) return true;
                if (party2Pet.guid != 0 && parUnitTargetguid == party2Pet.guid) return true;
                if (party3Pet.guid != 0 && parUnitTargetguid == party3Pet.guid) return true;
                if (party4Pet.guid != 0 && parUnitTargetguid == party4Pet.guid) return true;
                if (ObjectManager.PetObject.guid != 0 && parUnitTargetguid == ObjectManager.PetObject.guid) return true;

                if (party1Guid != 0 && parUnitTargetguid == party1Guid) return true;
                if (party2Guid != 0 && parUnitTargetguid == party2Guid) return true;
                if (party3Guid != 0 && parUnitTargetguid == party3Guid) return true;
                if (party4Guid != 0 && parUnitTargetguid == party4Guid) return true;
                if (parUnitTargetguid == playerGuid) return true;
            }
            else if (!tagged && hpPercent != 100 && hpPercent != 0) return true;
            return false;
        }

        internal static Objects.UnitObject party1
        {
            get
            {
                return ObjectManager.GetPlayerByGuid(ObjectManager.party1Guid);
            }
        }

        internal static Objects.UnitObject party1Pet
        {
            get
            {
                return GetPetByPlayerGuid(party1Guid);
            }
        }

        internal static Objects.UnitObject party2
        {
            get
            {
                return ObjectManager.GetPlayerByGuid(ObjectManager.party2Guid);
            }
        }

        internal static Objects.UnitObject party2Pet
        {
            get
            {
                return GetPetByPlayerGuid(party2Guid);
            }
        }

        internal static Objects.UnitObject party3
        {
            get
            {
                return ObjectManager.GetPlayerByGuid(ObjectManager.party3Guid);
            }
        }

        internal static Objects.UnitObject party3Pet
        {
            get
            {
                return GetPetByPlayerGuid(party3Guid);
            }
        }

        internal static Objects.UnitObject party4
        {
            get
            {
                return ObjectManager.GetPlayerByGuid(ObjectManager.party4Guid);
            }
        }

        internal static Objects.UnitObject party4Pet
        {
            get
            {
                return GetPetByPlayerGuid(party4Guid);
            }
        }

        internal static Objects.UnitObject leaderPet
        {
            get
            {
                return GetPetByPlayerGuid(leaderGuid);
            }
        }

        internal static uint lastMovemmentPacket
        {
            get
            {
                try
                {
                    return BmWrapper.memory.ReadUInt(leader.baseAdd + 0xA50);
                }
                catch { return 0; }
            }
        }

        internal static uint lastMovemmentPacketParty1
        {
            get
            {
                try
                {
                    return BmWrapper.memory.ReadUInt(party1.baseAdd + 0xA50);
                }
                catch { return 0; }
            }
        }

        internal static Objects.UnitObject GetPlayerByGuid(UInt64 guid)
        {
            if (IsIngame == 1)
            {
                if (guid != 0)
                {
                    List<Objects.UnitObject> tmpUnits = UnitObjectList;
                    foreach (Objects.UnitObject u in tmpUnits)
                    {
                        if (!u.isUnit)
                        {
                            if (u.guid == guid)
                            {
                                return u;
                            }
                        }
                    }
                }
            }
            return new Objects.UnitObject(0, 0, 0);
        }

        internal static Objects.UnitObject GetPetByPlayerGuid(UInt64 guid)
        {
            if (IsIngame == 1)
            {
                if (guid != 0)
                {
                    List<Objects.UnitObject> tmpUnits = UnitObjectList;
                    foreach (Objects.UnitObject u in tmpUnits)
                    {
                        if (u.isPlayerPet)
                        {
                            if (u.summonedBy == guid)
                            {
                                return u;
                            }
                        }
                    }
                }
            }
            return new Objects.UnitObject(0, 0, 0);
        }

        internal static Objects.UnitObject GetUnitByGuid(UInt64 guid)
        {
            if (IsIngame == 1)
            {
                if (guid != 0)
                {
                    List<Objects.UnitObject> tmpUnits = UnitObjectList;
                    foreach (Objects.UnitObject u in tmpUnits)
                    {
                        if (u.isUnit)
                        {
                            if (u.guid == guid)
                            {
                                return u;
                            }
                        }
                    }
                }
            }
            return new Objects.UnitObject(0, 0, 0);
        }

        internal static Objects.UnitObject GetTargetByGuid()
        {
            if (IsIngame == 1)
            {
                UInt64 tmp = targetGuid;
                if (tmp != 0)
                {
                    List<Objects.UnitObject> tmpUnits = UnitObjectList;
                    foreach (Objects.UnitObject u in tmpUnits)
                    {
                        if (u.guid == tmp)
                        {
                            return u;
                        }
                    }
                }
            }
            return new Objects.UnitObject(0, 0, 0);
        }

        internal static Objects.UnitObject GetUnitByName(string mobName)
        {
            Objects.UnitObject tmpObj = new Objects.UnitObject(0, 0, 0);
            if (IsIngame == 1)
            {
                List<Objects.UnitObject> tmpUnits = UnitObjectList;
                float nearest = float.MaxValue;

                foreach (Objects.UnitObject u in tmpUnits)
                {
                    if (u.isUnit && u.UnitName == mobName)
                    {
                        try
                        {
                            float diff;
                            if ((diff = ObjectManager.PlayerObject.Pos.differenceTo(u.Pos)) < nearest)
                            {
                                tmpObj = u;
                                nearest = diff;
                            }
                        }
                        catch
                        {
                            return tmpObj;
                        }
                    }
                }
            }
            return tmpObj;
        }

        internal static Objects.UnitObject GetUnitById(int ID)
        {
            Objects.UnitObject tmpObj = new Objects.UnitObject(0, 0, 0);
            if (IsIngame == 1)
            {
                List<Objects.UnitObject> tmpUnits = UnitObjectList;
                float nearest = float.MaxValue;

                foreach (Objects.UnitObject u in tmpUnits)
                {
                    if (u.isUnit && u.NpcId == ID)
                    {
                        try
                        {
                            float diff;
                            if ((diff = ObjectManager.PlayerObject.Pos.differenceTo(u.Pos)) < nearest)
                            {
                                tmpObj = u;
                                nearest = diff;
                            }
                        }
                        catch
                        {
                            return tmpObj;
                        }
                    }
                }
            }
            return tmpObj;
        }

        internal static List<Objects.GameObject> GetGameObjectsCreatedByPlayer()
        {
            UInt64 tmpPlayerGuid = playerGuid;
            List<Objects.GameObject> tmpObj = new List<Objects.GameObject>();
            if (IsIngame == 1)
            {
                List<Objects.GameObject> tmpGameObjectList = GameObjectList;
                for (int i = 0; i < tmpGameObjectList.Count; i++)
                {
                    UInt64 tmpCreatedBy = tmpGameObjectList[i].createdBy;
                    if (tmpCreatedBy != 0)
                    {
                        if (tmpCreatedBy == tmpPlayerGuid)
                        {
                            tmpObj.Add(tmpGameObjectList[i]);
                        }
                    }
                }
            }
            return tmpObj;
        }

        internal static Objects.GameObject GetGameObjectByName(string objName)
        {
            Objects.GameObject tmpGameObj = new Objects.GameObject(0, 0, 0);
            if (IsIngame == 1)
            {
                List<Objects.GameObject> tmpGameObjectList = GameObjectList;
                float nearest = float.MaxValue;

                for (int i = 0; i < tmpGameObjectList.Count; i++)
                {
                    if (tmpGameObjectList[i].name == objName)
                    {
                        try
                        {
                            float dif;
                            if ((dif = tmpGameObjectList[i].Pos.differenceToPlayer()) < nearest)
                            {
                                tmpGameObj = tmpGameObjectList[i];
                                nearest = dif;
                            }
                        }
                        catch
                        {
                            return tmpGameObj;
                        }
                    }
                }
            }
            return tmpGameObj;
        }

        internal static Objects.GameObject GetGameObjectByName(string objName, List<UInt64> exclude)
        {
            Objects.GameObject tmpGameObj = new Objects.GameObject(0, 0, 0);
            if (IsIngame == 1)
            {
                List<Objects.GameObject> tmpGameObjectList = GameObjectList;
                float nearest = float.MaxValue;

                for (int i = 0; i < tmpGameObjectList.Count; i++)
                {
                    if (tmpGameObjectList[i].name == objName)
                    {
                        if (!exclude.Contains(tmpGameObjectList[i].guid))
                        {
                            try
                            {
                                float dif;
                                if ((dif = tmpGameObjectList[i].Pos.differenceToPlayer()) < nearest)
                                {
                                    tmpGameObj = tmpGameObjectList[i];
                                    nearest = dif;
                                }
                            }
                            catch
                            {
                                return tmpGameObj;
                            }
                        }
                    }
                }
            }
            return tmpGameObj;
        }

        internal static List<Objects.GameObject> GetGameObjectsByName(string objName)
        {
            Objects.GameObject tmpGameObj = new Objects.GameObject(0, 0, 0);
            List<Objects.GameObject> objects = new List<Objects.GameObject>();
            if (IsIngame == 1)
            {
                List<Objects.GameObject> tmpGameObjectList = GameObjectList;
                for (int i = 0; i < tmpGameObjectList.Count; i++)
                {
                    if (tmpGameObjectList[i].name == objName)
                    {
                        objects.Add(tmpGameObjectList[i]);
                    }
                }
            }
            return objects;
        }

        internal static Objects.GameObject GetGameObjectById(int ID)
        {
            Objects.GameObject tmpGameObj = new Objects.GameObject(0, 0, 0);
            if (IsIngame == 1)
            {
                List<Objects.GameObject> tmpGameObjectList = GameObjectList;
                float nearest = float.MaxValue;

                for (int i = 0; i < tmpGameObjectList.Count; i++)
                {
                    if (tmpGameObjectList[i].objectId == ID)
                    {
                        try
                        {
                            float dif;
                            if ((dif = tmpGameObjectList[i].Pos.differenceToPlayer()) < nearest)
                            {
                                tmpGameObj = tmpGameObjectList[i];
                                nearest = dif;
                            }
                        }
                        catch
                        {
                            return tmpGameObj;
                        }
                    }
                }
            }
            return tmpGameObj;
        }
        #endregion

        #region objectmanager
        #region for the fishbot to ignore old bobber til it despawns
        internal static List<UInt64> bobberBl = new List<UInt64>();
        #endregion

        #region some checks / timers
        internal static bool ExecuteOnce = true;
        private static bool ScanBags = true;
        private static Helper.cTimer ScanBagTimer = new Helper.cTimer(2000);
        private static Stopwatch EmergencyTimer = new Stopwatch();
        private static bool foundPlayer = false;
        private static uint curObjType = 0;
        #endregion

        #region objects
        private static Objects.ContainerObject tmpBag1;
        private static Objects.ContainerObject tmpBag2;
        private static Objects.ContainerObject tmpBag3;
        private static Objects.ContainerObject tmpBag4;

        private static List<Objects.UnitObject> tmpUnitList;
        private static List<Objects.GameObject> tmpGameObjectList;
        #endregion

        private static void getObjPulse()
        {
            //if ()
            {
                uint curObj = 0, nextObj = 0;

                #region Do before itterating over objects
                try
                {
                    curObj = BmWrapper.memory.ReadUInt((uint)Offsets.objectManager.FirstObj + objectManager);
                    // Get executed every 2 seconds
                    if (ScanBagTimer.IsReady())
                    {
                        ScanBags = true; // Tell ObjectManager to itterate over bags
                        // Writing DC byte
                        BmWrapper.memory.WriteByte(0x00B41D98, 0);
                        IsWowCrashed();
                    }
                }
                catch { ScanBags = false; }
                #endregion

                nextObj = curObj;
                #region assigning tmpobjects
                if (ScanBags)
                {
                    tmpBag1 = new Objects.ContainerObject(0, 0);
                    tmpBag2 = new Objects.ContainerObject(0, 1);
                    tmpBag3 = new Objects.ContainerObject(0, 2);
                    tmpBag4 = new Objects.ContainerObject(0, 3);
                }

                tmpUnitList = new List<Objects.UnitObject>();
                tmpGameObjectList = new List<Objects.GameObject>();
                #endregion

                foundPlayer = false;
                EmergencyTimer.Start();
                while (curObj != 0 && (curObj & 1) == 0)
                {
                    #region read guid and type
                    try
                    {
                        curObjType = BmWrapper.memory.ReadByte(curObj + (uint)Offsets.objectManager.ObjType);
                    }
                    catch
                    {
                        curObjType = 0;
                    }
                    #endregion

                    switch (curObjType)
                    {
                        case (byte)ObjTypes.OT_CONTAINER:
                            //###########################
                            if (ScanBags)
                            {
                                #region is object a container
                                UInt64 curObjGuid = BmWrapper.memory.ReadUInt64(curObj + (uint)Offsets.objectManager.CurObjGuid);
                                #region iterate over bag 1
                                if (curObjGuid == tmpBag1.guid)
                                {
                                    try
                                    {
                                        tmpBag1.baseAdd = curObj;
                                        uint descriptor = BmWrapper.memory.ReadUInt(curObj + 0x8);
                                        int slots = tmpBag1.TotalSlots;
                                        for (int i = 1; i < slots + 1; i++)
                                        {
                                            UInt64 BagSlotItemGuid = BmWrapper.memory.ReadUInt64(
                                                                        descriptor + 0xC0 + (uint)i * 8);
                                            if (BagSlotItemGuid == 0)
                                            {
                                                tmpBag1.FreeSlots = tmpBag1.FreeSlots + 1;
                                            }
                                        }
                                    }
                                    catch { }

                                }
                                #endregion

                                #region iterate over bag 2
                                if (curObjGuid == tmpBag2.guid)
                                {
                                    try
                                    {
                                        tmpBag2.baseAdd = curObj;

                                        uint descriptor = BmWrapper.memory.ReadUInt(curObj + 0x8);
                                        int slots = tmpBag2.TotalSlots;
                                        for (int i = 1; i < slots + 1; i++)
                                        {
                                            UInt64 BagSlotItemGuid = BmWrapper.memory.ReadUInt64(
                                                                        descriptor + 0xC0 + (uint)i * 8);
                                            if (BagSlotItemGuid == 0)
                                            {
                                                tmpBag2.FreeSlots = tmpBag2.FreeSlots + 1;
                                            }
                                            //else
                                            //{
                                            //    tmpItemsInBagGuid.Add(BagSlotItemGuid);
                                            //}
                                        }
                                    }
                                    catch { }
                                }
                                #endregion

                                #region iterate over bag 3
                                if (curObjGuid == tmpBag3.guid)
                                {
                                    try
                                    {
                                        tmpBag3.baseAdd = curObj;

                                        uint descriptor = BmWrapper.memory.ReadUInt(curObj + 0x8);
                                        int slots = tmpBag3.TotalSlots;
                                        for (int i = 1; i < slots + 1; i++)
                                        {
                                            UInt64 BagSlotItemGuid = BmWrapper.memory.ReadUInt64(
                                                                    descriptor + 0xC0 + (uint)i * 8);
                                            if (BagSlotItemGuid == 0)
                                            {
                                                tmpBag3.FreeSlots = tmpBag3.FreeSlots + 1;
                                            }
                                            //else
                                            //{
                                            //    tmpItemsInBagGuid.Add(BagSlotItemGuid);
                                            //}
                                        }
                                    }
                                    catch { }
                                }
                                #endregion

                                #region iterate over bag 4
                                if (curObjGuid == tmpBag4.guid)
                                {
                                    tmpBag4.baseAdd = curObj;

                                    uint descriptor = BmWrapper.memory.ReadUInt(curObj + 0x8);
                                    int slots = tmpBag4.TotalSlots;
                                    for (int i = 1; i < slots + 1; i++)
                                    {
                                        try
                                        {
                                            UInt64 BagSlotItemGuid = BmWrapper.memory.ReadUInt64(
                                                                    descriptor + 0xC0 + (uint)i * 8);
                                            if (BagSlotItemGuid == 0)
                                            {
                                                tmpBag4.FreeSlots = tmpBag4.FreeSlots + 1;
                                            }
                                            //else
                                            //{
                                            //    tmpItemsInBagGuid.Add(BagSlotItemGuid);
                                            //}
                                        }
                                        catch { }
                                    }

                                }
                                #endregion
                                #endregion
                            }
                            //###########################
                            break;

                        case (byte)ObjTypes.OT_PLAYER:
                        case (byte)ObjTypes.OT_UNIT:
                            //###########################
                            #region Is Object a Unit/Player
                            try
                            {
                                Objects.UnitObject tmpObj = new Objects.UnitObject(curObj,
                                    BmWrapper.memory.ReadUInt(curObj + 0x8),
                                    BmWrapper.memory.ReadUInt64(curObj + (uint)Offsets.objectManager.CurObjGuid));
                                tmpObj.isUnit = curObjType == (byte)ObjTypes.OT_UNIT ? true : false;

                                if (tmpObj.guid == playerGuid)
                                {
                                    ObjectManager.PlayerObject = tmpObj;
                                    foundPlayer = true;
                                }
                                tmpUnitList.Add(tmpObj);
                            }
                            catch { }
                            #endregion
                            //###########################
                            break;

                        case (byte)ObjTypes.OT_GAMEOBJ:
                            //###########################
                            #region Is Object an gameobject?
                            try
                            {
                                Objects.GameObject tmpGameObj = new Objects.GameObject(curObj,
                                    BmWrapper.memory.ReadUInt(curObj + 0x8),
                                    BmWrapper.memory.ReadUInt64(curObj + (uint)Offsets.objectManager.CurObjGuid));
                                tmpGameObjectList.Add(tmpGameObj);
                                tmpGameObj = null;
                            }
                            catch { }
                            #endregion
                            //###########################
                            break;
                    }
                    #region getting next object
                    try
                    {
                        nextObj = BmWrapper.memory.ReadUInt(curObj + (uint)Offsets.objectManager.NextObj);
                    }
                    catch { }
                    if (nextObj == curObj)
                    {
                        break;
                    }
                    else
                    {
                        curObj = nextObj;
                    }

                    if (EmergencyTimer.Elapsed.TotalMilliseconds > 200)
                    {
                        EmergencyTimer.Reset();
                        Thread.Sleep(1);
                        tickRate = tickTimer.Elapsed.TotalMilliseconds;
                        tickTimer.Reset();
                        tickTimer.Start();
                    }
                    #endregion
                }
                EmergencyTimer.Reset();

                #region assigning tmpobjects to real objects
                if (!foundPlayer) PlayerObject = new Objects.UnitObject(0, 0, 0);
                UnitObjectList = tmpUnitList;
                GameObjectList = tmpGameObjectList;
                tmpUnitList = null;
                tmpGameObjectList = null;

                if (ScanBags)
                {
                    try
                    {
                        int tmpBaseBagFreeSlots = 0;
                        for (int i = 0; i < 16; i++)
                        {
                            UInt64 BagSlotItemGuid = BmWrapper.memory.ReadUInt64(BmWrapper.memory.ReadUInt(PlayerObject.baseAdd + 0x8) + 0x850 + (uint)i * 8);
                            if (BagSlotItemGuid == 0)
                            {
                                tmpBaseBagFreeSlots = tmpBaseBagFreeSlots + 1;
                            }
                        }
                        baseBagFreeSlots = tmpBaseBagFreeSlots;

                        Bag1 = tmpBag1;
                        Bag2 = tmpBag2;
                        Bag3 = tmpBag3;
                        Bag4 = tmpBag4;
                    }
                    catch { }
                    ScanBags = false;
                }
                #endregion
            }
        }
        #endregion
    }
}
