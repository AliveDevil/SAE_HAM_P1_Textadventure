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
		public const int GameHeight = 27;

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
			Console.SetBufferSize(GameWidth + 2, GameHeight + 3);
			Console.SetWindowSize(GameWidth + 2, GameHeight + 3);
		}
		private static void PrefixResolution()
		{
			if (Console.WindowHeight > GameHeight + 3)
			{
				Console.WindowHeight = GameHeight + 3;
			}
			else
			{
				Console.BufferHeight = GameHeight + 3;
			}
			if (Console.WindowWidth > GameWidth + 2)
			{
				Console.WindowWidth = GameWidth + 2;
			}
			else
			{
				Console.BufferHeight = GameWidth + 2;
			}
		}

		private static void PerformWrite()
		{
			ClearConsole();
			DrawScene();
		}
		private static void PerformInput()
		{
			Console.Write("Aktion> ");
			currentScene.PerformAction(Console.ReadLine());
		}

		private static void DrawChar(int x, int y, char @char)
		{
			Console.SetCursorPosition(x, Console.WindowHeight - y);
			Console.Write(@char);
		}

		private static void ClearConsole()
		{
			for (int x = 0; x < Console.WindowWidth; x++)
			{
				for (int y = 0; y < Console.WindowHeight; y++)
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
				DrawChar(centeredX + i, GameHeight, currentScene.Title[i]);
			}
		}
		private static void DrawDescription()
		{
			int y = GameHeight - 3;
		}
		private static void DrawActions()
		{

		}
		private static CellType ResolveCellType(int x, int y)
		{
			return CellType.Content;
		}
	}
}
