/*
 * Author: Jöran Malek
 */

using TextAdventure.Properties;
using TextAdventure.Scenes.Components;

namespace TextAdventure.Scenes.Levels
{
	/// <summary>
	/// The hall into the mages room.
	/// </summary>
	public class Level02Scene : LevelScene
	{
		public override string Title { get { return Resources.Room2_Title; } }
		public override bool DrawActions { get { return base.DrawActions; } }
		public override string Description { get { return Resources.Room2_Description; } }

		public Level02Scene()
		{
			AddComponent(new DoorComponent("door", OpenDoor));
		}

		protected override string OnNoActionFound()
		{
			return Resources.Room2_Fail;
		}

		private bool OpenDoor(Component component)
		{
			SceneManager.LoadScene<Level03Scene>();
			return true;
		}
	}
}
