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
		/// <summary>
		/// Main Menus Description.
		/// </summary>
		public override string Description { get { return Resources.MainMenu_Description; } }

		/// <summary>
		/// Main Menus Title.
		/// </summary>
		public override string Title { get { return Resources.MainMenu_Title; } }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public MainMenuScene()
			: base()
		{
			RegisterAction(StartAdventureAction);
			RegisterAction(CreditsAction);
			RegisterAction(ExitAction);
		}

		/// <summary>
		/// Opens credits.
		/// </summary>
		/// <returns>True.</returns>
		[Action("credits", "MainMenu_Credits")]
		private bool CreditsAction()
		{
			SceneManager.LoadScene<CreditsScene>();
			return true;
		}

		/// <summary>
		/// Quits application.
		/// </summary>
		/// <returns>True.</returns>
		[Action("exit", "MainMenu_Exit")]
		private bool ExitAction()
		{
			SceneManager.Exit();
			return true;
		}

		/// <summary>
		/// Starts the adventure.
		/// </summary>
		/// <returns>True.</returns>
		[Action("start", "MainMenu_Start")]
		private bool StartAdventureAction()
		{
			SceneManager.RegisterGlobalComponent(new Player(true));
			SceneManager.LoadScene<TextAdventure.Scenes.Levels.Tower.EntryRoom>();
			return true;
		}
	}
}
