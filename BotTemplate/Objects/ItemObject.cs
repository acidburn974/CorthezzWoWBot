using BotTemplate.Objects.BaseObject;

namespace BotTemplate.Objects
{
    internal class ItemObject : gObject
    {
        internal ItemObject()
        {
            this.guid = 0x0;
            this.baseAdd = 0x0;
            this.id = 0;
            this.stackCount = 0;
            this.charges = 0;
            this.name = "";
        }

        internal int id;
        internal int stackCount;
        internal int charges;
        internal string name;
    }
}
