using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo
{
    public enum GameState { Playing, MenuMain, MenuModificationSelection, MenuPause, MenuLost, MenuWon, LevelEditor, Exit, MenuControls, CreditScreen, MenuSettings, LaunchMenu, OnlineLobby, SearchLobby, WaitForServerStart}
    public class StateMachine
    {
        public bool networkGame;
        public GameState gameState;
        private GameState previousState;
        private static StateMachine instance;
        public static StateMachine Instance
        {
            get
            {
                if (instance == null) instance = new StateMachine();
                return instance;
            }
        }

        
        private StateMachine()
        {
            gameState = GameState.MenuMain;
            previousState = GameState.MenuMain;
        }

        public void ChangeState(GameState gameState)
        {
            previousState = this.gameState;
            this.gameState = gameState;
            if (networkGame)
            {
                if(gameState == GameState.MenuModificationSelection || gameState == GameState.MenuPause ||gameState == GameState.Playing || gameState == GameState.MenuWon || gameState == GameState.MenuLost)
                    Network.NetworkThread.Instance.BroadCastNewGameState(gameState);
            }
        }

        public void RemoteChangeState(GameState gameState)
        {
            previousState = this.gameState;
            this.gameState = gameState;
        }

        public void Back()
        {
            GameState state = gameState;
            gameState = previousState;
            previousState = state;
        }
    }
}
