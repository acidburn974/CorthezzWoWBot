using System;
using System.Diagnostics;
using System.Collections.Generic;
using Fasm;

namespace Magic
{
	/// <summary>
	/// Provides class instance methods for the manipulation of memory and general 'hacking' of another process.
	/// </summary>
	sealed public partial class BlackMagic
	{
		#region Variables
		//##CONSTANTS#########################################################
		private const uint RETURN_ERROR = 0;

		//##VARIABLES#########################################################
		/// <summary>
		/// Gets or sets whether Debug privileges will be set when opening a process.
		/// </summary>
		public bool SetDebugPrivileges = true;

		private bool m_bProcessOpen = false;
		/// <summary>
		/// Get whether a process is open for manipulation.
		/// </summary>
		public bool IsProcessOpen { get { return m_bProcessOpen; } }

		private bool m_bThreadOpen = false;
		/// <summary>
		/// Gets whether a process is open for manipulation.
		/// </summary>
		public bool IsThreadOpen { get { return m_bThreadOpen; } }

		private IntPtr m_hProcess = IntPtr.Zero;
		/// <summary>
		/// Gets the handle of the currently opened process.
		/// </summary>
		public IntPtr ProcessHandle { get { return m_hProcess; } }

		private int m_ProcessId = 0;
		/// <summary>
		/// Gets the Id of the currently opened process.
		/// </summary>
		public int ProcessId { get { return m_ProcessId; } }

		private IntPtr m_hWnd = IntPtr.Zero;
		/// <summary>
		/// Gets the handle of the main window of the currently opened process.
		/// </summary>
		public IntPtr WindowHandle { get { return m_hWnd; } }

		private int m_ThreadId = 0;
		/// <summary>
		/// Gets the Id of the currently opened thread.
		/// </summary>
		public int ThreadId { get { return m_ThreadId; } }

		private IntPtr m_hThread = IntPtr.Zero;
		/// <summary>
		/// Gets the handle to the currently opened thread.
		/// </summary>
		public IntPtr ThreadHandle { get { return m_hThread; } }

		private ProcessModule m_MainModule;
		/// <summary>
		/// Gets the main module of the opened process.
		/// </summary>
		public ProcessModule MainModule { get { return m_MainModule; } }

		private ProcessModuleCollection m_Modules;
		/// <summary>
		/// Gets the collection of modules currently loaded in the target process.
		/// </summary>
		public ProcessModuleCollection Modules { get { return m_Modules; } }

		/// <summary>
		/// Assembles mnemonics into bytecode and allows for injection and execution.
		/// </summary>
		public ManagedFasm Asm { get; set; }
		#endregion

		#region Constructors
		//##CONSTRUCTORS######################################################
		/// <summary>
		/// Allows interfacing with an external process (memory manipulation, thread manipulation, etc.)
		/// </summary>
		public BlackMagic()
		{
			Asm = new ManagedFasm();
			m_Data = new List<PatternDataEntry>();

			if (m_bProcessOpen && m_hProcess != IntPtr.Zero)
				Asm.SetProcessHandle(m_hProcess);
		}

		/// <summary>
		/// Allows interfacing with an external process (memory manipulation, thread manipulation, etc.)
		/// </summary>
		/// <param name="ProcessId">Process Id of process with which we wish to interact.</param>
		public BlackMagic(int ProcessId) : this()
		{
			m_bProcessOpen = Open(ProcessId);
		}

		/// <summary>
		/// Allows interfacing with an external process (memory manipulation, thread manipulation, etc.)
		/// </summary>
		/// <param name="WindowHandle">Window handle of main window of process with which we wish to interact.</param>
		public BlackMagic(IntPtr WindowHandle) : this(SProcess.GetProcessFromWindow(WindowHandle))
		{ }
		#endregion

		#region Destructor
		//##DESTRUCTOR########################################################
		/// <summary>
		/// Closes all handles.
		/// </summary>
		~BlackMagic()
		{
			this.Close();
		}
		#endregion

		#region Open
		//##OPEN###############################################################
		/// <summary>
		/// Opens a process and its main thread for interaction.
		/// </summary>
		/// <param name="ProcessId">Process Id of process with which we wish to interact.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool Open(int ProcessId)
		{
			if (ProcessId == 0)
				return false;

			if (ProcessId == m_ProcessId)
				return true;

			if (m_bProcessOpen)
				this.CloseProcess();

			if (SetDebugPrivileges)
				System.Diagnostics.Process.EnterDebugMode();

			m_bProcessOpen = (m_hProcess = SProcess.OpenProcess(ProcessId)) != IntPtr.Zero;

			if (m_bProcessOpen)
			{
				m_ProcessId = ProcessId;
				m_hWnd = SWindow.FindWindowByProcessId(ProcessId);

				m_Modules = Process.GetProcessById(m_ProcessId).Modules;
				m_MainModule = m_Modules[0];

				if (Asm == null)
					Asm = new ManagedFasm(m_hProcess);
				else
					Asm.SetProcessHandle(m_hProcess);
			}

			return m_bProcessOpen;
		}

		/// <summary>
		/// Opens a process for interaction.
		/// </summary>
		/// <param name="WindowHandle">Window handle of main window of process with which we wish to interact.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool Open(IntPtr WindowHandle)
		{
			if (WindowHandle == IntPtr.Zero)
				return false;

			return this.Open(SProcess.GetProcessFromWindow(WindowHandle));
		}

		/// <summary>
		/// Opens the specified thread for manipulation.
		/// </summary>
		/// <param name="dwThreadId">ID of the thread to be opened.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool OpenThread(int dwThreadId)
		{
			if (dwThreadId == 0)
				return false;

			if (dwThreadId == m_ThreadId)
				return true;

			if (m_bThreadOpen)
				this.CloseThread();

			m_bThreadOpen = (m_hThread = SThread.OpenThread(dwThreadId)) != IntPtr.Zero;

			if (m_bThreadOpen)
				m_ThreadId = dwThreadId;

			return m_bThreadOpen;
		}

		/// <summary>
		/// Opens the main thread of the process already opened by this class object.
		/// </summary>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool OpenThread()
		{
			if (m_bProcessOpen)
				return this.OpenThread(SThread.GetMainThreadId(m_ProcessId));
			return false;
		}

		/// <summary>
		/// Opens a process and its main thread for manipulation.
		/// </summary>
		/// <param name="dwProcessId">Id of the target process.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool OpenProcessAndThread(int dwProcessId)
		{
			if (this.Open(dwProcessId) && this.OpenThread())
				return true;

			this.Close();
			return false;
		}

		/// <summary>
		/// Opens a process and its main thread for manipulation.
		/// </summary>
		/// <param name="WindowHandle">Handle to the main window of the target process.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool OpenProcessAndThread(IntPtr WindowHandle)
		{
			if (this.Open(WindowHandle) && this.OpenThread())
				return true;

			this.Close();
			return false;
		}
		#endregion

		#region Close
		//##CLOSE#########################################################
		/// <summary>
		/// Closes process and thread handles of open process (or does nothing if neither is open).
		/// </summary>
		public void Close()
		{
			Asm.Dispose();

			this.CloseProcess();
			this.CloseThread();
		}

		/// <summary>
		/// Closes the handle to the open process (or does nothing if a process has not been opened).
		/// </summary>
		public void CloseProcess()
		{
			if (m_hProcess != IntPtr.Zero)
				Imports.CloseHandle(m_hProcess);

			m_hProcess = IntPtr.Zero;
			m_hWnd = IntPtr.Zero;
			m_ProcessId = 0;

			m_MainModule = null;
			m_Modules = null;

			m_bProcessOpen = false;

			Asm.SetProcessHandle(IntPtr.Zero);
		}

		/// <summary>
		/// Closes the handle to the open thread (or does nothing if a thread has not been opened).
		/// </summary>
		public void CloseThread()
		{
			if (m_hThread != IntPtr.Zero)
				Imports.CloseHandle(m_hThread);

			m_hThread = IntPtr.Zero;
			m_ThreadId = 0;

			m_bThreadOpen = false;
		}
		#endregion

		#region Modules
		/// <summary>
		/// Gets the full file path of the main module of the opened process.
		/// </summary>
		/// <returns>Returns a string representing the full file path of the main module of the opened process.</returns>
		public string GetModuleFilePath()
		{
			return m_MainModule.FileName;
		}

		/// <summary>
		/// Gets the full file path of the specified module loaded by the opened process.
		/// </summary>
		/// <param name="index">Index of the module whose full file path is wanted.</param>
		/// <returns>Returns a string representing the full file path of the specified module loaded by the opened process.</returns>
		public string GetModuleFilePath(int index)
		{
			return m_Modules[index].FileName;
		}

		/// <summary>
		/// Gets the full file path of the specified module loaded by the opened process.
		/// </summary>
		/// <param name="sModuleName"></param>
		/// <returns></returns>
		public string GetModuleFilePath(string sModuleName)
		{
			foreach (ProcessModule pMod in m_Modules)
				if (pMod.ModuleName.ToLower().Equals(sModuleName.ToLower()))
					return pMod.FileName;

			return String.Empty;
		}

		/// <summary>
		/// Gets the module loaded by the opened process that matches the given string.
		/// </summary>
		/// <param name="sModuleName">String specifying which module to return.</param>
		/// <returns>Returns the module loaded by the opened process that matches the given string.</returns>
		public ProcessModule GetModule(string sModuleName)
		{
			foreach (ProcessModule pMod in m_Modules)
				if (pMod.ModuleName.ToLower().Equals(sModuleName.ToLower()))
					return pMod;

			return null;
		}

		/// <summary>
		/// Gets the module loaded by the opened process in which the given address resides.
		/// </summary>
		/// <param name="dwAddress">An address inside the process owned by the module that will be returned.</param>
		/// <returns>Returns null on failure, or the module that owns the given address on success.</returns>
		public ProcessModule GetModule(uint dwAddress)
		{
			foreach (ProcessModule pMod in m_Modules)
				if ((uint)pMod.BaseAddress <= dwAddress && ((uint)pMod.BaseAddress + pMod.ModuleMemorySize) >= dwAddress)
					return pMod;

			return null;
		}
		#endregion
	}
}
