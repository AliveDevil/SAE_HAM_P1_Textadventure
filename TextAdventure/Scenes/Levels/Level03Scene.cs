/*
 * Author: Jöran Malek
 */

using System;
using TextAdventure.Properties;

namespace TextAdventure.Scenes.Levels
{
	public sealed class Level03Scene : LevelScene
	{
		public override string Title { get { return Resources.Room3_Title; } }
		public override string Description { get { return Resources.Room3_Description; } }

		protected override string OnNoActionFound()
		{
			return Resources.Room3_Fail;
		}
	}
}
