using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotTemplate.Helper;
using System.Windows.Forms;
using System.Threading;

namespace BotTemplate.Interact
{
    internal static class Endscene
    {
        private static uint endscene = 0;

        internal static uint Return
        {
            get
            {
                return endscene;
            }
        }

        private static void inject(String[] asm, uint Addr)
        {
            BmWrapper.memory.Asm.Clear();
            foreach (string str in asm)
            {
                BmWrapper.memory.Asm.AddLine(str);
            }
            BmWrapper.memory.Asm.Inject(Addr);
        }

        private static uint IsSceneEnd = 0x5A17B6;
        private static uint IsSceneEnd2 = 0x5A17B6 + 0x6;
        private static byte[] oldBytes = new byte[] { 0xFF, 0x91, 0xA8, 0x00, 0x00, 0x00 };


        private static uint detourPtr;
        private static uint isReady;
        private static uint endScenePtr;

        internal static void Init()
        {
            detourPtr = Inject.AllocateCaves(0x256);
            endScenePtr = Inject.AllocateCaves(0x4);
            isReady = Inject.AllocateCaves(0x4);

            if (detourPtr != 0 && isReady != 0 && endScenePtr != 0)
            {
                BmWrapper.memory.WriteUInt(isReady, 0);

                string[] detour = new string[]
                {
                    "pushfd",
                    "pushad",
                    "mov eax, [ecx + 0xA8]",
                    "mov [" + endScenePtr + "], eax",
                    "mov eax, 0x01",
                    "mov [" + isReady + "], eax",
                    "popad",
                    "popfd",
                    "call DWORD[ecx + 0xA8]",
                    "jmp " + (uint)IsSceneEnd2,
                };
                inject(detour, detourPtr);

                string[] jmpToDetour = new string[]
                {
                    "jmp " + (uint)detourPtr,
                    "nop",

                };
                inject(jmpToDetour, IsSceneEnd);
                
                while (BmWrapper.memory.ReadUInt(isReady) == 0);

                endscene = BmWrapper.memory.ReadUInt(endScenePtr);
                BmWrapper.memory.WriteBytes(IsSceneEnd, oldBytes);

                BmWrapper.memory.FreeMemory(endScenePtr);
                BmWrapper.memory.FreeMemory(isReady);
                BmWrapper.memory.FreeMemory(detourPtr);
            }
        }
    }
}
