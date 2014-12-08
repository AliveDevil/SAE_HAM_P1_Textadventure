/*
 * Author: Jöran Malek
 */

using System;
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
		private Action callback;

		/// <summary>
		/// 
		/// </summary>
		public virtual string Name { get { return this.GetType().Name; } }
		/// <summary>
		/// 
		/// </summary>
		public virtual string Action { get { return "interact"; } }

		public Component(Action callback)
		{
			this.callback = callback;
		}

		/// <summary>
		/// Checks whether provided parameters match current component.
		/// </summary>
		/// <param name="action"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool CanInteract(string action, string name)
		{
			return action.Equals(Action, StringComparison.InvariantCultureIgnoreCase) && name.Equals(Name, StringComparison.InvariantCultureIgnoreCase);
		}

		/// <summary>
		/// Executes current callback.
		/// </summary>
		public void Interact()
		{
			callback();
		}
	}
}
