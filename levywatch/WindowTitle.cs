﻿using System;
using System.Text;
using System.Runtime.InteropServices;

static class WindowTitle
{
	[DllImport("user32.dll")]
	static extern IntPtr GetForegroundWindow();

	[DllImport("user32.dll")]
	static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

	public static string GetActiveWindowTitle()
	{
		const int nChars = 256;
		StringBuilder Buff = new StringBuilder(nChars);
		IntPtr handle = GetForegroundWindow();

		if (GetWindowText(handle, Buff, nChars) > 0)
			return Buff.ToString();

		return null;
	}
}