using System;

namespace Magic
{
	sealed public partial class BlackMagic
	{
		/// <summary>
		/// Injects a dll into a process by creating a remote thread on LoadLibrary.
		/// </summary>
		/// <param name="szDllPath">Full path of the dll that will be injected.</param>
		/// <returns>Returns the base address of the injected dll on success, zero on failure.</returns>
		public uint InjectDllCreateThread(string szDllPath)
		{
			if (!m_bProcessOpen)
				return RETURN_ERROR;

			return SInject.InjectDllCreateThread(m_hProcess, szDllPath);
		}

		/// <summary>
		/// Injects a dll into a process by hijacking the given thread and redirecting it to LoadLibrary.
		/// </summary>
		/// <param name="szDllPath">Full path of the dll that will be injected.</param>
		/// <returns>Returns the base address of the injected dll on success, zero on failure.</returns>
		public uint InjectDllRedirectThread(string szDllPath)
		{
			if (!m_bProcessOpen)
				return RETURN_ERROR;

			if (m_bThreadOpen)
				return SInject.InjectDllRedirectThread(m_hProcess, m_hThread, szDllPath);

			return SInject.InjectDllRedirectThread(m_hProcess, m_ProcessId, szDllPath);
		}

		/// <summary>
		/// Injects a dll into a process by hijacking the given thread and redirecting it to LoadLibrary.
		/// </summary>
		/// <param name="hThread">Handle to the thread which will be hijacked.</param>
		/// <param name="szDllPath">Full path of the dll that will be injected.</param>
		/// <returns>Returns the base address of the injected dll on success, zero on failure.</returns>
		public uint InjectDllRedirectThread(IntPtr hThread, string szDllPath)
		{
			if (!m_bProcessOpen)
				return RETURN_ERROR;

			return SInject.InjectDllRedirectThread(m_hProcess, hThread, szDllPath);
		}
	}
}