using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarGo
{
    class LobbySearch : Menu
    {
        //2 InputFelder: IP adresse und Code.  Dazu 2 Buttons zum bestätigen
        //Liste mit verfügbaren Servern, hoch und runterscrollbar, Server auswählen zum joinen
        private Texture2D background;
        private Texture2D carrierTexture;
        private SpriteFont spriteFont;

        private Texture2D playerBox;
        private string inputIP;
        private string inputCode;

        public LobbySearch(SpriteBatch spriteBatchInit, Game1 game):base(spriteBatchInit,game,3)
        {
            background = TextureCollection.Instance.GetTexture(TextureType.Menu_Background);
            carrierTexture = TextureCollection.Instance.GetTexture(TextureType.MainMenuCarrier);
            HUD.graphicsDevice = spriteBatchInit.GraphicsDevice;
            spriteFont = FontCollection.Instance.GetFont(FontCollection.Fonttyp.MainMenuButtonFont);
            playerBox = HUD.createLifebar(playerBox, 600, 400, 0, 2, Color.Transparent, Color.Transparent, Color.Black);

            buttons = new List<Vector2>();
            buttons.Add(new Vector2(200, 300));
            buttons.Add(new Vector2(200, 400));
            buttons.Add(new Vector2(200, 500));
            buttons.Add(new Vector2(200, 600));

            texts = new string[]{
            "Join with IP",
            "Join with InviteCode",
            ""
            }
            ;
            

        }

        public void Update()
        {
            Network.NetworkThread.Instance.ConnectToServer("localhost");
            StateMachine.Instance.ChangeState(GameState.OnlineLobby);
            base.Update();
        }

        private void PasteIP()
        {
            string text = Clipboard.GetText();
            inputIP = text;
        }

        private void PasteCode()
        {
            string text = Clipboard.GetText();
            inputCode = text;
        }

        protected override void Back(int clientID, InputController inputController)
        {
            throw new NotImplementedException();
        }

        protected override void ConfirmSelection(int clientID, InputController inputController)
        {
            throw new NotImplementedException();
        }

        internal void Draw()
        {
            

        }
    }
}
