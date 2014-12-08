/*
 * Author: Jöran Malek
 */

using System;
using TextAdventure.Attributes;
using TextAdventure.Properties;
using TextAdventure.Scenes.Levels;

namespace TextAdventure.Scenes
{
	/// <summary>
	/// It's the main menu.
	/// </summary>
	public sealed class MainMenuScene : Scene
	{
		public override string Title { get { return Resources.MainMenu_Title; } }
		public override bool DrawActions { get { return true; } }
		public override string Description { get { return Resources.MainMenu_Description; } }

		public override void Initialize()
		{
			RegisterAction(StartAdventureAction);
			RegisterAction(CreditsAction);
			RegisterAction(ExitAction);
		}

		[Action("start", "MainMenu_Start")]
		private void StartAdventureAction()
		{
			SceneManager.LoadScene<Level01Scene>();
		}

		[Action("credits", "MainMenu_Credits")]
		private void CreditsAction()
		{
			SceneManager.LoadScene<CreditsScene>();
		}

		[Action("exit", "MainMenu_Exit")]
		private void ExitAction()
		{
			SceneManager.Exit();
		}
	}
}
