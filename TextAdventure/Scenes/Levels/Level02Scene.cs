/*
 * Author: Jöran Malek
 */

using TextAdventure.Properties;

namespace TextAdventure.Scenes.Levels
{
	public class Level02Scene : LevelScene
	{
		public override string Title { get { return Resources.Room2_Title; } }
		public override bool DrawActions { get { return base.DrawActions; } }
		public override string Description { get { return Resources.Room2_Description; } }

		public Level02Scene()
		{

		}

		protected override string OnNoActionFound()
		{
			return Resources.Room2_Fail;
		}
	}
}
