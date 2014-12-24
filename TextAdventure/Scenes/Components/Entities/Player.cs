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

		public event ComponentCallback Rename;
		public event ComponentCallback Attack;

		protected override bool CheckName { get { return false; } }

		public Player(bool enabled)
			: base(null, enabled, baseDamage, baseHealth)
		{
			RegisterCallback("call", Rename);
			RegisterCallback("say", Rename);
			RegisterCallback("attack", Attack);
		}

		public void ComponentToInventory(Component component)
		{

		}

		public void SetName(string name)
		{
			Name = name;
		}
	}
}
