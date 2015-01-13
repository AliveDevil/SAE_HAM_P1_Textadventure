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
		/// Returns some description in this scene. See Tower_Entrance_Description in Resources.
		/// </summary>
		public override string Description { get { return Resources.Tower_Entrance_Description; } }

		/// <summary>
		/// Returns tower ground. See Tower_Entrance_Title in Resources.
		/// </summary>
		public override string Title { get { return Resources.Tower_Entrance_Title; } }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public TowerEntrance()
		{
			SceneManager.GetComponentByType<Player>().Attack += playerAttack;

			Goblin goblin = Goblin.SmallGoblin("goblin");
			goblin.Died += goblinDied;
			AddComponent(goblin);

			ChangeRoomComponent path = new ChangeRoomComponent("path", false);
			path.Follow += followPath;
			AddComponent(path);
		}

		/// <summary>
		/// Clean up of resources.
		/// </summary>
		public override void Dispose()
		{
			SceneManager.GetComponentByType<Player>().Attack -= playerAttack;
			base.Dispose();
		}

		/// <summary>
		/// Loads next scene in forest.
		/// </summary>
		private void followPath(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<Forest.Glade>();
			e.Handled = true;
		}

		/// <summary>
		/// Message that goblin has died and enable path.
		/// </summary>
		private void goblinDied(object sender, ComponentEventArgs e)
		{
			Component component = sender as Component;
			if (component != null)
			{
				PostMessage(Resources.Goblin_Died);
				PostMessage(Resources.Tower_Entrance_Progress);
				RemoveComponent(component);
				FindComponent<ChangeRoomComponent>().Enabled = true;
			}
		}

		/// <summary>
		/// Attack that goblin!
		/// </summary>
		private void playerAttack(object sender, ComponentEventArgs e)
		{
			(sender as Entity).Attack(FindComponent(e.Parameter) as Entity);
			e.Handled = true;
		}
	}
}
