using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BananaRTSWP8.Framework.Managers
{
	public class RenderManager
	{
		private static ContentManager cm;
		private static GraphicsDevice graphicsDevice;
		private static SpriteBatch spriteBatch;
		private static bool isSpriteBatchRunning;
		private static IDictionary<string, Texture2D> textures;
		private static IDictionary<string, SpriteFont> fonts;
		private static Texture2D nextTexture;
		private static Vector2 scroll;
		public static Vector2 Scroll
		{
			get
			{
				return scroll;
			}
			set
			{
				scroll = value;
			}
		}

		/// <summary>
		/// Static constructor for initializing necessary objects.
		/// </summary>
		static RenderManager()
		{
			textures = new Dictionary<string, Texture2D>();
			fonts = new Dictionary<string, SpriteFont>();
			nextTexture = null;
			scroll = Vector2.Zero;
		}

		/// <summary>
		/// Method for initializing the RenderManager with data that can't be retrieved by the static constructor.
		/// </summary>
		/// <param name="GDevice">Reference to the graphics device.</param>
		public static void Initialize(GraphicsDevice GDevice, ContentManager CM)
		{
			cm = CM;
			graphicsDevice = GDevice;
			spriteBatch = new SpriteBatch(graphicsDevice);
			isSpriteBatchRunning = false;
		}

		/// <summary>
		/// Creates a RenderTarget2D.
		/// </summary>
		/// <param name="Key">Key to associate with the new RenderTarget2D</param>
		/// <param name="Width">Width of the new RenderTarget2D.</param>
		/// <param name="Height">Height of the new RenderTarget2D.</param>
		public static void CreateRenderTarget(string Key, int Width, int Height)
		{
			textures[Key] = new RenderTarget2D(graphicsDevice, Width, Height);
		}

		/// <summary>
		/// Stops the current SpriteBatch and switches out the RenderTarget2D, restarting the SpriteBatch at the end.
		/// </summary>
		/// <param name="Key">Key associated with the RenderTarget2D.</param>
		/// <param name="ClearColor">Optional: Color to clear the screen with.</param>
		/// <param name="SortMode">Optional: SpriteSortMode to use.</param>
		/// <param name="BlState">Optional: BlendState to use.</param>
		public static void SetRenderTarget(string Key, bool BeginSpriteBatch, Nullable<Color> ClearColor = null, SpriteSortMode SortMode = SpriteSortMode.FrontToBack, BlendState BlState = null)
		{
			if (isSpriteBatchRunning)
			{
				spriteBatch.End();
			}

			graphicsDevice.SetRenderTarget(Key != null ? (textures[Key] as RenderTarget2D) : null);

			if (ClearColor != null)
			{
				graphicsDevice.Clear(ClearColor.Value);
			}

			if (BeginSpriteBatch)
			{
				spriteBatch.Begin(SortMode, BlState == null ? null : BlState);
			}
		}

		public static void BeginSpriteBatching(SpriteSortMode SortMode = SpriteSortMode.FrontToBack, BlendState BlState = null)
		{
			spriteBatch.Begin(SortMode, BlState);
		}

		public static void EndSpriteBatching()
		{
			spriteBatch.End();
		}

		/// <summary>
		/// Retrieves and stores a specified texture.  Use in conjunction with a null value for Key on DrawQuad when drawing from the same Texture2D multiple times sequentially.
		/// </summary>
		/// <param name="Key">Key associated with the Texture2D</param>
		public static void SetTexture(string Key)
		{
			nextTexture = textures[Key];
		}

		public static void LoadTexture(string Key, string Path)
		{
			textures[Key] = cm.Load<Texture2D>(Path);
		}

		public static void LoadFont(string Key, string Path)
		{
			fonts[Key] = cm.Load<SpriteFont>(Path);
		}

		public static void DrawQuad(string Key, Vector2 Position, Nullable<Color> Col = null, float Angle = 0.0f, float Scale = 1.0f, Nullable<Vector2> Origin = null, float Depth = 1.0f)
		{
			nextTexture = Key == null ? nextTexture : textures[Key];
			spriteBatch.Draw(
				nextTexture, 
				Position, 
				new Rectangle(0, 0, nextTexture.Width, nextTexture.Height), 
				Col == null ? Color.White : Col.Value, 
				Angle, 
				Origin == null ? Vector2.Zero : new Vector2(Origin.Value.X * (float)nextTexture.Width, Origin.Value.Y * (float)nextTexture.Height), 
				Scale, 
				SpriteEffects.None, 
				Depth);
		}

		public static void DrawString(string Key, string Text, Vector2 Position, float Scale, Color Col)
		{
			spriteBatch.DrawString(fonts[Key], Text, Position, Col, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 1.0f);
		}
	}
}
