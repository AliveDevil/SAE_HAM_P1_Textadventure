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
		/// <summary>
		/// Horizontal lines on border.
		/// </summary>
		public const char BorderHorizontalChar = '-';

		/// <summary>
		/// Vertical lines on border.
		/// </summary>
		public const char BorderVerticalChar = '|';

		/// <summary>
		/// There are borders around the game. Please consider them in the buffer.
		/// </summary>
		public const int BufferHeight = GameHeight + 4;

		/// <summary>
		/// There are borders around the game. Please consider them in the buffer.
		/// </summary>
		public const int BufferWidth = GameWidth + 2;

		/// <summary>
		/// Char for four corners.
		/// </summary>
		public const char CornerChar = '+';

		/// <summary>
		/// This is nothing.
		/// </summary>
		public const char EmptyChar = ' ';

		/// <summary>
		/// Games ratio is 16/9. So 36 is a multiple of 9.
		/// </summary>
		public const int GameHeight = 36;

		/// <summary>
		/// Games ratio is 16/9. So 64 is a multiple of 16.
		/// </summary>
		public const int GameWidth = 64;

		/// <summary>
		/// Store for current scene.
		/// </summary>
		private static Scene currentScene;

		/// <summary>
		/// Do we want to exit?
		/// </summary>
		private static bool exit = false;

		/// <summary>
		/// Y-axis offset for messages (default after Title and Description before Actions, right
		/// there in the middle.).
		/// </summary>
		private static int messageY = 0;

		/// <summary>
		/// A simple random number generator.
		/// </summary>
		private static Random randomNumberGenerator = new Random();

		/// <summary>
		/// Components that are globally registered (like the player).
		/// </summary>
		private static List<Component> registeredComponents = new List<Component>();

		/// <summary>
		/// Gives readonly access to current scene.
		/// </summary>
		public static Scene CurrentScene { get { return currentScene; } }

		/// <summary>
		/// Gives readonly access to random number generator.
		/// </summary>
		public static Random RandomNumberGenerator { get { return randomNumberGenerator; } }

		/// <summary>
		/// Gives readonly access to registered components.
		/// </summary>
		public static ReadOnlyCollection<Component> RegisteredComponents { get { return registeredComponents.AsReadOnly(); } }

		/// <summary>
		/// Prepare for exit.
		/// </summary>
		public static void Exit()
		{
			exit = true;
		}

		/// <summary>
		/// Find a component by given generic type.
		/// </summary>
		/// <typeparam id="T">Some component type.</typeparam>
		/// <returns>First found component or null.</returns>
		public static T GetComponentByType<T>() where T : Component
		{
			return RegisteredComponents.OfType<T>().FirstOrDefault();
		}

		/// <summary>
		/// Generically load a scene.
		/// </summary>
		/// <typeparam id="T">Scene to be loaded.</typeparam>
		/// <param id="arguments">Some arguments passed to the constructor of the scene.</param>
		/// <returns>True</returns>
		public static bool LoadScene<T>(params string[] arguments) where T : Scene
		{
			currentScene = (T)System.Activator.CreateInstance(typeof(T), arguments);
			return true;
		}

		/// <summary>
		/// Register a global component.
		/// </summary>
		/// <param id="component">Component to be registered.</param>
		public static void RegisterGlobalComponent(Component component)
		{
			if (!registeredComponents.Contains(component))
			{
				registeredComponents.Add(component);
			}
		}

		/// <summary>
		/// Infinite loop.
		/// </summary>
		public static void Run()
		{
			SetResolution();
			FastConsole.Initialize();
			while (!exit)
			{
				WriteScene();
				PerformInput();
			}
		}

		/// <summary>
		/// Just write everything. This is publicly available and only calls PerformWrite (which is private).
		/// </summary>
		public static void WriteScene()
		{
			PerformWrite();
		}

		#region Draw Stuff

		/// <summary>
		/// Fills FastConsole with some empty content.
		/// </summary>
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

		/// <summary>
		/// Draw every registered action (if actions should be drawn). Anchored at bottom.
		/// </summary>
		private static void DrawActions()
		{
			if (currentScene.DrawActions)
			{
				List<Line> lines = EnumerateActionDescriptions();
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

		/// <summary>
		/// Draws a text centered at current y-location.
		/// </summary>
		/// <param id="text">Some text.</param>
		/// <param id="y">Some y-offset.</param>
		private static void DrawCenteredText(string text, int y)
		{
			int centeredLength = text.Length / 2;
			int centeredX = GameWidth / 2 - centeredLength;
			for (int i = 0; i < text.Length; i++)
			{
				DrawChar(centeredX + i, y, text[i]);
			}
		}

		/// <summary>
		/// Draws a single char at given position to FastConsole.
		/// </summary>
		/// <param id="x">X-location</param>
		/// <param id="y">Y-location</param>
		/// <param id="char">Some char.</param>
		private static void DrawChar(int x, int y, char @char)
		{
			FastConsole.Write(ResolveX(x), ResolveY(y), @char);
		}

		/// <summary>
		/// Draws the description. Right below the title.
		/// </summary>
		private static void DrawDescription()
		{
			messageY += DrawTextBlock(currentScene.Description, 1);
		}

		/// <summary>
		/// Draw every scenes message. After description.
		/// </summary>
		private static void DrawMessages()
		{
			foreach (string message in currentScene.Messages)
			{
				messageY += DrawTextBlock(message, messageY);
			}
		}

		/// <summary>
		/// Draws every single text area.
		/// </summary>
		private static void DrawScene()
		{
			DrawTitle();
			DrawDescription();
			DrawMessages();
			DrawActions();
			FastConsole.Print();
		}

		/// <summary>
		/// Draws a block of text.
		/// </summary>
		/// <param id="text"></param>
		/// <param id="y"></param>
		/// <returns>Lines that are used.</returns>
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

		/// <summary>
		/// Draws a title. Top. Centered.
		/// </summary>
		private static void DrawTitle()
		{
			Console.Title = currentScene.Title;
			DrawCenteredText(currentScene.Title, 0);
			messageY++;
		}

		/// <summary>
		/// Enumerates
		/// </summary>
		/// <returns></returns>
		private static List<Line> EnumerateActionDescriptions()
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

		/// <summary>
		/// Hide cursor and draw the scene.
		/// </summary>
		private static void PerformWrite()
		{
			using (HideCursor hideCursor = new HideCursor())
			{
				ClearConsole();
				DrawScene();
			}
		}

		/// <summary>
		/// Set console dimension to specified buffer.
		/// </summary>
		private static void SetResolution()
		{
			Console.SetWindowSize(1, 1);
			Console.SetBufferSize(BufferWidth, BufferHeight);
			Console.SetWindowSize(BufferWidth, BufferHeight);
		}

		/// <summary>
		/// Splits lines by width and \n.
		/// </summary>
		/// <param id="text">Some text.</param>
		/// <returns>Every single line.</returns>
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

		/// <summary>
		/// Appends StringBuilders content to a list of string and clears it.
		/// </summary>
		/// <param id="lines">Collection of string the builder content should be added to.</param>
		/// <param id="builder">Some stringbuilder.</param>
		private static void WriteLineToStringBuilder(ICollection<string> lines, StringBuilder builder)
		{
			lines.Add(builder.ToString());
			builder.Clear();
		}

		#endregion Draw Stuff

		#region Postioning Stuff

		private static int Clamp(int val, int min, int max)
		{
			return val > min ? val < max ? val : max : min;
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

		private static void SetCursorPosition(int x, int y)
		{
			Console.SetCursorPosition(ResolveX(x), ResolveY(y));
		}

		#endregion Postioning Stuff

		#region Input Stuff

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

		private static void GlobalComponentInput(IList<string> arguments)
		{
			if (arguments.Count > 0)
			{
				foreach (var item in registeredComponents)
				{
					if (item.CanInteract(arguments.ToArray()) && item.Interact(arguments.ToArray()))
					{
						break;
					}
				}
			}
		}

		private static bool IsArgumentPart(char c)
		{
			return !(char.IsControl(c) || char.IsPunctuation(c) || char.IsSymbol(c) || char.IsWhiteSpace(c));
		}

		private static void PerformInput()
		{
			SetCursorPosition(-1, GameHeight + 1);
			Console.Write(Resources.Generic_InputFormat, Resources.Generic_Action);
			string input = Console.ReadLine();

			IList<string> arguments = ExtractArguments(input);
			if (!currentScene.PerformAction(arguments))
			{
				GlobalComponentInput(arguments);
			}
		}

		#endregion Input Stuff
	}
}
