using System.Collections.Generic;

namespace TextAdventure.Scenes
{
	public abstract class Scene
	{
		protected delegate void HandleAction();
		private Dictionary<string, HandleAction> actions = new Dictionary<string, HandleAction>();

		protected void RegisterAction(string key, HandleAction action)
		{
			actions[key.ToLower()] = action;
		}

		public abstract void Write();

		public virtual void Initialize() { }

		public void PerformAction(string key)
		{
			HandleAction handle;
			if (actions.TryGetValue(key.ToLower(), out handle))
			{
				handle();
			}
		}
	}
}
