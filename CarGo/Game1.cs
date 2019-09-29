using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CarGo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    
    public enum GameState {Playing,MenuMain, MenuModificationSelection,MenuPause,MenuLost,MenuWon, LevelEditor, Exit }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Scene scene;
        MainMenu mainMenu;

        private GameState gameState;
        public GameState GameState { get => gameState; set => gameState = value; }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            this.IsFixedTimeStep = true;//false;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d); //60);
#if DEBUG
            // Debug Code

#else
            // Release Code
            graphics.ToggleFullScreen();
#endif
            Content.RootDirectory = "Content";
            GameState = GameState.MenuMain;
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
            mainMenu = new MainMenu(spriteBatch, new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Content);

            // Add a player for each connected Controller
            int playercount = 0;
            //for (PlayerIndex index = PlayerIndex.One; index <= PlayerIndex.Four; index++)
            //{
            //    if (GamePad.GetCapabilities(index).IsConnected)
            //    {
            //        scene.addPlayer(index, new Vector2(400 + (int)index * 100, 400),CarType.Small, CarFrontType.Bumper, AbilityType.RocketLauncher);
            //        playercount++;
            //    }
            //}
            if (GamePad.GetCapabilities(PlayerIndex.One).IsConnected)
            {
                scene.addPlayer(PlayerIndex.One, new Vector2(400 + (int)PlayerIndex.One * 100, 400), CarType.Medium, CarFrontType.Spikes, AbilityType.RocketLauncher);
                playercount++;
            }
            if (GamePad.GetCapabilities(PlayerIndex.Two).IsConnected)
            {
                scene.addPlayer(PlayerIndex.Two, new Vector2(400 + (int)PlayerIndex.Two * 100, 400), CarType.Small, CarFrontType.Bumper, AbilityType.RocketLauncher);
                playercount++;
            }
            if (GamePad.GetCapabilities(PlayerIndex.Three).IsConnected)
            {
                scene.addPlayer(PlayerIndex.Three, new Vector2(400 + (int)PlayerIndex.Three * 100, 400), CarType.Small, CarFrontType.Bumper, AbilityType.RocketLauncher);
                playercount++;
            }
            if (GamePad.GetCapabilities(PlayerIndex.Four).IsConnected)
            {
                scene.addPlayer(PlayerIndex.Four, new Vector2(400 + (int)PlayerIndex.Four * 100, 400), CarType.Small, CarFrontType.Bumper, AbilityType.RocketLauncher);
                playercount++;
            }


            //Debug: Wenn keine Controller angeschlossen sind erstelle einen Spieler um mit der Tastatur zu spielen
            if (playercount==0)scene.addPlayer(PlayerIndex.Four, new Vector2(400, 400),CarType.Big, CarFrontType.Bumper, AbilityType.TrapLauncher);
            
          // scene.addPlayer(PlayerIndex.Four, new Vector2(800, 400), CarType.Medium, CarFrontType.Bumper, AbilityType.RocketLauncher);
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
            gameState = this.GameState; //save current game state
            // TODO: Add your update logic here
            switch (this.GameState)
            {
                case GameState.Playing:
                    scene.Update(gameTime);
                    break;
                case GameState.MenuPause:
                    break;
                case GameState.MenuMain:
                    if (mainMenu.Update(GameState) != gameState) //if gamestate is changed in Menu 
                        GameState = mainMenu.Update(GameState); //update gamestate                   
                    break;
                case GameState.Exit:
                    Exit();
                    break;
                default:break;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Cornsilk);

            switch (this.GameState)
            {
                case GameState.Playing:
                    scene.Draw(gameTime);
                    break;
                case GameState.MenuPause:
                    break;
                case GameState.MenuMain:
                    mainMenu.Draw();
                    break;
                default: break; ;
            }
            
        }
    }
}
