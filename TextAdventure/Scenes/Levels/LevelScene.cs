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
		/// Removes a component from components.
		/// </summary>
		/// <param name="component">Component that should be removed.</param>
		public void RemoveComponent(Component component)
		{
			components.Remove(component);
		}
		/// <summary>
		/// Tries to find a component by name.
		/// </summary>
		/// <param name="name">String to search for.</param>
		/// <returns>Found component (or null if none found).</returns>
		public Component FindComponent(string name)
		{
			return components.Where(component => component.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
		}
		/// <summary>
		/// Tries to find a component by Type.
		/// </summary>
		/// <typeparam name="T">Component Generic Type.</typeparam>
		/// <returns>Found component (or null if none found).</returns>
		public T FindComponent<T>() where T : Component
		{
			return components.OfType<T>().FirstOrDefault();
		}

		/// <summary>
		/// Adds a component to 
		/// </summary>
		/// <param name="component">Component to be added.</param>
		protected void AddComponent(Component component)
		{
			components.Add(component);
		}
		/// <summary>
		/// Handle every other input.
		/// </summary>
		/// <param name="arguments">Additional parameters.</param>
		/// <returns>If an action has been found and run.</returns>
		protected override bool HandleInput(IList<string> arguments)
		{
			Component interactComponent = null;

			if (arguments != null)
			{
				if (arguments.Count > 1)
				{
					interactComponent = InteractableComponents(component =>
						component.CanInteract(arguments[0], arguments[1]));
				}
				else if (arguments.Count == 1)
				{
					interactComponent = InteractableComponents(component =>
						component.Name.Equals(arguments[0], StringComparison.OrdinalIgnoreCase));
				}
			}

			if (interactComponent != null)
			{
				return interactComponent.Interact(arguments[0], string.Join(" ", arguments.Skip(1)));
			}

			return false;
		}

		/// <summary>
		/// Returns every component that matches comparer.
		/// </summary>
		/// <param name="comparer">Comparer to evaluate components.</param>
		/// <returns>First found component or null.</returns>
		private Component InteractableComponents(Predicate<Component> comparer)
		{
			return components.Where(item => item.Enabled && comparer(item)).FirstOrDefault();
		}
	}
}
