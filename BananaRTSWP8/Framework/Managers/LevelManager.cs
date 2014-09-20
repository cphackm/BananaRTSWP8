using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BananaRTSWP8.Framework.Chunks.Structural;

namespace BananaRTSWP8.Framework.Managers
{
	public class LevelManager
	{
		private static IDictionary<string, AbstractGameLevel> levels;
		private static AbstractGameLevel currentLevel;
		public static AbstractGameLevel CurrentLevel
		{
			get
			{
				return currentLevel;
			}
		}

		static LevelManager()
		{
			levels = new Dictionary<string, AbstractGameLevel>();
		}

		public static void RegisterLevel(string Key, AbstractGameLevel Level)
		{
			levels[Key] = Level;
		}

		public static void SetLevel(string Key)
		{
			currentLevel = levels[Key];
		}

		public static void Update()
		{
			currentLevel.Update();
		}

		public static void Render()
		{
			currentLevel.Render();
		}
	}
}
