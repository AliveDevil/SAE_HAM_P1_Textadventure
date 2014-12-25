/*
 * Author: Jöran Malek
 */

using TextAdventure.Properties;
using TextAdventure.Scenes.Components;
using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Levels
{
	public sealed class Level03Scene : LevelScene
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
				return string.Format(Resources.Room3_Description_Quest, SceneManager.GetComponentByType<Player>().Name);
			}
		}

		public Level03Scene()
		{
			SceneManager.GetComponentByType<Player>().Rename += PlayerRename;
		}

		private bool PlayerRename(ComponentEventArgs e)
		{
			Player player = e.Component as Player;

			if (!string.IsNullOrEmpty(player.Name))
			{
				return false;
			}

			player.SetName(e.Parameter);
			return true;
		}
	}
}
