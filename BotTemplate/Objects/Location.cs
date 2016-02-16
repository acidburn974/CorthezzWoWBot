using System;
using BotTemplate.Helper;
using BotTemplate.Constants;
using BotTemplate.Interact;

namespace BotTemplate.Objects
{
    // Save WoW Locations
    internal class Location
    {
        // 0 -> fixed point
        // 1 -> unit
        // 2 -> gameobject
        // 3 -> corpse
        internal byte modifier;
        internal uint AddBase;
        internal uint descriptor;

        internal Location()
        {
            x = 0f;
            y = 0f;
            z = 0f;
            modifier = 0;
        }
        
        internal Location(float X, float Y, float Z)
        {
            x = X;
            y = Y;
            z = Z;
            modifier = 0;
        }

        internal Location(uint parBase, uint parDescriptor, byte parModifier)
        {
            modifier = parModifier;
            AddBase = parBase;
            descriptor = parDescriptor;
        }

        private float privX;
        private float privY;
        private float privZ;

        internal float x
        {
            get
            {
                switch (modifier)
                {
                    case 0:
                        return privX;

                    case 1:
                        try
                        {
                            return BmWrapper.memory.ReadFloat(AddBase + 0x9B8);
                        }
                        catch { return 0; }
                        
                    case 2:
                        try
                        {
                            return BmWrapper.memory.ReadFloat(descriptor + 0x3C);
                        }
                        catch { return 0; }

                    case 3:
                        try
                        {
                            return BmWrapper.memory.ReadFloat(0x00B4E284);
                        }
                        catch { return 0; }

                    default:
                        return 0;
                }
            }

            set
            {
                privX = value;
            }
        }
        internal float y
        {
            get
            {
                switch (modifier)
                {
                    case 0:
                        return privY;

                    case 1:
                        try
                        {
                            return BmWrapper.memory.ReadFloat(AddBase + (uint)Offsets.descriptors.UnitPosY);
                        }
                        catch { return 0; }

                    case 2:
                        try
                        {
                            return BmWrapper.memory.ReadFloat(descriptor + 0x40);
                        }
                        catch { return 0; }


                    case 3:
                        try
                        {
                            return BmWrapper.memory.ReadFloat(0x00B4E288);
                        }
                        catch { return 0; }

                    default:
                        return 0;
                }
            }

            set
            {
                privY = value;
            }
        }
        internal float z
        {
            get
            {
                switch (modifier)
                {
                    case 0:
                        return privZ;

                    case 1:
                        try
                        {
                            return BmWrapper.memory.ReadFloat(AddBase + (uint)Offsets.descriptors.UnitPosZ);
                        }
                        catch { return 0; }

                    case 2:
                        try
                        {
                            return BmWrapper.memory.ReadFloat(descriptor + 0x44);
                        }
                        catch { return 0; }

                    case 3:
                        try
                        {
                            return BmWrapper.memory.ReadFloat(0x00B4E28C);
                        }
                        catch { return 0; }


                    default:
                        return 0;
                }
            }

            set
            {
                privZ = value;
            }
        }

        internal float differenceTo(Location to)
        {
            return (float)Math.Sqrt(Math.Pow(this.x - to.x, 2) + Math.Pow(this.y - to.y, 2));
        }

        internal float differenceToPlayer()
        {
            Objects.Location tmp = ObjectManager.PlayerObject.Pos;
            return (float)Math.Sqrt(Math.Pow(this.x - tmp.x, 2) + Math.Pow(this.y - tmp.y, 2));
        }
    }
}
