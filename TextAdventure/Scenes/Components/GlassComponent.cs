/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure.Scenes.Components
{
	public sealed class GlassComponent : Component
	{
		private static readonly string[] activators = {
														  "use",
														  "take",
														  "drink"
													  };

		public GlassComponent(string name, Action<Component> callback) : base(name, activators, callback) { }
	}
}
