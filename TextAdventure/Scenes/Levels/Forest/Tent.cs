/*
 * Author: Jöran Malek
 */

using System.Globalization;
using System.Linq;
using TextAdventure.Properties;
using TextAdventure.Scenes.Components;
using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Levels.Forest
{
	public sealed class Tent : LevelScene
	{
		public override string Description
		{
			get
			{
				return Resources.Forest_Tent_Description;
			}
		}

		public override string Title
		{
			get
			{
				return Resources.Forest_Tent_Title;
			}
		}

		public Tent()
		{
			SceneManager.GetComponentByType<Player>().Attack += Player_Attack;

			for (int i = 0; i < 5; i++)
			{
				Goblin goblin = Goblin.MediumGoblin("goblin");
				goblin.Died += Goblin_Died;
				goblin.Enabled = false;
				AddComponent(goblin);
			}
			FindComponent<Goblin>().Enabled = true;

			Orc orc = Orc.GreatOrc("orc");
			orc.Enabled = false;
			orc.Died += Orc_Died;
			AddComponent(orc);
		}

		public override void Dispose()
		{
			SceneManager.GetComponentByType<Player>().Attack -= Player_Attack;
			base.Dispose();
		}

		private void Goblin_Died(object sender, Components.ComponentEventArgs e)
		{
			RemoveComponent(sender as Component);
			PostMessage(CultureInfo.CurrentCulture, Resources.Forest_Tent_GoblinDied, FindComponents<Goblin>().Count());
			if (!FindComponents<Goblin>().Any())
			{
				PostMessage(Resources.Forest_Tent_Discussion);
			}
			e.Handled = true;
		}

		private void Orc_Died(object sender, Components.ComponentEventArgs e)
		{
			SceneManager.LoadScene<GameOverScene>(Resources.Forest_Tent_Finished);
			e.Handled = true;
		}

		private void Player_Attack(object sender, Components.ComponentEventArgs e)
		{
			(sender as Entity).Attack(FindComponent(e.Parameter) as Entity);
			e.Handled = true;
		}
	}
}
