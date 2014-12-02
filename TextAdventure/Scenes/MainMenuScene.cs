/*
 * Author: Jöran Malek
 */

using System;
using TextAdventure.Attributes;

namespace TextAdventure.Scenes
{
	public sealed class MainMenuScene : Scene
	{
		public override string Title { get { return "TextAdventure"; } }

		public override void Initialize()
		{
			RegisterAction(StartAdventureAction);
			RegisterAction(CreditAction);
			RegisterAction(ExitAction);
		}

		[Action("start", "Starte das Abenteuer")]
		private void StartAdventureAction()
		{

		}

		[Action("credits", "Credits anzeigen")]
		private void CreditAction()
		{
			SceneManager.LoadScene<CreditsScene>();
		}

		[Action("exit", "Beenden")]
		private void ExitAction()
		{
			SceneManager.Exit();
		}
	}
}
