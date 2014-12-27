/*
 * Author: Jöran Malek
 */

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TextAdventure.Attributes;
using TextAdventure.Scenes;

namespace TextAdventure
{
	internal static class Program
	{
		//public static readonly Random Random = new Random();

		#region Helper Methods

		/// <summary>
		/// Returns all actions from one scene and returns a dictionary of Key &lt;-&gt; Description.
		/// </summary>
		/// <param name="scene">Some scene.</param>
		/// <returns>A dictionary for every single action as Key &lt;-&gt; Description.</returns>
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

		#endregion Helper Methods

		/// <summary>
		/// Default entry point.
		/// </summary>
		private static void Main()
		{
			Scenes.SceneManager.LoadScene<Scenes.MainMenuScene>();
			Scenes.SceneManager.Run();
		}
	}
}
