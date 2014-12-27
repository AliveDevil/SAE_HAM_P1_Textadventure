﻿/*
 * Author: Jöran Malek
 */

using TextAdventure.Properties;
using TextAdventure.Scenes.Components;
using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Levels.Tower
{
	public sealed class TowerEntrance : LevelScene
	{
		public override string Title { get { return Resources.Room4_Title; } }
		public override string Description { get { return Resources.Room4_Description; } }

		public TowerEntrance()
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
				PostMessage(Resources.Goblin_Died);
				PostMessage(Resources.Room4_Progress);
				RemoveComponent(component);
				FindComponent<ChangeRoomComponent>().Enabled = true;
			}
		}

		private void PlayerAttack(object sender, ComponentEventArgs e)
		{
			(sender as Entity).Attack(FindComponent(e.Parameter) as Entity);
			e.Handled = true;
		}
	}
}