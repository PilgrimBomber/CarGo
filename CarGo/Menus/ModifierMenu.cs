using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace CarGo
{
    class ModifierMenu
    {
        private List<RotRectangle> playerBoxes;
        private List<RotRectangle> selectionDescription;
        private List<RotRectangle> carSelectionBoxes; //for drawing
        private List<RotRectangle> frontSelectionBoxes; //for drawing
        private List<RotRectangle> abilitiesSelectionBoxes; //for drawing 
        //List Stage: if all 3 then all ready
        //List Player: which is connected 
        private bool[] gamePadConnected;
        private CarType[] carTypes;
        private CarFrontType[] frontTypes;
        private AbilityType[] abilityTypes; 
        private Texture2D showSelectionTexture;
        private Texture2D testing;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private Vector2[] boxConers;
        private Game1 theGame;
        private KeyboardState previousKeyBoardState;
        private GamePadState previousState;
        private SoundEffectInstance soundHorn;

        public ModifierMenu(SpriteBatch spriteBatchInit, Game1 game )
        {
            spriteBatch = spriteBatchInit;
            theGame = game;

            //GamePads connected
            gamePadConnected = new bool[4];
            for (PlayerIndex index = PlayerIndex.One; index <= PlayerIndex.Four; index++)
            {
                if (GamePad.GetCapabilities(index).IsConnected)
                {
                    gamePadConnected[(int)index] = true;
                }
                else { gamePadConnected[(int)index] = false; }
            }
         
            //Player 1-4 font
            playerBoxes = new List<RotRectangle>();
            for (int i = 0; i < 4; i++)
            {
                playerBoxes.Add(new RotRectangle(0, new Vector2(460 + (int)i * 400, 100), new Vector2(100, 32)));
            }

            //What to change
            selectionDescription = new List<RotRectangle>();
            for (int i = 0; i < 3; i++)
            {
                selectionDescription.Add(new RotRectangle(0, new Vector2(100, 360 + (int)i * 250), new Vector2(100, 32)));
            }

            //Car
            carSelectionBoxes = new List<RotRectangle>();
            for (int i = 0; i < 4; i++)
            {
                carSelectionBoxes.Add(new RotRectangle(0, new Vector2(460 + (int)i * 400, 360), new Vector2(100, 32)));
            }
            carTypes = new CarType[4];

            //Front
            frontSelectionBoxes = new List<RotRectangle>(); 
            for (int i = 0; i < 4; i++)
            {
                frontSelectionBoxes.Add(new RotRectangle(0, new Vector2(460 + (int)i * 400, 360 + 1 * 250), new Vector2(100, 32)));
            }
            frontTypes = new CarFrontType[4];

            //Abilities
            abilitiesSelectionBoxes = new List<RotRectangle>();
            for (int i = 0; i < 4; i++)
            {
                abilitiesSelectionBoxes.Add(new RotRectangle(0, new Vector2(460 + (int)i * 400, 360 + 2 * 250), new Vector2(100, 32)));
            }
            abilityTypes = new AbilityType[4];

            spriteFont = FontCollection.getInstance().GetFont(FontCollection.Fonttyp.MainMenuButtonFont);
            soundHorn = SoundCollection.getInstance().GetSoundInstance(SoundType.Car_Horn);


        }

        public void Draw()
        {
            spriteBatch.Begin();

            int i = 0;

            //Player 
            foreach (RotRectangle rotRectangle in playerBoxes)
            {
                //spriteBatch.DrawString(spriteFont, "Play", rotRectangle.Center, Color.Black);
                switch (i)
                {
                    case 0:
                        boxConers = rotRectangle.Corners;
                        spriteBatch.DrawString(spriteFont, "Player 1", boxConers[2], Color.Blue);   
                        i++;
                        break;
                    case 1:
                        boxConers = rotRectangle.Corners;
                        spriteBatch.DrawString(spriteFont, "Player 2", boxConers[2], Color.Red);   
                        i++;
                        break;
                    case 2:
                        boxConers = rotRectangle.Corners;
                        spriteBatch.DrawString(spriteFont, "Player 3", boxConers[2], Color.Green);   
                        i++;
                        break;
                    case 3:
                        boxConers = rotRectangle.Corners;
                        spriteBatch.DrawString(spriteFont, "Player 4", boxConers[2], Color.Pink);   
                        i++;
                        break;
                }
            }

            //Description 
            i = 0; //Clear i
            foreach (RotRectangle rotRectangle in selectionDescription)
            {
                //spriteBatch.DrawString(spriteFont, "Play", rotRectangle.Center, Color.Black);
                switch (i)
                {
                    case 0:
                        boxConers = rotRectangle.Corners;
                        spriteBatch.DrawString(spriteFont, "Car Type", boxConers[2], Color.Black);
                        i++;
                        break;
                    case 1:
                        boxConers = rotRectangle.Corners;
                        spriteBatch.DrawString(spriteFont, "Front", boxConers[2], Color.Black);
                        i++;
                        break;
                    case 2:
                        boxConers = rotRectangle.Corners;
                        spriteBatch.DrawString(spriteFont, "Abilities", boxConers[2], Color.Black);
                        i++;
                        break;
                }
            }

            //Selection Car
            i = 0;
            foreach (RotRectangle rotRectangle in carSelectionBoxes)
            {
                //spriteBatch.DrawString(spriteFont, "Play", rotRectangle.Center, Color.Black);
                switch (i)
                {
                    case 0:
                        boxConers = rotRectangle.Corners;
                        showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Car_Medium); ;
                        spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.Cornsilk);
                        i++;
                        break;
                    case 1:
                        boxConers = rotRectangle.Corners;
                        showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Car_Small); ;
                        spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.Cornsilk);
                        i++;
                        break;
                    case 2:
                        boxConers = rotRectangle.Corners;
                        showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Car_Big); ;
                        spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.Cornsilk);
                        i++;
                        break;
                    case 3:
                        boxConers = rotRectangle.Corners;
                        showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Cargo); ;
                        spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.Cornsilk);
                        i++;
                        break;
                }
            }

            //Selection Front
            //TODO: Scale Texture spriteBatch.Draw(texture, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.0f, SpriteEffects.None, 0f);
            i = 0;
            foreach (RotRectangle rotRectangle in frontSelectionBoxes)
                {
                    //spriteBatch.DrawString(spriteFont, "Play", rotRectangle.Center, Color.Black);
                    switch (i)
                    {
                        case 0:
                            boxConers = rotRectangle.Corners;
                            showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Front_Bumper); ;
                            spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.Cornsilk);
                            i++;
                            break;
                        case 1:
                            boxConers = rotRectangle.Corners;
                            showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Front_Big_Spikes); ;
                            spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.Cornsilk);
                            i++;
                            break;
                        case 2:
                            boxConers = rotRectangle.Corners;
                            showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Front_Spikes); ;
                            spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.Cornsilk);
                            i++;
                            break;
                        case 3:
                            boxConers = rotRectangle.Corners;
                            showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Front_Big_Bumper); ;
                            spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.Cornsilk);
                            i++;
                            break;
                    }
                }

            //Selection Abilities
            i = 0;
            foreach (RotRectangle rotRectangle in abilitiesSelectionBoxes)
                    {
                        //spriteBatch.DrawString(spriteFont, "Play", rotRectangle.Center, Color.Black);
                        switch (i)
                        {
                            case 0:
                                boxConers = rotRectangle.Corners;
                                showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Active_FlameThrower); ;
                                spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.Cornsilk);
                                i++;
                                break;
                            case 1:
                                boxConers = rotRectangle.Corners;
                                showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Active_RocketLauncher); ;
                                spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.Cornsilk);
                                i++;
                                break;
                            case 2:
                                boxConers = rotRectangle.Corners;
                                showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Active_Shockwave); ;
                                spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.Cornsilk);
                                i++;
                                break;
                            case 3:
                                boxConers = rotRectangle.Corners;
                                showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Active_Trap); ;
                                spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.Cornsilk);
                                i++;
                                break;
                        }
                    }

            spriteBatch.End();
        }

        public void Update()
        {
            this.Input();
        }

        public void Input()
        {

            if (GamePad.GetCapabilities(PlayerIndex.One).IsConnected)
            {
                GamePadState state = GamePad.GetState(PlayerIndex.One);

                if (state.ThumbSticks.Left.Y < 0f && previousState.ThumbSticks.Left.Y == 0)
                {
                    
                }
                if (state.ThumbSticks.Left.Y > 0f && previousState.ThumbSticks.Left.Y == 0)
                {
                    
                }
                //May go in the end
                if (state.IsButtonDown(Buttons.B))
                {
                    soundHorn.Play();
                    theGame.GameState = GameState.MenuMain;
                }
                previousState = state;
            }
            else //use Keyboard
            {
                KeyboardState keyboardstate = Keyboard.GetState();
                //Up
                if (keyboardstate.IsKeyDown(Keys.W) && previousKeyBoardState.IsKeyUp(Keys.W) || keyboardstate.IsKeyDown(Keys.Up) && previousKeyBoardState.IsKeyUp(Keys.Up))
                {
                    
                }

                //Down
                if (keyboardstate.IsKeyDown(Keys.S) && previousKeyBoardState.IsKeyUp(Keys.S) || keyboardstate.IsKeyDown(Keys.Down) && previousKeyBoardState.IsKeyUp(Keys.Down))
                {
                    
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Back))
                {
                    soundHorn.Play();
                    theGame.GameState = GameState.MenuMain;
                }
                previousKeyBoardState = keyboardstate;
            }
            
        }
    }
}
