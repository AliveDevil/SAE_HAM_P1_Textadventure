/*
 * Author: Jöran Malek
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TextAdventure.Scenes.Components
{
	/// <summary>
	/// <para>Base class for interacting components.</para>
	/// <para>Default usage in case-insensitive input is [Action] [Id].</para>
	/// <para>Example: open door</para>
	/// </summary>
	public abstract class Component : IDisposable
	{
		private delegate void EnumerableArrayAction<TRefElement>(ref TRefElement refElement);
		private delegate void EnumerableArrayAction<TElement, TRefElement>(TElement element, ref TRefElement refElement);

		private Activator[] activators;
		private Dictionary<string, EventHandler<ComponentEventArgs>> callbacks;
		private string id;
		private IEnumerable<Activator> optionalActivators;
		private IEnumerable<Activator> requiredActivators;

		/// <summary>
		/// Is this component enabled?
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		/// Returns current components id.
		/// </summary>
		public string Id
		{
			get { return id; }
			protected set { id = (value ?? "").ToUpperInvariant(); }
		}

		/// <summary>
		/// Should id be checked?
		/// </summary>
		protected virtual bool CheckActivators { get { return true; } }

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param id="id">Lookup id.</param>
		/// <param id="enabled">Is this component enabled.</param>
		protected Component(string id, bool enabled, params Activator[] activators)
		{
			this.Id = id;
			this.Enabled = enabled;
			this.activators = activators ?? new Activator[] { };
			this.callbacks = new Dictionary<string, EventHandler<ComponentEventArgs>>();
			this.requiredActivators = this.activators.Where(activator => activator.Required);
			this.optionalActivators = this.activators.Where(activator => !activator.Required);
		}

		/// <summary>
		/// Checks whether provided parameters match current component.
		/// </summary>
		/// <param id="action"></param>
		/// <param id="id"></param>
		/// <returns></returns>
		public bool CanInteract(string[] activators)
		{
			if (!Enabled)
			{
				return false;
			}
			if (activators == null || activators.Length == 0)
			{
				return false;
			}

			bool actionFound = false;
			EnumerableAction(activators, (ref string element) =>
			{
				if (callbacks.ContainsKey(element))
				{
					actionFound = true;
					element = null;
				}
			});

			bool requiredFound = false;
			bool optionalRequired = false;
			bool optionalFound = true;

			if (CheckActivators)
			{
				EnumerableAction(requiredActivators, activators, (Activator activator, ref string search) =>
				{
					bool found = string.Equals(activator.Key, search, StringComparison.OrdinalIgnoreCase);
					if (found)
					{
						requiredFound |= string.Equals(activator.Key, search, StringComparison.OrdinalIgnoreCase);
						search = null;
					}
				});
				EnumerableAction(optionalActivators, activators, (Activator activator, ref string search) =>
				{
					optionalRequired = true;
					optionalFound &= string.Equals(activator.Key, search, StringComparison.OrdinalIgnoreCase);
					search = null;
				});
			}
			else
			{
				requiredFound = true;
			}

			return actionFound && requiredFound && (!optionalRequired || optionalFound);
		}

		/// <summary>
		/// Executes current callback.
		/// </summary>
		public bool Interact(string[] parameter)
		{
			IEnumerable<EventHandler<ComponentEventArgs>> callback = callbacks
				.Where(element => parameter.Contains(element.Key))
				.Select(element => element.Value);
			ComponentEventArgs args = new ComponentEventArgs(parameter);

			if (callback.Any())
			{
				callback.First()(this, args);
			}

			return args.Handled;
		}

		/// <summary>
		/// Adds callback to callbacks.
		/// </summary>
		/// <param id="action">Action key.</param>
		/// <param id="callback">Function called.</param>
		/// <returns>Current component.</returns>
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

		private static void EnumerableAction<TEnumerable, TArray>(IEnumerable<TEnumerable> source, TArray[] elements, EnumerableArrayAction<TEnumerable, TArray> action) where TArray : class
		{
			foreach (var item in source)
			{
				for (int i = 0; i < elements.Length; i++)
				{
					if (elements[i] != null)
					{
						action(item, ref elements[i]);
					}
				}
			}
		}

		private static void EnumerableAction<TArray>(TArray[] elements, EnumerableArrayAction<TArray> action) where TArray : class
		{
			for (int i = 0; i < elements.Length; i++)
			{
				if (elements[i] != null)
				{
					action(ref elements[i]);
				}
			}
		}

		public virtual void Dispose() { }
	}
}
