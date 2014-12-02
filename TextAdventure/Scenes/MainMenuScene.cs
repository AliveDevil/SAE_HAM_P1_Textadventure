/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure.Scenes
{
	public sealed class MainMenuScene : Scene
	{
		private const string introText =
			"+------------------------+\n" +
			"|     TextAdventure      |\n" +
			"| Aktionen               |\n" +
			"| Play       Spiel start |\n" +
			"| Credits    Credits     |\n" +
			"| Exit       Beenden     |\n" +
			"+------------------------+\n";


		public override void Initialize()
		{
			RegisterAction("Exit", ExitAction);
		}

		public override void Write()
		{
			Console.WriteLine(introText);
		}

		private void ExitAction()
		{
			SceneManager.Exit();
		}
	}
}
