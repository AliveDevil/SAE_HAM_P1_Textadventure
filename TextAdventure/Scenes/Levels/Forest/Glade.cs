/*
 * Author: Jöran Malek
 */

using TextAdventure.Scenes.Components;
using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Levels.Forest
{
	public sealed class Glade : LevelScene
	{
		private ChangeRoomComponent path;

		private Goblin smallGoblin, mediumGoblin;

		public override string Description { get { return base.Description; } }

		public override string Title { get { return base.Title; } }

		public Glade()
		{
			smallGoblin = Goblin.SmallGoblin("goblin");
			smallGoblin.Enabled = true;
			smallGoblin.Died += smallGoblin_Died;
			AddComponent(smallGoblin);
			mediumGoblin = Goblin.MediumGoblin("goblin");
			mediumGoblin.Enabled = false;
			mediumGoblin.Died += mediumGoblin_Died;
			AddComponent(mediumGoblin);
			path = new ChangeRoomComponent("door", false);
			path.Follow += path_Follow;
			AddComponent(path);
		}

		private void mediumGoblin_Died(object sender, ComponentEventArgs e)
		{
			mediumGoblin.Enabled = false;
			path.Enabled = true;
		}

		private void path_Follow(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<BurnedGlade>();
		}

		private void smallGoblin_Died(object sender, Components.ComponentEventArgs e)
		{
			RemoveComponent(sender as Component);
			smallGoblin.Enabled = false;
			mediumGoblin.Enabled = true;
		}
	}
}
