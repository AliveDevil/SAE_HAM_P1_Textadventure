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
	public abstract class LevelScene : Scene
	{
		private List<Component> components = new List<Component>() { };

		public override bool DrawActions { get { return false; } }

		protected ReadOnlyCollection<Component> Components { get { return components.AsReadOnly(); } }

		public T FindComponent<T>() where T : Component
		{
			return components.OfType<T>().FirstOrDefault();
		}

		protected void AddComponent(Component component)
		{
			components.Add(component);
		}
		protected override bool HandleInput(List<string> arguments)
		{
			Component interactComponent = null;

			if (arguments.Count > 1)
			{
				interactComponent = InteractableComponents(component =>
					component.CanInteract(arguments[0], arguments[1]));
			}
			else if (arguments.Count == 1)
			{
				interactComponent = InteractableComponents(component =>
					component.Name.Equals(arguments[0], StringComparison.InvariantCultureIgnoreCase));
			}

			if (interactComponent != null)
			{
				return interactComponent.Interact(arguments[0], string.Join(" ", arguments.Skip(1)));
			}
			
			return false;
		}

		private Component InteractableComponents(Predicate<Component> comparer)
		{
			return components.Where(item => item.Enabled && comparer(item)).FirstOrDefault();
		}
	}
}
