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

		public event ComponentCallback Attack;
		public event ComponentCallback Rename;

		public bool HasName { get { return !string.IsNullOrEmpty(Name); } }

		protected override bool CheckName { get { return false; } }

		public Player(bool enabled)
			: base(null, enabled, baseDamage, baseHealth)
		{
			RegisterCallback("attack", Attack);
			RegisterCallback("call", Rename);
			RegisterCallback("say", Rename);
		}

		public void SetName(string name)
		{
			Name = name;
		}
	}
}
