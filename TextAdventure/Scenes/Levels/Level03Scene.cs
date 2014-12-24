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
		public override string Description { get { return Resources.Room3_Description_AskForName; } }

		public Level03Scene()
		{
			SceneManager.GetComponentByType<Player>().Rename += PlayerRename;
		}

		protected override string OnNoActionFound()
		{
			return Resources.Room3_Fail;
		}

		private bool PlayerRename(Component component, string parameter)
		{
			Player player = SceneManager.GetComponentByType<Player>();

			if (!string.IsNullOrEmpty(player.Name))
			{
				return false;
			}

			player.SetName(parameter);
			return true;
		}
	}
}
