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

namespace BananaRTSWP8.RTSGame.Objects.Buildings
{
	public abstract class TwoUnitBuildingContextMenu : FactionAssetContextMenu
	{
		private const int BUBBLE_A_ID = 0;
		private const int BUBBLE_B_ID = 1;

		private static readonly Vector2 BUBBLE_A_POS = new Vector2(-128.0f, -64.0f);
		private static readonly Vector2 BUBBLE_B_POS = new Vector2(128.0f, -64.0f);
		private const float COUNT_BUBBLE_DISTANCE = 96.0f;
		private static readonly Vector2[] COUNT_BUBBLES = 
		{ 
			new Vector2((float)Math.Cos(MathHelper.Pi) * COUNT_BUBBLE_DISTANCE, (float)Math.Sin(MathHelper.Pi) * COUNT_BUBBLE_DISTANCE),
			new Vector2((float)Math.Cos(MathHelper.Pi + MathHelper.PiOver4) * COUNT_BUBBLE_DISTANCE, (float)Math.Sin(MathHelper.Pi + MathHelper.PiOver4) * COUNT_BUBBLE_DISTANCE),
			new Vector2((float)Math.Cos(MathHelper.Pi + MathHelper.PiOver4 * 2.0f) * COUNT_BUBBLE_DISTANCE, (float)Math.Sin(MathHelper.Pi + MathHelper.PiOver4 * 2.0f) * COUNT_BUBBLE_DISTANCE),
			new Vector2((float)Math.Cos(MathHelper.Pi + MathHelper.PiOver4 * 3.0f) * COUNT_BUBBLE_DISTANCE, (float)Math.Sin(MathHelper.Pi + MathHelper.PiOver4 * 3.0f) * COUNT_BUBBLE_DISTANCE)
		};

		protected Timer unitBubbleTimerIn;
		protected Timer unitBubbleTimerOut;
		protected Timer unitCountBubbleTimerIn;
		protected Timer unitCountBubbleTimerOut;

		protected int touchedBubble;
		protected bool isTouchingA;
		protected bool isTouchingB;
		protected bool isAroundA;
		protected bool isAroundB;
		protected bool[] isTouchingCount;

		public TwoUnitBuildingContextMenu(Vector2 Position) : base()
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
			isTouchingA = false;
			isTouchingB = false;
			isAroundA = false;
			isAroundB = false;
			isTouchingCount = new bool[] { false, false, false, false};
		}

		public override void Update()
		{
			if (unitBubbleTimerIn.IsCompleted && !unitBubbleTimerOut.IsRunning)
			{
				if (InputManager.GetTouchCount() > 0)
				{
					TouchLocation tl = InputManager.GetTouchPoint(0);
					float distanceToA = Vector2.DistanceSquared(pos + BUBBLE_A_POS, tl.Position);
					float distanceToB = Vector2.DistanceSquared(pos + BUBBLE_B_POS, tl.Position);
					isTouchingA = distanceToA <= (64 * 64);
					isTouchingB = distanceToB <= (64 * 64);
					isAroundA = distanceToA <= (128 * 128);
					isAroundB = distanceToB <= (128 * 128);
					for (int i = 0; i < 4; i++)
					{
						isTouchingCount[i] = Vector2.DistanceSquared(pos + (isAroundA ? BUBBLE_A_POS : BUBBLE_B_POS) + COUNT_BUBBLES[i], tl.Position) <= (32.0f * 32.0f);
					}

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
						if (!isAroundA && !isAroundB)
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
				}
				else
				{
					if (unitCountBubbleTimerIn.IsRunning || unitCountBubbleTimerIn.IsCompleted)
					{
						unitCountBubbleTimerIn.ResetTimer(false);
						unitCountBubbleTimerOut.ResetTimer(true);
					}

					if (isAroundA)
					{
						if (isTouchingA)
						{
							BubbleAAction(1);
						}
						else
						{
							for (int i = 0; i < 4; i++)
							{
								if (isTouchingCount[i])
								{
									BubbleAAction(i + 2);
									break;
								}
							}
						}
					}
					else if (isAroundB)
					{
						if (isTouchingB)
						{
							BubbleBAction(1);
						}
						else
						{
							for (int i = 0; i < 4; i++)
							{
								if (isTouchingCount[i])
								{
									BubbleBAction(i + 2);
									break;
								}
							}
						}
					}

					touchedBubble = -1;
					isTouchingA = false;
					isTouchingB = false;
					isAroundA = false;
					isAroundB = false;
					for (int i = 0; i < 4; i++)
					{
						isTouchingCount[i] = false;
					}
				}
			}
		}

		public override void Render()
		{
			if (unitBubbleTimerIn.IsRunning || unitBubbleTimerIn.IsCompleted || unitBubbleTimerOut.IsRunning)
			{
				RenderManager.DrawQuad(
					Key: "CONTEXT_MENU_CIRCLE", 
					Position: pos + BUBBLE_A_POS, 
					Scale: unitBubbleTimerOut.IsRunning ? BananaMath.Qerp(0.5f, 0.7f, 0.0f, unitBubbleTimerOut.Status) : BananaMath.Qerp(0, 0.7f, 0.5f, unitBubbleTimerIn.Status), 
					Origin: new Vector2(0.5f),
					Col: isTouchingA ? Color.LightGray : Color.White);
				RenderManager.DrawQuad(
					Key: "CONTEXT_MENU_CIRCLE", 
					Position: pos + BUBBLE_B_POS, 
					Scale: unitBubbleTimerOut.IsRunning ? BananaMath.Qerp(0.5f, 0.7f, 0.0f, unitBubbleTimerOut.Status) : BananaMath.Qerp(0, 0.7f, 0.5f, unitBubbleTimerIn.Status), 
					Origin: new Vector2(0.5f),
					Col: isTouchingB ? Color.LightGray : Color.White);
			}

			if (unitCountBubbleTimerIn.IsRunning || unitCountBubbleTimerIn.IsCompleted || unitCountBubbleTimerOut.IsRunning)
			{
				if (touchedBubble != -1)
				{
					for (int i = 0; i < 4; i++)
					{
						Vector2 bubblePos = (touchedBubble == BUBBLE_A_ID ? BUBBLE_A_POS : BUBBLE_B_POS) + COUNT_BUBBLES[i];
						RenderManager.DrawQuad(
							Key: "CONTEXT_MENU_CIRCLE", 
							Position: pos + bubblePos, 
							Scale: unitCountBubbleTimerOut.IsRunning ? BananaMath.Qerp(0.2f, 0.3f, 0.0f, unitCountBubbleTimerOut.Status) : BananaMath.Qerp(0, 0.3f, 0.2f, unitCountBubbleTimerIn.Status), 
							Origin: new Vector2(0.5f),
							Col: isTouchingCount[i] ? Color.Gray : Color.LightGray);
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

		public abstract void BubbleAAction(int Count);

		public abstract void BubbleBAction(int Count);
	}
}
