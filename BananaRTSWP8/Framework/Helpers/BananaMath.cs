using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BananaRTSWP8.Framework.Helpers
{
	public class BananaMath
	{
		public static float Lerp(float A, float B, float X)
		{
			return A + (B - A) * X;
		}

		public static float Qerp(float A, float B, float C, float X)
		{
			return Lerp(Lerp(A, B, X), Lerp(B, C, X), X);
		}
	}
}
