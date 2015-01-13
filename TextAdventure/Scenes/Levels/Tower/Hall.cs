/*
 * Author: Jöran Malek
 */

using TextAdventure.Properties;
using TextAdventure.Scenes.Components;

namespace TextAdventure.Scenes.Levels.Tower
{
	/// <summary>
	/// The hall into the mages room.
	/// </summary>
	public sealed class Hall : LevelScene
	{
		/// <summary>
		/// Returns current rooms description.
		/// </summary>
		public override string Description { get { return Resources.Tower_Hall_Description; } }

		/// <summary>
		/// Halls title.
		/// </summary>
		public override string Title { get { return Resources.Tower_Hall_Title; } }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Hall()
		{
			ChangeRoomComponent door = new ChangeRoomComponent("door", true);
			door.Follow += OpenDoor;
			AddComponent(door);
		}

		/// <summary>
		/// Loads next scene.
		/// </summary>
		private void OpenDoor(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<MageRoom>();
			e.Handled = true;
		}
	}
}
