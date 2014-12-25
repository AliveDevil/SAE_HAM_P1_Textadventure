/*
 * Author: Jöran Malek
 */

using System.Globalization;
using TextAdventure.Attributes;
using TextAdventure.Properties;

namespace TextAdventure.Scenes
{
	public sealed class GameOverScene : Scene
	{
		public override string Title { get { return Resources.GameOver_Title; } }
		public override string Description
		{
			get
			{
				return string.Format(CultureInfo.CurrentCulture,
					Resources.GameOver_Description,
					Arguments != null && Arguments.Count > 0
					? Arguments[0]
					: "How did you finish? There is no information about it.");
			}
		}

		public GameOverScene(params string[] arguments)
			: base(arguments)
		{
			RegisterAction(BackAction);
			RegisterAction(QuitAction);
		}

		[Action("back", "GameOver_Back")]
		private bool BackAction()
		{
			SceneManager.LoadScene<MainMenuScene>();
			return false;
		}

		[Action("quit", "GameOver_uit")]
		private bool QuitAction()
		{
			SceneManager.Exit();
			return false;
		}
	}
}
