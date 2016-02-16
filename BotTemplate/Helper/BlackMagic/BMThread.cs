using System;

namespace Magic
{
	sealed public partial class BlackMagic
	{
		/// <summary>
		/// Executes code at a given address and returns the thread's exit code.
		/// </summary>
		/// <param name="dwStartAddress">Address to be executed.</param>
		/// <param name="dwParameter">Parameter to be passed to the code being executed.</param>
		/// <returns>Returns the exit code of the thread.</returns>
		public uint Execute(uint dwStartAddress, uint dwParameter)
		{
			IntPtr hThread;
			UIntPtr lpExitCode = UIntPtr.Zero;
			bool bSuccess = false;

			hThread = CreateRemoteThread(dwStartAddress, dwParameter);
			if (hThread == IntPtr.Zero)
				throw new Exception("Thread could not be remotely created.");

			bSuccess = (SThread.WaitForSingleObject(hThread, 10000) == WaitValues.WAIT_OBJECT_0);
			if (bSuccess)
				bSuccess = Imports.GetExitCodeThread(hThread, out lpExitCode);

			Imports.CloseHandle(hThread);

			if (!bSuccess)
				throw new Exception("Error waiting for thread to exit or getting exit code.");

			return (uint)lpExitCode;
		}

		/// <summary>
		/// Executes code at a given address and returns the thread's exit code.
		/// </summary>
		/// <param name="dwStartAddress">Address to be executed.</param>
		/// <returns>Returns the exit code of the thread.</returns>
		public uint Execute(uint dwStartAddress)
		{
			return Execute(dwStartAddress, 0);
		}

		/// <summary>
		/// Creates a thread inside another process' context.
		/// </summary>
		/// <param name="dwStartAddress">Address at which thread will start.</param>
		/// <param name="dwParameter">Parameter that will be passed to the thread.</param>
		/// <param name="dwCreationFlags">Flags that control creation of the thread.</param>
		/// <param name="dwThreadId">[Out] The id of the created thread.</param>
		/// <returns>Returns the handle of the created thread.</returns>
		public IntPtr CreateRemoteThread(uint dwStartAddress, uint dwParameter, uint dwCreationFlags, out uint dwThreadId)
		{
			if (m_bProcessOpen)
				return SThread.CreateRemoteThread(m_hProcess, dwStartAddress, dwParameter, dwCreationFlags, out dwThreadId);

			dwThreadId = 0;
			return IntPtr.Zero;
		}

		/// <summary>
		/// Creates a thread inside another process' context.
		/// </summary>
		/// <param name="dwStartAddress">Address at which thread will start.</param>
		/// <param name="dwParameter">Parameter that will be passed to the thread.</param>
		/// <param name="dwThreadId">[Out] The id of the created thread.</param>
		/// <returns>Returns the handle of the created thread.</returns>
		public IntPtr CreateRemoteThread(uint dwStartAddress, uint dwParameter, out uint dwThreadId)
		{
			return CreateRemoteThread(dwStartAddress, dwParameter, ThreadFlags.THREAD_EXECUTE_IMMEDIATELY, out dwThreadId);
		}

		/// <summary>
		/// Creates a thread inside another process' context.
		/// </summary>
		/// <param name="dwStartAddress">Address at which thread will start.</param>
		/// <param name="dwParameter">Parameter that will be passed to the thread.</param>
		/// <returns>Returns the handle of the created thread.</returns>
		public IntPtr CreateRemoteThread(uint dwStartAddress, uint dwParameter)
		{
			uint dwThreadId;
			return CreateRemoteThread(dwStartAddress, dwParameter, ThreadFlags.THREAD_EXECUTE_IMMEDIATELY, out dwThreadId);
		}

		/// <summary>
		/// Suspends execution of a thread.
		/// </summary>
		/// <param name="hThread">Handle to the thread to be suspended.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool SuspendThread(IntPtr hThread)
		{
			return (SThread.SuspendThread(hThread) == uint.MaxValue) ? false : true;
		}

		/// <summary>
		/// Suspends execution of the currently opened thread.
		/// </summary>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool SuspendThread()
		{
			return (m_bThreadOpen) ? this.SuspendThread(m_hThread) : false;
		}

		/// <summary>
		/// Resumes execution of a thread.
		/// </summary>
		/// <param name="hThread">Handle to the thread to be suspended.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool ResumeThread(IntPtr hThread)
		{
			return (SThread.ResumeThread(hThread) == uint.MaxValue) ? false : true;
		}

		/// <summary>
		/// Resumes execution of the currently opened thread.
		/// </summary>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool ResumeThread()
		{
			return (m_bThreadOpen) ? this.ResumeThread(m_hThread) : false;
		}
	}
}
