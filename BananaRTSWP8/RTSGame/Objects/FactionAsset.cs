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

namespace BananaRTSWP8.RTSGame.Objects
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

		public FactionAsset() : base()
		{
			contextTimer = new Timer(GlobalConstants.CONTEXT_MENU_START_TIME, false);
			level.RegisterUpdatableObject(contextTimer);
		}

		public override void Update()
		{
			base.Update();

			ActOnAsset();
		}

		public virtual void ActOnAsset()
		{
			if (InputManager.GetTouchCount() > 0)
			{
				TouchLocation tl = InputManager.GetTouchPoint(0);
				if (IsPointInAsset(tl))
				{
					if (!contextTimer.IsRunning && !contextTimer.IsCompleted)
					{
						contextTimer.StartTimer();
					}
					else if (contextTimer.IsCompleted)
					{
						contextMenu.Activate();
					}
				}
			}
			else
			{
				if (contextTimer.IsRunning)
				{
					contextTimer.ResetTimer(false);
				}
				else if (contextTimer.IsCompleted)
				{
					contextMenu.Deactivate();
				}
			}
		}

		public abstract bool IsPointInAsset(TouchLocation TL);
	}
}
