using System;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Magic
{
	/// <summary>
	/// Contains static methods for finding windows.
	/// </summary>
	public static class SWindow
	{
		private static object lWindowsLock = new object();
		private static List<IntPtr> lWindows;

		private static bool EnumWindowsCallback(IntPtr hWnd, IntPtr lParam)
		{
			lWindows.Add(hWnd);
			return true;
		}

		private static bool _EnumWindows()
		{
			lWindows = new List<IntPtr>();

			Imports.EnumWindowsProc callback = new Imports.EnumWindowsProc(EnumWindowsCallback);
			return Imports.EnumWindows(callback, IntPtr.Zero);
		}

		/// <summary>
		/// Gets all open windows, be they child or parent.
		/// </summary>
		/// <returns>Returns an array of window handles.</returns>
		public static IntPtr[] EnumWindows()
		{
			lock (lWindowsLock)
			{
				if (!_EnumWindows()) return null;

				return lWindows.ToArray();
			}
		}

		/// <summary>
		/// Gets all open main windows.
		/// </summary>
		/// <returns>Returns an array of window handles.</returns>
		public static IntPtr[] EnumMainWindows()
		{
			List<IntPtr> hWnds = new List<IntPtr>();
			Process[] procs = Process.GetProcesses();

			foreach (Process proc in procs)
				hWnds.Add(proc.MainWindowHandle);

			return hWnds.ToArray();
		}

		/// <summary>
		/// Finds all windows, parent and child, that match the supplied classname or window title.
		/// </summary>
		/// <param name="Classname">Classname of the window(s) to match.</param>
		/// <param name="WindowTitle">Window title of the window(s) to match.</param>
		/// <returns>Returns an array of window handles.</returns>
		public static IntPtr[] FindWindows(string Classname, string WindowTitle)
		{
			if (Classname == null) Classname = String.Empty;
			if (WindowTitle == null) WindowTitle = String.Empty;

			List<IntPtr> hWnds = new List<IntPtr>();

			lock (lWindowsLock)
			{
				if (!_EnumWindows()) return null;

				foreach (IntPtr hWnd in lWindows)
					if ((WindowTitle.Length > 0 && Imports.GetWindowTitle(hWnd) == WindowTitle) ||
						(Classname.Length > 0 && Imports.GetClassName(hWnd) == Classname))
						hWnds.Add(hWnd);
			}

			return hWnds.ToArray();
		}

		/// <summary>
		/// Finds the first window, parent and child, that match the supplied classname or window title.
		/// </summary>
		/// <param name="Classname">Classname of the window(s) to match.</param>
		/// <param name="WindowTitle">Window title of the window(s) to match.</param>
		/// <returns>Returns a window handle.</returns>
		public static IntPtr FindWindow(string Classname, string WindowTitle)
		{
			if (Classname == null) Classname = String.Empty;
			if (WindowTitle == null) WindowTitle = String.Empty;

			lock (lWindowsLock)
			{
				if (!_EnumWindows()) return IntPtr.Zero;


				foreach (IntPtr hWnd in lWindows)
					if ((WindowTitle.Length > 0 && Imports.GetWindowTitle(hWnd) == WindowTitle) ||
						(Classname.Length > 0 && Imports.GetClassName(hWnd) == Classname))
						return hWnd;
			}

			return IntPtr.Zero;
		}

		/// <summary>
		/// Finds all top-level windows that match the supplied classname or window title.
		/// </summary>
		/// <param name="Classname">Classname of the window(s) to match.</param>
		/// <param name="WindowTitle">Window title of the window(s) to match.</param>
		/// <returns>Returns an array of window handles.</returns>
		public static IntPtr[] FindMainWindows(string Classname, string WindowTitle)
		{
			if (Classname == null) Classname = String.Empty;
			if (WindowTitle == null) WindowTitle = String.Empty;

			List<IntPtr> hWnds = new List<IntPtr>();
			Process[] procs = Process.GetProcesses();

			foreach (Process proc in procs)
				if (proc.MainWindowHandle != IntPtr.Zero)
					if ((WindowTitle.Length > 0 && proc.MainWindowTitle == WindowTitle) ||
						(Classname.Length > 0 && Imports.GetClassName(proc.MainWindowHandle) == Classname))
						hWnds.Add(proc.MainWindowHandle);

			return hWnds.ToArray();
		}

		/// <summary>
		/// Finds the first top-level window that matches the supplied classname or window title.
		/// </summary>
		/// <param name="Classname">Classname of the window(s) to match.</param>
		/// <param name="WindowTitle">Window title of the window(s) to match.</param>
		/// <returns>Returns a window handle.</returns>
		public static IntPtr FindMainWindow(string Classname, string WindowTitle)
		{
			if (Classname == null) Classname = String.Empty;
			if (WindowTitle == null) WindowTitle = String.Empty;

			Process[] procs = Process.GetProcesses();

			foreach (Process proc in procs)
				if (proc.MainWindowHandle != IntPtr.Zero)
					if ((WindowTitle.Length > 0 && proc.MainWindowTitle == WindowTitle) ||
						(Classname.Length > 0 && Imports.GetClassName(proc.MainWindowHandle) == Classname))
						return proc.MainWindowHandle;

			return IntPtr.Zero;
		}

		/// <summary>
		/// Finds all windows, parent and child, that contain the supplied classname or window title.
		/// </summary>
		/// <param name="Classname">Classname of the window(s) to match.</param>
		/// <param name="WindowTitle">Window title of the window(s) to match.</param>
		/// <returns>Returns an array of window handles.</returns>
		public static IntPtr[] FindWindowsContains(string Classname, string WindowTitle)
		{
			if (Classname == null) Classname = String.Empty;
			if (WindowTitle == null) WindowTitle = String.Empty;

			List<IntPtr> hWnds = new List<IntPtr>();

			lock (lWindowsLock)
			{
				if (!_EnumWindows()) return null;

				foreach (IntPtr hWnd in lWindows)
					if ((WindowTitle.Length > 0 && Imports.GetWindowTitle(hWnd).Contains(WindowTitle)) ||
						(Classname.Length > 0 && Imports.GetClassName(hWnd).Contains(Classname)))
						hWnds.Add(hWnd);
			}

			return hWnds.ToArray();
		}

		/// <summary>
		/// Finds the first window, parent and child, that contains either of the supplied classname or window title.
		/// </summary>
		/// <param name="Classname">Classname of the window(s) to match.</param>
		/// <param name="WindowTitle">Window title of the window(s) to match.</param>
		/// <returns>Returns a window handle.</returns>
		public static IntPtr FindWindowContains(string Classname, string WindowTitle)
		{
			if (Classname == null) Classname = String.Empty;
			if (WindowTitle == null) WindowTitle = String.Empty;

			lock (lWindowsLock)
			{
				if (!_EnumWindows()) return IntPtr.Zero;


				foreach (IntPtr hWnd in lWindows)
					if ((WindowTitle.Length > 0 && Imports.GetWindowTitle(hWnd).Contains(WindowTitle)) ||
						(Classname.Length > 0 && Imports.GetClassName(hWnd).Contains(Classname)))
						return hWnd;
			}

			return IntPtr.Zero;
		}

		/// <summary>
		/// Finds all top-level windows that contain the supplied classname or window title.
		/// </summary>
		/// <param name="Classname">Classname of the window(s) to match.</param>
		/// <param name="WindowTitle">Window title of the window(s) to match.</param>
		/// <returns>Returns an array of window handles.</returns>
		public static IntPtr[] FindMainWindowsContains(string Classname, string WindowTitle)
		{
			if (Classname == null) Classname = String.Empty;
			if (WindowTitle == null) WindowTitle = String.Empty;

			List<IntPtr> hWnds = new List<IntPtr>();
			Process[] procs = Process.GetProcesses();

			foreach (Process proc in procs)
				if (proc.MainWindowHandle != IntPtr.Zero)
					if ((WindowTitle.Length > 0 && proc.MainWindowTitle.Contains(WindowTitle)) ||
						(Classname.Length > 0 && Imports.GetClassName(proc.MainWindowHandle).Contains(Classname)))
						hWnds.Add(proc.MainWindowHandle);

			return hWnds.ToArray();
		}

		/// <summary>
		/// Finds the first top-level window that contains the supplied classname or window title.
		/// </summary>
		/// <param name="Classname">Classname of the window(s) to match.</param>
		/// <param name="WindowTitle">Window title of the window(s) to match.</param>
		/// <returns>Returns a window handle.</returns>
		public static IntPtr FindMainWindowContains(string Classname, string WindowTitle)
		{
			if (Classname == null) Classname = String.Empty;
			if (WindowTitle == null) WindowTitle = String.Empty;

			Process[] procs = Process.GetProcesses();

			foreach (Process proc in procs)
				if (proc.MainWindowHandle != IntPtr.Zero)
					if ((WindowTitle.Length > 0 && proc.MainWindowTitle.Contains(WindowTitle)) ||
						(Classname.Length > 0 && Imports.GetClassName(proc.MainWindowHandle).Contains(Classname)))
						return proc.MainWindowHandle;

			return IntPtr.Zero;
		}

		/// <summary>
		/// Finds the main window of the provided process.
		/// </summary>
		/// <param name="ProcessName">Name of the process executable.</param>
		/// <returns>Returns a window handle.</returns>
		/// <remarks>ProcessName may contain the trailing extension or not, though it would be less problematic if any file extension were omitted (i.e. '.exe').</remarks>
		public static IntPtr FindWindowByProcessName(string ProcessName)
		{
			if (ProcessName.EndsWith(".exe"))
				ProcessName = ProcessName.Remove(ProcessName.Length - 4, 4);

			Process[] procs = Process.GetProcessesByName(ProcessName);
			if (procs == null || procs.Length == 0)
				return IntPtr.Zero;

			return procs[0].MainWindowHandle;
		}

		/// <summary>
		/// Finds all main windows that match the provided process name.
		/// </summary>
		/// <param name="ProcessName">Name of the process executable.</param>
		/// <returns>Returns an array of window handles.</returns>
		/// <remarks>ProcessName may contain the trailing extension or not, though it would be less problematic if any file extension were omitted (i.e. '.exe').</remarks>
		public static IntPtr[] FindWindowsByProcessName(string ProcessName)
		{
			List<IntPtr> hWnds = new List<IntPtr>();

			if (ProcessName.EndsWith(".exe"))
				ProcessName = ProcessName.Remove(ProcessName.Length - 4, 4);

			Process[] procs = Process.GetProcessesByName(ProcessName);
			if (procs == null || procs.Length == 0)
				return null;

			foreach (Process proc in procs)
				hWnds.Add(proc.MainWindowHandle);

			return hWnds.ToArray();
		}

		/// <summary>
		/// Finds the main window of the provided process.
		/// </summary>
		/// <param name="dwProcessId">The process Id of the process in question.</param>
		/// <returns>Returns a window handle.</returns>
		public static IntPtr FindWindowByProcessId(int dwProcessId)
		{
			Process proc = Process.GetProcessById(dwProcessId);
			return proc.MainWindowHandle;
		}
	}
}