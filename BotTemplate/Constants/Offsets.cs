using BotTemplate.Helper;
using System;

namespace BotTemplate.Constants
{
    internal static class Offsets
    {
        internal static uint baseAddress
        {
            get
            {
                return (uint)BmWrapper.memory.MainModule.BaseAddress;
            }
        }

        internal static readonly byte[] EndSceneOriginal = new byte[5] { 0x8B, 0xFF, 0x55, 0x8B, 0xEC };

        internal enum player : uint
        {
            Class = 0x827E81,
            IsIngame = 0xB4B424,
            IsGhost = 0x435A48,
            Name = 0x827D88,
            TargetGuid = 0x74E2D8,
            IsChannelingDescriptor = 0x240,
            IsCasting = 0xCECA88,
            ComboPoints1 = 0xE68,
            ComboPoints2 = 0x1029,
            CharacterCount = 0x00B42140
        }

        internal enum partyStuff : uint
        {
            leaderGuid = 0x00BC75F8,
            party1Guid = 0x00BC6F48,
            party2Guid = 0x00BC6F50,
            party3Guid = 0x00BC6F58,
            party4Guid = 0x00BC6F60,
        }

        internal enum CTMAction : uint
        {
            FaceTarget = 0x1,
            Stop = 0x3,
            WalkTo = 0x4,
            InteractNpc = 0x5,
            Loot = 0x6,
            InteractObject = 0x7
        }


        internal enum misc : uint
        {
            GameVersion = 0x00837C04,
            MapId = 0x84C498,
            AntiDc = 0x00B41D98,
            LoginState = 0xB41478
        }

        internal enum functions : uint
        {
            LastHardwareAction = 0x00CF0BC8,
            AutoLoot = 0x4C1FA0,
            ClickToMove = 0x00611130,
            GetText = 0x703BF0,
            DoString = 0x00704CD0,
            //EndScene = 0x005A1B60,
            GetEndscene = 0x5A17B6,
            IsLooting = 0x006126B0,
            GetLootSlots = 0x004C2260,
            OnRightClickObject = 0x005F8660,
            OnRightClickUnit = 0x60BEA0,
            SetFacing = 0x007C6F30,
            SendMovementPacket = 0x00600A30,
            PerformDefaultAction = 0x00481F60,
            CGInputControl__GetActive = 0x005143E0,
            CGInputControl__SetControlBit = 0x00515090,

        }

        internal enum controlBits : uint
        {
            Front = 0x10,
            Right = 0x200,
            Left = 0x100,
            Back = 0x20
        }

        internal enum opCodes : uint
        {
            turnRight = 0xBD,
            turnLeft = 0xBC,
            moveBack = 0xB6,
            moveFront = 0xB5,
            stop = 0xB7,
        }

        internal enum movementFlags : uint
        {
            None =          0x00000000,
            Forward =       0x00000001,
            Back =          0x00000002,
            TurnLeft =      0x00000010,
            TurnRight =     0x00000020,
            Stunned =       0x00001000,
            Swimming   =    0x00200000,
        }

        internal enum objectManager : uint
        {
            CurObjGuid = 0x30,
            ObjectManager = 0x00B41414,
            PlayerGuid = 0xc0,
            FirstObj = 0xac,
            NextObj = 0x3c,
            ObjType = 0x14,
            DescriptorOffset = 0x8,
            UnitPosX = 0x9B8,
            UnitPosY = 0x9BC,
            UnitPosZ =0x9BC + 4
        }

        internal enum hacks : uint
        {
            ChangeX = 0x9C0,
            ChangeY = 0x9C4,
            ChangeZ = 0x9C8
        }

        internal enum descriptors : uint
        {
            GotLoot = 0xB4,
            SummonedByGuid = 0x30,
            DynamicFlags = 0x23C,
            IsChanneling = 0x240,
            CreatedByGuid = 0x38,
            GameObjectCreatedByGuid = 0x18,
            UnitPosX = 0x9B8,
            UnitPosY = 0x9BC,
            UnitPosZ = 0x9BC + 4,
            movementFlags = 0x9E8,
            Health = 0x58,
            MaxHealth = 0x70,
            FactionId = 0x8C,
            Mana = 0x5C,
            MaxMana = 0x74,
            Rage = 0x60,
            TargetGuid = 0x40,
            CorpseOwnedBy = 0x18,
            ItemId = 0xC,
            ItemStackCount = 0x38,
            ContainerTotalSlots = 0x6c8,
            CorpseX = 0x24,
            CorpseY = 0x28,
            CorpseZ = 0x2c
        }

        internal enum classIds : uint
        {
            Warrior = 1,
            Paladin = 2,
            Hunter = 3,
            Rogue = 4,
            Priest = 5,
            Shaman = 7,
            Mage = 8,
            Warlock = 9,
            Druid = 11
        }

        internal enum buffs : uint
        {
            FirstBuff = 0xBC,
            FirstDebuff = 0x13C,
            NextBuff = 0x4
        }
    }
}
