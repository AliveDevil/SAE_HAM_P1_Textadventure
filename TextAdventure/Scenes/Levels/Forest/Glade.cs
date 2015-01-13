﻿/*
 * Author: Jöran Malek
 */
using System.Linq;
using TextAdventure.Properties;
using TextAdventure.Scenes.Components;
using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Levels.Forest
{
	public sealed class Glade : LevelScene
	{
		public override string Description { get { return Resources.Forest_Glade_Description_SmallGoblin; } }

		public override string Title { get { return Resources.Forest_Glade_Title; } }

		public Glade()
		{
			SceneManager.GetComponentByType<Player>().Attack += PlayerAttack;

			Goblin smallGoblin = Goblin.SmallGoblin("goblin");
			smallGoblin.Enabled = true;
			smallGoblin.Died += goblin_Died;
			AddComponent(smallGoblin);

			Goblin mediumGoblin = Goblin.MediumGoblin("goblin");
			mediumGoblin.Enabled = true;
			mediumGoblin.Died += goblin_Died;
			AddComponent(mediumGoblin);

			ChangeRoomComponent path = new ChangeRoomComponent("door", false);
			path.Follow += path_Follow;
			AddComponent(path);
		}

		private void goblin_Died(object sender, ComponentEventArgs e)
		{
			RemoveComponent(sender as Component);
			if (!FindComponents<Goblin>().Any())
			{
				FindComponent<ChangeRoomComponent>().Enabled = true;
			}
		}

		private void path_Follow(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<BurnedGlade>();
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
