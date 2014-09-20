using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BananaRTSWP8.Framework.Chunks.Dynamic;
using BananaRTSWP8.Framework.Chunks.Helpers;

namespace BananaRTSWP8.Game.Objects
{
	public abstract class FactionAsset : AbstractSprite
	{
		protected int faction;
		public int Faction
		{
			get
			{
				return faction;
			}
		}

		protected int health;
		public int Health
		{
			get
			{
				return health;
			}
		}

		protected bool isActive;
		public bool IsActive
		{
			get
			{
				return isActive;
			}
		}

		protected FactionAssetContextMenu contextMenu;

		protected Timer contextTimer;
	}
}
