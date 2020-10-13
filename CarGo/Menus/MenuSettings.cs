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
    public class MenuSettings:Menu
    {
        private Texture2D settingsBackground;
        private SpriteFont spriteFont;
        private List<Vector2> positions;
        private Texture2D volumeSoundBar;
        private Texture2D volumeMusicBar;
        private Texture2D carrierTexture;

        private String[] texts;
        private bool inputMode;
        private string inputName;
        private Keys[] lastKeys;

        public MenuSettings(SpriteBatch spriteBatchInit, Game1 game):base(spriteBatchInit,game,7)
        {
            settingsBackground = TextureCollection.Instance.GetTexture(TextureType.MainMenuBackground);
            spriteFont = FontCollection.Instance.GetFont(FontCollection.Fonttyp.MainMenuButtonFont);

            volumeMusicBar= HUD.createLifebar(volumeMusicBar, 300, 50, Settings.Instance.VolumeMusic * 100,0, new Color(42, 64, 28), Color.Transparent, Color.Transparent);
            volumeSoundBar= HUD.createLifebar(volumeSoundBar, 300, 50, Settings.Instance.VolumeSound * 100, 0, new Color(42, 64, 28), Color.Transparent, Color.Transparent);
            carrierTexture = TextureCollection.Instance.GetTexture(TextureType.MainMenuCarrier);
            inputName = Settings.Instance.PlayerName;
            lastKeys = Keyboard.GetState().GetPressedKeys();

            texts = new String[7];
            texts[0] = "Difficulty";
            texts[1] = "";
            texts[2] = "Music";
            texts[3] = "Sounds";
            texts[4] = "Name";
            texts[5] = "Confirm";
            texts[6] = "Cancel";

            switch (Settings.Instance.Difficulty)
            {
                case Difficulty.Noob:
                    texts[1] = "Noob";
                    break;
                case Difficulty.Easy:
                    texts[1] = "Easy";
                    break;
                case Difficulty.Normal:
                    texts[1] = "Normal";
                    break;
                case Difficulty.Hard:
                    texts[1] = "Hard";
                    break;
                case Difficulty.Extreme:
                    texts[1] = "Extreme";
                    break;
            }


            positions = new List<Vector2>();

            positions.Add(new Vector2(300,100));
            positions.Add(new Vector2(300,200));
            positions.Add(new Vector2(300,300));
            positions.Add(new Vector2(300,500));
            positions.Add(new Vector2(300,700));
            positions.Add(new Vector2(300, 850));
            positions.Add(new Vector2(300, 950));
        }

        public void Update()
        {
            base.Update();
            if (inputMode)
            {
                Keys[] keys = Keyboard.GetState().GetPressedKeys();
                foreach (Keys key in keys)
                {
                    if(key!=Keys.LeftShift && key != Keys.RightShift)
                    if (!lastKeys.Contains(key))
                    {
                        inputName += InputHandler.KeyToString(key, keys.Contains(Keys.LeftShift) || keys.Contains(Keys.RightShift));
                        if (key == Keys.Back) if (inputName.Length == 1) inputName = inputName.Substring(0, inputName.Length - 1); else inputName = inputName.Substring(0, inputName.Length - 2);
                    }
                }
                lastKeys = keys;
            }
            
        }

        public void Draw()
        {
            spriteBatch.Begin();

            spriteBatch.Draw(settingsBackground, new Vector2(0, 0), Color.White);

            for (int i = 0; i < numButtons; i++)
            {
                spriteBatch.DrawString(spriteFont, texts[i], positions[i], Color.Black);
            }

            spriteBatch.Draw(volumeMusicBar, positions[2] + new Vector2(0,60), Color.White);
            spriteBatch.Draw(volumeSoundBar, positions[3] + new Vector2(0, 60), Color.White);

            if (inputName != null && inputName.Length > 0) spriteBatch.DrawString(spriteFont, inputName, positions[4] + new Vector2(40, 60), Color.Black);

            spriteBatch.Draw(carrierTexture, positions[stage] + new Vector2(-300, -25), Color.White);

            spriteBatch.End();
        }

        protected override void Up(int clientID, InputController inputController)
        {
            if(!inputMode)base.Up(clientID, inputController);
        }

        protected override void Down(int clientID, InputController inputController)
        {
            if (!inputMode) base.Down(clientID, inputController);
        }

        protected override void Back(int clientID, InputController inputController)
        {
            inputMode = false;
            stage = 6;
        }

        protected override void ConfirmSelection(int clientID, InputController inputController)
        {
            switch (stage)
            {
                case 4:
                    if (!inputMode) inputMode = true;
                    else
                    {
                        if (inputName != null && inputName.Length > 0) Settings.Instance.PlayerName = inputName;
                        inputMode = false;
                    }
                    break;
                case 5:
                    Settings.Instance.saveSettings();
                    StateMachine.Instance.Back();
                    stage = 0;
                    break;
                case 6:
                    Settings.Instance.loadSettings();
                    volumeMusicBar = HUD.createLifebar(volumeMusicBar, 300, 50, Settings.Instance.VolumeMusic * 100, 0, new Color(42, 64, 28), Color.Transparent, Color.Transparent);
                    volumeSoundBar = HUD.createLifebar(volumeSoundBar, 300, 50, Settings.Instance.VolumeSound * 100, 0, new Color(42, 64, 28), Color.Transparent, Color.Transparent);
                    theGame.UpdateMusicVolume();
                    theGame.scene.UpdateAllVolumes();
                    StateMachine.Instance.Back();
                    stage = 0;
                    break;
                
                default:
                    stage = 5;
                    break;

            }
        }

        protected override void Right(int clientID, InputController inputController)
        {
            if (stage == 0)
            {
                switch (Settings.Instance.Difficulty)
                {
                    case Difficulty.Noob:
                        Settings.Instance.Difficulty = Difficulty.Easy;
                        texts[1] = "Easy";
                        break;
                    case Difficulty.Easy:
                        Settings.Instance.Difficulty = Difficulty.Normal;
                        texts[1] = "Normal";
                        break;
                    case Difficulty.Normal:
                        Settings.Instance.Difficulty = Difficulty.Hard;
                        texts[1] = "Hard";
                        break;
                    case Difficulty.Hard:
                        Settings.Instance.Difficulty = Difficulty.Extreme;
                        texts[1] = "Extreme";
                        break;
                    case Difficulty.Extreme:
                        Settings.Instance.Difficulty = Difficulty.Noob;
                        texts[1] = "Noob";
                        break;
                }
            }
            if (stage == 2)
            {
                Settings.Instance.VolumeMusic += 0.05f;
                theGame.UpdateMusicVolume();
                volumeMusicBar = HUD.createLifebar(volumeMusicBar, 300, 50, Settings.Instance.VolumeMusic * 100, 0, new Color(42, 64, 28), Color.Transparent, Color.Transparent);
            }
            if (stage == 3)
            {
                Settings.Instance.VolumeSound += 0.05f;
                theGame.scene.UpdateAllVolumes();
                volumeSoundBar = HUD.createLifebar(volumeSoundBar, 300, 50, Settings.Instance.VolumeSound * 100, 0, new Color(42, 64, 28), Color.Transparent, Color.Transparent);
            }
        }

        protected override void Left(int clientID, InputController inputController)
        {
            if (stage == 0)
            {
                switch (Settings.Instance.Difficulty)
                {
                    case Difficulty.Noob:
                        Settings.Instance.Difficulty = Difficulty.Extreme;
                        texts[1] = "Extreme";
                        break;
                    case Difficulty.Easy:
                        Settings.Instance.Difficulty = Difficulty.Noob;
                        texts[1] = "Noob";
                        break;
                    case Difficulty.Normal:
                        Settings.Instance.Difficulty = Difficulty.Easy;
                        texts[1] = "Easy";
                        break;
                    case Difficulty.Hard:
                        Settings.Instance.Difficulty = Difficulty.Normal;
                        texts[1] = "Normal";
                        break;
                    case Difficulty.Extreme:
                        Settings.Instance.Difficulty = Difficulty.Hard;
                        texts[1] = "Hard";
                        break;
                }
            }
            if (stage == 2)
            {
                Settings.Instance.VolumeMusic -= 0.05f;
                theGame.UpdateMusicVolume();
                volumeMusicBar = HUD.createLifebar(volumeMusicBar, 300, 50, Settings.Instance.VolumeMusic * 100, 0, new Color(42, 64, 28), Color.Transparent, Color.Transparent);
            }
            if (stage == 3)
            {
                Settings.Instance.VolumeSound -= 0.05f;
                theGame.scene.UpdateAllVolumes();
                volumeSoundBar = HUD.createLifebar(volumeSoundBar, 300, 50, Settings.Instance.VolumeSound * 100, 0, new Color(42, 64, 28), Color.Transparent, Color.Transparent);
            }
        }


        //when changing SoundVolume call UpdateVolume() for all Entities
    }
}
