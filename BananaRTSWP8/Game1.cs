using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BananaRTSWP8.RTSGame;
using BananaRTSWP8.Framework.Managers;
using BananaRTSWP8.RTSGame.Levels;

namespace BananaRTSWP8
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
			
			// Create the battlefield level
			Battlefield battlefield = new Battlefield();

			// Explicitly set the current level
			LevelManager.SetLevel(GlobalConstants.BATTLEFIELD_LEVEL);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
			// Setup the render manager
			RenderManager.Initialize(GraphicsDevice, this.Content);

			RenderManager.LoadTexture("DEBUG_BUILDING", "Buildings/Debug/debug");
			RenderManager.LoadTexture("CONTEXT_MENU_CIRCLE", "UI/ContextMenus/circle");
			RenderManager.LoadFont("DEBUG_FONT", "UI/Debug/fontMenuItem");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Update the GameManager, which keeps track of the time delta and shit
			GameManager.Update(gameTime);
			InputManager.Update();

			// Update the current level
			LevelManager.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

			LevelManager.Render();

            base.Draw(gameTime);
        }
    }
}
