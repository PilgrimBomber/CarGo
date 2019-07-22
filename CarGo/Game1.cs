﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CarGo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    
    public enum GameState {Playing,Menu,Pause,Lost,Won }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Scene scene;
        private GameState gameState;
        public GameState GameState { get => gameState; set => gameState = value; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            #if DEBUG
            // Debug Code

            #else
            // Release Code
            graphics.ToggleFullScreen();
            #endif
            Content.RootDirectory = "Content";
            GameState = GameState.Playing;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            spriteBatch = new SpriteBatch(GraphicsDevice);
            scene = new Scene(spriteBatch, Content, new Vector2(graphics.PreferredBackBufferWidth,graphics.PreferredBackBufferHeight));
            // Add a player for each connected Controller
            int playercount = 0;
            for (PlayerIndex index = PlayerIndex.One; index <= PlayerIndex.Four; index++)
            {
                if (GamePad.GetCapabilities(index).IsConnected)
                {
                    scene.addPlayer(index, new Vector2(400 + (int)index * 100, 400),CarType.Light,CarFrontType.Bumper, AbilityType.RocketLauncher);
                    playercount++;
                }
            }
            //Debug: Wenn keine Controller angeschlossen sind erstelle einen Spieler um mit der Tastatur zu spielen
            if(playercount==0)scene.addPlayer(PlayerIndex.One, new Vector2(400, 400),CarType.Light, CarFrontType.Spikes, AbilityType.RocketLauncher);

            //Add Cargo
            scene.addCargo(new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2));


        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            switch (this.GameState)
            {
                case GameState.Playing:
                    scene.Update(gameTime);
                    break;
                case GameState.Pause:
                    break;
                default:break; ;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Cornsilk);

            // TODO: Add your drawing code here
            scene.Draw(gameTime);
        }
    }
}
