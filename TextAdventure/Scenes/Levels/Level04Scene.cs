/*
 * Author: Jöran Malek
 */

using TextAdventure.Properties;
using TextAdventure.Scenes.Components;
using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Levels
{
	public sealed class Level04Scene : LevelScene
	{
		public override string Title { get { return Resources.Room4_Title; } }
		public override string Description { get { return Resources.Room4_Description; } }

		public Level04Scene()
		{
			SceneManager.GetComponentByType<Player>().Attack += PlayerAttack;
			Goblin goblin = Goblin.SmallGoblin("goblin");
			goblin.Died += GoblinDied;
			ChangeRoomComponent path = new ChangeRoomComponent("path", false);
			path.Open += FollowPath;
		}

		private bool FollowPath(ComponentEventArgs e)
		{
			return SceneManager.LoadScene<Level05Scene>();
		}

		private bool GoblinDied(ComponentEventArgs e)
		{
			Message(Resources.Goblin_Died);
			Message(Resources.Room4_Progress);
			RemoveComponent(e.Component);
			return true;
		}

		private bool PlayerAttack(ComponentEventArgs e)
		{
			return (e.Component as Entity).Attack(FindComponent(e.Parameter) as Entity); ;
		}
	}
}
