/*
 * Author: Jöran Malek
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventure.Scenes
{
	/// <summary>
	/// Things one might want to ignore.
	/// </summary>
	public static class SceneManager
	{
		public const int GameWidth = 64;
		public const int GameHeight = 36;
		public const int BufferWidth = GameWidth + 2;
		public const int BufferHeight = GameHeight + 4;
		public const char EmptyChar = ' ';
		public const char CornerChar = '+';
		public const char BorderHorizontalChar = '-';
		public const char BorderVerticalChar = '|';

		private static bool exit = false;
		private static int messageY = 0;

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
		public static void Message(string message)
		{
			DrawTextBlock(message, messageY);
			messageY += message.Length / GameWidth + 1;
		}

		private static void SetResolution()
		{
			Console.SetWindowSize(1, 1);
			Console.SetBufferSize(BufferWidth, BufferHeight);
			Console.SetWindowSize(BufferWidth, BufferHeight);
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
			ConsoleKeyInfo key;
			string input = "";
			while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
			{
				if (key.Key == ConsoleKey.Backspace && input.Length > 0)
				{
					input = input.Substring(0, input.Length - 1);
				}
				else
				{
					input += key.KeyChar;
				}
				SetCursorPosition(7, GameHeight + 1);
				Console.Write(input);
			}
			currentScene.PerformAction(input);
		}

		#region Draw Stuff
		private static void ClearConsole()
		{
			messageY = 0;
			for (int x = -1; x <= GameWidth; x++)
			{
				for (int y = -1; y <= GameHeight + 1; y++)
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
			Console.Title = currentScene.Title;
			DrawCenteredText(currentScene.Title, 0);
			messageY++;
		}
		private static void DrawDescription()
		{
			DrawTextBlock(currentScene.Description, 1);
			messageY += currentScene.Description.Length / GameWidth + 1;
		}
		private static void DrawActions()
		{
			if (currentScene.DrawActions)
			{
				List<Line> lines = EnumerateActions();
				int maxHeight = lines.Sum(line => line.Lines.Count);
				int startY = GameHeight - maxHeight;

				int currentY = startY;

				foreach (Line line in lines)
				{
					for (int i = 0; i < line.Key.Length; i++)
					{
						DrawChar(i, currentY, line.Key[i]);
					}
					for (int i = 0; i < line.Lines.Count; i++)
					{
						for (int j = 0; j < line.Lines[i].Length; j++)
						{
							DrawChar(GameWidth - line.Lines[i].Length + j, currentY, line.Lines[i][j]);
						}
						currentY++;
					}
				}

				DrawCenteredText("Actions", GameHeight - maxHeight - 1);
			}
		}
		private static void DrawChar(int x, int y, char @char)
		{
			SetCursorPosition(x, y);
			Console.Write(@char);
		}
		private static List<Line> EnumerateActions()
		{
			Dictionary<string, string> actions = currentScene.GetActions();
			List<Line> lines = new List<Line>();
			int maxKeyLength = actions.Max(pair => pair.Key.Length);
			int startX = maxKeyLength + 1;
			int maxLength = GameWidth - startX;

			foreach (KeyValuePair<string, string> pair in actions)
			{
				Line line = new Line(pair.Key, startX);
				string lineString = "";
				for (int i = 0; i < pair.Value.Length; i++)
				{
					if (lineString.Length < maxLength)
					{
						lineString += pair.Value[i];
					}
					else
					{
						line.Lines.Add(lineString);
						lineString = pair.Value[i].ToString();
					}
				}
				if (!string.IsNullOrEmpty(lineString))
				{
					line.Lines.Add(lineString);
				}
				lines.Add(line);
			}

			return lines;
		}
		private static void DrawCenteredText(string text, int y)
		{
			int centeredLength = text.Length / 2;
			int centeredX = GameWidth / 2 - centeredLength;
			for (int i = 0; i < text.Length; i++)
			{
				DrawChar(centeredX + i, y, text[i]);
			}
		}
		private static void DrawTextBlock(string text, int y)
		{
			for (int x = 0; x < text.Length; x++)
			{
				DrawChar(x % GameWidth, x / GameWidth + 1, text[x]);
			}
		}
		#endregion

		#region Postioning Stuff
		private static void SetCursorPosition(int x, int y)
		{
			Console.SetCursorPosition(ResolveX(x), ResolveY(y));
		}
		private static CellType ResolveCellType(int x, int y)
		{
			if ((x == -1 && y == -1)
				|| (x == -1 && y == GameHeight)
				|| (x == GameWidth && y == -1)
				|| (x == GameWidth && y == GameHeight))
			{
				return CellType.Corner;
			}
			if ((x == -1 || x == GameWidth) && y < GameHeight)
			{
				return CellType.BorderVertical;
			}
			if ((y == -1 || y == GameHeight) && x < GameWidth)
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
