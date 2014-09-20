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

namespace BananaRTSWP8.Game.Objects.Buildings
{
	public abstract class Building : FactionAsset
	{
		public Building()
		{
			contextTimer = new Timer(GlobalConstants.CONTEXT_MENU_START_TIME, false);
			level.RegisterUpdatableObject(contextTimer);
		}

		public override void Update()
		{
			ActOnBuilding();
		}

		public virtual void ActOnBuilding()
		{
			if (InputManager.GetTouchCount() > 0)
			{
				TouchLocation tl = InputManager.GetTouchPoint(0);
				if (IsPointInBuilding(tl))
				{
					if (!contextTimer.IsRunning)
					{
						contextTimer.StartTimer();
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

		public abstract bool IsPointInBuilding(TouchLocation TL);
	}
}
