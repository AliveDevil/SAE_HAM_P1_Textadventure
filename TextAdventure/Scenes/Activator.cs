using System;

namespace TextAdventure.Scenes
{
	public struct Activator
	{
		private string key;

		private bool required;

		public string Key
		{
			get { return key; }
			set { key = value; }
		}

		public bool Required
		{
			get { return required; }
			set { required = value; }
		}

		public Activator(string key, bool required)
		{
			this.key = key;
			this.required = required;
		}

		public static bool operator !=(Activator left, Activator right)
		{
			if (left == null)
			{
				throw new ArgumentNullException("left");
			}
			if (right == null)
			{
				throw new ArgumentException("right");
			}
			return !left.Equals(right);
		}

		public static bool operator ==(Activator left, Activator right)
		{
			if (left == null)
			{
				throw new ArgumentNullException("left");
			}
			if (right == null)
			{
				throw new ArgumentException("right");
			}
			return left.Equals(right);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Activator))
			{
				return false;
			}
			Activator other = (Activator)obj;
			return Key == other.Key && Required == other.Required;
		}

		public override int GetHashCode()
		{
			return Key.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("{{{0} | {1}}}", Key, Required);
		}
	}
}
