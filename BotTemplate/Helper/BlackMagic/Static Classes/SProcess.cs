using System;
using System.Diagnostics;

namespace Magic
{
	/// <summary>
	/// Contains static methods for finding and opening a process.
	/// </summary>
	public static class SProcess
	{
		/// <summary>
		/// Open process for external manipulation.
		/// </summary>
		/// <param name="dwProcessId">Process ID of external process to be opened.</param>
		/// <param name="dwAccessRights">Required access to external process.</param>
		/// <returns>Returns IntPtr.Zero on failure, non-zero on success.</returns>
		public static IntPtr OpenProcess(int dwProcessId, uint dwAccessRights)
		{
			return Imports.OpenProcess(dwAccessRights, false, dwProcessId);
		}

		/// <summary>
		/// Open process for external manipulation.  PROCESS_ALL_ACCESS will automatically be granted and handle will not be inherited.
		/// </summary>
		/// <param name="dwProcessId">The unique process ID of the external program.</param>
		/// <returns>Returns IntPtr.Zero on failure, non-zero on success.</returns>
		public static IntPtr OpenProcess(int dwProcessId)
		{
			return OpenProcess(dwProcessId, AccessRights.PROCESS_ALL_ACCESS);
		}

		/// <summary>
		/// Open process for external manipulation.
		/// </summary>
		/// <param name="hWnd">Handle to the main window of the process to be opened.</param>
		/// <param name="dwAccessRights">Required access to external process.</param>
		/// <returns>Returns IntPtr.Zero on failure, non-zero on success.</returns>
		public static IntPtr OpenProcess(IntPtr hWnd, uint dwAccessRights)
		{
			return OpenProcess(GetProcessFromWindow(hWnd), dwAccessRights);
		}

		/// <summary>
		/// Open process for external manipulation.  PROCESS_ALL_ACCESS will automatically be granted and handle will not be inherited.
		/// </summary>
		/// <param name="hWnd">Main window handle of the process.</param>
		/// <returns>Returns IntPtr.Zero on failure, non-zero on success.</returns>
		public static IntPtr OpenProcess(IntPtr hWnd)
		{
			//get process id, then open with PROCESS_ALL_ACCESS rights
			return OpenProcess(GetProcessFromWindow(hWnd), AccessRights.PROCESS_ALL_ACCESS);
		}

		/// <summary>
		/// Gets the process ID of the process that created the supplied window handle.
		/// </summary>
		/// <param name="hWnd">Handle to the main window of the process in question.</param>
		/// <returns>Returns non-zero on success, zero on failure.</returns>
		public static int GetProcessFromWindow(IntPtr hWnd)
		{
			int dwProcessId = 0;
			Imports.GetWindowThreadProcessId(hWnd, out dwProcessId);
			return dwProcessId;
		}

		/// <summary>
		/// Gets the process ID of the process that created the first window to match the given window title.
		/// </summary>
		/// <param name="WindowTitle">Title of the main window whose process id we want.</param>
		/// <returns>Returns non-zero on success, zero on failure.</returns>
		public static int GetProcessFromWindowTitle(string WindowTitle)
		{
			IntPtr hWnd = SWindow.FindWindow(null, WindowTitle);
			if (hWnd == IntPtr.Zero)
				return 0;

			return GetProcessFromWindow(hWnd);
		}

		/// <summary>
		/// Returns an array of process ids of processes that match given window title.
		/// </summary>
		/// <param name="WindowTitle">Title of windows to match.</param>
		/// <returns>Returns null on failure, array of integers populated with process ids on success.</returns>
		public static int[] GetProcessesFromWindowTitle(string WindowTitle)
		{
			IntPtr[] hWnds = SWindow.FindWindows(null, WindowTitle);
			if (hWnds == null || hWnds.Length == 0)
				return null;

			int[] ret = new int[hWnds.Length];

			for (int i = 0; i < ret.Length; i++)
				ret[i] = GetProcessFromWindow(hWnds[i]);

			return ret;
		}

		/// <summary>
		/// Gets the process ID of the process that created the first window to match the given window title.
		/// </summary>
		/// <param name="Classname">Classname of the main window whose process id we want.</param>
		/// <returns>Returns non-zero on success, zero on failure.</returns>
		public static int GetProcessFromClassname(string Classname)
		{
			IntPtr hWnd = SWindow.FindWindow(Classname, null);
			if (hWnd == IntPtr.Zero)
				return 0;

			return GetProcessFromWindow(hWnd);
		}

		/// <summary>
		/// Returns an array of process ids of processes that match given window title.
		/// </summary>
		/// <param name="Classname">Classname of windows to match.</param>
		/// <returns>Returns null on failure, array of integers populated with process ids on success.</returns>
		public static int[] GetProcessesFromClassname(string Classname)
		{
			IntPtr[] hWnds = SWindow.FindWindows(Classname, null);
			if (hWnds == null || hWnds.Length == 0)
				return null;

			int[] ret = new int[hWnds.Length];

			for (int i = 0; i < ret.Length; i++)
				ret[i] = GetProcessFromWindow(hWnds[i]);

			return ret;
		}

		/// <summary>
		/// Gets the process id of the process whose executable name matches that which is supplied.
		/// </summary>
		/// <param name="ProcessName">Name of the executable to match.</param>
		/// <returns>Returns non-zero on success, zero on failure.</returns>
		public static int GetProcessFromProcessName(string ProcessName)
		{
			if (ProcessName.EndsWith(".exe"))
				ProcessName = ProcessName.Remove(ProcessName.Length - 4, 4);

			Process[] procs = Process.GetProcessesByName(ProcessName);
			if (procs == null || procs.Length == 0)
				return 0;

			return procs[0].Id;
		}
	}
}