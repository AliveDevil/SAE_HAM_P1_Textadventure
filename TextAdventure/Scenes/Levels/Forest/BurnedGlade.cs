/*
 * Author: Jöran Malek
 */

using System.Linq;
using TextAdventure.Properties;
using TextAdventure.Scenes.Components;
using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Levels.Forest
{
	public sealed class BurnedGlade : LevelScene
	{
		private ChangeRoomComponent tentEntrance;

		public override string Description { get { return Resources.Forest_BurnedGlade_Description; } }

		public override string Title { get { return Resources.Forest_BurnedGlade_Title; } }

		public BurnedGlade()
		{
			TakeableComponent chest = new TakeableComponent("chest", true, new Activator("chest", true));
			chest.Interact += chest_Interact;
			AddComponent(chest);

			ChangeRoomComponent tentEntrance = new ChangeRoomComponent("entrance", false);
			tentEntrance.Follow += tentEntrance_Follow;
			AddComponent(tentEntrance);

			Goblin leftGoblin = Goblin.MediumGoblin("leftGoblin", new Activator("goblin", true), new Activator("left", false));
			leftGoblin.Died += goblin_Died;
			AddComponent(leftGoblin);

			Goblin rightGoblin = Goblin.MediumGoblin("rightGoblin", new Activator("goblin", true), new Activator("right", false));
			rightGoblin.Died += goblin_Died;
			AddComponent(rightGoblin);
		}

		private void chest_Interact(object sender, ComponentEventArgs e)
		{
			RemoveComponent(sender as Component);
			PostMessage(Resources.Forest_BurnedGlade_Chest);
			SceneManager.GetComponentByType<Player>().IncreaseStrength(7);
			e.Handled = true;
		}

		private void goblin_Died(object sender, ComponentEventArgs e)
		{
			RemoveComponent(sender as Component);
			PostMessage(Resources.Forest_BurnedGlade_GoblinDied);
			if (!FindComponents<Goblin>().Any())
			{
				PostMessage(Resources.Forest_BurnedGlade_FreeEntrance);
				FindComponent<ChangeRoomComponent>().Enabled = true;
			}
			e.Handled = true;
		}

		private void tentEntrance_Follow(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<Tent>();
			e.Handled = true;
		}
	}
}
