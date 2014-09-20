using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using BananaRTSWP8.Framework.Chunks.Dynamic;

namespace BananaRTSWP8.RTSGame.Objects
{
	public abstract class FactionAssetContextMenu : AbstractSprite
	{
		protected bool isActive;
		public bool IsActive
		{
			get
			{
				return isActive;
			}
		}

		public virtual void Activate()
		{
			isActive = true;
		}

		public virtual void Deactivate()
		{
			isActive = false;
		}
	}
}
