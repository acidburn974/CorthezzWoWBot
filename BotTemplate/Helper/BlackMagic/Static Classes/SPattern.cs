using System;
using System.Diagnostics;

namespace Magic
{
	/// <summary>
	/// Provides static methods for finding a pattern or signature inside the memory of another process and its modules.
	/// </summary>
	public static class SPattern
	{
		//public static uint FindPattern(IntPtr hProcess, ProcessModule pMod, string szPattern, string szMask)
		//{
		//	return FindPattern(hProcess, (uint)pMod.BaseAddress, pMod.ModuleMemorySize, szPattern, szPattern, ' ');
		//}

		/// <summary>
		/// Finds a pattern or signature inside another process.
		/// </summary>
		/// <param name="hProcess">Handle to the process in whose memory pattern will be found.</param>
		/// <param name="pMod">Module which will be searched for the pattern.</param>
		/// <param name="szPattern">A character-delimited string representing the pattern to be found.</param>
		/// <param name="szMask">A string of 'x' (match), '!' (not-match), or '?' (wildcard).</param>
		/// <param name="Delimiter">Determines how the string will be split.  If null, defaults to ' '.</param>
		/// <returns>Returns 0 on failure, or the address of the start of the pattern on success.</returns>
		public static uint FindPattern(IntPtr hProcess, ProcessModule pMod, string szPattern, string szMask, params char[] Delimiter)
		{
			return FindPattern(hProcess, (uint)pMod.BaseAddress, pMod.ModuleMemorySize, szPattern, szMask, Delimiter);
		}

		/// <summary>
		/// Finds a pattern or signature inside another process.
		/// </summary>
		/// <param name="hProcess">Handle to the process in whose memory pattern will be found.</param>
		/// <param name="pMods">An array of modules which will be searched for the pattern.</param>
		/// <param name="szPattern">A character-delimited string representing the pattern to be found.</param>
		/// <param name="szMask">A string of 'x' (match), '!' (not-match), or '?' (wildcard).</param>
		/// <param name="Delimiter">Determines how the string will be split.  If null, defaults to ' '.</param>
		/// <returns>Returns 0 on failure, or the address of the start of the pattern on success.</returns>
		public static uint FindPattern(IntPtr hProcess, ProcessModule[] pMods, string szPattern, string szMask, params char[] Delimiter)
		{
			return FindPattern(hProcess, new ProcessModuleCollection(pMods), szPattern, szMask, Delimiter);
		}

		/// <summary>
		/// Finds a pattern or signature inside another process.
		/// </summary>
		/// <param name="hProcess">Handle to the process in whose memory pattern will be found.</param>
		/// <param name="pMods">An array of modules which will be searched for the pattern.</param>
		/// <param name="szPattern">A character-delimited string representing the pattern to be found.</param>
		/// <param name="szMask">A string of 'x' (match), '!' (not-match), or '?' (wildcard).</param>
		/// <param name="Delimiter">Determines how the string will be split.  If null, defaults to ' '.</param>
		/// <returns>Returns 0 on failure, or the address of the start of the pattern on success.</returns>
		public static uint FindPattern(IntPtr hProcess, ProcessModuleCollection pMods, string szPattern, string szMask, params char[] Delimiter)
		{
			uint dwRet = 0;

			foreach (ProcessModule pMod in pMods)
				if ((dwRet = FindPattern(hProcess, pMod, szPattern, szMask, Delimiter)) != 0)
					break;

			return dwRet;
		}

		//public static uint FindPattern(IntPtr hProcess, uint dwStart, int nSize, string szPattern, string szMask)
		//{
		//	return FindPattern(hProcess, dwStart, nSize, szPattern, szMask, ' ');
		//}

		/// <summary>
		/// Finds a pattern or signature inside another process.
		/// </summary>
		/// <param name="hProcess">Handle to the process in whose memory pattern will be found.</param>
		/// <param name="dwStart">Address on which the search will start.</param>
		/// <param name="nSize">Number of bytes in memory that will be searched.</param>
		/// <param name="szPattern">A character-delimited string representing the pattern to be found.</param>
		/// <param name="szMask">A string of 'x' (match), '!' (not-match), or '?' (wildcard).</param>
		/// <param name="Delimiter">Determines how the string will be split.  If null, defaults to ' '.</param>
		/// <returns>Returns 0 on failure, or the address of the start of the pattern on success.</returns>
		public static uint FindPattern(IntPtr hProcess, uint dwStart, int nSize, string szPattern, string szMask, params char[] Delimiter)
		{
			if (Delimiter == null)
				Delimiter = new char[1] { ' ' };

			string[] saPattern = szPattern.Split(Delimiter);
			byte[] bPattern = new byte[saPattern.Length];

			for (int i = 0; i < saPattern.Length; i++)
				bPattern[i] = Convert.ToByte(saPattern[i], 0x10);

			return FindPattern(hProcess, dwStart, nSize, bPattern, szMask);
		}

		/// <summary>
		/// Finds a pattern or signature inside another process.
		/// </summary>
		/// <param name="hProcess">Handle to the process in whose memory pattern will be found.</param>
		/// <param name="pMod">Module which will be searched for the pattern.</param>
		/// <param name="bPattern">A byte-array representing the pattern to be found.</param>
		/// <param name="szMask">A string of 'x' (match), '!' (not-match), or '?' (wildcard).</param>
		/// <returns>Returns 0 on failure, or the address of the start of the pattern on success.</returns>
		public static uint FindPattern(IntPtr hProcess, ProcessModule pMod, byte[] bPattern, string szMask)
		{
			return FindPattern(hProcess, (uint)pMod.BaseAddress, pMod.ModuleMemorySize, bPattern, szMask);
		}

		/// <summary>
		/// Finds a pattern or signature inside another process.
		/// </summary>
		/// <param name="hProcess">Handle to the process in whose memory pattern will be found.</param>
		/// <param name="pMods">An array of modules which will be searched for the pattern.</param>
		/// <param name="bPattern">A byte-array representing the pattern to be found.</param>
		/// <param name="szMask">A string of 'x' (match), '!' (not-match), or '?' (wildcard).</param>
		/// <returns>Returns 0 on failure, or the address of the start of the pattern on success.</returns>
		public static uint FindPattern(IntPtr hProcess, ProcessModule[] pMods, byte[] bPattern, string szMask)
		{
			return FindPattern(hProcess, new ProcessModuleCollection(pMods), bPattern, szMask);
		}

		/// <summary>
		/// Finds a pattern or signature inside another process.
		/// </summary>
		/// <param name="hProcess">Handle to the process in whose memory pattern will be found.</param>
		/// <param name="pMods">An array of modules which will be searched for the pattern.</param>
		/// <param name="bPattern">A byte-array representing the pattern to be found.</param>
		/// <param name="szMask">A string of 'x' (match), '!' (not-match), or '?' (wildcard).</param>
		/// <returns>Returns 0 on failure, or the address of the start of the pattern on success.</returns>
		public static uint FindPattern(IntPtr hProcess, ProcessModuleCollection pMods, byte[] bPattern, string szMask)
		{
			uint dwRet = 0;

			foreach (ProcessModule pMod in pMods)
				if ((dwRet = FindPattern(hProcess, pMod, bPattern, szMask)) != 0)
					break;

			return dwRet;
		}

		/// <summary>
		/// Finds a pattern or signature inside another process.
		/// </summary>
		/// <param name="hProcess">Handle to the process in whose memory pattern will be found.</param>
		/// <param name="dwStart">Address on which the search will start.</param>
		/// <param name="nSize">Number of bytes in memory that will be searched.</param>
		/// <param name="bPattern">A byte-array representing the pattern to be found.</param>
		/// <param name="szMask">A string of 'x' (match), '!' (not-match), or '?' (wildcard).</param>
		/// <returns>Returns 0 on failure, or the address of the start of the pattern on success.</returns>
		public static uint FindPattern(IntPtr hProcess, uint dwStart, int nSize, byte[] bPattern, string szMask)
		{
			if (bPattern == null || bPattern.Length == 0)
				throw new ArgumentNullException("bData");

			if (bPattern.Length != szMask.Length)
				throw new ArgumentException("bData and szMask must be of the same size");

			byte[] bData = SMemory.ReadBytes(hProcess, dwStart, nSize);
			if (bData == null)
				throw new Exception("Could not read memory in FindPattern.");

			return (uint)(dwStart + FindPattern(bData, bPattern, szMask));
		}

		/*
		/// <summary>
		/// Finds a pattern or signature inside another process.
		/// </summary>
		/// <param name="hProcess">Handle to the process in whose memory pattern will be found.</param>
		/// <param name="dwStart">Address on which the search will start.</param>
		/// <param name="nSize">Number of bytes in memory that will be searched.</param>
		/// <param name="bPattern">A byte-array representing the pattern to be found.</param>
		/// <param name="szMask">A string of 'x' (match), '!' (not-match), or '?' (wildcard).</param>
		/// <returns>Returns 0 on failure, or the address of the start of the pattern on success.</returns>
		public static uint FindPattern(IntPtr hProcess, uint dwStart, int nSize, byte[] bPattern, string szMask)
		{
			if (bPattern.Length != szMask.Length)
				throw new ArgumentException("bPattern and szMask are different lengths.");

			int ix, iy;
			bool bFound = false;

			byte[] bData = SMemory.ReadBytes(hProcess, dwStart, nSize);
			if (bData == null)
				throw new Exception("Could not read memory in FindPattern.");

			try
			{
				for (ix = 0; ix < bData.Length; ix++)
				{
					bFound = true;
					for (iy = 0; iy < bPattern.Length; iy++)
					{
						if ((szMask[iy] == 'x' && bPattern[iy] != bData[ix + iy]) ||
							(szMask[iy] == '!' && bPattern[iy] == bData[ix + iy]))
						{
							bFound = false;
							break;
						}
					}

					if (bFound)
						return (uint)(ix + dwStart);
				}
			}
			catch (Exception ex)
			{
#if DEBUG
				//Console.WriteLine(ex.Message);
#endif
			}

			return 0;
		}
 
		*/

		/// <summary>
		/// Finds a given pattern in an array of bytes.
		/// </summary>
		/// <param name="bData">Array of bytes in which to search for the pattern.</param>
		/// <param name="bPattern">A byte-array representing the pattern to be found.</param>
		/// <param name="szMask">A string of 'x' (match), '!' (not-match), or '?' (wildcard).</param>
		/// <returns>Returns 0 on failure, or the address of the start of the pattern on success.</returns>
		public static uint FindPattern(byte[] bData, byte[] bPattern, string szMask)
		{
			if (bData == null || bData.Length == 0)
				throw new ArgumentNullException("bData");

			if (bPattern == null || bPattern.Length == 0)
				throw new ArgumentNullException("bPattern");

			if (szMask == string.Empty)
				throw new ArgumentNullException("szMask");

			if (bPattern.Length != szMask.Length)
				throw new ArgumentException("Pattern and Mask lengths must be the same.");

			int ix, iy;
			bool bFound = false;
			int PatternLength = bPattern.Length;
			int DataLength = bData.Length - PatternLength;

			for (ix = 0; ix < DataLength; ix++)
			{
				bFound = true;
				for (iy = 0; iy < PatternLength; iy++)
				{
					if ((szMask[iy] == 'x' && bPattern[iy] != bData[ix + iy]) ||
						(szMask[iy] == '!' && bPattern[iy] == bData[ix + iy]))
					{
						bFound = false;
						break;
					}
				}

				if (bFound)
					return (uint)ix;
			}

			return 0;
		}
	}
}
