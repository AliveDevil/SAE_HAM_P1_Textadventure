/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure.Scenes.Components
{
	/// <summary>
	/// Represents a door.
	/// </summary>
	public sealed class DoorComponent : Component
	{
		private static readonly string[] activators = {
														  "use",
														  "open"
													  };

		public DoorComponent(string name, ComponentCallback callback) : base(name, activators, callback) { }
	}
}
