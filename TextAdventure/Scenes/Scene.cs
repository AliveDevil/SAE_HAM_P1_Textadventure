/*
 * Author: Jöran Malek
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using TextAdventure.Attributes;
using TextAdventure.Properties;

namespace TextAdventure.Scenes
{
	/// <summary>
	/// Base class for scenes the player might see.
	/// </summary>
	public abstract class Scene
	{
		private Dictionary<string, Action> actions;
		private List<string> messages;

		protected readonly ReadOnlyCollection<string> Arguments;

		public ReadOnlyDictionary<string, Action> Actions;
		public ReadOnlyCollection<string> Messages;

		public virtual string Title { get { return "Scene"; } }
		public virtual string Description { get { return string.Empty; } }
		public virtual bool DrawActions { get { return true; } }

		public Scene(params string[] arguments)
		{
			actions = new Dictionary<string, Action>();
			messages = new List<string>();
			Actions = new ReadOnlyDictionary<string, Action>(actions);
			Messages = new ReadOnlyCollection<string>(messages);
			Arguments = new ReadOnlyCollection<string>(arguments);
		}

		protected void Message(string message)
		{
			messages.Add(message);
		}
		protected void RegisterAction(Action method)
		{
			string key = method.GetMethodInfo().GetCustomAttributes<ActionAttribute>().Select(attribute => attribute.Key).FirstOrDefault().ToLower();
			if (!string.IsNullOrEmpty(key))
			{
				actions[key] = method;
			}
		}
		protected virtual void HandleInput(List<string> arguments)
		{
			Message(string.Format(Resources.InvalidAction, string.Join(" ", arguments)));
		}

		public virtual void Initialize() { }

		public void PerformAction(List<string> arguments)
		{
			if (arguments.Count > 0)
			{
				Action executeAction;
				if (actions.TryGetValue(arguments[0], out executeAction))
				{
					executeAction();
				}
				else
				{
					HandleInput(arguments);
				}
			}
		}
	}
}
