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
	public delegate bool ExecuteAction();

	/// <summary>
	/// Base class for scenes the player might see.
	/// </summary>
	public abstract class Scene
	{
		public ReadOnlyDictionary<string, ExecuteAction> Actions;
		public ReadOnlyCollection<string> Messages;

		protected readonly ReadOnlyCollection<string> Arguments;

		private Dictionary<string, ExecuteAction> actions;
		private List<string> messages;

		public virtual string Title { get { return "Scene"; } }
		public virtual string Description { get { return string.Empty; } }
		public virtual bool DrawActions { get { return true; } }

		public Scene(params string[] arguments)
		{
			actions = new Dictionary<string, ExecuteAction>();
			messages = new List<string>();
			Actions = new ReadOnlyDictionary<string, ExecuteAction>(actions);
			Messages = new ReadOnlyCollection<string>(messages);
			Arguments = new ReadOnlyCollection<string>(arguments);
		}

		protected void Message(string message)
		{
			messages.Add(message);
		}
		protected void RegisterAction(ExecuteAction method)
		{
			string key = method.GetMethodInfo().GetCustomAttributes<ActionAttribute>().Select(attribute => attribute.Key).FirstOrDefault().ToLower();
			if (!string.IsNullOrEmpty(key))
			{
				actions[key] = method;
			}
		}
		protected virtual bool HandleInput(List<string> arguments)
		{
			Message(string.Format(Resources.InvalidAction, string.Join(" ", arguments)));
			return true;
		}

		public virtual void Initialize() { }

		public bool PerformAction(List<string> arguments)
		{
			if (arguments.Count > 0)
			{
				ExecuteAction executeAction;
				if (actions.TryGetValue(arguments[0], out executeAction))
				{
					return executeAction();
				}
				else
				{
					return HandleInput(arguments);
				}
			}
			return false;
		}
	}
}
