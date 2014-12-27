/*
 * Author: Jöran Malek
 */

using System;
using TextAdventure.Properties;

namespace TextAdventure.Attributes
{
	/// <summary>
	/// Provides registering of actions in scenes.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class ActionAttribute : Attribute
	{
		private string description = "";
		private string key = "";

		/// <summary>
		/// Returns the assigned description key and looks it up in current ResourceManager.
		/// </summary>
		public string Description { get { return string.IsNullOrEmpty(description) ? Resources.NotFound : Resources.ResourceManager.GetString(description); } }

		/// <summary>
		/// Returns the assigned key.
		/// </summary>
		public string Key { get { return key; } }

		/// <summary>
		/// Creates a new instance of ActionAttribute.
		/// </summary>
		/// <param name="key">Which keyword should invoke assigned method.</param>
		/// <param name="description">ResourceKey for description.</param>
		public ActionAttribute(string key, string description)
		{
			this.key = key;
			this.description = description;
		}
	}
}
