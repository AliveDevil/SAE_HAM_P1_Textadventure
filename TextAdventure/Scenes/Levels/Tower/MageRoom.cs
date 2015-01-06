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
	/// <summary>
	/// Represents the mage room.
	/// </summary>
	public sealed class MageRoom : LevelScene
	{
		/// <summary>
		/// Asks player for id and gives first quest.
		/// </summary>
		public override string Description
		{
			get
			{
				if (!SceneManager.GetComponentByType<Player>().HasName)
				{
					return Resources.Tower_Mage_Description_AskForName;
				}
				return string.Format(CultureInfo.CurrentCulture, Resources.Tower_Mage_Description_Quest, SceneManager.GetComponentByType<Player>().Id);
			}
		}

		/// <summary>
		/// Mages room title. See Tower_Mage_Title in Resources.
		/// </summary>
		public override string Title { get { return Resources.Tower_Mage_Title; } }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public MageRoom()
		{
			SceneManager.GetComponentByType<Player>().Rename += PlayerRename;
			ChangeRoomComponent stairs = new ChangeRoomComponent("stairs", false);
			stairs.Follow += TakeStairs;
			AddComponent(stairs);
		}

		/// <summary>
		/// Renames current player (using Players Rename Event).
		/// </summary>
		private void PlayerRename(object sender, ComponentEventArgs e)
		{
			Player player = sender as Player;

			if (string.IsNullOrEmpty(player.Id))
			{
				player.SetName(e.Parameter);
				FindComponent<ChangeRoomComponent>().Enabled = true;
				e.Handled = true;
			}
		}

		/// <summary>
		/// Goes down stairs.
		/// </summary>
		private void TakeStairs(object sender, ComponentEventArgs e)
		{
			SceneManager.GetComponentByType<Player>().AddItem(new LifePotion());
			for (int i = 0; i < 5; i++)
			{
				SceneManager.GetComponentByType<Player>().AddItem(new HealthPotion());
				SceneManager.GetComponentByType<Player>().AddItem(new StrengthPotion());
			}
			SceneManager.LoadScene<TowerEntrance>();
			e.Handled = true;
		}
	}
}
