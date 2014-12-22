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
		public override bool DrawActions { get { return false; } }
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
			AddComponent(new SwitchComponent("switch", false, TurnLightSwitch));
			AddComponent(new DoorComponent("door", OpenDoor));
		}

		protected override string OnNoActionFound()
		{
			return Resources.Room1_Fail;
		}

		private bool OpenDoor(Component component)
		{
			SceneManager.LoadScene<Level02Scene>();
			return true;
		}
		private bool TurnLightSwitch(Component component)
		{
			SwitchComponent @switch = component as SwitchComponent;
			@switch.Switched = true;
			@switch.Enabled = false;
			Message(Resources.Room1_LightSwitch_TurnOn);
			AddComponent(new GlassComponent("glass", DrinkGlass));
			return true;
		}

		private bool DrinkGlass(Component component)
		{
			SceneManager.LoadScene<GameOverScene>(Resources.Room1_Died_Glass);
			return true;
		}
	}
}
