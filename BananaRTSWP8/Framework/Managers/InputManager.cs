using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace BananaRTSWP8.Framework.Managers
{
	public class InputManager
	{
		private static TouchCollection panel;

		static InputManager()
		{
			panel = TouchPanel.GetState();
		}

		public static void Update()
		{
			panel = TouchPanel.GetState();
		}

		public static int GetTouchCount()
		{
			return panel.Count;
		}

		public static TouchLocation GetTouchPoint(int Index)
		{
			return panel[Index];
		}
	}
}
