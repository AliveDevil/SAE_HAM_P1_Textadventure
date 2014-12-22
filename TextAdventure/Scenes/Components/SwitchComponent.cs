/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure.Scenes.Components
{
	public sealed class SwitchComponent : Component
	{
		private static readonly string[] activators = {
														  "use",
														  "turn",
														  "toggle",
														  "switch"
													  };

		public bool Switched { get; set; }

		public SwitchComponent(string name, bool switched, ComponentCallback callback)
			: base(name, activators, callback)
		{
			Switched = switched;
		}
	}
}
