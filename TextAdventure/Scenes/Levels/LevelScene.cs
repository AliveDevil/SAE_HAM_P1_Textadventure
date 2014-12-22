﻿/*
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

		public IEnumerable<T> FindComponent<T>() where T : Component
		{
			return components.OfType<T>();
		}

		protected void AddComponent(Component component)
		{
			components.Add(component);
		}
		protected override void HandleInput(List<string> arguments)
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
				interactComponent.Interact();
			}
			else
			{
				Message(OnNoActionFound());
			}
		}

		protected abstract string OnNoActionFound();

		private Component InteractableComponents(Func<Component, bool> comparer)
		{
			return components.Where(item => item.Enabled && comparer(item)).FirstOrDefault();
		}
	}
}