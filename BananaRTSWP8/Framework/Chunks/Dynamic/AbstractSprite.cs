using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BananaRTSWP8.Framework.Chunks.Dynamic
{
	public abstract class AbstractSprite : AbstractGameObject
	{
		protected Vector2 pos;
		public Vector2 Position 
		{ 
			get
			{
				return pos;
			}
		}

		protected float alpha;
		public float Alpha
		{
			get
			{
				return alpha;
			}
		}
	}
}
