/*
 * Author: Jöran Malek
 */

using System;
using System.Collections.Generic;
using System.Linq;
using TextAdventure.Properties;

namespace TextAdventure.Scenes.Components
{
	/// <summary>
	/// Replacement for Func&lt;Component, bool&gt;.
	/// </summary>
	/// <param name="component"></param>
	/// <returns></returns>
	public delegate bool ComponentCallback(Component component);

	/// <summary>
	/// <para>Base class for interacting components.</para>
	/// <para>Default usage in case-insensitive input is [Action] [Name].</para>
	/// <para>Example: open door</para>
	/// </summary>
	public abstract class Component
	{
		private Dictionary<string, ComponentCallback> callbacks;
		private ComponentCallback callback;
		private string name;
		private string[] activateOn;

		/// <summary>
		/// 
		/// </summary>
		public bool Enabled { get; set; }
		/// <summary>
		/// Returns current components name.
		/// </summary>
		public string Name { get { return name; } }

		public Component(string name, bool enabled)
		{
			this.name = name;
			this.Enabled = enabled;
			this.callbacks = new Dictionary<string, ComponentCallback>();
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
				callbacks.ContainsKey(action.ToLower());
		}

		/// <summary>
		/// Executes current callback.
		/// </summary>
		public bool Interact(string action)
		{
			ComponentCallback callback;
			if (callbacks.TryGetValue(action.ToLower(), out callback))
			{
				return callback(this);
			}
			return false;
		}

		protected Component RegisterCallback(string action, ComponentCallback callback)
		{
			action = action.ToLower();
			if (!callbacks.ContainsKey(action))
			{
				callbacks.Add(action, callback);
			}
			return this;
		}
	}
}
