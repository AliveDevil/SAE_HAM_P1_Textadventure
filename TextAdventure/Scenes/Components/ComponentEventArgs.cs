/*
 * Author: Jöran Malek
 */

using System;
using System.Collections.Generic;

namespace TextAdventure.Scenes.Components
{
	/// <summary>
	/// Arguments that are passed by Component Events.
	/// </summary>
	public class ComponentEventArgs : EventArgs
	{
		/// <summary>
		/// Is this already handled? (If at all).
		/// </summary>
		public bool Handled { get; set; }

		/// <summary>
		/// Additional parameter.
		/// </summary>
		public IList<string> Parameter { get; private set; }

		/// <summary>
		/// </summary>
		/// <param id="parameter"></param>
		public ComponentEventArgs(IList<string> parameter)
		{
			Handled = false;
			Parameter = parameter ?? new List<string>();
		}
	}
}
