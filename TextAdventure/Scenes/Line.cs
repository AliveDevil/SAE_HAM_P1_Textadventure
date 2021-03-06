﻿/*
 * Author: Jöran Malek
 */

using System.Collections.Generic;
using System.Linq;

namespace TextAdventure.Scenes
{
	/// <summary>
	/// Measuring unit for text in console.
	/// </summary>
	public struct Line
	{
		private string key;
		private List<string> lines;
		private int startX;

		/// <summary>
		/// Returns current key.
		/// </summary>
		public string Key { get { return key; } }

		/// <summary>
		/// A collection of every line.
		/// </summary>
		public IList<string> Lines { get { return lines; } }

		/// <summary>
		/// Returns X-axis offset.
		/// </summary>
		public int StartX { get { return startX; } }

		/// <summary>
		/// Constructor for setting some properties (except lines).
		/// </summary>
		/// <param name="key">The specified key.</param>
		/// <param name="startX">Some X-Offset.</param>
		public Line(string key, int startX)
		{
			this.key = key;
			this.startX = startX;
			this.lines = new List<string>();
		}

		/// <summary>
		/// Inequality operator.
		/// </summary>
		/// <param name="left">First line.</param>
		/// <param name="right">Second line.</param>
		/// <returns>Whether these are not equal.</returns>
		public static bool operator !=(Line left, Line right)
		{
			return left.key != right.key
				|| left.startX != right.startX
				|| !left.lines.Equals(right.lines);
		}

		/// <summary>
		/// Equality operator.
		/// </summary>
		/// <param name="left">First line.</param>
		/// <param name="right">Second line.</param>
		/// <returns>Whether these are equal.</returns>
		public static bool operator ==(Line left, Line right)
		{
			return left.key == right.key
				&& left.startX == right.startX
				&& left.lines.Equals(right.lines);
		}

		/// <summary>
		/// Does this equal with something else?
		/// </summary>
		/// <param name="obj">Object to be compared.</param>
		/// <returns>Whether these objects are equal.</returns>
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>
		/// Returns a (hopefully) unique HashCode.
		/// </summary>
		/// <returns>The HashCode.</returns>
		public override int GetHashCode()
		{
			return key.GetHashCode() ^ StartX ^ lines.Aggregate<string, int, int>(0, (previous, current) => previous ^ current.GetHashCode(), last => last);
		}
	}
}
