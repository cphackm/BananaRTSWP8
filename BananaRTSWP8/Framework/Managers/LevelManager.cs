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
		private static AbstractGameLevel currentLevel;
		public static AbstractGameLevel CurrentLevel
		{
			get
			{
				return currentLevel;
			}
		}

		public static void SetLevel(AbstractGameLevel Level)
		{
			currentLevel = Level;
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
