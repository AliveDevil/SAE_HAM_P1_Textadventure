/*
 * Author: Jöran Malek
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TextAdventure.Properties;
using TextAdventure.Scenes.Components.Items;

namespace TextAdventure.Scenes.Components.Entities
{
	/// <summary>
	/// </summary>
	public sealed class Player : Entity
	{
		public event EventHandler<ComponentEventArgs> Attack;

		public event EventHandler<ComponentEventArgs> Rename;

		private const int baseDamage = 5;
		private const int baseHealth = 100;
		private const string HeaderFormat = "= {0} =";
		private const string InventoryFormat = "{0}: {1}x\n";

		private const string StatsFormat =
			"{0}\n" +
			"{1}: {3}\n" +
			"{2}: {4}\n";

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

		protected override void ReceiveDamage(Entity attacker)
		{
			base.ReceiveDamage(attacker);
			if (attacker != null && IsDead())
			{
				SceneManager.LoadScene<GameOverScene>(string.Format(CultureInfo.CurrentCulture, Resources.Player_Died, attacker.Name, SceneManager.CurrentScene.Title));
			}
		}

		private void OnAttack(object sender, ComponentEventArgs e)
		{
			if (Attack != null)
			{
				Attack(sender, e);
			}
		}

		private void OnRename(object sender, ComponentEventArgs e)
		{
			if (Rename != null)
			{
				Rename(sender, e);
			}
		}

		private void ShowInventory(object sender, ComponentEventArgs e)
		{
			// anonymous types incoming.

			var groupedInventory = inventory.GroupBy(		// group the inventory.
				keySelector: entry => entry.GetType(),		// what should be grouped
				resultSelector: (key, enumerable) => new	// what is the result after grouping
				{
					Key = key.Name,							// get Types name.
					Count = enumerable.Count()				// just return an enumerable with key and count.
				});

			// Build generic output.
			StringBuilder builder = new StringBuilder();
			builder.AppendLine(string.Format(CultureInfo.CurrentCulture, HeaderFormat, Resources.Generic_Inventory));
			// Output every line in 
			foreach (var group in groupedInventory)
			{
				builder.AppendFormat(CultureInfo.CurrentCulture, InventoryFormat, group.Key, group.Count);
			}
			SceneManager.CurrentScene.PostMessage(builder.ToString());
			builder.Clear();
			e.Handled = true;
		}

		private void ShowStats(object sender, ComponentEventArgs e)
		{
			SceneManager.CurrentScene.PostMessage(
				CultureInfo.CurrentCulture,
				Resources.Generic_StatsFormat,
				Resources.Generic_Stats,
				Resources.Generic_Health,
				Health,
				Resources.Generic_Strength,
				Strength
			);
			e.Handled = true;
		}

		private void UseInventory(object sender, ComponentEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.Parameter))
			{
				var query = inventory.Where(entry => entry.Name.Equals(e.Parameter, StringComparison.OrdinalIgnoreCase));
				if (query.Any())
				{
					Item first = query.First();
					inventory.Remove(first);
					UsePotion(first as Potion);
					e.Handled = true;
				}
			}
		}

		private void UsePotion(Potion potion)
		{
			if (potion != null)
			{
				potion.Apply(this);
			}
		}
	}
}
