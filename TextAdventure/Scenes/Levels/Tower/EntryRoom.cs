/*
 * Author: Jöran Malek
 */

using System.Linq;
using TextAdventure.Properties;
using TextAdventure.Scenes.Components;

namespace TextAdventure.Scenes.Levels.Tower
{
	/// <summary>
	/// First scene the player will see.
	/// </summary>
	public sealed class EntryRoom : LevelScene
	{
		/// <summary>
		/// Entry rooms title. Used from Resources. See Room1_Title.
		/// </summary>
		public override string Title { get { return Resources.Room1_Title; } }
		/// <summary>
		/// Shows some description to user.
		/// </summary>
		public override string Description
		{
			get
			{
				if (Components.OfType<SwitchComponent>().First().Switched)
				{
					return Resources.Room1_Description_Light;
				}
				return Resources.Room1_Description_Dark;
			}
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		public EntryRoom()
		{
			SwitchComponent lightSwitch = new SwitchComponent("switch", true, false);
			lightSwitch.Switch += TurnLightSwitch;
			AddComponent(lightSwitch);
			ChangeRoomComponent door = new ChangeRoomComponent("door", true);
			door.Follow += OnFollow;
			AddComponent(door);
			GlassComponent glass = new GlassComponent("glass", false);
			glass.Drink += DrinkGlass;
			glass.Take += TakeGlass;
			AddComponent(glass);
		}

		/// <summary>
		/// Loads Hall-Scene on opening door.
		/// </summary>
		private void OnFollow(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<Hall>();
			e.Handled = true;
		}
		/// <summary>
		/// Switches current description.
		/// </summary>
		private void TurnLightSwitch(object sender, ComponentEventArgs e)
		{
			SwitchComponent @switch = sender as SwitchComponent;
			@switch.Switched = true;
			@switch.Enabled = false;
			PostMessage(Resources.Room1_LightSwitch_TurnOn);
			FindComponent<GlassComponent>().Enabled = true;
			e.Handled = true;
		}
		/// <summary>
		/// Kills player.
		/// </summary>
		private void DrinkGlass(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<GameOverScene>(Resources.Room1_Died_DrankGlass);
			e.Handled = true;
		}
		/// <summary>
		/// Kills player.
		/// </summary>
		private void TakeGlass(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<GameOverScene>(Resources.Room1_Died_TookGlass);
			e.Handled = true;
		}
	}
}
