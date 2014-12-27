/*
 * Author: Jöran Malek
 */

using System;

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
		public string Parameter { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		public ComponentEventArgs(string parameter)
		{
			Handled = false;
			Parameter = parameter;
		}
	}
}
