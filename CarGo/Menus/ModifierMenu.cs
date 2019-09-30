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
    public class ModifierMenu
    {
        private List<RotRectangle> playerBoxes;
        private List<RotRectangle> selectionDescription;
        private List<RotRectangle> carSelectionBoxes; //for drawing
        private List<RotRectangle> frontSelectionBoxes; //for drawing
        private List<RotRectangle> abilitiesSelectionBoxes; //for drawing 
        //List Stage: if all 3 then all ready
        //List Player: which is connected
        private int[] currentStage;
        private bool[] gamePadConnected;
        private CarType[] carTypes;
        private CarFrontType[] frontTypes;
        private AbilityType[] abilityTypes; 
        private Texture2D showSelectionTexture;
        private Texture2D background;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private Vector2[] boxConers;
        private Game1 theGame;
        private GamePadState[] previousState;
        private SoundEffectInstance soundHorn;
        private Texture2D selectionBoxBox;
        public ModifierMenu(SpriteBatch spriteBatchInit, Game1 game )
        {
            spriteBatch = spriteBatchInit;
            theGame = game;
            background = TextureCollection.getInstance().GetTexture(TextureType.Menu_Background);
            selectionBoxBox = TextureCollection.getInstance().GetTexture(TextureType.Menu_Selection_BoxBox);

            //GamePads connected
            gamePadConnected = new bool[4];
            currentStage = new int[4];
            for (int i = 0; i < 4; i++)
            {
                if(i==0) currentStage[i] = -1;
                else currentStage[i] = 0;
            }
            carTypes = new CarType[4];
            frontTypes = new CarFrontType[4];
            abilityTypes = new AbilityType[4];
            previousState = new GamePadState[4];
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

        public void Reset()
        {
            for (PlayerIndex index = PlayerIndex.One; index <= PlayerIndex.Four; index++)
            {
                currentStage[(int)index] = 0;
            }
        }

        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Vector2(0,0), Color.White);
            int i = 0;

            //Player 
            foreach (RotRectangle rotRectangle in playerBoxes)
            {
                //spriteBatch.DrawString(spriteFont, "Play", rotRectangle.Center, Color.Black);
                if(gamePadConnected[i])
                {
                    switch (i)
                    {
                        case 0:
                            boxConers = rotRectangle.Corners;
                            spriteBatch.DrawString(spriteFont, "Player 1", boxConers[2], Color.Blue);
                            break;
                        case 1:
                            boxConers = rotRectangle.Corners;
                            spriteBatch.DrawString(spriteFont, "Player 2", boxConers[2], Color.Red);
                            break;
                        case 2:
                            boxConers = rotRectangle.Corners;
                            spriteBatch.DrawString(spriteFont, "Player 3", boxConers[2], Color.Green);
                            break;
                        case 3:
                            boxConers = rotRectangle.Corners;
                            spriteBatch.DrawString(spriteFont, "Player 4", boxConers[2], Color.Pink);
                            break;
                    }
                }
                i++;
                
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
                            break;
                        case 1:
                            boxConers = rotRectangle.Corners;
                            spriteBatch.DrawString(spriteFont, "Front", boxConers[2], Color.Black);
                            break;
                        case 2:
                            boxConers = rotRectangle.Corners;
                            spriteBatch.DrawString(spriteFont, "Abilities", boxConers[2], Color.Black);
                            break;
                    }
                i++;
            }

            //Selection Car
            i = 0;
            foreach (RotRectangle rotRectangle in carSelectionBoxes)
            {
                //spriteBatch.DrawString(spriteFont, "Play", rotRectangle.Center, Color.Black);
                if (gamePadConnected[i])
                {

                    switch (carTypes[i])
                    {
                        case CarType.Medium:
                            boxConers = rotRectangle.Corners;
                            showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Car_Medium); ;
                            spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.White);
                            break;
                        case CarType.Small:
                            boxConers = rotRectangle.Corners;
                            showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Car_Small); ;
                            spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.White);
                            break;
                        case CarType.Big:
                            boxConers = rotRectangle.Corners;
                            showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Car_Big); ;
                            spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.White);
                            break;
                    }
                }
                i++;
            }

            //Selection Front
            //TODO: Scale Texture spriteBatch.Draw(texture, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.0f, SpriteEffects.None, 0f);
            i = 0;
            foreach (RotRectangle rotRectangle in frontSelectionBoxes)
            {
                //spriteBatch.DrawString(spriteFont, "Play", rotRectangle.Center, Color.Black);

                if (gamePadConnected[i])
                {
                    switch (frontTypes[i])
                    {
                        case CarFrontType.Bumper:
                            boxConers = rotRectangle.Corners;
                            showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Front_Bumper); ;
                            spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.White);
                            break;
                        case CarFrontType.Spikes:
                            boxConers = rotRectangle.Corners;
                            showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Front_Big_Spikes); ;
                            spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.White);
                            break;
                    }
                }
                i++;
            }

            //Selection Abilities
            i = 0;
            foreach (RotRectangle rotRectangle in abilitiesSelectionBoxes)
            {
                //spriteBatch.DrawString(spriteFont, "Play", rotRectangle.Center, Color.Black);

                if (gamePadConnected[i])
                {
                    switch (abilityTypes[i])
                    {
                        case AbilityType.Flamethrower:
                            boxConers = rotRectangle.Corners;
                            showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Menu_Select_Flamethrower); ;
                            spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.White);
                            break;
                        case AbilityType.RocketLauncher:
                            boxConers = rotRectangle.Corners;
                            showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Active_RocketLauncher); ;
                            spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.White);
                            break;
                        case AbilityType.Shockwave:
                            boxConers = rotRectangle.Corners;
                            showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Menu_Select_Shockwave); ;
                            spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.White);
                            break;
                        case AbilityType.TrapLauncher:
                            boxConers = rotRectangle.Corners;
                            showSelectionTexture = TextureCollection.getInstance().GetTexture(TextureType.Active_Trap); ;
                            spriteBatch.Draw(showSelectionTexture, boxConers[2], Color.White);
                            break;
                    }
                }
                i++;
            }

            
            for (PlayerIndex index = PlayerIndex.One; index <= PlayerIndex.Four; index++)
            {
                if (gamePadConnected[(int)index])
                {
                    switch(currentStage[(int)index])
                    {
                        case 0:
                            spriteBatch.Draw(selectionBoxBox, carSelectionBoxes[(int)index].Corners[2] - new Vector2(Math.Abs(carSelectionBoxes[(int)index].Offset.X/2), Math.Abs(carSelectionBoxes[(int)index].Offset.Y)), Color.White);
                            break;
                        case 1:
                            //boxConers = frontSelectionBoxes[(int)index].Corners;
                            //spriteBatch.Draw(selectionBoxBox, boxConers[2], Color.White);
                            spriteBatch.Draw(selectionBoxBox, frontSelectionBoxes[(int)index].Corners[2] - new Vector2(Math.Abs(frontSelectionBoxes[(int)index].Offset.X/2), Math.Abs(frontSelectionBoxes[(int)index].Offset.Y)), Color.White);

                            break;
                        case 2:
                            //boxConers = abilitiesSelectionBoxes[(int)index].Corners;
                            //spriteBatch.Draw(selectionBoxBox, boxConers[2], Color.White);
                            spriteBatch.Draw(selectionBoxBox, abilitiesSelectionBoxes[(int)index].Corners[2] - new Vector2(Math.Abs(abilitiesSelectionBoxes[(int)index].Offset.X/2), Math.Abs(abilitiesSelectionBoxes[(int)index].Offset.Y/2)), Color.White);

                            break;
                        default:
                            break;
                    }
                    
                    
                }
            }


            spriteBatch.End();
        }

        public void Update()
        {
            this.Input();
            if(CheckReady())
            {
                for (PlayerIndex index = PlayerIndex.One; index <= PlayerIndex.Four; index++)
                {
                    if (gamePadConnected[(int)index])
                    {
                        theGame.scene.addPlayer(index, new Vector2(400 + (int)index * 100, 400), carTypes[(int)index], frontTypes[(int)index], abilityTypes[(int)index]);
                        theGame.GameState = GameState.Playing;
                    }
                }
            }
        }

        public void Input()
        {

            for (PlayerIndex index = PlayerIndex.One; index <= PlayerIndex.Four; index++)
            {
                
                if (gamePadConnected[(int)index])
                {
                    GamePadState state = GamePad.GetState(index);

                    if ((state.ThumbSticks.Left.X < 0f && previousState[(int)index].ThumbSticks.Left.X == 0) || (state.IsButtonDown(Buttons.DPadLeft) && previousState[(int)index].IsButtonUp(Buttons.DPadLeft))) 
                    {

                        switch(currentStage[(int)index])
                        {
                            case 0:
                                if ((int)carTypes[(int)index] > 0) carTypes[(int)index]--;
                                else carTypes[(int)index] = CarType.Big;
                                break;
                            case 1:
                                if ((int)frontTypes[(int)index] > 0) frontTypes[(int)index]--;
                                else frontTypes[(int)index] = CarFrontType.Bumper;
                                break;
                            case 2:
                                if ((int)abilityTypes[(int)index] > 0) abilityTypes[(int)index]--;
                                else abilityTypes[(int)index] = AbilityType.TrapLauncher;
                                break;
                        }
                    }
                    if ((state.ThumbSticks.Left.X > 0f && previousState[(int)index].ThumbSticks.Left.X == 0) || (state.IsButtonDown(Buttons.DPadRight) && previousState[(int)index].IsButtonUp(Buttons.DPadRight)))
                    {
                        switch (currentStage[(int)index])
                        {
                            case 0:
                                if (carTypes[(int)index] != CarType.Big) carTypes[(int)index]++;
                                else carTypes[(int)index] = CarType.Small;
                                break;
                            case 1:
                                if (frontTypes[(int)index] != CarFrontType.Bumper) frontTypes[(int)index]++;
                                else frontTypes[(int)index] = CarFrontType.Spikes;
                                break;
                            case 2:
                                if (abilityTypes[(int)index] != AbilityType.TrapLauncher) abilityTypes[(int)index]++;
                                else abilityTypes[(int)index] = AbilityType.Flamethrower;
                                break;
                        }
                    }

                    if(state.IsButtonDown(Buttons.A) && previousState[(int)index].IsButtonUp(Buttons.A))
                    {
                        if (currentStage[(int)index] < 3) currentStage[(int)index]++;
                    }

                    if (state.IsButtonDown(Buttons.B) && previousState[(int)index].IsButtonUp(Buttons.B))
                    {
                        if (currentStage[(int)index] > 0) currentStage[(int)index]--;
                    }

                    if(state.IsButtonDown(Buttons.Back) && previousState[(int)index].IsButtonUp(Buttons.Back))
                    {
                        theGame.GameState = GameState.MenuMain;
                    }

                    previousState[(int)index] = state;
                }




            }


            
            //if (GamePad.GetCapabilities(PlayerIndex.One).IsConnected)
            //{
            //    GamePadState state = GamePad.GetState(PlayerIndex.One);

            //    if (state.ThumbSticks.Left.Y < 0f && previousState.ThumbSticks.Left.Y == 0)
            //    {
                    
            //    }
            //    if (state.ThumbSticks.Left.Y > 0f && previousState.ThumbSticks.Left.Y == 0)
            //    {
                    
            //    }
            //    //May go in the end
            //    if (state.IsButtonDown(Buttons.B))
            //    {
            //        soundHorn.Play();
            //        theGame.GameState = GameState.MenuMain;
            //    }
            //    previousState = state;
            //}
            //else //use Keyboard
            //{
            //    KeyboardState keyboardstate = Keyboard.GetState();
            //    //Up
            //    if (keyboardstate.IsKeyDown(Keys.W) && previousKeyBoardState.IsKeyUp(Keys.W) || keyboardstate.IsKeyDown(Keys.Up) && previousKeyBoardState.IsKeyUp(Keys.Up))
            //    {
                    
            //    }

            //    //Down
            //    if (keyboardstate.IsKeyDown(Keys.S) && previousKeyBoardState.IsKeyUp(Keys.S) || keyboardstate.IsKeyDown(Keys.Down) && previousKeyBoardState.IsKeyUp(Keys.Down))
            //    {
                    
            //    }
            //    if (Keyboard.GetState().IsKeyDown(Keys.Back))
            //    {
            //        soundHorn.Play();
            //        theGame.GameState = GameState.MenuMain;
            //    }
            //    previousKeyBoardState = keyboardstate;
            //}
            
        }

        public bool CheckReady()
        {
            bool ready = true;
            for (PlayerIndex index = PlayerIndex.One; index <= PlayerIndex.Four; index++)
            {
                if(gamePadConnected[(int)index] && currentStage[(int)index]!=3)
                {
                    ready = false;
                }
            }
            return ready;

        }
    }
}
