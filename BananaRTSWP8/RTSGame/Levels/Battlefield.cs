using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BananaRTSWP8.Framework.Chunks.Dynamic;
using BananaRTSWP8.Framework.Chunks.Structural;
using BananaRTSWP8.RTSGame.Objects.Units;
using BananaRTSWP8.RTSGame.Objects.Buildings;
using BananaRTSWP8.RTSGame.Objects.Buildings.DebugBuilding;

namespace BananaRTSWP8.RTSGame.Levels
{
	public class Battlefield : AbstractGameLevel
	{
		protected IList<Unit> units;
		protected IList<Building> buildings;

		public Battlefield() : base(GlobalConstants.BATTLEFIELD_LEVEL)
		{
			units = new List<Unit>();
			buildings = new List<Building>();

			DebugBuilding db = new DebugBuilding(5, 5);
			RegisterObject(db, true);
		}

		public override void Update()
		{
			base.Update();
		}

		public override void Render()
		{
			base.Render();
		}

		public override void RegisterObject(AbstractGameObject GameObject, bool Drawable)
		{
			base.RegisterObject(GameObject, Drawable);

			if (GameObject.GetType().IsSubclassOf(typeof(Unit)))
			{
				units.Add(GameObject as Unit);
			}
			else if (GameObject.GetType().IsSubclassOf(typeof(Building)))
			{
				buildings.Add(GameObject as Building);
			}
		}
	}
}
