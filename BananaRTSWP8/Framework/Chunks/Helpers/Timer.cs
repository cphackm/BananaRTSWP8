using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BananaRTSWP8.Framework.Managers;
using BananaRTSWP8.Framework.Chunks.Dynamic;

namespace BananaRTSWP8.Framework.Chunks.Helpers
{
	public class Timer : AbstractGameObject
	{
		protected bool isRunning;
		public bool IsRunning
		{
			get
			{
				return isRunning;
			}
		}
		protected bool isCompleted;
		public bool IsCompleted
		{
			get
			{
				return isCompleted;
			}
		}
		protected float time;
		public float RunTime
		{
			get
			{
				return time;
			}
		}
		protected float length;
		public float Length
		{
			get
			{
				return length;
			}
		}

		public float Status
		{
			get
			{
				return time / length;
			}
		}

		public Timer(float Length, bool Start)
		{
			length = Length;
			ResetTimer(Start);
		}

		public override void Update()
		{
			if (isRunning)
			{
				time += 1.0f * GameManager.DT;

				if (time >= length)
				{
					isRunning = false;
					isCompleted = true;
				}
			}
		}

		public override void Render() { }

		public void StartTimer()
		{
			isRunning = true;
		}

		public void StopTimer()
		{
			isRunning = false;
		}

		public void ToggleTimer()
		{
			isRunning = !isRunning;
		}

		public void ResetTimer(bool Start)
		{
			time = 0.0f;
			isRunning = Start;
		}
	}
}
