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
		private const int BUBBLE_A_ID = 0;
		private const int BUBBLE_B_ID = 1;

		private static readonly Vector2 BUBBLE_A_POS = new Vector2(-128.0f, -64.0f);
		private static readonly Vector2 BUBBLE_B_POS = new Vector2(128.0f, -64.0f);

		private const float COUNT_BUBBLE_DISTANCE = 96.0f;

		protected Timer unitBubbleTimerIn;
		protected Timer unitBubbleTimerOut;
		protected Timer unitCountBubbleTimerIn;
		protected Timer unitCountBubbleTimerOut;

		protected int touchedBubble;

		public DebugBuildingContextMenu(Vector2 Position) : base()
		{
			pos = Position;
			unitBubbleTimerIn = new Timer(GlobalConstants.CONTEXT_BUBBLE_TIME, false);
			level.RegisterUpdatableObject(unitBubbleTimerIn);
			unitBubbleTimerOut = new Timer(GlobalConstants.CONTEXT_BUBBLE_TIME, false);
			level.RegisterUpdatableObject(unitBubbleTimerOut);
			unitCountBubbleTimerIn = new Timer(GlobalConstants.CONTEXT_BUBBLE_TIME, false);
			level.RegisterUpdatableObject(unitCountBubbleTimerIn);
			unitCountBubbleTimerOut = new Timer(GlobalConstants.CONTEXT_BUBBLE_TIME, false);
			level.RegisterUpdatableObject(unitCountBubbleTimerOut);

			touchedBubble = -1;
		}

		public override void Update()
		{
			if (unitBubbleTimerIn.IsCompleted && !unitBubbleTimerOut.IsRunning)
			{
				if (InputManager.GetTouchCount() > 0)
				{
					TouchLocation tl = InputManager.GetTouchPoint(0);
					bool isTouchingA = Vector2.DistanceSquared(pos + BUBBLE_A_POS, tl.Position) <= (64 * 64);
					bool isTouchingB = Vector2.DistanceSquared(pos + BUBBLE_B_POS, tl.Position) <= (64 * 64);
					if (isTouchingA || isTouchingB)
					{
						if (!unitCountBubbleTimerIn.IsRunning && !unitCountBubbleTimerIn.IsCompleted && !unitCountBubbleTimerOut.IsRunning)
						{
							unitCountBubbleTimerIn.StartTimer();
							touchedBubble = isTouchingA ? BUBBLE_A_ID : BUBBLE_B_ID;
						}
						else if (!unitCountBubbleTimerOut.IsRunning && unitCountBubbleTimerIn.IsCompleted)
						{
							if ((touchedBubble == BUBBLE_A_ID && isTouchingB) || (touchedBubble == BUBBLE_B_ID && isTouchingA))
							{
								unitCountBubbleTimerIn.ResetTimer(false);
								unitCountBubbleTimerOut.ResetTimer(true);
							}
						}
					}
					else
					{
						if ((unitCountBubbleTimerIn.IsRunning || unitCountBubbleTimerIn.IsCompleted) && !unitCountBubbleTimerOut.IsRunning)
						{
							unitCountBubbleTimerIn.ResetTimer(false);
							unitCountBubbleTimerOut.ResetTimer(true);
						}
						else if (unitCountBubbleTimerOut.IsCompleted)
						{
							touchedBubble = -1;
						}
					}
				}
				else
				{
					if (unitCountBubbleTimerIn.IsRunning || unitCountBubbleTimerIn.IsCompleted)
					{
						unitCountBubbleTimerIn.ResetTimer(false);
						unitCountBubbleTimerOut.ResetTimer(true);
					}
				}
			}
		}

		public override void Render()
		{
			if (unitBubbleTimerIn.IsRunning || unitBubbleTimerIn.IsCompleted || unitBubbleTimerOut.IsRunning)
			{
				RenderManager.DrawQuad(Key: "CONTEXT_MENU_CIRCLE", Position: pos + BUBBLE_A_POS, Scale: unitBubbleTimerOut.IsRunning ? BananaMath.Qerp(0.5f, 0.7f, 0.0f, unitBubbleTimerOut.Status) : BananaMath.Qerp(0, 0.7f, 0.5f, unitBubbleTimerIn.Status), Origin: new Vector2(0.5f));
				RenderManager.DrawQuad(Key: "CONTEXT_MENU_CIRCLE", Position: pos + BUBBLE_B_POS, Scale: unitBubbleTimerOut.IsRunning ? BananaMath.Qerp(0.5f, 0.7f, 0.0f, unitBubbleTimerOut.Status) : BananaMath.Qerp(0, 0.7f, 0.5f, unitBubbleTimerIn.Status), Origin: new Vector2(0.5f));
			}

			if (unitCountBubbleTimerIn.IsRunning || unitCountBubbleTimerIn.IsCompleted || unitCountBubbleTimerOut.IsRunning)
			{
				if (touchedBubble != -1)
				{
					for (int i = 0; i < 4; i++)
					{
						float angle = MathHelper.Pi + MathHelper.PiOver4 * (float)i;
						Vector2 bubblePos = new Vector2((float)Math.Cos(angle) * COUNT_BUBBLE_DISTANCE, (float)Math.Sin(angle) * COUNT_BUBBLE_DISTANCE) + (touchedBubble == BUBBLE_A_ID ? BUBBLE_A_POS : BUBBLE_B_POS);
						RenderManager.DrawQuad(
							Key: "CONTEXT_MENU_CIRCLE", 
							Position: pos + bubblePos, 
							Scale: unitCountBubbleTimerOut.IsRunning ? BananaMath.Qerp(0.2f, 0.3f, 0.0f, unitCountBubbleTimerOut.Status) : BananaMath.Qerp(0, 0.3f, 0.2f, unitCountBubbleTimerIn.Status), 
							Origin: new Vector2(0.5f),
							Col: Color.LightGray);
					}
				}
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
