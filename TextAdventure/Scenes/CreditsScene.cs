/*
 * Author: Jöran Malek
 */

using TextAdventure.Attributes;
using TextAdventure.Properties;

namespace TextAdventure.Scenes
{
	/// <summary>
	/// Some credits.
	/// </summary>
	public sealed class CreditsScene : Scene
	{
		public override string Title { get { return Resources.Credits_Title; } }
		public override string Description { get { return Resources.Credits_Description; } }

		public override void Initialize()
		{
			RegisterAction(BackAction);
		}

		[Action("back", "Credits_Back")]
		private bool BackAction()
		{
			SceneManager.LoadScene<MainMenuScene>();
			return true;
		}
	}
}
