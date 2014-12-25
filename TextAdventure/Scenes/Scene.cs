/*
 * Author: Jöran Malek
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using TextAdventure.Attributes;

namespace TextAdventure.Scenes
{
	public delegate bool ExecuteAction();

	/// <summary>
	/// Base class for scenes the player might see.
	/// </summary>
	public abstract class Scene
	{
		private List<string> messages;
		private Dictionary<string, ExecuteAction> actions;
		private IReadOnlyList<string> readonlyArguments;
		private IReadOnlyCollection<string> readonlyMessages;
		private IReadOnlyDictionary<string, ExecuteAction> readonlyActions;

		public IReadOnlyCollection<string> Messages
		{
			get { return readonlyMessages; }
		}
		public IReadOnlyDictionary<string, ExecuteAction> Actions
		{
			get { return readonlyActions; }
		}
		public virtual string Title
		{
			get { return "Scene"; }
		}
		public virtual string Description
		{
			get { return string.Empty; }
		}
		public virtual bool DrawActions
		{
			get { return true; }
		}

		protected IReadOnlyList<string> Arguments
		{
			get { return readonlyArguments; }
		}

		protected Scene(params string[] arguments)
		{
			actions = new Dictionary<string, ExecuteAction>();
			messages = new List<string>();
			readonlyActions = new ReadOnlyDictionary<string, ExecuteAction>(actions);
			readonlyMessages = new ReadOnlyCollection<string>(messages);
			readonlyArguments = new ReadOnlyCollection<string>(arguments);
		}

		public void AddMessage(string message)
		{
			messages.Add(message);
		}
		public bool PerformAction(IList<string> arguments)
		{
			if (arguments != null && arguments.Count > 0)
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
		public void ClearMessages()
		{
			string lastMessage = messages.Last();
			messages.Clear();
			AddMessage(lastMessage);
		}
		public virtual void Initialize() { }

		protected void RegisterAction(ExecuteAction method)
		{
			if (method != null)
			{
				string key = method.GetMethodInfo().GetCustomAttributes<ActionAttribute>().Select(attribute => attribute.Key).FirstOrDefault().ToUpperInvariant();
				if (!string.IsNullOrEmpty(key))
				{
					actions[key] = method;
				}
			}
		}
		protected virtual bool HandleInput(IList<string> arguments)
		{
			return false;
		}
	}
}
