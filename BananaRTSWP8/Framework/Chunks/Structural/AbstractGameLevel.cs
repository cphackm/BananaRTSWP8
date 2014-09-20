using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BananaRTSWP8.Framework.Managers;
using BananaRTSWP8.Framework.Chunks.Dynamic;

namespace BananaRTSWP8.Framework.Chunks.Structural
{
	public abstract class AbstractGameLevel
	{
		private IDictionary<string, object> localStates;

		private IList<AbstractGameObject> updatableObjects;
		private IList<AbstractGameObject> drawableObjects;

		public AbstractGameLevel()
		{
			localStates = new Dictionary<string, object>();

			updatableObjects = new List<AbstractGameObject>();
			drawableObjects = new List<AbstractGameObject>();
		}

		public bool SetLocalState(string Key, object State)
		{
			localStates[Key] = State;

			return true;
		}

		public bool DoesLocalStateExist(string Key)
		{
			return localStates.ContainsKey(Key);
		}

		public void RegisterObject(AbstractGameObject GameObject, bool Drawable)
		{
			RegisterUpdatableObject(GameObject);

			if (Drawable)
			{
				RegisterDrawableObject(GameObject);
			}
		}

		public void RegisterUpdatableObject(AbstractGameObject GameObject)
		{
			updatableObjects.Add(GameObject);
		}

		public void RegisterDrawableObject(AbstractGameObject GameObject)
		{
			drawableObjects.Add(GameObject);
		}

		public void Update()
		{
			foreach (AbstractGameObject ago in updatableObjects)
			{
				ago.Update();
			}
		}

		public void Render()
		{
			RenderManager.BeginSpriteBatching();

			foreach (AbstractGameObject ago in drawableObjects)
			{
				ago.Render();
			}

			RenderManager.EndSpriteBatching();
		}
	}
}
