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
    public class MainMenu
    {
        private List<RotRectangle> ButtonHitboxes; //Button as Hitbox for Selection, 
        private Texture2D MainMenuBackground;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private RotRectangle carrierBox;
        //private RotRectangle carBox;
        private Texture2D carrierTexture;
        //private Texture2D carTexture;
        private Vector2[] carrierBoxConers;
        //private Vector2[] carBoxCorners;
        private Vector2[] buttonBoxCorners;
        private SoundEffectInstance soundHorn;
        private Game1 theGame;
        private KeyboardState previousKeyBoardState;
        private GamePadState previousState;

        public MainMenu(SpriteBatch spriteBatchInit,Vector2 screenSize, ContentManager contentManager, Game1 game)
        {
            spriteBatch = spriteBatchInit;
            theGame = game;
            previousKeyBoardState = Keyboard.GetState();

            //Boxes
            //Create Buttons 
            ButtonHitboxes = new List<RotRectangle>(); 
            for(int i = 0; i<4; i++)
            {
                ButtonHitboxes.Add(new RotRectangle(0, new Vector2(450, 360 + (int)i * 100), new Vector2(100, 32)));
            }
            //Selectionboxes
            carrierBox = new RotRectangle(0, new Vector2(300, 360), new Vector2(300, 55));
            
            //carBox = new RotRectangle(1.57f, new Vector2(160, 420), new Vector2(50, 50));
            //carBoxCorners = carBox.Corners;
            
            //Texture
            //Set Background 
            //TextureCollection.getInstance().loadTextures(contentManager);
            MainMenuBackground = TextureCollection.getInstance().GetTexture(TextureType.MainMenuBackground);
            carrierTexture = TextureCollection.getInstance().GetTexture(TextureType.MainMenuCarrier);
            //carTexture = TextureCollection.getInstance().GetTexture(TextureType.Car_Medium);

            //Set font for Buttontext 
            FontCollection.getInstance().LoadFonts(contentManager);
            spriteFont = FontCollection.getInstance().GetFont(FontCollection.Fonttyp.MainMenuButtonFont);

           soundHorn = SoundCollection.getInstance().GetSoundInstance(SoundType.Car_Horn);
        }

        //Draw the Menu 
        public void Draw()
        {
            spriteBatch.Begin();

            //Draw Background and Selection
            //spriteBatch.Draw(texture, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(MainMenuBackground, new Vector2(0, 0), Color.Cornsilk);
            carrierBoxConers = carrierBox.Corners;
            spriteBatch.Draw(carrierTexture, carrierBoxConers[2], Color.Cornsilk);
            //spriteBatch.Draw(carTexture, carBox.Center - carBox.Offset, null, Color.Cornsilk, carBox.RotationRad, carBox.Offset, 1.0f, SpriteEffects.None, 0f);

            //Draw Buttons
            int i = 0;
            foreach(RotRectangle rotRectangle in ButtonHitboxes)
            {
                //spriteBatch.DrawString(spriteFont, "Play", rotRectangle.Center, Color.Black);
                switch (i)
                {
                    case 0:
                        buttonBoxCorners = rotRectangle.Corners;
                        spriteBatch.DrawString(spriteFont, "Play", buttonBoxCorners[2], Color.Black);   //rotRectangle.Center, Color.Black);
                        i++;
                        break;
                    case 1:
                        buttonBoxCorners = rotRectangle.Corners;
                        spriteBatch.DrawString(spriteFont, "Controls", buttonBoxCorners[2], Color.Black);
                        i++;
                        break;
                    case 2:
                        buttonBoxCorners = rotRectangle.Corners;
                        spriteBatch.DrawString(spriteFont, "Credits", buttonBoxCorners[2], Color.Black);
                        i++;
                        break;
                    case 3:
                        buttonBoxCorners = rotRectangle.Corners;
                        spriteBatch.DrawString(spriteFont, "Exit", buttonBoxCorners[2], Color.Black);
                        i++;
                        break;
                }   
                
            }
            
            spriteBatch.End();
        }
     
        public void Update()
        {
            //TODO: Gamepad Input
            //Check which Button is selected 
            this.Input();
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
            {
                //Play
                if (CollisionCheck.CheckCollision(carrierBox, ButtonHitboxes[0]) == true)
                {
                    soundHorn.Play();
                    theGame.GameState = GameState.MenuModificationSelection;
                }
                //Options (Non existent atm)
                if (CollisionCheck.CheckCollision(carrierBox, ButtonHitboxes[1]) == true)
                {
                    theGame.GameState = GameState.MenuControls;
                }

                //Credits (non existent atm)
                //if (CollisionCheck.CheckCollision(carrierBox, ButtonHitboxes[2]) == true)
                //{
                //    return stateofgame = GameState.Playing;
                //}
                //Exit
                if (CollisionCheck.CheckCollision(carrierBox, ButtonHitboxes[3]) == true)
                {
                    soundHorn.Play();
                    theGame.GameState = GameState.Exit;
                }
            }
        }

        private void Input()
        {
            if (GamePad.GetCapabilities(PlayerIndex.One).IsConnected)
            {
                GamePadState state = GamePad.GetState(PlayerIndex.One);

                if (state.ThumbSticks.Left.Y < 0f && previousState.ThumbSticks.Left.Y == 0)
                {
                    if (carrierBox.Center.Y >= ButtonHitboxes[3].Center.Y)
                    { return; }
                    else
                    {
                        carrierBox.Move(new Vector2(0, 100));
                    }
                }
                if (state.ThumbSticks.Left.Y > 0f && previousState.ThumbSticks.Left.Y == 0)
                {
                    if (carrierBox.Center.Y <= ButtonHitboxes[0].Center.Y)
                    { return; }
                    else
                    {
                        carrierBox.Move(new Vector2(0, -100));
                    }
                }

                previousState = state;
            }
            else //use Keyboard
            {
                KeyboardState keyboardstate = Keyboard.GetState();
                //Up
                if (keyboardstate.IsKeyDown(Keys.W) && previousKeyBoardState.IsKeyUp(Keys.W) || keyboardstate.IsKeyDown(Keys.Up) && previousKeyBoardState.IsKeyUp(Keys.Up))
                {
                    if (carrierBox.Center.Y <= ButtonHitboxes[0].Center.Y)
                    { return; }
                    else
                    {
                        carrierBox.Move(new Vector2(0, -100));
                    }
                }

                //Down
                if (keyboardstate.IsKeyDown(Keys.S) && previousKeyBoardState.IsKeyUp(Keys.S) || keyboardstate.IsKeyDown(Keys.Down) && previousKeyBoardState.IsKeyUp(Keys.Down))
                {
                    if (carrierBox.Center.Y >= ButtonHitboxes[3].Center.Y)
                    { return; }
                    else
                    {
                        carrierBox.Move(new Vector2(0, 100));
                    }
                }
                previousKeyBoardState = keyboardstate;
            }
        }

    }
}

//public void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color);
//spriteBatch.Draw(texture, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.0f, SpriteEffects.None, 0f);
//public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth);
//public void Draw(Texture2D texture, Vector2 position, Color color);
//new Vector2(400 + (int)index * 100, 400) spawn car

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace CarGo
{
*/
