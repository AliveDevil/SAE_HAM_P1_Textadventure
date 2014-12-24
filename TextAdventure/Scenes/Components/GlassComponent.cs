/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure.Scenes.Components
{
	/// <summary>
	/// Represents a glass.
	/// </summary>
	public sealed class GlassComponent : Component
	{
		public event ComponentCallback Take;
		public event ComponentCallback Drink;

		public GlassComponent(string name, bool enabled)
			: base(name, enabled)
		{
			RegisterCallback("take", OnTake);
			RegisterCallback("drink", OnDrink);
			RegisterCallback("use", OnDrink);
		}

		private bool OnDrink(Component component, string parameter)
		{
			if (Drink != null)
			{
				return Drink(component, parameter);
			}
			return false;
		}
		private bool OnTake(Component component, string parameter)
		{
			if (Take != null)
			{
				return Take(component, parameter);
			}
			return false;
		}
	}
}
