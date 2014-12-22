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
		private static readonly string[] activators = {
														  "use",
														  "take",
														  "drink"
													  };

		public GlassComponent(string name, ComponentCallback callback) : base(name, activators, callback) { }
	}
}
