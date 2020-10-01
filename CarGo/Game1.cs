using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace CarGo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Scene scene;
        MainMenu mainMenu;
        PostGameMenu postGameMenu;
        MenuControls menuControls;
        CreditScreen creditScreen;
        MenuSettings menuSettings;
        MenuPause menuPause;
        public ModifierMenu modifierMenu;
        SoundEffectInstance music;
        Network.NetworkThread networkThread = new Network.NetworkThread();
        Network.LocalUpdates localUpdates;
        int clientNumber;
        public bool networkGame = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            this.IsFixedTimeStep = true;//false;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d); //60);

            localUpdates = new Network.LocalUpdates(scene);

#if DEBUG
            // Debug Code

#else
            // Release Code
            graphics.ToggleFullScreen();
#endif
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
            // TODO: Add your initialization logic here
            Settings.Instance.loadSettings();
            TextureCollection.Instance.SetContent( Content);
            SoundCollection.Instance.SetContent(Content);
            FontCollection.Instance.LoadFonts(Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            scene = new Scene(spriteBatch, Content, new Vector2(graphics.PreferredBackBufferWidth,graphics.PreferredBackBufferHeight),this);
            mainMenu = new MainMenu(spriteBatch, this);
            postGameMenu = new PostGameMenu(spriteBatch, this);
            modifierMenu = new ModifierMenu(spriteBatch, this);
            menuSettings = new MenuSettings(spriteBatch, this);
            menuControls = new MenuControls(spriteBatch, this);
            menuPause = new MenuPause(spriteBatch, this);
            creditScreen = new CreditScreen(spriteBatch, this);
            music = SoundCollection.Instance.GetSoundInstance(SoundType.Menu_Music);
            music.IsLooped = true;
            music.Volume = 0.5f * Settings.Instance.VolumeMusic;
            music.Play();

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

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (/*GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||*/ Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (networkGame)
            {
                localUpdates.Update();
            }
                // TODO: Add your update logic here
                switch (StateMachine.Instance.gameState)
            {
                case GameState.Playing:
                    scene.Update(gameTime);
                    break;
                case GameState.MenuPause:
                    menuPause.Update();
                    break;
                case GameState.MenuMain:
                    mainMenu.Update();               
                    break;
                case GameState.MenuModificationSelection:
                    modifierMenu.Update();
                    break;
                case GameState.Exit:
                    Exit();
                    break;
                case GameState.MenuWon:
                    postGameMenu.Update();
                    break;
                case GameState.MenuLost:
                    postGameMenu.Update();
                    break;
                case GameState.MenuControls:
                    menuControls.Update();
                    break;
                case GameState.CreditScreen:
                    creditScreen.Update();
                    break;
                case GameState.MenuSettings:
                    menuSettings.Update();
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
            GraphicsDevice.Clear(Color.White);

            switch (StateMachine.Instance.gameState)
            {
                case GameState.Playing:
                    scene.Draw(gameTime);
                    break;
                case GameState.MenuPause:
                    scene.Draw(gameTime);
                    menuPause.Draw();
                    break;
                case GameState.MenuMain:
                    mainMenu.Draw();
                    break;
                case GameState.MenuModificationSelection:
                    modifierMenu.Draw();
                    break;
                case GameState.MenuWon:
                    postGameMenu.Draw();
                    break;
                case GameState.MenuLost:
                    postGameMenu.Draw();
                    break;
                case GameState.MenuControls:
                    menuControls.Draw();
                    break;
                case GameState.CreditScreen:
                    creditScreen.Draw();
                    break;
                case GameState.MenuSettings:
                    menuSettings.Draw();
                    break;
                default: break; ;
            }
            
        }

        public void UpdateMusicVolume()
        {
            music.Volume = 0.5f * Settings.Instance.VolumeMusic;
        }
    }
}
