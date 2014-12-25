using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Scenes.Components
{
	public class ComponentEventArgs : EventArgs
	{
		public Component Component { get; private set; }
		public string Parameter { get; private set; }

		public ComponentEventArgs(Component component, string parameter)
		{
			Component = component;
			Parameter = parameter;
		}
	}
}
