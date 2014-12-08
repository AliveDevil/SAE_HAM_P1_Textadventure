/*
 * Author: Jöran Malek
 */

using System.Collections.Generic;

namespace TextAdventure.Scenes
{
	public struct Line
	{
		public string Key;
		public List<string> Lines;
		public int StartX;

		public Line(string key, int startX)
		{
			Key = key;
			StartX = startX;
			Lines = new List<string>();
		}
	}
}
