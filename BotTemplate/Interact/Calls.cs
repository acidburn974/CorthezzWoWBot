using System;
using BotTemplate.Helper;
using BotTemplate.Constants;
using System.Runtime.InteropServices;
using BotTemplate.Objects;
using System.Text;
using BotTemplate.Engines;
using System.Windows.Forms;
using System.Diagnostics;

namespace BotTemplate.Interact
{
    internal static class Calls
    {
        #region thread locks
        private static readonly object Call_Lock = new object();
        #endregion

        #region Send Movement Update
        internal static void SendMovementUpdate(uint OpCode, uint timeStamp)
        {
            lock (Inject.inject_Lock)
            {
                String[] asm = new String[]
                        { 
                                "mov ECX, [" + Inject.PlayerPtr + "]",
                                "push 0",
                                "push 0",
                                "push " + (uint)OpCode,
                                "push " + (uint)timeStamp,
                                "call " + (uint)Offsets.functions.SendMovementPacket, // Send Packet
                                "retn",   

                        };
                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " Calling Send Movement Update");
                Inject.InjectAndExecute(asm, true);
            }
        }
        #endregion
        // ^ Offset by Cencil

        #region Get Timestamp
        internal static uint GetTimestamp()
        {

            // Write the asm stuff for Lua_DoString
            String[] asm = new String[] 
                                {
                                    "call " + (uint)0x0042B790,
                                    "retn",    
                                };
            return Inject.InjectAndExecuteReturn(asm, false, false);
        }
        #endregion
        // ^ Offset by Cencil

        #region Turn Character
        internal static float PI = (float)Math.PI;

        internal static bool IsFacing(Objects.Location Loc)
        {
            float f = (float)Math.Atan2(Loc.y - ObjectManager.PlayerObject.Pos.y, Loc.x - ObjectManager.PlayerObject.Pos.x);

            if (f < 0.0f)
            {
                f = f + PI * 2.0f;
            }
            else
            {
                if (f > PI * 2)
                {
                    f = f - PI * 2.0f;
                }
            }

            return f == ObjectManager.Facing ? true : false;
        }
        internal static void TurnCharacter(Objects.Location Loc)
        {
            lock (Inject.inject_Lock)
            {

                float f = (float)Math.Atan2(Loc.y - ObjectManager.PlayerObject.Pos.y, Loc.x - ObjectManager.PlayerObject.Pos.x);

                if (f < 0.0f)
                {
                    f = f + PI * 2.0f;
                }
                else
                {
                    if (f > PI * 2)
                    {
                        f = f - PI * 2.0f;
                    }
                }

                // Write the asm stuff for Lua_DoString
                String[] asm = new String[] 
                            {
                                //"mov EAX, [" + Inject.PlayerPtr + "]",
                                "mov ECX, [" + Inject.PlayerPtr + "]", // movement struct
                                "add ECX, 0x9A8",
                                "push 0x" + FloatToHex32(f),
                                "call " + (uint)Offsets.functions.SetFacing, // Set Facing Funktion
                                "retn"
                            };
                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " Calling Set Facing Update");
                Inject.InjectAndExecute(asm, true);

                SendMovementUpdate(0xDA, (uint)Environment.TickCount);
            }
        }
        #endregion
        // ^ Offset by Cencil

        #region DoString
        internal static void DoString(string code)
        {
            lock (Inject.inject_Lock)
            {

                if (ObjectManager.ExecuteOnce && ObjectManager.playerPtr != 0)
                {
                    ObjectManager.ExecuteOnce = false;
                    Register.LuaFunctions();
                    Ingame.PlaceAutoAttackAndShoot();
                }

                bool firstBool = BmWrapper.memory.WriteASCIIString(Inject.DoStringTextPtr, code + "\0");
                // Write the asm stuff for Lua_DoString
                String[] asm = new String[] 
                            {
                                "mov EDX, 0",
                                "mov ECX, " + (uint)Inject.DoStringTextPtr,
                                "call " + (uint)Offsets.functions.DoString,
                                "retn",    
                            };
                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " Calling Do String");
                Inject.InjectAndExecute(asm, false);

            }
        }
        #endregion

        internal static void PacketZoneChange()
        {
            String[] asm = new String[] 
                                    {
                                        "push ebp",
                                        "mov ebp, esp",
                                        "sub esp, 0x18",
                                        "xor esi, esi",

                                        "mov [ebp-0x14], esi",
                                        "mov [ebp-0x10], esi",
                                        "mov [ebp-0x0C], esi",
                                        "mov [ebp-0x8], esi",
                                        "mov eax, 0x0FFFFFFFF",
                                        "mov [ebp-0x4], eax",
                                        "mov eax, 0x007FF9E4",
                                        "mov [ebp-0x18], eax",
                                        
                                        "push 0x1F4                            ",
                                        "lea ecx, [ebp-0x18]",
                                        "mov eax, 0x00418190",
                                        "call eax",

                                        "push 0x5EF",
                                        "lea ecx, [ebp-0x18]",
                                        "mov eax, 0x00418190",
                                        "call eax ",

                                        "lea ecx, [ebp-18h]",
                                        "mov [ebp-4], esi",
                                        "mov eax, 0x005AB630",
                                        "call eax",

                                        "mov eax, 0x0FFFFFFFF",
                                        "cmp [ebp-0x0C], eax",
                                        "mov eax, 0x007FF9E4",
                                        "mov [ebp-0x18], eax",
                                        "jz @Exit",
                                        "lea eax, [ebp-0x0C]",
                                        "push eax",
                                        "lea ecx, [ebp-0x10]",
                                        "push ecx",
                                        "lea edx, [ebp-0x14]",
                                        "push edx",
                                        "lea ecx, [ebp-0x18]",
                                        "mov eax, 0x007FF9E8",
                                        "mov eax, [eax]",
                                        "call eax",
                                        "@Exit:",

                                        "mov esp, ebp",
                                        "pop ebp",

                                        ///////////////////////////////////
                                        //Wrapper.mem.ReadUInt(Wrapper.mem.ReadUInt(Wrapper.mem.ReadUInt(0xC7BCD4) + 0x88) + 0x28);
                                        
                                        //"call 0x005AB000",
                                        "retn"
                                    };
            Inject.InjectAndExecute(asm, true);

        }

        internal static void PacketSendLogout()
        {
            String[] asm = new String[] 
                                    {
                                        "push ebp",
                                        "mov ebp, esp",
                                        "sub esp, 0x18",
                                        "xor esi, esi",

                                        "mov [ebp-0x14], esi",
                                        "mov [ebp-0x10], esi",
                                        "mov [ebp-0x0C], esi",
                                        "mov [ebp-0x8], esi",
                                        "mov eax, 0x0FFFFFFFF",
                                        "mov [ebp-0x4], eax",
                                        "mov eax, 0x007FF9E4",
                                        "mov [ebp-0x18], eax",
                                        
                                        "push 0x04B",    
                                        //"push 0x1F4                            ",
                                        "lea ecx, [ebp-0x18]",
                                        "mov eax, 0x00418190",
                                        "call eax",

                                        "push 0x0",
                                        //"push 0x5EF",
                                        "lea ecx, [ebp-0x18]",
                                        "mov eax, 0x00418190",
                                        "call eax ",

                                        "lea ecx, [ebp-18h]",
                                        "mov [ebp-4], esi",
                                        "mov eax, 0x005AB630",
                                        "call eax",

                                        "mov eax, 0x0FFFFFFFF",
                                        "cmp [ebp-0x0C], eax",
                                        "mov eax, 0x007FF9E4",
                                        "mov [ebp-0x18], eax",
                                        "jz @Exit",
                                        "lea eax, [ebp-0x0C]",
                                        "push eax",
                                        "lea ecx, [ebp-0x10]",
                                        "push ecx",
                                        "lea edx, [ebp-0x14]",
                                        "push edx",
                                        "lea ecx, [ebp-0x18]",
                                        "mov eax, 0x007FF9E8",
                                        "mov eax, [eax]",
                                        "call eax",
                                        "@Exit:",

                                        "mov esp, ebp",
                                        "pop ebp",

                                        ///////////////////////////////////
                                        //Wrapper.mem.ReadUInt(Wrapper.mem.ReadUInt(Wrapper.mem.ReadUInt(0xC7BCD4) + 0x88) + 0x28);
                                        
                                        //"call 0x005AB000",
                                        "retn"
                                    };
            Inject.InjectAndExecute(asm, true);

        }

        #region Get Text multiple arguments
        internal static string[] GetText(string command, string[] arguments, int returnLength)
        {
            lock (Inject.inject_Lock)
            {
                // Create a string array with the number of arguments and fill it with empty strings
                int num;
                string[] strArray = new string[arguments.Length];
                for (num = 0; num < strArray.Length; num++)
                {
                    strArray[num] = string.Empty;
                }





                if (ObjectManager.ExecuteOnce && ObjectManager.playerPtr != 0)
                {
                    ObjectManager.ExecuteOnce = false;
                    Register.LuaFunctions();
                    Ingame.PlaceAutoAttackAndShoot();
                }
                bool firstBool = BmWrapper.memory.WriteASCIIString(Inject.DoStringTextPtr, command + "\0");

                String[] asm = new String[]
                        {
                            "mov EDX, 0",
                            "mov ECX, " + (uint)Inject.DoStringTextPtr,
                            "call " + (uint)Offsets.functions.DoString,
                            "retn", 
                        };
                Inject.InjectAndExecute(asm, false);

                for (int i = 0; i < arguments.Length; i++)
                {

                    bool thirdBool = BmWrapper.memory.WriteASCIIString(Inject.GetTextArgumentsPtr, arguments[i] + "\0");
                    if (thirdBool)
                    {
                        // Write the asm stuff for Lua_DoString
                        String[] asm2 = new String[] 
                                {
                                    "push 0",
                                    "or edx, 0FFFFFFFFh",
                                    "mov ecx, " + (uint)Inject.GetTextArgumentsPtr,
                                    "call " + (uint)Offsets.functions.GetText,
                                    "retn",    
                                };

                        uint argumentValue = Inject.InjectAndExecuteReturn(asm2, true, false);
                        if (argumentValue != 0)
                        {
                            strArray[i] = BmWrapper.memory.ReadASCIIString(argumentValue, returnLength);
                        }
                    }

                }

                return strArray;
            }
        }
        #endregion

        #region Get Text single argument
        internal static string GetText(string command, string argument, int returnLength)
        {
            lock (Inject.inject_Lock)
            {
                string str = "";


                if (ObjectManager.ExecuteOnce && ObjectManager.playerPtr != 0)
                {
                    ObjectManager.ExecuteOnce = false;
                    Register.LuaFunctions();
                    Ingame.PlaceAutoAttackAndShoot();
                }
                bool firstBool = BmWrapper.memory.WriteASCIIString(Inject.DoStringTextPtr, command + "\0");

                String[] asm = new String[]
                        {
                            "mov EDX, 0",
                            "mov ECX, " + (uint)Inject.DoStringTextPtr,
                            "call " + (uint)Offsets.functions.DoString,
                            "retn", 
                        };
                Inject.InjectAndExecute(asm, false);



                bool thirdBool = BmWrapper.memory.WriteASCIIString(Inject.GetTextArgumentsPtr, argument + "\0");
                if (thirdBool)
                {
                    // Write the asm stuff for Lua_DoString
                    String[] asm2 = new String[] 
                                {
                                    "push 0",
                                    "or edx, 0FFFFFFFFh",
                                    "mov ecx, " + Inject.GetTextArgumentsPtr,
                                    "call " + (uint)Offsets.functions.GetText,
                                    "retn",    
                                };
                    //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " Calling Get Text");
                    uint argumentValue = Inject.InjectAndExecuteReturn(asm2, true, false);
                    if (argumentValue != 0)
                    {
                        str = BmWrapper.memory.ReadASCIIString(argumentValue, returnLength);
                    }
                }

                return str;
            }
        }
        #endregion
        // ^ Offset by Cencil

        #region AutoLoot
        internal static void AutoLoot()
        {
            lock (Inject.inject_Lock)
            {
                // Write the asm stuff for Lua_DoString
                String[] asm = new String[] 
                    {
                        "call " + (uint)Offsets.functions.AutoLoot,
                        "retn",    
                    };
                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " Calling Auto Loot");
                Inject.InjectAndExecute(asm, true);
            }
        }
        #endregion
        // ^ Offset by Cencil

        #region GetLootSlots
        internal static byte GetLootSlots()
        {
            lock (Inject.inject_Lock)
            {
                String[] asm = new String[] 
                    {
                        "call " + (uint)Offsets.functions.GetLootSlots,
                        "retn",    
                    };
                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " Calling Get Loot Slots");
                return BmWrapper.memory.ReadByte(Inject.InjectAndExecuteReturn(asm, false, true));
            }
        }
        #endregion
        // ^ Offset by Cencil

        #region IsLooting
        internal static byte IsLooting()
        {
            lock (Inject.inject_Lock)
            {
                // Write the asm stuff for Lua_DoString
                String[] asm = new String[] 
                        {
                            "mov ecx, [" + Inject.PlayerPtr + "]",
                            "call " + (uint)Offsets.functions.IsLooting,
                            "retn",    
                        };
                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " Calling Is Looting");
                return BmWrapper.memory.ReadByte(Inject.InjectAndExecuteReturn(asm, false, true));
            }
        }
        #endregion

        #region IsLooting
        internal static void CastAoe(string parSpellName, Objects.Location parSpellPos)
        {
            lock (Inject.inject_Lock)
            {
                DoString("CastSpellByName('" + parSpellName + "')");

                uint posStruc = BmWrapper.memory.AllocateMemory(12);

                if (posStruc != 0)
                {
                    bool b1 = BmWrapper.memory.WriteFloat(posStruc, parSpellPos.x);
                    bool b2 = BmWrapper.memory.WriteFloat(posStruc + 4, parSpellPos.y);
                    bool b3 = BmWrapper.memory.WriteFloat(posStruc + 8, parSpellPos.z);

                    if (b1 && b2 && b3)
                    {

                        // Write the asm stuff for Lua_DoString
                        String[] asm = new String[] 
                        {
                            "mov eax, 0x40",
                            "mov [0xCECAC0], eax",
                            
                            "mov eax, [" + Inject.PlayerPtr + "]",
                            "mov ecx, " + (uint)posStruc,
                            "call " + (uint)0x6E60F0,
                            "retn",    
                        };
                        Inject.InjectAndExecute(asm, true);
                    }
                    BmWrapper.memory.FreeMemory(posStruc);
                }
            }
        }
        #endregion
        // ^ Offset by Cencil

        #region AntiAFK
        internal static void AntiAfk()
        {
            BmWrapper.memory.WriteInt(0x00CF0BC8, Environment.TickCount);
        }
        #endregion
        // ^ Offset by Cencil

        #region OnRightClick
        internal static void OnRightClickObject(uint baseAddr, int autoLoot)
        {
            lock (Inject.inject_Lock)
            {

                if (baseAddr != 0 && (autoLoot == 1 || autoLoot == 0))
                {
                    // Write the asm stuff for Lua_DoString
                    String[] asm = new String[] 
                        {
                            "push " + autoLoot,
                            "mov ECX, " + (uint)baseAddr,
                            "call " + (uint)Offsets.functions.OnRightClickObject,
                            "retn",    
                        };
                    //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " Calling On Rightclick Object");
                    Inject.InjectAndExecute(asm, true);
                }

            }
        }

        internal static void OnRightClickUnit(uint baseAddr, int AutoLoot)
        {
            lock (Inject.inject_Lock)
            {
                
                    if (baseAddr != 0 && (AutoLoot == 1 || AutoLoot == 0))
                    {
                        // Write the asm stuff for Lua_DoString
                        String[] asm = new String[] 
                        {
                        "push " + AutoLoot,
                        "mov ECX, " + baseAddr,
                        "call " + (uint)Offsets.functions.OnRightClickUnit,
                        "retn",    
                        };

                        Inject.InjectAndExecute(asm, true);
                    }
                
            }
        }
        #endregion
        // ^ Offset by Cencil

        #region SetTarget
        internal static void SetTarget(UInt64 guid)
        {
            lock (Call_Lock)
            {

                byte[] guidBytes = BitConverter.GetBytes(guid);
                String[] asm = new String[] 
                    {
                        "push " + BitConverter.ToInt32(guidBytes, 4),
                        "push " + BitConverter.ToInt32(guidBytes, 0),
                        "call " + (uint)0x493540,
                        "retn",    
                    };
                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " Calling Set Target");
                Inject.InjectAndExecute(asm, true);

            }
        }
        #endregion
        // ^ Offset by Cencil

        #region Stop running
        internal static void StopRunning()
        {
            lock (Inject.inject_Lock)
            {
                Calls.SetControlBit((uint)Offsets.controlBits.Front, 0);
                Calls.SetControlBit((uint)Offsets.controlBits.Back, 0);
            }
        }
        #endregion
        // ^ Offset by Cencil

        #region Set Control Bit
        internal static uint CGInputControl__GetActive()
        {
            lock (Inject.inject_Lock)
            {
                // Write the asm stuff for Lua_DoString
                String[] asm = new String[] 
                                {
                                    "call " + (uint)Offsets.functions.CGInputControl__GetActive,
                                    "retn",    
                                };
                return Inject.InjectAndExecuteReturn(asm, true, true);
            }
        }

        internal static void SetControlBit(uint bit, int state)
        {
            lock (Inject.inject_Lock)
            {
                String[] asm = new String[]
                    {
                        "call " + (uint)Offsets.functions.CGInputControl__GetActive,
                        "mov ECX, EAX",
                        "push 0" ,
                        "push " + (uint)Environment.TickCount,
                        "push " + (int)state,
                        "push " + (uint)bit,
                        "call " + (uint)Offsets.functions.CGInputControl__SetControlBit, // Set Facing Funktion
                        "retn"
                    };
                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " Calling Set Control Bit");
                Inject.InjectAndExecute(asm, true);
            }
        }
        #endregion
        // ^ Offset by Cencil

        #region SetMovementFlags
        internal static bool MovementContainsFlag(uint flag)
        {
            return (ObjectManager.PlayerObject.movementState & flag) == flag ? true : false;
        }

        internal static bool TargetMovementContainsFlag(uint flag)
        {
            return (ObjectManager.PlayerObject.movementState & flag) == flag ? true : false;
        }

        internal static bool MovementContains(uint movementFlags, uint flag)
        {
            return (movementFlags & flag) == flag ? true : false;
        }

        internal static bool MovementIsOnly(uint flag)
        {
            return ObjectManager.PlayerObject.movementState == flag ? true : false;
        }

        internal static bool TargetMovementIsOnly(uint flag)
        {
            return ObjectManager.TargetObject.movementState == flag ? true : false;
        }

        internal static void SetMovementFlags(uint flags)
        {
            try
            {
                BmWrapper.memory.WriteUInt(ObjectManager.PlayerObject.baseAdd + (uint)Offsets.descriptors.movementFlags, flags);
            }
            catch { }
        }
        #endregion
        // ^ Offset by Cencil

        #region send land
        private static float[] values = new float[] { 2, 12, 21, 30, 39, 48, 57, 66, 75, 83, 92 };
        internal static float SendFallLand()
        {
            lock (Inject.inject_Lock)
            {
                int range = 0;
                float tmpHealthPercent = ObjectManager.PlayerObject.healthPercent;
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] <= tmpHealthPercent - 3)
                    {
                        range = i;
                    }
                    else
                    {
                        break;
                    }
                }

                float z = (range * 5 + 15);
                float oldHigh = ObjectManager.PlayerObject.Pos.z;
                float toGo = oldHigh - z;


                //Calls.DoString("Jump()");
                String[] asm = new String[]
                        { 
                                "mov eax, [" + Inject.PlayerPtr + "]",
                                //"mov ebx, [eax + " + (uint)Offsets.descriptors.UnitPosZ + "]",
                                //"fld dword[eax + " + (uint)Offsets.descriptors.UnitPosZ + "]",

                                //"mov dword[ecx], 0x" + FloatToHex32(-z),
                                //"fld dword[ecx]",
                                "mov dword[eax + " + (uint)Offsets.descriptors.UnitPosZ + "], 0x" + FloatToHex32(toGo),

                                //"faddp",
                                //"fstp dword[eax + " + (uint)Offsets.descriptors.UnitPosZ + "]",
                                
                                "mov ecx, 825",
                                "mov [eax + 0xA20], ecx",
                                
                                "mov ECX, [" + Inject.PlayerPtr + "]",
                                "push 0",
                                "push 0",
                                "push 0x0C9",
                                "push " + (uint)Environment.TickCount,
                                "call " + (uint)Offsets.functions.SendMovementPacket, // Send Packet

                                "mov eax, [" + Inject.PlayerPtr + "]",
                                "mov dword[eax + " + (uint)Offsets.descriptors.UnitPosZ + "], 0x" + FloatToHex32(oldHigh),
                                

                                "mov ECX, [" + Inject.PlayerPtr + "]",
                                "push 0",
                                "push 0",
                                "push 0x0EE",
                                "push " + (uint)Environment.TickCount,
                                "call " + (uint)Offsets.functions.SendMovementPacket, // Send Packet
                                "retn",   

                        };

                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " Calling Send Movement Update");
                Inject.InjectAndExecute(asm, true);
                return z;
            }
        }
        #endregion

        private static string FloatToHex32(float value)
        {
            byte[] tmpBytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(tmpBytes);
            }
            string str = "";
            for (int i = 0; i < tmpBytes.Length; i++)
            {
                str += tmpBytes[i].ToString("X2");
            }

            return str;

        }

        #region ASM tele
        internal static void CancelTele()
        {
            cancelTele = true;
            String[] asm = new String[]
                    {
                        "mov esi, [" + Inject.PlayerPtr + "]",
                        "mov eax, 0x" + lastX,
                        "mov [esi + " + (uint)Offsets.descriptors.UnitPosX + "], eax",
                        "mov eax, 0x" + lastY,
                        "mov [esi + " + (uint)Offsets.descriptors.UnitPosY + "], eax",
                        "mov eax, 0x" + lastZ,
                        "mov [esi + " + (uint)Offsets.descriptors.UnitPosZ + "], eax",
                        "mov ecx, [" + (uint)Offsets.objectManager.ObjectManager +"]", 

                        "mov ecx, [" + (uint)Offsets.objectManager.ObjectManager +"]",
                        "mov eax, [ecx + 0xc0]" ,
                        "mov [0xC4DA98], eax",
                        "retn"
                    };
            Inject.InjectAndExecute(asm, true);
        }
        private static string lastX;
        private static string lastY;
        private static string lastZ;
        internal static bool cancelTele;

        private static string x;
        private static string y;
        private static string z;
        internal static void SetTeleStart(float parX, float parY, float parZ)
        {
            cancelTele = false;
            x = FloatToHex32(parX);
            y = FloatToHex32(parY);
            z = FloatToHex32(parZ);
        }

        //internal static bool StepTelePreview(float parX, float parY, float parZ, float parSeconds)
        //{
        //    lock (Inject.inject_Lock)
        //    {
        //        if (!cancelTele)
        //        {
        //            lastZ = FloatToHex32(parZ);
        //            lastY = FloatToHex32(parY);
        //            lastX = FloatToHex32(parX);

        //            // timestamp -> z -> y -> x -> ptr
        //            String[] asm = new String[]
        //            {
        //                "push " + parSeconds * 1000,
        //                "push 0x" + lastZ,
        //                "push 0x" + lastY,
        //                "push 0x" + lastX,
        //                //"push " + (uint)ptr,
        //                "call " + Inject.PorterPreview,
        //                "retn"
        //            };
        //            Inject.InjectAndExecute(asm, true);
        //            return BmWrapper.memory.ReadUInt(Inject.PlayerPtr) != 0;
        //        }
        //        return false;
        //    }
        //}

        internal static bool StepTelePreview(float parX, float parY, float parZ, float parSeconds)
        {
            lock (Inject.inject_Lock)
            {
                if (!cancelTele && ObjectManager.playerPtr != 0)
                {
                    lastZ = FloatToHex32(parZ);
                    lastY = FloatToHex32(parY);
                    lastX = FloatToHex32(parX);

                    // timestamp -> z -> y -> x -> ptr
                    String[] asm = new String[]
                    {
                        "push " + parSeconds * 1000,
                        "push 0x" + lastZ,
                        "push 0x" + lastY,
                        "push 0x" + lastX,
                        //"push " + (uint)ptr,
                        "call " + Inject.PorterPreview,
                        "push 0",
                        "push 0",
                        "mov ecx, [" + Inject.PlayerPtr + "]",
                        "call " + (uint)0x005FAC70,

                        "retn"
                    };
                    Inject.InjectAndExecute(asm, true);
                    return BmWrapper.memory.ReadUInt(Inject.PlayerPtr) != 0;
                }
                return false;
            }
        }

        internal static bool StepTele(float parX, float parY, float parZ,  float parSeconds, uint finalPort)
        {
            lock (Inject.inject_Lock)
            {
                if (!cancelTele && ObjectManager.playerPtr != 0)
                {
                    lastZ = FloatToHex32(parZ);
                    lastY = FloatToHex32(parY);
                    lastX = FloatToHex32(parX);

                    // timestamp -> z -> y -> x -> ptr
                    String[] asm = new String[]
                    {
                        "push " + finalPort, // is last port? 1 = yes
                        "push 0x" + z,
                        "push 0x" + y,
                        "push 0x" + x,
                        "push " + parSeconds * 1000,
                        "push 0x" + lastZ,
                        "push 0x" + lastY,
                        "push 0x" + lastX,
                        "call " + Inject.Porter,
                        "retn"
                    };

                    Inject.InjectAndExecute(asm, true);
                    return true;
                }
                return false;
            }
        }
        #endregion
    }
}
