using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BananaRTSWP8.Framework.Chunks.Structural;
using BananaRTSWP8.Game.Objects.Units;

namespace BananaRTSWP8.Game.Levels
{
	public class Battlefield : AbstractGameLevel
	{
		protected IList<Unit> units;

		public Battlefield() : base()
		{
			units = new List<Unit>();
		}
	}
}
