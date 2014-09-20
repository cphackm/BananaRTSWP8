using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BananaRTSWP8.Framework.Chunks.Dynamic;

namespace BananaRTSWP8.RTSGame.Objects.Units
{
	public abstract class Unit : FactionAsset
	{
		protected Vector2 moveTarget;
		protected Unit attackTarget;

		public void MoveTo(Vector2 MoveTarget)
		{
			isActive = true;
			moveTarget = MoveTarget;
		}

		public void AttackUnit(Unit AttackTarget)
		{
			if (AttackTarget.Faction == this.faction)
			{
				isActive = true;
				attackTarget = AttackTarget;
			}
		}
	}
}
