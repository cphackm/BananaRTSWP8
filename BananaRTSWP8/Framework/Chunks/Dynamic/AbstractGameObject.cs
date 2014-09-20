using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BananaRTSWP8.Framework.Managers;
using BananaRTSWP8.Framework.Chunks.Structural;

namespace BananaRTSWP8.Framework.Chunks.Dynamic
{
	public abstract class AbstractGameObject
	{
		protected AbstractGameLevel level;

		public AbstractGameObject()
		{
			level = LevelManager.CurrentLevel;
		}

		public abstract void Update();
		public abstract void Render();
	}
}
