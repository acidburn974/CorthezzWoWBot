using System;
using System.Diagnostics;

namespace Magic
{
	/// <summary>
	/// Static methods for manipulating threads.
	/// </summary>
	public static class SThread
	{
		/// <summary>
		/// Opens a thread for manipulation.
		/// </summary>
		/// <param name="dwDesiredAccess">The desired access rights to the thread in question.</param>
		/// <param name="dwThreadId">The ID of the thread in question.</param>
		/// <returns>Returns a handle to the thread allowing manipulation.</returns>
		public static IntPtr OpenThread(uint dwDesiredAccess, int dwThreadId)
		{
			return Imports.OpenThread(dwDesiredAccess, false, (uint)dwThreadId);
		}

		/// <summary>
		/// Opens a thread for manipulation.  AccessRights.THREAD_ALL_ACCESS is automatically granted.
		/// </summary>
		/// <param name="dwThreadId">The ID of the thread in question.</param>
		/// <returns>Returns a handle to the thread allowing manipulation.</returns>
		public static IntPtr OpenThread(int dwThreadId)
		{
			return Imports.OpenThread(AccessRights.THREAD_ALL_ACCESS, false, (uint)dwThreadId);
		}

		/// <summary>
		/// Gets the main thread ID of a given process.
		/// </summary>
		/// <param name="dwProcessId">The ID of the process whose main thread ID will be returned.</param>
		/// <returns>Returns non-zero on success, zero on failure.</returns>
		public static int GetMainThreadId(int dwProcessId)
		{
			if (dwProcessId == 0)
				return 0;

			Process proc = Process.GetProcessById(dwProcessId);
			return proc.Threads[0].Id;
		}

		/// <summary>
		/// Gets the main thread ID of a given process.
		/// </summary>
		/// <param name="hWindowHandle">The window handle of the process whose main thread ID will be returned.</param>
		/// <returns>Returns non-zero on success, zero on failure.</returns>
		public static int GetMainThreadId(IntPtr hWindowHandle)
		{
			if (hWindowHandle == IntPtr.Zero)
				return 0;

			return GetMainThreadId(SProcess.GetProcessFromWindow(hWindowHandle));
		}

		/// <summary>
		/// Gets the main thread of a given process.
		/// </summary>
		/// <param name="dwProcessId">The ID of the process whose main thread will be returned.</param>
		/// <returns>Returns the main thread on success, null on failure.</returns>
		public static ProcessThread GetMainThread(int dwProcessId)
		{
			if (dwProcessId == 0)
				return null;


			Process proc = Process.GetProcessById(dwProcessId);
			return proc.Threads[0];
		}

		/// <summary>
		/// Gets the main thread of a given process.
		/// </summary>
		/// <param name="hWindowHandle">The window handle of the process whose main thread will be returned.</param>
		/// <returns>Returns the main thread on success, null on failure.</returns>
		public static ProcessThread GetMainThread(IntPtr hWindowHandle)
		{
			if (hWindowHandle == IntPtr.Zero)
				return null;

			return GetMainThread(SProcess.GetProcessFromWindow(hWindowHandle));
		}

		/// <summary>
		/// Gets the context of a given thread.
		/// </summary>
		/// <param name="hThread">Handle to the thread for which the context will be returned.</param>
		/// <param name="ContextFlags">Determines which set(s) of registers will be returned.</param>
		/// <returns>Returns the context of the thread.  If failure, sets CONTEXT.ContextFlags to zero.</returns>
		public static CONTEXT GetThreadContext(IntPtr hThread, uint ContextFlags)
		{
			CONTEXT ctx = new CONTEXT();
			ctx.ContextFlags = ContextFlags;

			if (!Imports.GetThreadContext(hThread, ref ctx))
				ctx.ContextFlags = 0;

			return ctx;
		}

		/// <summary>
		/// Sets the context of a given thread.
		/// </summary>
		/// <param name="hThread">Handle to the thread for which the context will be set.</param>
		/// <param name="ctx">CONTEXT structure to which the thread's context will be set.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public static bool SetThreadContext(IntPtr hThread, CONTEXT ctx)
		{
			return Imports.SetThreadContext(hThread, ref ctx);
		}

		/// <summary>
		/// Suspends execution of a given thread.
		/// </summary>
		/// <param name="hThread">Handle to the thread that will be suspended.</param>
		/// <returns>Returns (DWORD)-1 on failure, otherwise the suspend count of the thread.</returns>
		public static uint SuspendThread(IntPtr hThread)
		{
			return Imports.SuspendThread(hThread);
		}

		/// <summary>
		/// Resumes execution of a given thread.
		/// </summary>
		/// <param name="hThread">Handle to the thread that will be suspended.</param>
		/// <returns>Returns (DWORD)-1 on failure, otherwise the previous suspend count of the thread.</returns>
		public static uint ResumeThread(IntPtr hThread)
		{
			return Imports.ResumeThread(hThread);
		}

		/// <summary>
		/// Terminates the specified thread.
		/// </summary>
		/// <param name="hThread">Handle to the thread to exit.</param>
		/// <param name="dwExitCode">Exit code that will be stored in the thread object.</param>
		/// <returns>Returns zero on failure, non-zero on success.</returns>
		public static uint TerminateThread(IntPtr hThread, uint dwExitCode)
		{
			return Imports.TerminateThread(hThread, dwExitCode);
		}

		/// <summary>
		/// Creates a thread inside another process' context.
		/// </summary>
		/// <param name="hProcess">Handle to the process inside which thread will be created.</param>
		/// <param name="dwStartAddress">Address at which thread will start.</param>
		/// <param name="dwParameter">Parameter that will be passed to the thread.</param>
		/// <returns>Returns the handle of the created thread.</returns>
		public static IntPtr CreateRemoteThread(IntPtr hProcess, uint dwStartAddress, uint dwParameter)
		{
			uint dwThreadId;
			return CreateRemoteThread(hProcess, dwStartAddress, dwParameter, ThreadFlags.THREAD_EXECUTE_IMMEDIATELY, out dwThreadId);
		}

		/// <summary>
		/// Creates a thread inside another process' context.
		/// </summary>
		/// <param name="hProcess">Handle to the process inside which thread will be created.</param>
		/// <param name="dwStartAddress">Address at which thread will start.</param>
		/// <param name="dwParameter">Parameter that will be passed to the thread.</param>
		/// /// <param name="dwThreadId">[Out] The id of the created thread.</param>
		/// <returns>Returns the handle of the created thread.</returns>
		public static IntPtr CreateRemoteThread(IntPtr hProcess, uint dwStartAddress, uint dwParameter, out uint dwThreadId)
		{
			return CreateRemoteThread(hProcess, dwStartAddress, dwParameter, ThreadFlags.THREAD_EXECUTE_IMMEDIATELY, out dwThreadId);
		}

		/// <summary>
		/// Creates a thread inside another process' context.
		/// </summary>
		/// <param name="hProcess">Handle to the process inside which thread will be created.</param>
		/// <param name="dwStartAddress">Address at which thread will start.</param>
		/// <param name="dwParameter">Parameter that will be passed to the thread.</param>
		/// <param name="dwCreationFlags">Flags that control creation of the thread.</param>
		/// <param name="dwThreadId">[Out] The id of the created thread.</param>
		/// <returns>Returns the handle of the created thread.</returns>
		public static IntPtr CreateRemoteThread(IntPtr hProcess, uint dwStartAddress, uint dwParameter, uint dwCreationFlags, out uint dwThreadId)
		{
			IntPtr hThread, lpThreadId;

			hThread = Imports.CreateRemoteThread(hProcess, IntPtr.Zero, 0, (IntPtr)dwStartAddress, (IntPtr)dwParameter, dwCreationFlags, out lpThreadId);
			dwThreadId = (uint)lpThreadId;

			return hThread;
		}

		/// <summary>
		/// Gets the exit code of the specified thread.
		/// </summary>
		/// <param name="hThread">Handle to the thread whose exit code is wanted.</param>
		/// <returns>Returns 0 on failure, non-zero on success.</returns>
		public static uint GetExitCodeThread(IntPtr hThread)
		{
			UIntPtr dwExitCode;
			if (!Imports.GetExitCodeThread(hThread, out dwExitCode))
				throw new Exception("GetExitCodeThread failed.");
			return (uint)dwExitCode;
		}

		/// <summary>
		/// Waits for an object to enter a signaled state.
		/// </summary>
		/// <param name="hObject">The object for which to wait.</param>
		/// <returns>Returns one of the values in the static WaitValues class.</returns>
		public static uint WaitForSingleObject(IntPtr hObject)
		{
			return Imports.WaitForSingleObject(hObject, WaitValues.INFINITE);
		}
		
		/// <summary>
		/// Waits for an object to enter a signaled state.
		/// </summary>
		/// <param name="hObject">The object for which to wait.</param>
		/// <param name="dwMilliseconds">Number of milliseconds to wait.</param>
		/// <returns>Returns one of the values in the static WaitValues class.</returns>
		public static uint WaitForSingleObject(IntPtr hObject, uint dwMilliseconds)
		{
			return Imports.WaitForSingleObject(hObject, dwMilliseconds);
		}
	}
}
