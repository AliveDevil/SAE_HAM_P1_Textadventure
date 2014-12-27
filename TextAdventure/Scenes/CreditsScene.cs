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
		/// <summary>
		/// Credits Title. Unspectacular "Credits".
		/// </summary>
		public override string Title { get { return Resources.Credits_Title; } }
		/// <summary>
		/// Some text by me.
		/// </summary>
		public override string Description { get { return Resources.Credits_Description; } }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public CreditsScene()
			: base()
		{
			RegisterAction(BackAction);
		}

		/// <summary>
		/// Returns to main menu.
		/// </summary>
		/// <returns>True.</returns>
		[Action("back", "Credits_Back")]
		private bool BackAction()
		{
			SceneManager.LoadScene<MainMenuScene>();
			return true;
		}
	}
}
