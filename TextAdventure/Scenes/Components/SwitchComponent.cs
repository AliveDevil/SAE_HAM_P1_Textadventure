using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Scenes.Components
{
	public sealed class SwitchComponent : Component
	{
		private static readonly string[] activators = {
														  "use",
														  "turn",
														  "toggle",
														  "switch"
													  };

		public bool Switched { get; set; }

		public SwitchComponent(string name, bool switched, Action<Component> callback)
			: base(name, activators, callback)
		{
			Switched = switched;
		}
	}
}
