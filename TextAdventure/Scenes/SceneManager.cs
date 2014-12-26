/*
 * Author: Jöran Malek
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TextAdventure.Properties;
using TextAdventure.Scenes.Components;

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

		private static List<Component> registeredComponents = new List<Component>();
		private static bool exit = false;
		private static int messageY = 0;
		private static Scene currentScene;
		private static Random randomNumberGenerator = new Random();

		public static Scene CurrentScene { get { return currentScene; } }
		public static Random RandomNumberGenerator { get { return randomNumberGenerator; } }
		public static ReadOnlyCollection<Component> RegisteredComponents { get { return registeredComponents.AsReadOnly(); } }

		public static bool LoadScene<T>(params string[] arguments) where T : Scene
		{
			currentScene = (T)Activator.CreateInstance(typeof(T), arguments);
			currentScene.Initialize();
			return true;
		}
		public static void Run()
		{
			SetResolution();
			while (!exit)
			{
				WriteScene();
				PerformInput();
			}
		}
		public static void WriteScene()
		{
			PerformWrite();
		}
		public static void Exit()
		{
			exit = true;
		}
		public static void RegisterGlobalComponent(Component component)
		{
			if (!registeredComponents.Contains(component))
			{
				registeredComponents.Add(component);
			}
		}
		public static T GetComponentByType<T>() where T : Component
		{
			return RegisteredComponents.OfType<T>().FirstOrDefault() as T;
		}

		#region Draw Stuff
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
			DrawMessages();
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
			messageY += DrawTextBlock(currentScene.Description, 1);
		}
		private static void DrawMessages()
		{
			foreach (string message in currentScene.Messages)
			{
				messageY += DrawTextBlock(message, messageY);
			}
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

				DrawCenteredText(Resources.Generic_Actions, GameHeight - maxHeight - 1);
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

			if (actions.Any())
			{
				int maxKeyLength = actions.Max(pair => pair.Key.Length);
				int startX = maxKeyLength + 1;
				int maxLength = GameWidth - startX;

				ExportActionLines(actions, lines, startX, maxLength);
			}

			return lines;
		}
		private static void ExportActionLines(Dictionary<string, string> actions, List<Line> lines, int startX, int maxLength)
		{
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
		private static int DrawTextBlock(string text, int y)
		{
			string[] lines = SplitLines(text);
			foreach (string line in lines)
			{
				for (int i = 0; i < line.Length; i++)
				{
					DrawChar(i, y, line[i]);
				}
				y += 1;
			}
			return lines.Length;
		}
		private static string[] SplitLines(string text)
		{
			List<string> lines = new List<string>();

			int x = 0;
			StringBuilder currentLine = new StringBuilder();
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] == '\n')
				{
					x = 0;
					WriteLineToStringBuilder(lines, currentLine);
					continue;
				}
				else if (x >= GameWidth)
				{
					x = 0;
					WriteLineToStringBuilder(lines, currentLine);
				}
				currentLine.Append(text[i]);
				x += 1;
			}
			if (currentLine.Length > 0)
			{
				lines.Add(currentLine.ToString());
			}

			return lines.ToArray();
		}
		private static void WriteLineToStringBuilder(List<string> lines, StringBuilder builder)
		{
			lines.Add(builder.ToString());
			builder.Clear();
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

		#region Input Stuff
		private static void PerformInput()
		{
			SetCursorPosition(-1, GameHeight + 1);
			Console.Write(Resources.Generic_InputFormat, Resources.Generic_Action);
			string input = Console.ReadLine();

			List<string> arguments = ExtractArguments(input);
			if (!currentScene.PerformAction(arguments))
			{
				GlobalComponentInput(arguments);
			}
		}
		private static List<string> ExcludedParts()
		{
			return Resources.StrikeWords.Split(',').ToList();
		}
		private static List<string> ExtractArguments(string input)
		{
			List<string> excludedArguments = ExcludedParts();
			List<string> arguments = new List<string>();

			string argument = "";
			for (int i = 0; i < input.Length; i++)
			{
				char c = input[i];
				if (IsArgumentPart(c))
				{
					argument += c;
				}
				else if (!string.IsNullOrEmpty(argument))
				{
					AddToArguments(ref argument, excludedArguments, arguments);
				}
			}
			if (!string.IsNullOrEmpty(argument))
			{
				AddToArguments(ref argument, excludedArguments, arguments);
			}

			return arguments;
		}
		private static void AddToArguments(ref string argument, List<string> exclude, List<string> arguments)
		{
			bool inExcluded = false;
			foreach (var item in exclude)
			{
				inExcluded = inExcluded | argument.Equals(item, StringComparison.OrdinalIgnoreCase);
			}
			if (!inExcluded)
			{
				arguments.Add(argument.ToUpperInvariant());
			}
			argument = "";
		}
		private static bool IsArgumentPart(char c)
		{
			return !(char.IsControl(c) || char.IsPunctuation(c) || char.IsSymbol(c) || char.IsWhiteSpace(c));
		}
		private static void GlobalComponentInput(List<string> arguments)
		{
			if (arguments.Count > 0)
			{
				foreach (var item in registeredComponents)
				{
					if (item.CanInteract(arguments[0], null) && item.Interact(arguments[0], string.Join(", ", arguments.Skip(1))))
					{
						break;
					}
				}
			}
		}
		#endregion
	}
}
