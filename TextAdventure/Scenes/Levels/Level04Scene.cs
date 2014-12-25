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
			AddComponent(goblin);
			ChangeRoomComponent path = new ChangeRoomComponent("path", false);
			path.Follow += FollowPath;
			AddComponent(path);
		}

		private void FollowPath(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<Level05Scene>();
			e.Handled = true;
		}

		private void GoblinDied(object sender, ComponentEventArgs e)
		{
			Component component = sender as Component;
			if (component != null)
			{
				AddMessage(Resources.Goblin_Died);
				AddMessage(Resources.Room4_Progress);
				RemoveComponent(component);
				FindComponent<ChangeRoomComponent>().Enabled = true;
				e.Handled = true;
			}
		}

		private void PlayerAttack(object sender, ComponentEventArgs e)
		{
			(sender as Entity).Attack(FindComponent(e.Parameter) as Entity);
			e.Handled = true;
		}
	}
}
