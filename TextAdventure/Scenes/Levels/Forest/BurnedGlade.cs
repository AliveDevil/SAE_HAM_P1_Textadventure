/*
 * Author: Jöran Malek
 */

using System.Linq;
using TextAdventure.Properties;
using TextAdventure.Scenes.Components;
using TextAdventure.Scenes.Components.Entities;
using TextAdventure.Scenes.Components.Items;

namespace TextAdventure.Scenes.Levels.Forest
{
	/// <summary>
	/// The burned glade.
	/// </summary>
	public sealed class BurnedGlade : LevelScene
	{
		/// <summary>
		/// The default description.
		/// </summary>
		public override string Description { get { return Resources.Forest_BurnedGlade_Description; } }

		/// <summary>
		/// The scenes title.
		/// </summary>
		public override string Title { get { return Resources.Forest_BurnedGlade_Title; } }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public BurnedGlade()
		{
			SceneManager.GetComponentByType<Player>().Attack += player_Attack;

			ChangeRoomComponent tentEntrance = new ChangeRoomComponent("tent", false);
			tentEntrance.Follow += tentEntrance_Follow;
			AddComponent(tentEntrance);

			Goblin leftGoblin = Goblin.MediumGoblin("goblin");
			leftGoblin.Enabled = false;
			leftGoblin.Died += goblin_Died;
			AddComponent(leftGoblin);

			Goblin rightGoblin = Goblin.MediumGoblin("goblin");
			rightGoblin.Enabled = true;
			rightGoblin.Died += goblin_Died;
			AddComponent(rightGoblin);

			TakeableComponent chest = new TakeableComponent("chest", true);
			chest.Interact += chest_Interact;
			AddComponent(chest);
		}

		/// <summary>
		/// Dispose things.
		/// </summary>
		public override void Dispose()
		{
			SceneManager.GetComponentByType<Player>().Attack -= player_Attack;
			base.Dispose();
		}

		/// <summary>
		/// Add things to players inventory.
		/// </summary>
		private void chest_Interact(object sender, ComponentEventArgs e)
		{
			RemoveComponent(sender as Component);
			PostMessage(Resources.Forest_BurnedGlade_Chest);

			Player player = SceneManager.GetComponentByType<Player>();
			player.IncreaseStrength(7);
			for (int i = 0; i < 5; i++)
			{
				player.AddItem(new HealthPotion());
			}
			e.Handled = true;
		}

		/// <summary>
		/// Executed on death of any goblin.
		/// </summary>
		private void goblin_Died(object sender, ComponentEventArgs e)
		{
			RemoveComponent(sender as Component);
			PostMessage(Resources.Forest_BurnedGlade_Goblin);

			if (!FindComponents<Goblin>().Any())
			{
				PostMessage(Resources.Forest_BurnedGlade_GoToTent);
				FindComponent<ChangeRoomComponent>().Enabled = true;
			}
			e.Handled = true;
		}

		/// <summary>
		/// Executed on attack of player.
		/// </summary>
		private void player_Attack(object sender, ComponentEventArgs e)
		{
			(sender as Entity).Attack(FindComponent(e.Parameter) as Entity);
			e.Handled = true;
		}

		/// <summary>
		/// Progression.
		/// </summary>
		private void tentEntrance_Follow(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<Tent>();
			e.Handled = true;
		}
	}
}
