/*
 * Author: Jöran Malek
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TextAdventure.Attributes;
using TextAdventure.Scenes;

namespace TextAdventure
{
	static class Program
	{
		//public static readonly Random Random = new Random();
		#region Helper Methods
		public static Dictionary<string, string> GetActions(this Scene scene)
		{
			IEnumerable<ExecuteAction> actionValues = scene.Actions.Values.Cast<ExecuteAction>();
			Dictionary<string, string> actionPairs = new Dictionary<string, string>();

			foreach (ExecuteAction action in actionValues)
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

		static void Main()
		{
			Scenes.SceneManager.LoadScene<Scenes.MainMenuScene>();
			Scenes.SceneManager.Run();
		}
	}
}
