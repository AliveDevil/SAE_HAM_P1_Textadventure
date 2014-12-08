/*
 * Author: Jöran Malek
 */

using System.Collections.Generic;

namespace TextAdventure.Scenes
{
	/// <summary>
	/// 
	/// </summary>
	public struct Line
	{
		public string Key;
		public int StartX;
		public List<string> Lines;

		public Line(string key, int startX)
		{
			Key = key;
			StartX = startX;
			Lines = new List<string>();
		}
	}
}
