/*
 * Author: Jöran Malek
 */

using System;
using System.Collections.Generic;
using System.Globalization;

namespace TextAdventure.Scenes.Components
{
	/// <summary>
	/// <para>Base class for interacting components.</para>
	/// <para>Default usage in case-insensitive input is [Action] [Name].</para>
	/// <para>Example: open door</para>
	/// </summary>
	public abstract class Component
	{
		private Dictionary<string, EventHandler<ComponentEventArgs>> callbacks;

		/// <summary>
		/// 
		/// </summary>
		public bool Enabled { get; set; }
		/// <summary>
		/// Returns current components name.
		/// </summary>
		public string Name { get; protected set; }

		protected virtual bool CheckName { get { return true; } }

		protected Component(string name, bool enabled)
		{
			this.Name = name;
			this.Enabled = enabled;
			this.callbacks = new Dictionary<string, EventHandler<ComponentEventArgs>>();
		}

		/// <summary>
		/// Checks whether provided parameters match current component.
		/// </summary>
		/// <param name="action"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool CanInteract(string action, string name)
		{
			/*
			 * Return true if
			 *	Enabled
			 *	and either
			 *		not CheckName
			 *		or components name equals given name invariant culture ignore case.
			 *	and if callbacks contain given key.
			 */
			return Enabled &&
				(!CheckName || (!string.IsNullOrEmpty(name) && name.Equals(Name, StringComparison.OrdinalIgnoreCase))) &&
				(!string.IsNullOrEmpty(action) && callbacks.ContainsKey(action.ToUpper(CultureInfo.InvariantCulture)));
		}

		/// <summary>
		/// Executes current callback.
		/// </summary>
		public bool Interact(string action, string parameter)
		{
			EventHandler<ComponentEventArgs> callback;
			ComponentEventArgs args = new ComponentEventArgs(parameter);

			if (!string.IsNullOrEmpty(action) && callbacks.TryGetValue(action.ToUpperInvariant(), out callback))
			{
				callback(this, args);
			}

			return args.Handled;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="action"></param>
		/// <param name="callback"></param>
		/// <returns></returns>
		protected Component RegisterCallback(string action, EventHandler<ComponentEventArgs> callback)
		{
			if (!string.IsNullOrEmpty(action))
			{
				action = action.ToUpperInvariant();
				if (!callbacks.ContainsKey(action))
				{
					callbacks.Add(action, callback);
				}
			}
			return this;
		}
	}
}
