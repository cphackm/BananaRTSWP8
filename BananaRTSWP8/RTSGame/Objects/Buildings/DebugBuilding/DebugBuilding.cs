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

namespace BananaRTSWP8.RTSGame.Objects.Buildings.DebugBuilding
{
	public class DebugBuilding : Building
	{
		public DebugBuilding(int GridX, int GridY) : base(2, 2, GridX, GridY)
		{
			pos = new Vector2((GridX + 1) * GlobalConstants.TILE_WIDTH, (GridY + 1) * GlobalConstants.TILE_HEIGHT);
			contextMenu = new DebugBuildingContextMenu(pos);
			level.RegisterObject(contextMenu, true);
		}

		public override void Update()
		{
			base.Update();
		}

		public override void Render()
		{
			RenderManager.DrawQuad(Key: "DEBUG_BUILDING", Position: new Vector2(gridX * GlobalConstants.TILE_WIDTH, (gridY - 2) * GlobalConstants.TILE_HEIGHT));
		}

		public override bool IsPointInAsset(TouchLocation TL)
		{
			if (TL.Position.X >= gridX * GlobalConstants.TILE_WIDTH && TL.Position.X <= (gridX + width) * GlobalConstants.TILE_WIDTH)
			{
				if (TL.Position.Y >= gridY * GlobalConstants.TILE_HEIGHT && TL.Position.Y <= (gridY + height) * GlobalConstants.TILE_HEIGHT)
				{
					return true;
				}
			}

			return false;
		}
	}
}
