/*
 * Author: Jöran Malek
 */

using System.Linq;
using TextAdventure.Properties;
using TextAdventure.Scenes.Components;
using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Levels.Forest
{
	/// <summary>
	/// Some glade.
	/// </summary>
	public sealed class Glade : LevelScene
	{
		/// <summary>
		/// Glades description.
		/// </summary>
		public override string Description { get { return Resources.Forest_Glade_Description; } }

		/// <summary>
		/// Glades title.
		/// </summary>
		public override string Title { get { return Resources.Forest_Glade_Title; } }


		/// <summary>
		/// Default constructor.
		/// </summary>
		public Glade()
		{
			SceneManager.GetComponentByType<Player>().Attack += player_Attack;

			Goblin smallGoblin = Goblin.SmallGoblin("goblin");
			smallGoblin.Enabled = true;
			smallGoblin.Died += goblin_Died;
			AddComponent(smallGoblin);

			Goblin mediumGoblin = Goblin.MediumGoblin("goblin");
			mediumGoblin.Enabled = true;
			mediumGoblin.Died += goblin_Died;
			AddComponent(mediumGoblin);

			ChangeRoomComponent path = new ChangeRoomComponent("path", false);
			path.Follow += path_Follow;
			AddComponent(path);
		}

		/// <summary>
		/// Cleaning up events.
		/// </summary>
		public override void Dispose()
		{
			SceneManager.GetComponentByType<Player>().Attack -= player_Attack;
			base.Dispose();
		}

		/// <summary>
		/// Some goblin died.
		/// </summary>
		private void goblin_Died(object sender, ComponentEventArgs e)
		{
			RemoveComponent(sender as Component);
			PostMessage(Resources.Forest_Glade_GoblinDefeated);
			if (!FindComponents<Goblin>().Any())
			{
				PostMessage(Resources.Forest_Glade_FreeEntrance);
				FindComponent<ChangeRoomComponent>().Enabled = true;
			}
		}

		/// <summary>
		/// Loads next scene.
		/// </summary>
		private void path_Follow(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<BurnedGlade>();
		}

		/// <summary>
		/// Attack that goblin!
		/// </summary>
		private void player_Attack(object sender, ComponentEventArgs e)
		{
			(sender as Entity).Attack(FindComponent(e.Parameter) as Entity);
			e.Handled = true;
		}
	}
}
