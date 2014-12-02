using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
