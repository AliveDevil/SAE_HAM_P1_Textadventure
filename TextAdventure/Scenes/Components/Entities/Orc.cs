/*
 * Author: Jöran Malek
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Scenes.Components.Entities
{
	/// <summary>
	/// Some orc.
	/// </summary>
	public sealed class Orc : Entity
	{
		/// <summary>
		/// Private constructor. Nothing should ever create an instance from this.
		/// </summary>
		/// <param name="name">Components name.</param>
		/// <param name="damage">Entities damage.</param>
		/// <param name="health">Entities health.</param>
		private Orc(string name, int damage, int health)
			: base(name, true, damage, health)
		{
		}

		/// <summary>
		/// Creates an instance of orc with default values.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static Orc GreatOrc(string name)
		{
			return new Orc(name, 24, 134);
		}

		/// <summary>
		/// Tries to defend against attacker.
		/// </summary>
		/// <param name="attacker">Entity attacking current entity.</param>
		protected override void ReceiveDamage(Entity attacker)
		{
			base.ReceiveDamage(attacker);
			if (!IsDead() && attacker != null)
			{
				Attack(attacker);
			}
		}
	}
}
