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
	}
}
