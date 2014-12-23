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
			RegisterCallback("take", Take);
			RegisterCallback("drink", Drink);
			RegisterCallback("use", Drink);
		}
	}
}
