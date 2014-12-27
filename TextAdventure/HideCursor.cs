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
		/// <summary>
		/// Hides cursor on construct.
		/// </summary>
		public HideCursor()
		{
			Console.CursorVisible = false;
		}

		/// <summary>
		/// Shows cursor on dispose.
		/// </summary>
		public void Dispose()
		{
			Console.CursorVisible = true;
		}
	}
}
