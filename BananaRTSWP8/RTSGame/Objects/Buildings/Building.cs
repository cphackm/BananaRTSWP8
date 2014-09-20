using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using BananaRTSWP8.Framework.Managers;
using BananaRTSWP8.Framework.Chunks.Dynamic;
using BananaRTSWP8.Framework.Chunks.Helpers;

namespace BananaRTSWP8.RTSGame.Objects.Buildings
{
	public abstract class Building : FactionAsset
	{
		protected int width;
		protected int height;
		protected int gridX;
		protected int gridY;

		public Building(int Width, int Height, int GridX, int GridY) : base()
		{
			width = Width;
			height = Height;
			gridX = GridX;
			gridY = GridY;
		}
	}
}
