using BotTemplate.Objects.BaseObject;
using System;
using BotTemplate.Helper;
using BotTemplate.Constants;

namespace BotTemplate.Objects
{
    internal sealed class GameObject : gObject
    {
        internal GameObject(uint parBase, uint parDescriptor, UInt64 parGuid)
        {
            baseAdd = parBase;
            descriptor = parDescriptor;
            guid = parGuid;
            Pos = new Location(baseAdd, parDescriptor, 2);
        }

        internal Location Pos;
        internal UInt64 createdBy
        {
            get
            {
                try
                {
                    if (baseAdd == 0 || guid == 0) return 0;
                    return BmWrapper.memory.ReadUInt64(descriptor + (uint)Offsets.descriptors.GameObjectCreatedByGuid);
                }
                catch { return 0; }
            }
        }

        internal int state
        {
            get
            {
                try
                {
                    if (baseAdd == 0 || guid == 0) return 0;
                    return BmWrapper.memory.ReadInt(descriptor + 0x38);
                }
                catch { return 0; }
            }
        }

        internal byte isBusy
        {
            get
            {
                try
                {
                    if (baseAdd == 0 || guid == 0) return 0;
                    byte tmp = BmWrapper.memory.ReadByte(baseAdd + 0x27C);
                    tmp ^= 1;
                    return tmp;
                }
                catch { return 0; }
            }
        }

        internal int objectId
        {
            get
            {
                try
                {
                    if (baseAdd == 0 || guid == 0) return 0;
                    return BmWrapper.memory.ReadInt(baseAdd + 0x294);
                }
                catch { return 0; }
            }
        }

        internal string name
        {
            get
            {
                try
                {
                    if (baseAdd == 0 || guid == 0) return "";
                    return BmWrapper.memory.ReadASCIIString((BmWrapper.memory.ReadUInt((BmWrapper.memory.ReadUInt(baseAdd + 0x214) + 0x8))), 40);
                }
                catch
                {
                    return "";
                }
            }
        }
    }
}
