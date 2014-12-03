/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure
{
	public sealed class HideCursor : IDisposable
	{
		public HideCursor()
		{
			Console.CursorVisible = false;
		}
		public void Dispose()
		{
			Console.CursorVisible = true;
		}
	}
}
