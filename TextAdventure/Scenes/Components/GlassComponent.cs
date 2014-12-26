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
		public event EventHandler<ComponentEventArgs> Take;
		public event EventHandler<ComponentEventArgs> Drink;

		public GlassComponent(string name, bool enabled)
			: base(name, enabled)
		{
			RegisterCallback("take", OnTake);
			RegisterCallback("drink", OnDrink);
			RegisterCallback("use", OnDrink);
		}

		private void OnDrink(object sender, ComponentEventArgs e)
		{
			if (Drink != null)
			{
				Drink(sender, e);
			}
		}
		private void OnTake(object sender, ComponentEventArgs e)
		{
			if (Take != null)
			{
				Take(sender, e);
			}
		}
	}
}
