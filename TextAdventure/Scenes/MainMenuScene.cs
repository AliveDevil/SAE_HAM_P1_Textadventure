/*
 * Author: Jöran Malek
 */

using System;
using TextAdventure.Attributes;
using TextAdventure.Scenes.Levels;

namespace TextAdventure.Scenes
{
	public sealed class MainMenuScene : Scene
	{
		public override string Title { get { return Properties.Resources.MainMenu_Title; } }

		public override void Initialize()
		{
			RegisterAction(StartAdventureAction);
			RegisterAction(CreditsAction);
			RegisterAction(ExitAction);
		}

		public override string Description()
		{
			return Properties.Resources.MainMenu_Description;
		}

		[Action("start", Properties.Resources.MainMenu_Start)]
		private void StartAdventureAction()
		{
			SceneManager.LoadScene<Level01Scene>();
		}

		[Action("credits", Properties.Resources.MainMenu_Credits)]
		private void CreditsAction()
		{
			SceneManager.LoadScene<CreditsScene>();
		}

		[Action("exit", Properties.Resources.MainMenu_Exit)]
		private void ExitAction()
		{
			SceneManager.Exit();
		}
	}
}
