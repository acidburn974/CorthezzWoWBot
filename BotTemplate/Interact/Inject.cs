using System;
using BotTemplate.Helper;
using BotTemplate.Constants;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading;
using BotTemplate.Forms;

namespace BotTemplate.Interact
{
    internal static class Inject
    {
        #region Imports
        [DllImport("kernel32", EntryPoint = "VirtualAllocEx")]
        private static extern uint VirtualAllocEx(IntPtr hProcess, uint dwAddress, int nSize, uint dwAllocationType, uint dwProtect);

        internal static uint AllocateCaves(int nSize)
        {
            return VirtualAllocEx(BmWrapper.memory.ProcessHandle, 0, nSize, 0x00001000 | 0x00002000, 0x40);
        }

        internal static uint AllocateAt(uint address, int nSize)
        {
            return VirtualAllocEx(BmWrapper.memory.ProcessHandle, address, nSize, 0x00001000 | 0x00002000, 0x40);
        }
        #endregion

        #region Variables
        private static Random random = new Random();

        // Saves the first 5 bytes of the EndScene function
        private static String[] OverwrittenInstructions = new String[]
            {
                "mov EDI,EDI",
                "PUSH EBP",
                "MOV EBP,ESP",
            };

        private const int overByteCount = 5;
        // 9

        // Pointer to: Codecave, detour
        private static UInt32 DetourPtr;
        private static UInt32 codeCavePtr;
        private static UInt32 heartBeatPorterPtr;
        private static UInt32 heartBeatPorterPreviewPtr;
        
        internal static UInt32 DoStringTextPtr;
        internal static UInt32 GetTextArgumentsPtr;
        internal static UInt32 PlayerPtr;
        internal static UInt32 click2PortToggle;
        internal static UInt32 click2PortCoords;
        internal static UInt32 performDefaultActionDetour;

        internal static UInt32 timeStampMdofier;
        internal static UInt32 SendMovementUpdateDetourPtr;

        internal static UInt32 ptrStore1 = 0x007FEDAC;
        internal static UInt32 ptrStore2 = 0x007FEDAC + 4;
        internal static UInt32 isAttached = 0x007FEDB4;

        internal static UInt32 Porter
        {
            get
            {
                return heartBeatPorterPtr;
            }
        }

        internal static UInt32 PorterPreview
        {
            get
            {
                return heartBeatPorterPreviewPtr;
            }
        }

        // Size of our codecave
        private const int codeCaveSize = 0x256;

        // Used to trigger the jmp to the Codecave
        private static UInt32 codeCaveEnablePtr;
        private static UInt32 ingameCodeCaveEnablePtr;

        // Get function returnvalues
        private static UInt32 returnValue;

        internal static bool isHookApplied
        {
            get;
            private set;
        }

        internal static void Init()
        {
            byte[] LuaProtectionDisabler = new byte[] { 0xB8, 0x01, 0x00, 0x00, 0x00, 0xc3 };
            BmWrapper.memory.WriteByte(0x004C21C0, 0xEB); // Loot patch
            BmWrapper.memory.WriteBytes(0x494a57, LuaProtectionDisabler); // Lua Protection
            BmWrapper.memory.WriteByte(isAttached, 1);
        }
        #endregion

        #region Inject function
        private static void inject(String[] asm, uint Addr)
        {
            lock (inject_Lock)
            {
                BmWrapper.memory.Asm.Clear();
                foreach (string str in asm)
                {
                    BmWrapper.memory.Asm.AddLine(str);
                }
                BmWrapper.memory.Asm.Inject(Addr);
            }
        }
        #endregion

        #region Apply / Restore functrion
        internal static bool Apply()
        {
            if (!isHookApplied)
            {
                Endscene.Init();
                byte[] tmpBytes = BmWrapper.memory.ReadBytes(Endscene.Return, 5);
                bool x = tmpBytes.SequenceEqual(Offsets.EndSceneOriginal);
                if (x)
                {
                    DetourPtr = AllocateCaves(0x4AC);
                    codeCavePtr = AllocateCaves(codeCaveSize);
                    codeCaveEnablePtr = AllocateCaves(0x4);
                    ingameCodeCaveEnablePtr = AllocateCaves(0x4);
                    PlayerPtr = AllocateCaves(0x4);
                    heartBeatPorterPtr = AllocateCaves(0xFF);
                    heartBeatPorterPreviewPtr = AllocateCaves(0xFF);
                    Log.Add(heartBeatPorterPreviewPtr.ToString("X8"));
                    returnValue = AllocateCaves(0x4);
                    DoStringTextPtr = AllocateCaves(0x1400);
                    GetTextArgumentsPtr = AllocateCaves(0xA);
                    click2PortToggle = AllocateCaves(0x1);
                    click2PortCoords = AllocateCaves(0x12);
                    performDefaultActionDetour = AllocateCaves(0xFF);
                    
                    if (returnValue != 0 && heartBeatPorterPtr != 0 && PlayerPtr != 0
                        && codeCaveEnablePtr != 0 && codeCavePtr != 0 && DetourPtr != 0 && ingameCodeCaveEnablePtr != 0
                        && DoStringTextPtr != 0
                        && GetTextArgumentsPtr != 0
                        && click2PortToggle != 0
                        && click2PortCoords != 0
                        && performDefaultActionDetour != 0
                        )
                    {
                        Log.Add("EndScene: " + Endscene.Return);
                        Log.Add("click2PortToggle: " + click2PortToggle.ToString("X8"));
                        Log.Add("click2PortCoords: " + click2PortCoords.ToString("X8"));
                        Log.Add("PerformDefaultActionDetour: " + performDefaultActionDetour.ToString("X8"));
                        CreatePerformDefaultActionDetour();
                        SetJumpToPerformDefaultActionDetour();

                        bool bool1 = BmWrapper.memory.ReadBytes(ptrStore1, 4).SequenceEqual(new byte[] { 0x00, 0x00, 0x00, 0x00 });
                        bool bool2 = BmWrapper.memory.ReadBytes(ptrStore2, 4).SequenceEqual(new byte[] { 0x00, 0x00, 0x00, 0x00 });
                        if (bool1 && bool2)
                        {
                            timeStampMdofier = AllocateCaves(0x4);
                            SendMovementUpdateDetourPtr = AllocateCaves(0x4AC);
                            if (timeStampMdofier != 0 && SendMovementUpdateDetourPtr != 0)
                            {
                                BmWrapper.memory.WriteUInt(timeStampMdofier, 0);
                                //Log.Add("PTR store 1: " + ptrStore1.ToString("X8"));
                                //Log.Add("PTR store 2: " + ptrStore2.ToString("X8"));
                                Log.Add("timeStampModifier: " + timeStampMdofier.ToString("X8"));
                                //Log.Add("SendMovementUpdate Detour: " + SendMovementUpdateDetourPtr.ToString("X8"));
                                BmWrapper.memory.WriteUInt(ptrStore1, timeStampMdofier);
                                BmWrapper.memory.WriteUInt(ptrStore2, SendMovementUpdateDetourPtr);
                                CreateSendMovementUpdateDetour();
                                SetJumpToSendMovementUpdateDetour();
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            timeStampMdofier = BmWrapper.memory.ReadUInt(ptrStore1);
                            SendMovementUpdateDetourPtr = BmWrapper.memory.ReadUInt(ptrStore2);
                            Log.Add("timeStampModifier: " + timeStampMdofier.ToString("X8"));
                            
                        }

                        Log.Add(timeStampMdofier.ToString("X8"));
                        BmWrapper.memory.WriteInt(returnValue, 0);
                        CreateCodeCave();
                        CreateDetour();
                        CreateHeartBeatPorter();
                        CreateHeartBeatPorterPreview();
                        SetEndSceneJmpToDetour();
                        isHookApplied = true;
                        return true;
                    }
                }
            }
            return false;
        }

        internal static void Restore()
        {
            // Lets check if were hooked
            if (!isHookApplied)
            {
                // Were not hooked, so im gonna do nothing.
                return;
            }

            // Lets restore the orignal asm
            BmWrapper.memory.WriteBytes(Endscene.Return, Offsets.EndSceneOriginal);
            BmWrapper.memory.WriteBytes((uint)Offsets.functions.PerformDefaultAction, new byte[] {0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x1C});
            //BmWrapper.memory.WriteBytes((uint)Offsets.functions.SendMovementPacket, new byte[] { 0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x18 });
            BmWrapper.memory.FreeMemory(codeCavePtr);
            BmWrapper.memory.FreeMemory(DetourPtr);
            BmWrapper.memory.FreeMemory(heartBeatPorterPtr);
            BmWrapper.memory.FreeMemory(heartBeatPorterPreviewPtr);
            BmWrapper.memory.FreeMemory(PlayerPtr);
            BmWrapper.memory.FreeMemory(codeCaveEnablePtr);
            BmWrapper.memory.FreeMemory(ingameCodeCaveEnablePtr);
            BmWrapper.memory.FreeMemory(returnValue);
            BmWrapper.memory.FreeMemory(DoStringTextPtr);
            BmWrapper.memory.FreeMemory(GetTextArgumentsPtr);
            BmWrapper.memory.FreeMemory(click2PortCoords);
            BmWrapper.memory.FreeMemory(click2PortToggle);
            BmWrapper.memory.FreeMemory(performDefaultActionDetour);
            BmWrapper.memory.WriteByte(isAttached, 0);
            // Lets make IsApplied false
            isHookApplied = false;
        }

        #endregion

        #region Create Detour / Codecave function
        private static void CreateCodeCave()
        {
            String[] CodeCaveASM = new String[]
            {
                    "popad",
                    "popfd",
                    "jmp " + ((uint)Endscene.Return + (uint) 5).ToString()
                };

            inject(CodeCaveASM, codeCavePtr);
        }

        private static void SetEndSceneJmpToDetour()
        {
            String[] JmpASM = new String[]
            {
                "jmp " + DetourPtr.ToString(),
            };

            inject(JmpASM, (uint)Endscene.Return);
        }

        private static void CreateHeartBeatPorterPreview()
        {
            // timestamp -> z -> y -> x -> ptr
            String[] HeartBeatPorterASM = new String[]
                    {
                        //"mov eax, 0x1",
                        "pop EBP",
                        
                        //"pop EAX",
                        //"mov esi, [0xC7BCD4]",
                        //"mov esi, [esi + 0x88]",
                        //"mov esi, [esi + 0x28]",
                        "mov esi, [" + PlayerPtr + "]",

                        "mov eax, [esi + " + (uint)Offsets.descriptors.movementFlags + "]",
                        "and eax, 1b",
                        "mov [esi + " + (uint)Offsets.descriptors.movementFlags + "], eax",
                        
                        "pop eax",
                        "mov [esi + " + (uint)Offsets.descriptors.UnitPosX + "], eax",
                        
                        "pop eax",
                        "mov [esi + " + (uint)Offsets.descriptors.UnitPosY + "], eax",
                        
                        "pop eax",
                        "mov [esi + " + (uint)Offsets.descriptors.UnitPosZ + "], eax",
                        
                        "call " + (uint)0x0042B790, // get the current timestamp
                        
                        "pop EDI",
                        "add [" + Inject.timeStampMdofier + "], EDI", // add seconds to timestampmodifier
                        "mov ECX, ESI", // move player ptr in ecx
                        "push 0",
                        "push 0",
                        "push 0xEE", // heartbeat opcode
                        "push EAX", // push timestamp
                        "call " + (uint)Offsets.functions.SendMovementPacket, // Send Packet

                        "mov eax, [esi + " + (uint)Offsets.descriptors.movementFlags + "]",
                        "and eax, 0b",
                        "mov [esi + " + (uint)Offsets.descriptors.movementFlags + "], eax",
                        "push ebp",
                        
                        "retn"
                    };
            inject(HeartBeatPorterASM, heartBeatPorterPreviewPtr);
        }

        private static void CreateHeartBeatPorter()
        {
            // timestamp -> z -> y -> x -> ptr
            String[] HeartBeatPorterASM = new String[]
                    {
                        //"mov eax, 0x1",
                        "pop EBP",
                        
                        //"pop EAX",
                        //"mov esi, [0xC7BCD4]",
                        //"mov esi, [esi + 0x88]",
                        //"mov esi, [esi + 0x28]",
                        "mov esi, [" + PlayerPtr + "]",
                        
                        "mov eax, [esi + " + (uint)Offsets.descriptors.movementFlags + "]",
                        "or eax, 1b",
                        "mov [esi + " + (uint)Offsets.descriptors.movementFlags + "], eax",
                        
                        "pop eax",
                        "mov [esi + " + (uint)Offsets.descriptors.UnitPosX + "], eax",
                        
                        "pop eax",
                        "mov [esi + " + (uint)Offsets.descriptors.UnitPosY + "], eax",
                        
                        "pop eax",
                        "mov [esi + " + (uint)Offsets.descriptors.UnitPosZ + "], eax",

                        "mov eax, [" + (uint)Offsets.objectManager.ObjectManager +"]", 
                        "mov eax, [eax + 0xc0]" ,
                        "mov [0xC4DA98], eax",

                        "call " + (uint)0x0042B790, // get the current timestamp

                        "pop EDI",
                        "add [" + Inject.timeStampMdofier + "], EDI", // add seconds to timestampmodifier
                        "mov ECX, ESI", // move player ptr in ecx
                        "push 0",
                        "push 0",
                        "push 0xEE", // heartbeat opcode
                        "push EAX", // push timestamp
                        "call " + (uint)Offsets.functions.SendMovementPacket, // Send Packet

                        "mov eax, 0",
                        "mov [0xC4DA98], eax",

                        "mov eax, [esi + " + (uint)Offsets.descriptors.movementFlags + "]",
                        "and eax, 0b",
                        "mov [esi + " + (uint)Offsets.descriptors.movementFlags + "], eax",

                        "pop edx",
                        "pop ecx",
                        "pop ebx",
                        
                        "pop eax",
                        //"mov edi,edi",
                        
                        "cmp eax,0",
                        //lets make a jne
                        "jne @out",
                        
                        "mov [esi + " + (uint)Offsets.descriptors.UnitPosX + "], edx",
                        "mov [esi + " + (uint)Offsets.descriptors.UnitPosY + "], ecx",
                        "mov [esi + " + (uint)Offsets.descriptors.UnitPosZ + "], ebx",

                        "push ebp",
                        "retn",
                        
                        "@out:",
                        "mov eax, [" + (uint)Offsets.objectManager.ObjectManager +"]", 
                        "mov eax, [eax + 0xc0]" ,
                        "mov [0xC4DA98], eax",
                        "push ebp",
                        "retn"
                    };
            inject(HeartBeatPorterASM, heartBeatPorterPtr);
        }

        private static void SetJumpToSendMovementUpdateDetour()
        {
            String[] DetourASM = new String[]
            {
                "jmp " + ((uint)SendMovementUpdateDetourPtr),
                "nop"
            };
            inject(DetourASM, (uint)Offsets.functions.SendMovementPacket);
        }

        private static void SetJumpToPerformDefaultActionDetour()
        {
            String[] DetourASM = new String[]
            {
                "jmp " + ((uint)performDefaultActionDetour),
                "nop"
            };
            inject(DetourASM, (uint)Offsets.functions.PerformDefaultAction);
        }


        private static void CreateSendMovementUpdateDetour()
        {
            String[] DetourASM = new String[]
            {
                "PUSH EBP",
                "MOV EBP,ESP",
                "SUB ESP,0x18",

                //"pushfd",
                //"pushad",
                
                "push eax",
                "mov eax, [" + timeStampMdofier + "]",
                "add [esp + 36], eax",
                "pop eax",
                
                //"popad",
                //"popfd",

                "jmp " + ((uint)Offsets.functions.SendMovementPacket + (uint) 6).ToString()
            };
            inject(DetourASM, SendMovementUpdateDetourPtr);
        }

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        private static void CreatePerformDefaultActionDetour()
        {
            ProcessModule pm = BmWrapper.memory.GetModule("USER32.dll");
            int getKeyStateAddr = GetProcAddress(pm.BaseAddress, "GetAsyncKeyState").ToInt32();
            int getShowWindowAddr = GetProcAddress(pm.BaseAddress, "ShowWindow").ToInt32();

            String[] DetourASM = new String[]
            {
                // perform default action 6 first bytes
                "PUSH EBP",
                "MOV EBP,ESP",
                "SUB ESP,0x1C",
                // perform default action 6 first bytes

                "pushfd",
                "pushad",

                "mov eax, [ecx + 0x360]",
                "mov [" + click2PortCoords + "], eax",

                "mov eax, [ecx + 0x364]",
                "mov [" + click2PortCoords + " + 0x4], eax",

                "mov eax, [ecx + 0x368]",
                "mov [" + click2PortCoords + " + 0x8], eax",
                
                "PUSH 0xA2",
                "CALL " + (uint)getKeyStateAddr,
                "AND EAX, 0xFFFF8000",
                "CMP EAX, 0xFFFF8000",
                "JNE @outSix",

                "mov eax, 1",
                "mov [" + click2PortToggle + "], eax",

                "@outSix:",

                "popad",
                "popfd",

                "jmp " + ((uint)Offsets.functions.PerformDefaultAction + (uint)6).ToString(),
            };
            inject(DetourASM, performDefaultActionDetour);
        }

        private static void CreateDetour()
        {
            String[] DetourASM = new String[]
            {
                // IsSceneEnd 5 first bytes
                //"push ebx",
                //"push esi",
                //"push edi",
                //"mov edi, ecx",
                "mov EDI,EDI",
                "PUSH EBP",
                "MOV EBP,ESP",

                "pushfd",
                "pushad",

                // ######### call always #############
                //lets test if were enabled
                "mov eax, [" + codeCaveEnablePtr + "]",
                "cmp eax, 1",
                //lets make a jne
                "jne @outOne",
                "mov esi, 0",
                "mov [" + returnValue + "], esi",
                //lets call the "user" injected code
                "call " + codeCavePtr,
                //lets get a return value
                "mov [" + returnValue + "], eax",
                //Lets dissable 
                "mov edx,0",
                "mov [" + codeCaveEnablePtr + "],edx",
                //lets end the jne
                "@outOne:",
                // ######### call always #############

                // ######### call only if ingame #############
                //lets test if were enabled
                "mov eax, [" + ingameCodeCaveEnablePtr + "]",
                "cmp eax, 1",
                //lets make a jne
                "jne @outThree",
                "mov esi, 0",
                "mov [" + PlayerPtr + "], esi",
                "mov [" + returnValue + "], esi",
                "mov esi, 1",
                "cmp [0xB4B424], esi",
                "jne @outTwo",
                "mov esi, [0xC7BCD4]",
                "mov esi, [esi + 0x88]",
                "mov esi, [esi + 0x28]",
                "mov [" + PlayerPtr + "], esi",
                //lets call the "user" injected code
                "call " + codeCavePtr,
                //lets get a return value
                "mov [" + returnValue + "], eax",
                "@outTwo:",
                //Lets dissable 
                "mov edx,0",
                "mov [" + ingameCodeCaveEnablePtr + "],edx",
                //lets end the jne
                "@outThree:",
                // ######### call only if ingame #############

                "popad",
                "popfd",

                //lets jmp back
                "jmp " + ((uint)Endscene.Return + (uint)5).ToString(),

            };

            //inject(OverwrittenInstructions, DetourPtr);

            //bool firstBool = BmWrapper.memory.WriteBytes(DetourPtr, OverwrittenBytes);
            //if (firstBool)
            {
                inject(DetourASM, DetourPtr);
            }
        }
        #endregion

        #region Inject and Execute
        internal static bool isWorking
        {
            private set;
            get;
        }

        internal static readonly object inject_Lock = new object();

        private static cTimer waitForDetour = new cTimer(5);
        internal static void InjectAndExecute(String[] ASM, bool needToBeIngame)
        {
            try
            {
                lock (inject_Lock)
                {
                    //Lets Inject the passed ASM
                    inject(ASM, codeCavePtr);
                    
                    //Lets enable
                    bool firstBool = false;
                    if (needToBeIngame)
                    {
                        firstBool = BmWrapper.memory.WriteInt(ingameCodeCaveEnablePtr, 1);
                    }
                    else
                    {
                        firstBool = BmWrapper.memory.WriteInt(codeCaveEnablePtr, 1);
                    }
                    if (firstBool)
                    {
                        //while were enabled lets sleep
                        if (needToBeIngame)
                        {
                            while (BmWrapper.memory.ReadInt(ingameCodeCaveEnablePtr) == 1) Thread.CurrentThread.Join(10);
                        }
                        else
                        {
                            while (BmWrapper.memory.ReadInt(codeCaveEnablePtr) == 1) Thread.CurrentThread.Join(10);
                        }
                    }
                }

            }
            catch
            { }
        }

        internal static uint InjectAndExecuteReturn(String[] ASM, bool IsResultString, bool needToBeIngame)
        {
            //Lets open a new byte list
            //byte[] tempsByte = new byte[0];

            try
            {
                lock (inject_Lock)
                {
                    // reset return value pointer

                    //Lets Inject the passed ASM
                    inject(ASM, codeCavePtr);

                    //Lets enable the hook
                    bool secondBool = false;
                    if (needToBeIngame)
                    {
                        secondBool = BmWrapper.memory.WriteInt(ingameCodeCaveEnablePtr, 1);
                    }
                    else
                    {
                        secondBool = BmWrapper.memory.WriteInt(codeCaveEnablePtr, 1);
                    }
                    if (secondBool)
                    {
                        //while were enabled lets sleep
                        if (needToBeIngame)
                        {
                            while (BmWrapper.memory.ReadInt(ingameCodeCaveEnablePtr) == 1) Thread.CurrentThread.Join(10);
                        }
                        else
                        {
                            while (BmWrapper.memory.ReadInt(codeCaveEnablePtr) == 1) Thread.CurrentThread.Join(10);
                        }
                        if (!IsResultString)
                        {
                            return returnValue;
                        }
                        else
                        {
                            return BmWrapper.memory.ReadUInt(returnValue);
                        }
                    }

                }

            }
            catch { }

            return 0x0;

        }
        #endregion
    }
}
