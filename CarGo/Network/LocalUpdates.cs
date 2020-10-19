using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using Lidgren.Network.Xna;
using Microsoft.Xna.Framework;

namespace CarGo.Network
{
    public class LocalUpdates
    {
        
        Scene scene;
        StateMachine stateMachine;

        private float updatesPerSecond;
        private float timePerUpdate;
        private float timeSinceLastUpdate;
        
        LobbyOnline lobbyOnline;
        NetworkThread networkThread;
        Game1 game;
        //public List<NetIncomingMessage> incomingMessages;
        public LocalUpdates(Game1 game, LobbyOnline lobbyOnline)
        {
            this.game = game;
            this.scene = scene;
            this.lobbyOnline = lobbyOnline;
            updatesPerSecond = 100;
            timePerUpdate = 1000 / updatesPerSecond;
            timeSinceLastUpdate = 0;
            //incomingMessages = new List<NetIncomingMessage>();
        }

        public void SetNetworkThread(NetworkThread networkThread)
        {
            this.networkThread = networkThread;
        }
        public void SetScene(Scene scene)
        {
            this.scene = scene;
        }

        public void Update(GameTime gameTime)
        {
            timeSinceLastUpdate += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastUpdate >= timePerUpdate)
            {
                timeSinceLastUpdate = 0;
                SendUpdates();
            }
        }

        private void SendUpdates()
        {
            if(networkThread.isMainClient)
            {
                foreach(Entity entity in scene.entities)
                {
                    if(entity.entityType != EntityType.Player)
                        networkThread.BroadCastEntityUpdate(ObjectMessageType.UpdatePosition, entity);
                    
                }
            }
            foreach(Player player in scene.localPlayers)
            {
                networkThread.BroadCastEntityUpdate(ObjectMessageType.UpdatePosition, player);
            }
        }

        public void ParseUnconnectedMessage(NetIncomingMessage im)
        {
            var type = (MessageType)im.ReadByte();
            if(type== MessageType.ServerInList)
            {
                long key = im.ReadInt64();
                CarGoServer.ServerData serverData= new CarGoServer.ServerData();
                im.ReadAllFields(serverData);
                game.lobbySearch.AddServerData(serverData);
            }
            if(type== MessageType.ServerOnline)
            {
                networkThread.serverRunning = true;
            }
        }
        public void ParseMessage(NetIncomingMessage im)
        {
            //Parse and Apply all Changes
            var del = im.ReadByte();
            var type = im.ReadByte();
            CarGoServer.ServerData serverData= new CarGoServer.ServerData();
            switch ((MessageType)type)
            {
                case MessageType.GameState:
                    GameState newState = (GameState)im.ReadByte();
                    StateMachine.Instance.RemoteChangeState(newState);
                    //Update GameState

                    break;
                case MessageType.ObjectUpdate:
                    var objectMessageType = (ObjectMessageType)im.ReadByte();
                    var objectID = im.ReadInt32();
                    Vector2 center;
                    int clientID;
                    switch (objectMessageType)
                    {
                        case ObjectMessageType.PlayerSpawn:
                            clientID = im.ReadInt32();
                            center = XNAExtensions.ReadVector2(im);
                            CarType carType = (CarType)im.ReadByte();
                            CarFrontType carFrontType = (CarFrontType)im.ReadByte();
                            AbilityType abilityType = (AbilityType)im.ReadByte();
                            scene.RemoteAddPlayer(clientID, center, objectID, carType, carFrontType, abilityType, lobbyOnline.GetOnlinePlayer(clientID));
                            break;
                        case ObjectMessageType.Spawn:
                            EntityType entityType = (EntityType)im.ReadByte();
                            center = XNAExtensions.ReadVector2(im);
                            scene.RemoteAddEntity(entityType, center, objectID);
                            break;
                        case ObjectMessageType.Despawn:
                            scene.RemoteRemoveEntity(objectID);
                            break;
                        case ObjectMessageType.UpdatePosition:
                            center = XNAExtensions.ReadVector2(im);
                            float rotation = im.ReadFloat();
                            Vector2 velocity = XNAExtensions.ReadVector2(im);
                            scene.RemoteUpdatePosition(objectID, center, rotation, velocity);
                            break;
                        case ObjectMessageType.StateChange:
                            break;
                        case ObjectMessageType.UpdateHitpoints:
                            int newHitpoints = im.ReadInt32();
                            scene.RemoteUpdateHitpoints(objectID, newHitpoints);
                            break;
                    }
                    break;
                case MessageType.ReceiveClientNumber:
                    clientID = im.ReadInt32();
                    ID_Manager.Instance.ClientNumber = clientID;
                    lobbyOnline.AddOnlinePlayer(Settings.Instance.PlayerName, clientID, PreferredInput.Instance.GetPreferredInput);
                    break;
                case MessageType.IntroduceClient:
                    clientID = im.ReadInt32();
                    var clientName = im.ReadString();
                    lobbyOnline.AddOnlinePlayer(clientName, clientID, InputController.Remote);
                    //create new onlinePlayer for Lobby
                    break;
                case MessageType.MenuInput:
                    Menu currentmenu = game.GetCurrentMenu();
                    clientID = im.ReadInt32();
                    currentmenu.RemoteInput((InputType)im.ReadByte(), clientID);
                    break;
                case MessageType.ReceiveServerInfo:
                    im.ReadAllFields(serverData);
                    //string serverName = im.ReadString();
                    //string address = im.ReadString() + ":" + networkThread.port;//im.SenderEndPoint.ToString();
                    lobbyOnline.serverData = serverData;
                    break;
                case MessageType.PlayerReady:
                    clientID = im.ReadInt32();
                    lobbyOnline.SetPlayerReady(clientID);

                    break;
                case MessageType.Chat:
                    string message = im.ReadString();
                    game.menuPause.AddChatMessage(message);
                    break;
                case MessageType.ServerInList:
                    long key = im.ReadInt64();
                    serverData = new CarGoServer.ServerData();
                    im.ReadAllFields(serverData);
                    game.lobbySearch.AddServerData(serverData);
                    break;
                case MessageType.ReceiveServerAddress:
                    break;
            }
        }


        private Type GetType(ObjectType objectType)
        {
            Type type = typeof(Program);

            switch (objectType)
            {
                case ObjectType.Player: type = typeof(CarGo.Player); break;

                    //...
            }
            return type;
        }


        private int RequestObjectID()
        {
            return 0;
        }

    }
}
