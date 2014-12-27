/*
 * Author: Jöran Malek
 */

using System.Globalization;
using TextAdventure.Properties;
using TextAdventure.Scenes.Components;
using TextAdventure.Scenes.Components.Entities;
using TextAdventure.Scenes.Components.Items;

namespace TextAdventure.Scenes.Levels.Tower
{
	public sealed class MageRoom : LevelScene
	{
		public override string Title { get { return Resources.Room3_Title; } }
		public override string Description
		{
			get
			{
				if (!SceneManager.GetComponentByType<Player>().HasName)
				{
					return Resources.Room3_Description_AskForName;
				}
				return string.Format(CultureInfo.CurrentCulture, Resources.Room3_Description_Quest, SceneManager.GetComponentByType<Player>().Name);
			}
		}

		public MageRoom()
		{
			SceneManager.GetComponentByType<Player>().Rename += PlayerRename;
			ChangeRoomComponent stairs = new ChangeRoomComponent("stairs", false);
			stairs.Follow += TakeStairs;
			AddComponent(stairs);
		}

		private void PlayerRename(object sender, ComponentEventArgs e)
		{
			Player player = sender as Player;

			if (string.IsNullOrEmpty(player.Name))
			{
				player.SetName(e.Parameter);
				FindComponent<ChangeRoomComponent>().Enabled = true;
				e.Handled = true;
			}
		}

		private void TakeStairs(object sender, ComponentEventArgs e)
		{
			SceneManager.GetComponentByType<Player>().AddItem(new LifePotion());
			for (int i = 0; i < 5; i++)
			{
				SceneManager.GetComponentByType<Player>().AddItem(new HealthPotion());
				SceneManager.GetComponentByType<Player>().AddItem(new StrengthPotion());
			}
			//SceneManager.GetComponentByType<Player>().AddItem(new )
			SceneManager.LoadScene<TowerEntrance>();
			e.Handled = true;
		}
	}
}
