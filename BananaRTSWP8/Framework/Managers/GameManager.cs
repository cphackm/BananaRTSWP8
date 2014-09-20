using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BananaRTSWP8.Framework.Managers
{
	public class GameManager
	{
		public static float DT;

		private static IDictionary<string, object> globalStates;

		static GameManager()
		{
			DT = 0.0f;
			globalStates = new Dictionary<string, object>();
		}

		public static void Update(GameTime Time)
		{
			DT = (float)Time.ElapsedGameTime.TotalMilliseconds / 1000.0f;
		}

		public static bool SetGlobalState(string Key, object State)
		{
			globalStates[Key] = State;

			return true;
		}

		public static bool DoesGlobalStateExist(string Key)
		{
			return globalStates.ContainsKey(Key);
		}
	}
}
