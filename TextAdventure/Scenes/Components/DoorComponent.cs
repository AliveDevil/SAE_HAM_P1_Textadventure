using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Scenes.Components
{
	public sealed class DoorComponent : Component
	{
		private static readonly string[] activators = {
														  "use",
														  "open"
													  };

		public DoorComponent(string name, Action<Component> callback) : base(name, activators, callback) { }
	}
}
