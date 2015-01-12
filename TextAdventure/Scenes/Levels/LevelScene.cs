/*
 * Author: Jöran Malek
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TextAdventure.Scenes.Components;

namespace TextAdventure.Scenes.Levels
{
	/// <summary>
	/// Base class for levels.
	/// </summary>
	public abstract class LevelScene : Scene
	{
		/// <summary>
		/// Some components that should be checked on input.
		/// </summary>
		private List<Component> components = new List<Component>() { };

		/// <summary>
		/// Don't draw actions because there are none.
		/// </summary>
		public override bool DrawActions { get { return false; } }

		/// <summary>
		/// Gives readonly access to current components.
		/// </summary>
		protected ReadOnlyCollection<Component> Components { get { return components.AsReadOnly(); } }

		/// <summary>
		/// Tries to find a component by id.
		/// </summary>
		/// <param id="id">String to search for.</param>
		/// <returns>Found component (or null if none found).</returns>
		public Component FindComponent(string name)
		{
			return components.Where(component => component.Id.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
		}

		/// <summary>
		/// Tries to find a component by Type.
		/// </summary>
		/// <typeparam id="T">Component Generic Type.</typeparam>
		/// <returns>Found component (or null if none found).</returns>
		public T FindComponent<T>() where T : Component
		{
			return components.OfType<T>().FirstOrDefault();
		}

		public T FindComponent<T>(string name) where T : Component
		{
			return components.OfType<T>()
				.Where(component => component.Id.Equals(name, StringComparison.OrdinalIgnoreCase))
				.FirstOrDefault();
		}

		/// <summary>
		/// Tries to find all components by id.
		/// </summary>
		/// <param id="id">String to search for.</param>
		/// <returns>Found component (or null if none found).</returns>
		public IEnumerable<Component> FindComponents(string name)
		{
			return components.Where(component => component.Id.Equals(name, StringComparison.OrdinalIgnoreCase));
		}

		/// <summary>
		/// Tries to find all components by Type.
		/// </summary>
		/// <typeparam id="T">Component Generic Type.</typeparam>
		/// <returns>Found component (or null if none found).</returns>
		public IEnumerable<T> FindComponents<T>() where T : Component
		{
			return components.OfType<T>();
		}

		/// <summary>
		/// Removes a component from components.
		/// </summary>
		/// <param id="component">Component that should be removed.</param>
		public void RemoveComponent(Component component)
		{
			component.Dispose();
			components.Remove(component);
		}

		/// <summary>
		/// Adds a component to
		/// </summary>
		/// <param id="component">Component to be added.</param>
		protected void AddComponent(Component component)
		{
			components.Add(component);
		}

		/// <summary>
		/// Handle every other input.
		/// </summary>
		/// <param id="arguments">Additional parameters.</param>
		/// <returns>If an action has been found and run.</returns>
		protected override bool HandleInput(IList<string> arguments)
		{
			Component interactComponent = null;

			if (arguments != null)
			{
				if (arguments.Count > 1)
				{
					interactComponent = InteractableComponents(component =>
						component.CanInteract(arguments.ToArray())).FirstOrDefault();
				}
				else if (arguments.Count == 1)
				{
					interactComponent = InteractableComponents(component =>
						component.Id.Equals(arguments[0], StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
				}
			}

			if (interactComponent != null)
			{
				return interactComponent.Interact(arguments.ToArray());
			}

			return false;
		}

		/// <summary>
		/// Returns every component that matches comparer.
		/// </summary>
		/// <param id="comparer">Comparer to evaluate components.</param>
		/// <returns>First found component or null.</returns>
		private IEnumerable<Component> InteractableComponents(Predicate<Component> comparer)
		{
			return components.Where(item => item.Enabled && comparer(item));
		}

		//protected Component FindComponentByActivator(params string[] activators)
		//{
		//	InteractableComponents(component => component.CanInteract(activators));
		//	List<Component> componentList = new List<Component>();
		//	for (int i = 0; i < activators.Length; i++)
		//	{
		//	}
		//}
	}
}
