/*
 * Author: Jöran Malek
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using TextAdventure.Scenes;
using TextAdventure.Attributes;

namespace TextAdventure
{
	static class Program
	{
		#region Helper Methods
		public static Dictionary<string, string> GetActions(this Scene scene)
		{
			IEnumerable<Action> actionValues = scene.Actions.Values.Cast<Action>();
			Dictionary<string, string> actionPairs = new Dictionary<string, string>();

			foreach (Action action in actionValues)
			{
				ActionAttribute attribute = action.Method.GetCustomAttribute<ActionAttribute>();
				if (attribute != null)
				{
					actionPairs[attribute.Key] = attribute.Description;
				}
			}

			return actionPairs;
		}
		#endregion

		static void Main(string[] args)
		{
			Scenes.SceneManager.LoadScene<Scenes.MainMenuScene>();
			Scenes.SceneManager.Run();
		}
	}
}
