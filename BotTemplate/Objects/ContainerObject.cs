using BotTemplate.Objects.BaseObject;
using BotTemplate.Helper;
using BotTemplate.Interact;
using BotTemplate.Constants;

namespace BotTemplate.Objects
{
    internal class ContainerObject : gObject
    {
        internal ContainerObject()
        {
            this.baseAdd = 0x0;
            this.guid = 0x0;
            this.FreeSlots = 0;
        }

        internal ContainerObject(uint parBase ,uint bagSlot)
        {
            this.baseAdd = parBase;
            this.guid = BmWrapper.memory.ReadUInt64(0xBDD060 + (uint)(bagSlot * 8));
        }

        internal int TotalSlots
        {
            get
            {
                try
                {
                    return BmWrapper.memory.ReadInt(baseAdd +
                                            (uint)Offsets.descriptors.ContainerTotalSlots);
                }
                catch
                {
                    return 0;
                }
            }
        }

        internal int FreeSlots;
    }
}
