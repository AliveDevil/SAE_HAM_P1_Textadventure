/*
 * Author: Jöran Malek
 */

using TextAdventure.Properties;
using TextAdventure.Scenes.Components;
using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Levels.Tower
{
	/// <summary>
	/// Ground entrance of tower.
	/// </summary>
	public sealed class TowerEntrance : LevelScene
	{
		/// <summary>
		/// Returns some description in this scene. See Room4_Description in Resources.
		/// </summary>
		public override string Description { get { return Resources.Room4_Description; } }

		/// <summary>
		/// Returns tower ground. See Room4_Title in Resources.
		/// </summary>
		public override string Title { get { return Resources.Room4_Title; } }

		/// <summary>
		/// Default constructor.
		/// </summary>
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

		/// <summary>
		/// Loads next scene in forest.
		/// </summary>
		private void FollowPath(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<Level05Scene>();
			e.Handled = true;
		}

		/// <summary>
		/// Message that goblin has died and enable path.
		/// </summary>
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

		/// <summary>
		/// Attack that goblin!
		/// </summary>
		private void PlayerAttack(object sender, ComponentEventArgs e)
		{
			(sender as Entity).Attack(FindComponent(e.Parameter) as Entity);
			e.Handled = true;
		}
	}
}
