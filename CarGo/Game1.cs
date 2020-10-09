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
        LobbyOnline lobbyOnline;
        PostGameMenu postGameMenu;
        MenuControls menuControls;
        CreditScreen creditScreen;
        MenuSettings menuSettings;
        MenuPause menuPause;
        LaunchMenu launchMenu;
        WaitForServerStart waitForServerStart;
        LoadingScreen loadingScreen;
        LobbySearch lobbySearch;
        public ModifierMenu modifierMenu;
        SoundEffectInstance music;
        public Network.NetworkThread networkThread;
        Network.LocalUpdates localUpdates;

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
            lobbyOnline = new LobbyOnline(spriteBatch, this);
            localUpdates = new Network.LocalUpdates(this, lobbyOnline);
            networkThread = new Network.NetworkThread(localUpdates);
            scene = new Scene(spriteBatch, Content, new Vector2(graphics.PreferredBackBufferWidth,graphics.PreferredBackBufferHeight),this);
            lobbySearch = new LobbySearch(spriteBatch,this);
            localUpdates.SetNetworkThread(networkThread);
            localUpdates.SetScene(scene);
            mainMenu = new MainMenu(spriteBatch, this);
            postGameMenu = new PostGameMenu(spriteBatch, this);
            modifierMenu = new ModifierMenu(spriteBatch, this);
            menuSettings = new MenuSettings(spriteBatch, this);
            menuControls = new MenuControls(spriteBatch, this);
            menuPause = new MenuPause(spriteBatch, this);
            creditScreen = new CreditScreen(spriteBatch, this);
            loadingScreen = new LoadingScreen(spriteBatch, this);
            launchMenu = new LaunchMenu(spriteBatch, this,networkThread);
            waitForServerStart = new WaitForServerStart(networkThread);
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

            if (StateMachine.Instance.networkGame)
            {
                localUpdates.Update(gameTime);
                PreferredInput.Instance.Update();
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
                case GameState.LevelEditor:
                    break;
                case GameState.LaunchMenu:
                    launchMenu.Update();
                    break;
                case GameState.OnlineLobby:
                    lobbyOnline.Update();
                    break;
                case GameState.SearchLobby:
                    lobbySearch.Update();
                    break;
                case GameState.WaitForServerStart:
                    waitForServerStart.Update();
                    break;
                default: break;
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
                case GameState.LevelEditor:
                    break;
                case GameState.Exit:
                    break;
                case GameState.LaunchMenu:
                    launchMenu.Draw();
                    break;
                case GameState.OnlineLobby:
                    lobbyOnline.Draw();
                    break;
                case GameState.SearchLobby:
                    lobbySearch.Draw();
                    break;
                case GameState.WaitForServerStart:
                    loadingScreen.Draw();
                    break;
                default: break; ;
            }

        }

        public Menu GetCurrentMenu()
        {

            switch (StateMachine.Instance.gameState)
            {
                case GameState.MenuMain:
                    return mainMenu;
                    break;
                case GameState.MenuModificationSelection:
                    return modifierMenu;
                    break;
                case GameState.MenuPause:
                    return menuPause;
                    break;
                case GameState.MenuLost:
                    return postGameMenu;
                    break;
                case GameState.MenuWon:
                    return postGameMenu;
                    break;
                case GameState.MenuControls:
                    return menuControls;
                    break;
                case GameState.CreditScreen:
                    return creditScreen;
                    break;
                case GameState.MenuSettings:
                    return menuSettings;
                    break;
                case GameState.LaunchMenu:
                    return launchMenu;
                    break;
                case GameState.OnlineLobby:
                    return lobbyOnline;
                    break;
                case GameState.SearchLobby:
                    return lobbySearch;
                    break;
                case GameState.WaitForServerStart:
                    return waitForServerStart;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                    return null;
            }
        }
        
        public void UpdateMusicVolume()
        {
            music.Volume = 0.5f * Settings.Instance.VolumeMusic;
        }
        
    }
}