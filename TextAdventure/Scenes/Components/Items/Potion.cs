using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Scenes.Components.Items
{
	public abstract class Potion : Item
	{
		public Potion(string name) : base(name) { }

		public abstract void Apply(Component caller);
	}
}
