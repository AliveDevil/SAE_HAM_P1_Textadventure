using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Scenes
{
	static class SceneManager
	{
		private static bool exit = false;
		private static Scene currentScene;

		public static void LoadScene<T>() where T : Scene, new()
		{
			currentScene = new T();
			currentScene.Initialize();
		}

		public static void Run()
		{
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

		private static void PerformWrite()
		{
			Console.Clear();
			Console.SetCursorPosition(0, 0);
			currentScene.Write();
		}
		private static void PerformInput()
		{
			Console.Write("Aktion> ");
			currentScene.PerformAction(Console.ReadLine());
		}
	}
}
