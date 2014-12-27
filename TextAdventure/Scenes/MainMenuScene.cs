/*
 * Author: Jöran Malek
 */

using System;
using TextAdventure.Attributes;
using TextAdventure.Properties;
using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes
{
	/// <summary>
	/// It's the main menu.
	/// </summary>
	public sealed class MainMenuScene : Scene
	{
		public override string Title { get { return Resources.MainMenu_Title; } }
		public override string Description { get { return Resources.MainMenu_Description; } }

		public override void Initialize()
		{
			RegisterAction(StartAdventureAction);
			RegisterAction(CreditsAction);
			RegisterAction(ExitAction);
		}

		[Action("start", "MainMenu_Start")]
		private bool StartAdventureAction()
		{
			SceneManager.RegisterGlobalComponent(new Player(true));
			SceneManager.LoadScene<TextAdventure.Scenes.Levels.Tower.EntryRoom>();
			return true;
		}

		[Action("credits", "MainMenu_Credits")]
		private bool CreditsAction()
		{
			SceneManager.LoadScene<CreditsScene>();
			return true;
		}

		[Action("exit", "MainMenu_Exit")]
		private bool ExitAction()
		{
			SceneManager.Exit();
			return true;
		}
	}
}
