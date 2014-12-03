/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure.Scenes
{
	static class SceneManager
	{
		public const char EmptyChar = ' ';
		public const char CornerChar = '+';
		public const char BorderHorizontalChar = '-';
		public const char BorderVerticalChar = '|';
		public const int GameWidth = 48;
		public const int BufferWidth = GameWidth + 2;
		public const int GameHeight = 27;
		public const int BufferHeight = GameHeight + 4;

		private static bool exit = false;
		private static Scene currentScene;

		public static void LoadScene<T>() where T : Scene, new()
		{
			currentScene = new T();
			currentScene.Initialize();
		}

		public static void Run()
		{
			SetResolution();
			while (!exit)
			{
				PerformWrite();
				PerformInput();
			}
		}

		public static void Exit()
		{
			exit = true;
		}

		private static void SetResolution()
		{
			PrefixResolution();
			Console.SetBufferSize(BufferWidth, BufferHeight);
			Console.SetWindowSize(BufferWidth, BufferHeight);
		}
		private static void PrefixResolution()
		{
			if (Console.WindowHeight > BufferHeight)
			{
				Console.WindowHeight = BufferHeight;
			}
			else
			{
				Console.BufferHeight = BufferHeight;
			}
			if (Console.WindowWidth > BufferWidth)
			{
				Console.WindowWidth = BufferWidth;
			}
			else
			{
				Console.BufferHeight = BufferWidth;
			}
		}

		private static void PerformWrite()
		{
			using (HideCursor hideCursor = new HideCursor())
			{
				ClearConsole();
				DrawScene();
			}
		}
		private static void PerformInput()
		{
			SetCursorPosition(-1, GameHeight + 1);
			Console.Write("Action> ");
			currentScene.PerformAction(Console.ReadLine());
		}

		#region Draw Stuff
		private static void ClearConsole()
		{
			for (int x = -1; x <= GameWidth; x++)
			{
				for (int y = -2; y <= GameHeight; y++)
				{
					switch (ResolveCellType(x, y))
					{
						case CellType.Corner:
							DrawChar(x, y, CornerChar);
							break;
						case CellType.BorderHorizontal:
							DrawChar(x, y, BorderHorizontalChar);
							break;
						case CellType.BorderVertical:
							DrawChar(x, y, BorderVerticalChar);
							break;
						case CellType.Content:
						default:
							DrawChar(x, y, EmptyChar);
							break;
					}
				}
			}
		}
		private static void DrawScene()
		{
			DrawTitle();
			DrawDescription();
			DrawActions();
		}
		private static void DrawTitle()
		{
			int centeredLength = currentScene.Title.Length / 2;
			int centeredX = GameWidth / 2 - centeredLength;
			for (int i = 0; i < currentScene.Title.Length; i++)
			{
				DrawChar(centeredX + i, 0, currentScene.Title[i]);
			}
		}
		private static void DrawDescription()
		{
			int y = GameHeight - 3;
		}
		private static void DrawActions()
		{
			if (currentScene.DrawActions)
			{

			}
		}
		private static void DrawChar(int x, int y, char @char)
		{
			SetCursorPosition(x, y);
			Console.Write(@char);
		}
		#endregion

		#region Postioning Stuff
		private static void SetCursorPosition(int x, int y)
		{
			Console.SetCursorPosition(ResolveX(x), ResolveY(y));
		}
		private static CellType ResolveCellType(int x, int y)
		{
			if ((x == -1 && y == -1) || (x == -1 && y == GameHeight) || (x == GameWidth && y == -1) || (x == GameWidth && y == GameHeight))
			{
				return CellType.Corner;
			}
			if (x == -1 || x == GameWidth)
			{
				return CellType.BorderVertical;
			}
			if (y == -1 || y == GameHeight)
			{
				return CellType.BorderHorizontal;
			}
			return CellType.Content;
		}
		/// <summary>
		/// </summary>
		private static int ResolveX(int x)
		{
			return Clamp(x + 1, 0, BufferWidth - 1);
		}
		/// <summary>
		/// </summary>
		private static int ResolveY(int y)
		{
			return Clamp(y + 1, 0, BufferHeight - 1);
		}
		private static int Clamp(int val, int min, int max)
		{
			return val > min ? val < max ? val : max : min;
		}
		#endregion
	}
}
