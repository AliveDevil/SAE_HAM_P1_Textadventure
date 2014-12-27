/*
 * Author: Jöran Malek
 */

using System.Globalization;
using TextAdventure.Attributes;
using TextAdventure.Properties;

namespace TextAdventure.Scenes
{
	/// <summary>
	/// Game over man.
	/// </summary>
	public sealed class GameOverScene : Scene
	{
		/// <summary>
		/// A message that current game is finished and player exited with given arguments.
		/// </summary>
		public override string Description
		{
			get
			{
				return string.Format(CultureInfo.CurrentCulture,
					Resources.GameOver_Description,
					Arguments != null && Arguments.Count > 0
					? Arguments[0]
					: Resources.GameOver_Default);
			}
		}

		/// <summary>
		/// Returns a title. Think it's "Game finished".
		/// </summary>
		public override string Title { get { return Resources.GameOver_Title; } }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public GameOverScene(params string[] arguments)
			: base(arguments)
		{
			RegisterAction(BackAction);
			RegisterAction(QuitAction);
		}

		/// <summary>
		/// Returns to main menu.
		/// </summary>
		/// <returns>True.</returns>
		[Action("back", "GameOver_Back")]
		private bool BackAction()
		{
			SceneManager.LoadScene<MainMenuScene>();
			return false;
		}

		/// <summary>
		/// Quits current application.
		/// </summary>
		/// <returns>True.</returns>
		[Action("quit", "GameOver_uit")]
		private bool QuitAction()
		{
			SceneManager.Exit();
			return false;
		}
	}
}
