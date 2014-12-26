/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure.Scenes.Components
{
	/// <summary>
	/// 
	/// </summary>
	public class ComponentEventArgs : EventArgs
	{
		/// <summary>
		/// 
		/// </summary>
		public bool Handled { get; set; }
		/// <summary>
		/// 
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
