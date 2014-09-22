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
	public class DebugBuildingContextMenu : TwoUnitBuildingContextMenu
	{
		public DebugBuildingContextMenu(Vector2 Position) : base(Position)
		{
		}

		public override void Update()
		{
			base.Update();
		}

		public override void Render()
		{
			base.Render();
		}

		public override void Activate()
		{
			base.Activate();
		}

		public override void Deactivate()
		{
			base.Deactivate();
		}

		public override void BubbleAAction(int Count)
		{
			GameManager.SetGlobalState(GlobalConstants.TU_BUILD_CM_D_KEY, "A" + Count.ToString());
		}

		public override void BubbleBAction(int Count)
		{
			GameManager.SetGlobalState(GlobalConstants.TU_BUILD_CM_D_KEY, "B" + Count.ToString());
		}
	}
}
