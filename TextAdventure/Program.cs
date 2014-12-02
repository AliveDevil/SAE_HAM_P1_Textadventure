/*
 * Author: Jöran Malek
 */


namespace TextAdventure
{
	class Program
	{
		static void Main(string[] args)
		{
			Scenes.SceneManager.LoadScene<Scenes.MainMenuScene>();
			Scenes.SceneManager.Run();
		}
	}
}
