using System;
using BotTemplate.Interact;
using BotTemplate.Objects.BaseObject;
using BotTemplate.Constants;
using BotTemplate.Helper;

namespace BotTemplate.Objects
{
    internal sealed class UnitObject : gObject
    {
        #region constructor
        internal UnitObject(uint parBase, uint parDescriptor, UInt64 parGuid)
        {
            this.baseAdd = parBase;
            this.guid = parGuid;
            this.descriptor = parDescriptor;
            Pos = new Location(parBase, descriptor, 1);
        }
        #endregion

        internal Location Pos;

        #region infos about dead mobs
        internal bool isTapped
        {
            get
            {
                return (dynFlags & (uint)enumDynFlags.tagged) == (uint)enumDynFlags.tagged ? true : false;
            }
        }

        internal bool isLootable
        {
            get
            {
                if (health == 0)
                {
                    return (dynFlags & (uint)enumDynFlags.isDeadMobMine) == (uint)enumDynFlags.isDeadMobMine ? true : false;
                }
                return false;
            }
        }
        #endregion

        #region current cast / channel info
        internal int isChanneling
        {
            get
            {
                try
                {
                    if (baseAdd == 0 || guid == 0) return 0;
                    return BmWrapper.memory.ReadInt(descriptor + (uint)Offsets.descriptors.IsChanneling);
                }
                catch
                {
                    return 0;
                }
            }
        }

        internal bool isFishing
        {
            get
            {
                return isChanneling != 0 ? true : false;
            }
        }
        #endregion

        #region what type of mob do we have?
        internal UInt64 summonedBy
        {
            get
            {
                try
                {
                    if (baseAdd == 0 || guid == 0) return 0;
                    return BmWrapper.memory.ReadUInt64(descriptor + (uint)Offsets.descriptors.SummonedByGuid);
                }
                catch
                {
                    return 0;
                }
            }
        }

        internal int NpcId
        {
            get
            {
                try
                {
                    if (baseAdd == 0 || guid == 0 || !isUnit) return 0;
                    return BmWrapper.memory.ReadInt(baseAdd + 0xE74);
                }
                catch
                {
                    return 0;
                }
            }
        }

        internal bool isPlayerPet
        {
            get
            {
                return summonedBy != 0;
            }
        }

        //internal int[] buffs;
        //internal int[] debuffs;

        internal bool isUnit;
        #endregion

        #region movement infos
        private enum enumDynFlags : uint
        {
            tagged = 0x4,
            isDeadMobMine = 0x1
        }

        internal uint movementState
        {
            get
            {
                try
                {
                    if (baseAdd == 0 || guid == 0) return 0;
                    return BmWrapper.memory.ReadUInt(baseAdd + (uint)Offsets.descriptors.movementFlags);
                }
                catch
                {
                    return 0;
                }
            }
        }
        internal uint dynFlags
        {
            get
            {
                try
                {
                    if (baseAdd == 0 || guid == 0) return 0;
                    return BmWrapper.memory.ReadUInt(descriptor + (uint)Offsets.descriptors.DynamicFlags);
                }
                catch
                {
                    return 0;
                }
            }
        }


        internal string UnitName
        {
            get
            {
                try
                {
                    if (isUnit)
                    {
                        if (baseAdd == 0 || guid == 0) return "";
                        return BmWrapper.memory.ReadASCIIString((BmWrapper.memory.ReadUInt(BmWrapper.memory.ReadUInt(baseAdd + 0xB30))), 30);
                    }
                    else
                    {
                        uint nameBase = BmWrapper.memory.ReadUInt(Offsets.baseAddress + 0x80E230);
                        UInt64 nextGuid = BmWrapper.memory.ReadUInt64(nameBase + 0xc);
                        bool success = true;
                        while (nextGuid != guid)
                        {
                            nameBase = BmWrapper.memory.ReadUInt(nameBase);
                            if ((nextGuid = BmWrapper.memory.ReadUInt64(nameBase + 0xc)) == 0)
                            {
                                success = false;
                                break;

                            }
                        }
                        if (success)
                        {
                            return BmWrapper.memory.ReadASCIIString(nameBase + 0x14, 20);
                        }
                        else
                        {
                            return "";
                        }
                    }
                    //return "";
                }
                catch
                {
                    return "";
                }
            }
        }
        
        #endregion

        #region values
        internal UInt64 targetGuid
        {
            get
            {
                try
                {
                    if (baseAdd == 0 || guid == 0) return 0;
                    return BmWrapper.memory.ReadUInt64(descriptor + (uint)Offsets.descriptors.TargetGuid);
                }
                catch
                {
                    return 0;
                }
            }
        }

        internal int IsCasting
        {
            get
            {
                try
                {
                    if (baseAdd == 0 || guid == 0) return 0;
                    int id = BmWrapper.memory.ReadInt(baseAdd + 0xC8C);
                    if (isUnit)
                    {
                        return id;
                    }
                    else
                    {
                        if (ObjectManager.arrContains(ObjectManager.WarriorHS, id)) id = 0;
                        return id;
                    }
                }
                catch
                {
                    return 0;
                }
            }
        }


        internal int factionId
        {
            get
            {
                try
                {
                    if (baseAdd == 0 || guid == 0) return 0;
                    return BmWrapper.memory.ReadInt(descriptor + (uint)Offsets.descriptors.FactionId);
                }
                catch
                {
                    return 0;
                }
            }
        }

        internal int health
        {
            get
            {
                try
                {
                    if (baseAdd == 0 || guid == 0) return 0;
                    return BmWrapper.memory.ReadInt(descriptor + (uint)Offsets.descriptors.Health);
                }
                catch
                {
                    return 0;
                }
            }
        }
        internal int maxHealth
        {
            get
            {
                try
                {
                    if (baseAdd == 0 || guid == 0) return 0;
                    return BmWrapper.memory.ReadInt(descriptor + (uint)Offsets.descriptors.MaxHealth);
                }
                catch
                {
                    return 0;
                }
            }
        }
        internal float healthPercent
        {
            get
            {
                return ((float)health / (float)maxHealth) * 100;
            }
        }

        internal int mana
        {
            get
            {
                try
                {
                    if (baseAdd == 0 || guid == 0) return 0;
                    return BmWrapper.memory.ReadInt(descriptor + (uint)Offsets.descriptors.Mana);
                }
                catch
                {
                    return 0;
                }
            }
        }
        internal int maxMana
        {
            get
            {
                try
                {
                    if (baseAdd == 0 || guid == 0) return 0;
                    return BmWrapper.memory.ReadInt(descriptor + (uint)Offsets.descriptors.MaxMana);
                }
                catch
                {
                    return 0;
                }
            }
        }
        internal float manaPercent
        {
            get
            {
                return ((float)mana / maxMana) * 100;
            }
        }

        internal int rage
        {
            get
            {
                try
                {
                    if (baseAdd == 0 || guid == 0) return 0;
                    return BmWrapper.memory.ReadInt(descriptor + (uint)Offsets.descriptors.Rage) / 10;
                }
                catch
                {
                    return 0;
                }
            }
        }
        
        #endregion
    }
}
