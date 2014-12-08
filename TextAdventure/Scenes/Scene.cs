﻿/*
 * Author: Jöran Malek
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using TextAdventure.Attributes;

namespace TextAdventure.Scenes
{
	public abstract class Scene
	{
		private Dictionary<string, Action> actions;

		public ReadOnlyDictionary<string, Action> Actions;

		public virtual string Title { get { return "Scene"; } }
		public virtual string Description { get { return string.Empty; } }
		public virtual bool DrawActions { get { return true; } }

		public Scene()
		{
			actions = new Dictionary<string, Action>();
			Actions = new ReadOnlyDictionary<string, Action>(actions);
		}

		protected void RegisterAction(Action method)
		{
			string key = method.GetMethodInfo().GetCustomAttributes<ActionAttribute>().Select(attribute => attribute.Key).FirstOrDefault().ToLower();
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
