/*
 * Author: Jöran Malek
 */

using System.Linq;
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
				return string.Format(Resources.GameOver_Description,
					Arguments != null && Arguments.Count > 0
					? Arguments[0]
					: "How did you finish? There is no information about it.");
			}
		}

		public GameOverScene(params string[] arguments)
			: base(arguments)
		{

		}
	}
}
