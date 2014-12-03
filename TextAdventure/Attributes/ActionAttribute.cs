/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure.Attributes
{
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class ActionAttribute : Attribute
	{
		private string key = "";
		private string description = "";

		public string Key { get { return key; } }
		public string Description { get { return description; } }

		public ActionAttribute(string key, string description)
		{
			this.key = key;
			this.description = description;
		}
	}
}
