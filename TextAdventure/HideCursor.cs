/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure
{
	/// <summary>
	/// <para>Hides the cursor on construct and shows it on using-block-end.</para>
	/// </summary>
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
