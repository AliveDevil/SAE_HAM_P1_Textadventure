/*
 * Author: Jöran Malek
 */

using System;
using System.Linq;
using TextAdventure.Properties;
using TextAdventure.Scenes.Components;
using TextAdventure.Scenes.Components.Entities;

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
			door.Open += OpenDoor;
			AddComponent(door);
			GlassComponent glass = new GlassComponent("glass", false);
			glass.Drink += DrinkGlass;
			glass.Take += TakeGlass;
			AddComponent(glass);
		}

		private bool OpenDoor(ComponentEventArgs e)
		{
			SceneManager.LoadScene<Level02Scene>();
			return true;
		}
		private bool TurnLightSwitch(ComponentEventArgs e)
		{
			SwitchComponent @switch = e.Component as SwitchComponent;
			@switch.Switched = true;
			@switch.Enabled = false;
			Message(Resources.Room1_LightSwitch_TurnOn);
			FindComponent<GlassComponent>().Enabled = true;
			return true;
		}
		private bool DrinkGlass(ComponentEventArgs e)
		{
			SceneManager.LoadScene<GameOverScene>(Resources.Room1_Died_DrankGlass);
			return true;
		}
		private bool TakeGlass(ComponentEventArgs e)
		{
			SceneManager.LoadScene<GameOverScene>(Resources.Room1_Died_TookGlass);
			//SceneManager.GetComponentByType<Player>().
			return true;
		}
	}
}
