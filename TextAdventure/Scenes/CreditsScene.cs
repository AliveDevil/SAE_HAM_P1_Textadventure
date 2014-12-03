/*
 * Author: Jöran Malek
 */

using TextAdventure.Attributes;

namespace TextAdventure.Scenes
{
	public sealed class CreditsScene : Scene
	{
		public override string Title { get { return Properties.Resources.Credits_Title; } }

		public override void Initialize()
		{
			RegisterAction(BackAction);
		}

		[Action("back", Properties.Resources.Credits_Back)]
		private void BackAction()
		{
			SceneManager.LoadScene<MainMenuScene>();
		}
	}
}
