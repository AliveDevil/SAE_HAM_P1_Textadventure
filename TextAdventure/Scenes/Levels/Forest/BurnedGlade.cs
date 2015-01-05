/*
 * Author: Jöran Malek
 */

using TextAdventure.Properties;
using TextAdventure.Scenes.Components;
using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Levels.Forest
{
	public sealed class BurnedGlade : LevelScene
	{
		private Goblin leftGoblin, rightGoblin;

		private ChangeRoomComponent tentEntrance;

		public override string Description { get { return Resources.Forest_BurnedGlade_Description; } }

		public override string Title { get { return Resources.Forest_BurnedGlade_Title; } }

		public BurnedGlade()
		{
			tentEntrance = new ChangeRoomComponent("entrance", false);
			tentEntrance.Follow += tentEntrance_Follow;
			AddComponent(tentEntrance);
			leftGoblin = Goblin.MediumGoblin("goblin");
			leftGoblin.Enabled = false;
			leftGoblin.Died += leftGoblin_Died;
			AddComponent(leftGoblin);
			rightGoblin = Goblin.MediumGoblin("goblin");
			rightGoblin.Enabled = true;
			rightGoblin.Died += rightGoblin_Died;
			AddComponent(rightGoblin);
		}

		private void leftGoblin_Died(object sender, ComponentEventArgs e)
		{
			RemoveComponent(sender as Component);
			PostMessage(Resources.Forest_BurnedGlade_LeftDied);
			tentEntrance.Enabled = true;
		}

		private void rightGoblin_Died(object sender, ComponentEventArgs e)
		{
			RemoveComponent(sender as Component);
			PostMessage(Resources.Forest_BurnedGlade_RightDied);
			leftGoblin.Enabled = true;
		}

		private void tentEntrance_Follow(object sender, ComponentEventArgs e)
		{
		}
	}
}
