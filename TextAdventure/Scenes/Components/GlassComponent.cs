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
		/// <summary>
		/// Raised on drink or use.
		/// </summary>
		public event EventHandler<ComponentEventArgs> Drink;

		/// <summary>
		/// Raised on take.
		/// </summary>
		public event EventHandler<ComponentEventArgs> Take;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="name">Current components name.</param>
		/// <param name="enabled">Is this component enabled?</param>
		public GlassComponent(string name, bool enabled)
			: base(name, enabled)
		{
			RegisterCallback("take", OnTake);
			RegisterCallback("drink", OnDrink);
			RegisterCallback("use", OnDrink);
		}

		/// <summary>
		/// Used OnDrink.
		/// </summary>
		private void OnDrink(object sender, ComponentEventArgs e)
		{
			if (Drink != null)
			{
				Drink(sender, e);
			}
		}

		/// <summary>
		/// Used on take.
		/// </summary>
		private void OnTake(object sender, ComponentEventArgs e)
		{
			if (Take != null)
			{
				Take(sender, e);
			}
		}
	}
}
