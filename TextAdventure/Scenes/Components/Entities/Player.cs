/*
 * Author: Jöran Malek
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextAdventure.Properties;
using TextAdventure.Scenes.Components.Items;

namespace TextAdventure.Scenes.Components.Entities
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class Player : Entity
	{
		const string HeaderFormat = "= {0} =";
		const string StatsFormat =
			"{0}\n" +
			"{1}: {3}\n" +
			"{2}: {4}\n";
		const string InventoryFormat = "{0}: {1}x\n";
		const int baseHealth = 100;
		const int baseDamage = 5;

		public event ComponentCallback Attack;
		public event ComponentCallback Rename;

		private List<Item> inventory;

		public bool HasName { get { return !string.IsNullOrEmpty(Name); } }

		protected override bool CheckName { get { return false; } }

		public Player(bool enabled)
			: base(null, enabled, baseDamage, baseHealth)
		{
			RegisterCallback("attack", OnAttack);
			RegisterCallback("call", OnRename);
			RegisterCallback("say", OnRename);
			RegisterCallback("inventory", ShowInventory);
			RegisterCallback("stats", ShowStats);
			RegisterCallback("use", UseInventory);
			inventory = new List<Item>();
		}

		public void AddItem(Item item)
		{
			inventory.Add(item);
		}
		public void SetName(string name)
		{
			Name = name;
		}

		private bool UseInventory(ComponentEventArgs e)
		{
			if (string.IsNullOrEmpty(e.Parameter))
			{
				return false;
			}
			var query = inventory.Where(entry => entry.Name.Equals(e.Parameter, StringComparison.InvariantCultureIgnoreCase));
			if (query.Any())
			{

			}
			return true;
		}
		private bool ShowStats(ComponentEventArgs e)
		{
			SceneManager.CurrentScene.Message(string.Format(StatsFormat, Resources.Generic_Stats, Resources.Generic_Health, Resources.Generic_Strength, Health, Strength));
			return true;
		}
		private bool ShowInventory(ComponentEventArgs e)
		{
			// anonymous types incoming.

			var groupedInventory = inventory.GroupBy(
				entry => entry.GetType(), // what should be grouped
				(key, enumerable) => new // what is the result after grouping
				{
					Key = key.Name, // get Types name.
					Count = enumerable.Count() // just return an enumerable with key and count.
				});
			StringBuilder builder = new StringBuilder();
			builder.AppendLine(string.Format(HeaderFormat, Resources.Generic_Inventory));
			foreach (var group in groupedInventory)
			{
				builder.AppendFormat(InventoryFormat, group.Key, group.Count);
			}
			SceneManager.CurrentScene.Message(builder.ToString());
			builder.Clear();
			return true;
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
