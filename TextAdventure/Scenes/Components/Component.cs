/*
 * Author: Jöran Malek
 */

using System;
using System.Linq;
using TextAdventure.Properties;

namespace TextAdventure.Scenes.Components
{
	/// <summary>
	/// <para>Base class for interacting components.</para>
	/// <para>Default usage in case-insensitive input is [Action] [Name].</para>
	/// <para>Example: open door</para>
	/// </summary>
	public abstract class Component
	{
		private Action<Component> callback;
		private string name;
		private string[] activateOn;

		/// <summary>
		/// 
		/// </summary>
		public bool Enabled { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get { return name; } }

		public Component(string name, string[] activateOn, Action<Component> callback)
		{
			this.name = name;
			this.callback = callback;
			this.activateOn = activateOn;
			this.Enabled = true;
		}

		/// <summary>
		/// Checks whether provided parameters match current component.
		/// </summary>
		/// <param name="action"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool CanInteract(string action, string name)
		{
			return name.Equals(Name, StringComparison.InvariantCultureIgnoreCase) &&
				activateOn.Any(item => item.Equals(action, StringComparison.InvariantCultureIgnoreCase));
		}

		/// <summary>
		/// Executes current callback.
		/// </summary>
		public void Interact()
		{
			callback(this);
		}
	}
}
