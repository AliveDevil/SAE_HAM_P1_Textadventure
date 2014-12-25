/*
 * Author: Jöran Malek
 */


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

		private bool OnDrink(ComponentEventArgs e)
		{
			if (Drink != null)
			{
				return Drink(e);
			}
			return false;
		}
		private bool OnTake(ComponentEventArgs e)
		{
			if (Take != null)
			{
				return Take(e);
			}
			return false;
		}
	}
}
