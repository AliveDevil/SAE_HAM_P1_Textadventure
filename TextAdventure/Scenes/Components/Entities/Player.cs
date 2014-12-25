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
			RegisterCallback("attack", OnAttack);
			RegisterCallback("call", OnRename);
			RegisterCallback("say", OnRename);
		}

		public void SetName(string name)
		{
			Name = name;
		}

		private bool OnAttack(ComponentEventArgs e)
		{
			if (Attack != null)
			{
				return Attack(e);
			}
			return false;
		}

		private bool OnRename(ComponentEventArgs e)
		{
			if (Rename != null)
			{
				return Rename(e);
			}
			return false;
		}
	}
}
