/*
 * Author: Jöran Malek
 */

using System.Collections.Generic;
using TextAdventure.Scenes.Components;
namespace TextAdventure.Scenes.Levels
{
	public sealed class Level01Scene : Scene
	{
		private List<Component> components = new List<Component>() { };

		public override string Title { get { return "Entry Room"; } }
		public override bool DrawActions { get { return false; } }
		public override string Description
		{
			get
			{
				return base.Description;
			}
		}
	}
}
