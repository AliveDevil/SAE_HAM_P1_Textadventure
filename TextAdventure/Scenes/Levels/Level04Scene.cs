/*
 * Author: Jöran Malek
 */

using TextAdventure.Scenes.Components;
using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Levels
{
	public sealed class Level04Scene : LevelScene
	{
		public override string Title { get { return base.Title; } }
		public override string Description { get { return base.Description; } }

		public Level04Scene()
		{
			SceneManager.GetComponentByType<Player>().Attack += PlayerAttack;
		}

		private bool PlayerAttack(ComponentEventArgs e)
		{
			Entity component = FindComponent(e.Parameter) as Entity;

			//Component component = FindComponent(e.Parameter);
			return true;
		}
	}
}
