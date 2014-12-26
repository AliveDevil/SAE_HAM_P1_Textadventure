/*
 * Author: Jöran Malek
 */

using System.Linq;
using TextAdventure.Properties;
using TextAdventure.Scenes.Components;

namespace TextAdventure.Scenes.Levels
{
	/// <summary>
	/// First scene the player will see.
	/// </summary>
	public sealed class Level01Scene : LevelScene
	{
		public override string Title { get { return Resources.Room1_Title; } }
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

		public Level01Scene()
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

		private void OnFollow(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<Level02Scene>();
			e.Handled = true;
		}
		private void TurnLightSwitch(object sender, ComponentEventArgs e)
		{
			SwitchComponent @switch = sender as SwitchComponent;
			@switch.Switched = true;
			@switch.Enabled = false;
			PostMessage(Resources.Room1_LightSwitch_TurnOn);
			FindComponent<GlassComponent>().Enabled = true;
			e.Handled = true;
		}
		private void DrinkGlass(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<GameOverScene>(Resources.Room1_Died_DrankGlass);
			e.Handled = true;
		}
		private void TakeGlass(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<GameOverScene>(Resources.Room1_Died_TookGlass);
			e.Handled = true;
		}
	}
}
