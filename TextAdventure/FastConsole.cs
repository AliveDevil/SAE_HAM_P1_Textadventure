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

		public static void Initialize()
		{
			bufferWidth = Console.BufferWidth;
			bufferHeight = Console.BufferHeight;
			buffer = new char[bufferWidth * (bufferHeight - 1)];
		}

		public static void Write(int x, int y, char c)
		{
			int index = GetIndex(x, y);
			if (!char.IsWhiteSpace(c))
			{
				buffer[index] = c;
			}
			else
			{
				buffer[index] = Empty;
			}
		}

		public static void Print()
		{
			Console.SetCursorPosition(0, 0);
			Console.Write(buffer, 0, buffer.Length);
		}

		private static int GetIndex(int x, int y)
		{
			return y * bufferWidth + x;
		}
	}
}
