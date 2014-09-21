using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using BananaRTSWP8.Framework.Helpers;
using BananaRTSWP8.Framework.Managers;
using BananaRTSWP8.Framework.Chunks.Dynamic;
using BananaRTSWP8.Framework.Chunks.Helpers;

namespace BananaRTSWP8.RTSGame.Objects.Buildings.DebugBuilding
{
	public class DebugBuildingContextMenu : FactionAssetContextMenu
	{
		protected Timer unitBubbleTimerIn;
		protected Timer unitBubbleTimerOut;

		public DebugBuildingContextMenu(Vector2 Position) : base()
		{
			pos = Position;
			unitBubbleTimerIn = new Timer(GlobalConstants.CONTEXT_BUBBLE_TIME, false);
			level.RegisterUpdatableObject(unitBubbleTimerIn);
			unitBubbleTimerOut = new Timer(GlobalConstants.CONTEXT_BUBBLE_TIME, false);
			level.RegisterUpdatableObject(unitBubbleTimerOut);
		}

		public override void Update()
		{
		}

		public override void Render()
		{
			if (unitBubbleTimerIn.IsRunning || unitBubbleTimerIn.IsCompleted)
			{
				RenderManager.DrawQuad(Key: "CONTEXT_MENU_CIRCLE", Position: pos + new Vector2(-128.0f, -64.0f), Scale: BananaMath.Qerp(0, 0.7f, 0.5f, unitBubbleTimerIn.Status), Origin: new Vector2(0.5f));
				RenderManager.DrawQuad(Key: "CONTEXT_MENU_CIRCLE", Position: pos + new Vector2(128.0f, -64.0f), Scale: BananaMath.Qerp(0, 0.7f, 0.5f, unitBubbleTimerIn.Status), Origin: new Vector2(0.5f));
			}
			else if (unitBubbleTimerOut.IsRunning)
			{
				RenderManager.DrawQuad(Key: "CONTEXT_MENU_CIRCLE", Position: pos + new Vector2(-128.0f, -64.0f), Scale: BananaMath.Qerp(0.5f, 0.7f, 0.0f, unitBubbleTimerOut.Status), Origin: new Vector2(0.5f));
				RenderManager.DrawQuad(Key: "CONTEXT_MENU_CIRCLE", Position: pos + new Vector2(128.0f, -64.0f), Scale: BananaMath.Qerp(0.5f, 0.7f, 0.0f, unitBubbleTimerOut.Status), Origin: new Vector2(0.5f));
			}
		}

		public override void Activate()
		{
			unitBubbleTimerOut.ResetTimer(false);
			unitBubbleTimerIn.StartTimer();
			base.Activate();
		}

		public override void Deactivate()
		{
			unitBubbleTimerIn.ResetTimer(false);
			unitBubbleTimerOut.StartTimer();
			base.Deactivate();
		}
	}
}
