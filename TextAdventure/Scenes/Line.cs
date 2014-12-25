/*
 * Author: Jöran Malek
 */

using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TextAdventure.Scenes
{
	/// <summary>
	/// 
	/// </summary>
	public struct Line
	{
		private string key;
		private int startX;
		private List<string> lines;

		public string Key { get { return key; } }
		public int StartX { get { return startX; } }
		public IList<string> Lines { get { return lines; } }

		public Line(string key, int startX)
		{
			this.key = key;
			this.startX = startX;
			this.lines = new List<string>();
		}

		public override int GetHashCode()
		{
			return key.GetHashCode() ^ StartX ^ lines.Aggregate<string, int, int>(0, (previous, current) => previous ^ current.GetHashCode(), last => last);
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public static bool operator !=(Line left, Line right)
		{
			return left.key != right.key
				|| left.startX != right.startX
				|| !left.lines.Equals(right.lines);
		}

		public static bool operator ==(Line left, Line right)
		{
			return left.key == right.key
				&& left.startX == right.startX
				&& left.lines.Equals(right.lines);
		}
	}
}
