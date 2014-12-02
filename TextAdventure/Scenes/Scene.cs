/*
 * Author: Jöran Malek
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TextAdventure.Attributes;

namespace TextAdventure.Scenes
{
	public abstract class Scene
	{
		private Dictionary<string, Action> actions = new Dictionary<string, Action>();

		public virtual string Title { get { return "Scene"; } }
		public virtual bool DrawActions { get { return true; } }

		protected void RegisterAction(Action method)
		{
			string key = method.GetMethodInfo().GetCustomAttributes<ActionAttribute>().Select(attribute => attribute.Key).FirstOrDefault();
			if (!string.IsNullOrEmpty(key))
			{
				actions[key] = method;
			}
		}

		public virtual void Initialize() { }

		public void PerformAction(string key)
		{
			Action handle;
			if (actions.TryGetValue(key.ToLower(), out handle))
			{
				handle();
			}
		}
	}
}
