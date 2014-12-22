using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Scenes.Components.Entities
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class Player : Entity
	{
		private const int baseHealth = 100;
		private const int baseDamage = 10;

		public Player(string name)
			: base(name, baseDamage, baseHealth, null, null)
		{
		}
	}
}
