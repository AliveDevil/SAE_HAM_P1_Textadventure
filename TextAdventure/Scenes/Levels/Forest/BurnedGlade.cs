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
	public sealed class BurnedGlade : LevelScene
	{
		public override string Description { get { return Resources.Forest_BurnedGlade_Description; } }

		public override string Title { get { return Resources.Forest_BurnedGlade_Title; } }

		public BurnedGlade()
		{
			SceneManager.GetComponentByType<Player>().Attack += player_Attack;

			ChangeRoomComponent tentEntrance = new ChangeRoomComponent("entrance", false);
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

		public override void Dispose()
		{
			SceneManager.GetComponentByType<Player>().Attack -= player_Attack;
			base.Dispose();
		}

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

		private void player_Attack(object sender, ComponentEventArgs e)
		{
			(sender as Entity).Attack(FindComponent(e.Parameter) as Entity);
			e.Handled = true;
		}

		private void tentEntrance_Follow(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<Tent>();
			e.Handled = true;
		}
	}
}
