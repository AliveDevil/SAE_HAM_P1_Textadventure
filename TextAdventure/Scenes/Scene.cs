/*
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
	/// <summary>
	/// Just a simple replacement for System.Func&lt;bool&gt;.
	/// </summary>
	/// <returns>If this action has been successful.</returns>
	public delegate bool ExecuteAction();

	/// <summary>
	/// Base class for scenes the player might see.
	/// </summary>
	public abstract class Scene : IDisposable
	{
		/// <summary>
		/// A store for registered actions. Like "back" or somthing like that.
		/// </summary>
		private Dictionary<string, ExecuteAction> actions;

		/// <summary>
		/// A list holding every message posted over time.
		/// </summary>
		private List<string> messages;

		/// <summary>
		/// Readonly store for actions.
		/// </summary>
		private IReadOnlyDictionary<string, ExecuteAction> readonlyActions;

		/// <summary>
		/// A readonly store for constructor arguments.
		/// </summary>
		private IReadOnlyList<string> readonlyArguments;

		/// <summary>
		/// Readonly store for messages.
		/// </summary>
		private IReadOnlyCollection<string> readonlyMessages;

		/// <summary>
		/// Returns current actions.
		/// </summary>
		public IReadOnlyDictionary<string, ExecuteAction> Actions
		{
			get { return readonlyActions; }
		}

		/// <summary>
		/// Returns current scenes description.
		/// </summary>
		public virtual string Description
		{
			get { return string.Empty; }
		}

		/// <summary>
		/// Whether this scene should draw actions or not.
		/// </summary>
		public virtual bool DrawActions
		{
			get { return true; }
		}

		/// <summary>
		/// Returns current messages.
		/// </summary>
		public IReadOnlyCollection<string> Messages
		{
			get { return readonlyMessages; }
		}

		/// <summary>
		/// Returns current scenes title.
		/// </summary>
		public virtual string Title
		{
			get { return "Scene"; }
		}

		/// <summary>
		/// Return current scenes arguments.
		/// </summary>
		protected IReadOnlyList<string> Arguments
		{
			get { return readonlyArguments; }
		}

		/// <summary>
		/// Abstract constructor.
		/// </summary>
		/// <param id="arguments"></param>
		protected Scene(params string[] arguments)
		{
			actions = new Dictionary<string, ExecuteAction>();
			messages = new List<string>();
			readonlyActions = new ReadOnlyDictionary<string, ExecuteAction>(actions);
			readonlyMessages = new ReadOnlyCollection<string>(messages);
			readonlyArguments = new ReadOnlyCollection<string>(arguments);
		}

		/// <summary>
		/// Just clear every message except last.
		/// </summary>
		public void ClearMessages()
		{
			string lastMessage = messages.Last();
			messages.Clear();
			PostMessage(lastMessage);
		}

		/// <summary>
		/// Execute an action which has arguments first item as key. Otherwise check in scene itself.
		/// </summary>
		/// <param id="arguments"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Adds a message to current messages list.
		/// </summary>
		/// <param id="message">Some message.</param>
		public void PostMessage(string message)
		{
			messages.Add(message);
		}

		/// <summary>
		/// Adds an formatted message to message list.
		/// </summary>
		/// <param id="formatProvider">Some format provider.</param>
		/// <param id="format">Simple format for message.</param>
		/// <param id="args">Arguments that should be replaced in format.</param>
		public void PostMessage(IFormatProvider formatProvider, string format, params object[] args)
		{
			PostMessage(string.Format(formatProvider, format, args));
		}

		/// <summary>
		/// Handles not recognized input.
		/// </summary>
		/// <param id="arguments">Some arguments.</param>
		/// <returns>Whether this has been successful.</returns>
		protected virtual bool HandleInput(IList<string> arguments)
		{
			return false;
		}

		/// <summary>
		/// Registers provided method.
		/// </summary>
		/// <param id="method">Some executeaction.</param>
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

		public virtual void Dispose()
		{
		}
	}
}
