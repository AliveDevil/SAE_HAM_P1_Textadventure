/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure
{
	/// <summary>
	/// <para>This fixes some flickering and rewriting of console.</para>
	/// <para>Uses Console.Write(char[]) to draw internal buffer.</para>
	/// </summary>
	public static class FastConsole
	{
		private const char Empty = '\0';

		private static int bufferWidth;
		private static int bufferHeight;
		private static char[] buffer;

		/// <summary>
		/// <para>Initializes internal buffer with consoles buffer width and height.</para>
		/// </summary>
		public static void Initialize()
		{
			bufferWidth = Console.BufferWidth;
			bufferHeight = Console.BufferHeight;
			buffer = new char[bufferWidth * (bufferHeight - 1)]; // exclude last row in buffer to avoid funny effects.
		}

		/// <summary>
		/// <para>Buffers given char at given position.</para>
		/// </summary>
		/// <param name="positionX">X coordinate (from left, starting from 0)</param>
		/// <param name="positionY">Y coordinate (from top, starting from 0)</param>
		/// <param name="value">Saved char</param>
		public static void Write(int positionX, int positionY, char value)
		{
			int index = GetIndex(positionX, positionY);
			if (!char.IsWhiteSpace(value))
			{
				buffer[index] = value;
			}
			else
			{
				buffer[index] = Empty;
			}
		}

		/// <summary>
		/// <para>Prints current buffer beginning at (0|0).</para>
		/// </summary>
		public static void Print()
		{
			Console.SetCursorPosition(0, 0);
			Console.Write(buffer, 0, buffer.Length);
		}

		/// <summary>
		/// Returns one dimensional index for two dimensional position.
		/// </summary>
		/// <param name="positionX">X coordinate (from left, starting from 0)</param>
		/// <param name="positionY">Y coordinate (from top, starting from 0)</param>
		/// <returns>One dimensional index for given position.</returns>
		private static int GetIndex(int positionX, int positionY)
		{
			return positionY * bufferWidth + positionX;
		}
	}
}
