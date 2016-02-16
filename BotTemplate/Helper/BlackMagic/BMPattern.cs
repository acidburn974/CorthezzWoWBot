using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Magic
{
	sealed public partial class BlackMagic
	{
		private class PatternDataEntry
		{
			public byte[] bData;
			public uint Start;
			public int Size;

			public PatternDataEntry() { }

			public PatternDataEntry(uint Start, int Size, byte[] bData)
			{
				this.Start = Start;
				this.Size = Size;
				this.bData = bData;
			}
		}

		private List<PatternDataEntry> m_Data;


		public uint FindPattern(byte[] bPattern, string szMask)
		{
			return FindPattern((uint)this.MainModule.BaseAddress, this.MainModule.ModuleMemorySize, bPattern, szMask);
		}

		public uint FindPattern(string szPattern, string szMask, char Delimiter)
		{
			string[] saPattern = szPattern.Split(Delimiter);
			byte[] bPattern = new byte[saPattern.Length];

			for (int i = 0; i < bPattern.Length; i++)
				bPattern[i] = Convert.ToByte(saPattern[i], 0x10);

			return FindPattern(bPattern, szMask);
		}

		public uint FindPattern(string szPattern, string szMask)
		{
			return FindPattern(szPattern, szMask, ' ');
		}

		public uint FindPattern(ProcessModule pModule, byte[] bPattern, string szMask)
		{
			return FindPattern((uint)pModule.BaseAddress, pModule.ModuleMemorySize, bPattern, szMask);
		}

		public uint FindPattern(ProcessModule pModule, string szPattern, string szMask, char Delimiter)
		{
			string[] saPattern = szPattern.Split(Delimiter);
			byte[] bPattern = new byte[saPattern.Length];

			for (int i = 0; i < bPattern.Length; i++)
				bPattern[i] = Convert.ToByte(saPattern[i], 0x10);

			return FindPattern(pModule, bPattern, szMask);
		}

		public uint FindPattern(ProcessModule pModule, string szPattern, string szMask)
		{
			return FindPattern(pModule, szPattern, szMask, ' ');
		}

		public uint FindPattern(ProcessModuleCollection pModules, byte[] bPattern, string szMask)
		{
			uint dwReturn = 0;

			foreach (ProcessModule pModule in pModules)
			{
				dwReturn = FindPattern(pModule, bPattern, szMask);
				if (dwReturn != 0)
					break;
			}

			return dwReturn;
		}

		public uint FindPattern(ProcessModuleCollection pModules, string szPattern, string szMask, char Delimiter)
		{
			string[] saPattern = szPattern.Split(Delimiter);
			byte[] bPattern = new byte[saPattern.Length];

			for (int i = 0; i < bPattern.Length; i++)
				bPattern[i] = Convert.ToByte(saPattern[i], 0x10);

			return FindPattern(pModules, bPattern, szMask);
		}

		public uint FindPattern(ProcessModuleCollection pModules, string szPattern, string szMask)
		{
			return FindPattern(pModules, szPattern, szMask, ' ');
		}

		public uint FindPattern(ProcessModule[] pModules, byte[] bPattern, string szMask)
		{
			return FindPattern(new ProcessModuleCollection(pModules), bPattern, szMask);
		}

		public uint FindPattern(ProcessModule[] pModules, string szPattern, string szMask, char Delimiter)
		{
			return FindPattern(new ProcessModuleCollection(pModules), szPattern, szMask, Delimiter);
		}

		public uint FindPattern(ProcessModule[] pModules, string szPattern, string szMask)
		{
			return FindPattern(new ProcessModuleCollection(pModules), szPattern, szMask, ' ');
		}

		public uint FindPattern(uint dwStart, int nSize, byte[] bPattern, string szMask)
		{
			PatternDataEntry dataentry = null;

			foreach (PatternDataEntry pda in m_Data)
			{
				if (dwStart == pda.Start && nSize == pda.Size)
				{
					dataentry = pda;
					break;
				}
			}

			if (dataentry == null)
			{
				dataentry = new PatternDataEntry(dwStart, nSize, this.ReadBytes(dwStart, nSize));
				m_Data.Add(dataentry);
			}

			return (uint)(dwStart + SPattern.FindPattern(dataentry.bData, bPattern, szMask));
		}

		public uint FindPattern(uint dwStart, int nSize, string szPattern, string szMask, char Delimiter)
		{
			string[] saPattern = szPattern.Split(Delimiter);
			byte[] bPattern = new byte[saPattern.Length];

			for (int i = 0; i < bPattern.Length; i++)
				bPattern[i] = Convert.ToByte(saPattern[i], 0x10);

			return FindPattern(dwStart, nSize, bPattern, szMask);
		}

		public uint FindPattern(uint dwStart, int nSize, string szPattern, string szMask)
		{
			return FindPattern(dwStart, nSize, szPattern, szMask, ' ');
		}
	}
}