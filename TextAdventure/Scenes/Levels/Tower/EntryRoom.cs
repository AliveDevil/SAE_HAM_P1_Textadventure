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
		/// Shows some description to user.
		/// </summary>
		public override string Description
		{
			get
			{
				if (Components.OfType<SwitchComponent>().First().Switched)
				{
					return Resources.Tower_Entry_Description_Light;
				}
				return Resources.Tower_Entry_Description_Dark;
			}
		}

		/// <summary>
		/// Entry rooms title. Used from Resources. See Tower_Entry_Title.
		/// </summary>
		public override string Title { get { return Resources.Tower_Entry_Title; } }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public EntryRoom()
		{
			SwitchComponent lightSwitch = new SwitchComponent("switch", true, false, new Activator("switch", true));
			lightSwitch.Switch += TurnLightSwitch;
			AddComponent(lightSwitch);
			ChangeRoomComponent door = new ChangeRoomComponent("door", true, new Activator("door", true));
			door.Follow += OnFollow;
			AddComponent(door);
			GlassComponent glass = new GlassComponent("glass", false, new Activator("glass", true), new Activator("red", false));
			glass.Drink += DrinkGlass;
			glass.Take += TakeGlass;
			AddComponent(glass);
		}

		/// <summary>
		/// Kills player.
		/// </summary>
		private void DrinkGlass(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<GameOverScene>(Resources.Tower_Entry_Died_DrankGlass);
			e.Handled = true;
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
		/// Kills player.
		/// </summary>
		private void TakeGlass(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<GameOverScene>(Resources.Tower_Entry_Died_TookGlass);
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
			PostMessage(Resources.Tower_Entry_LightSwitch_TurnOn);
			FindComponent<GlassComponent>().Enabled = true;
			e.Handled = true;
		}
	}
}
