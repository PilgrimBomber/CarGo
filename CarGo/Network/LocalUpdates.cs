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
        //public List<NetIncomingMessage> incomingMessages;
        public LocalUpdates(Scene scene, LobbyOnline lobbyOnline)
        {
            
            this.scene = scene;
            this.lobbyOnline = lobbyOnline;
            updatesPerSecond = 10;
            timePerUpdate = 1000 / updatesPerSecond;
            timeSinceLastUpdate = 0;
            //incomingMessages = new List<NetIncomingMessage>();
        }

        public void SetNetworkThread(NetworkThread networkThread)
        {
            this.networkThread = networkThread;
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
                    if(entity.entityType != EntityType.Player || ((Player)entity).local)
                        networkThread.BroadCastEntityUpdate(ObjectMessageType.UpdatePosition, entity);
                }
            }
            else
            {
                foreach(Player player in scene.localPlayers)
                {
                    networkThread.BroadCastEntityUpdate(ObjectMessageType.UpdatePosition, player);
                }
            }
        }


        public void ParseMessage(NetIncomingMessage im)
        {
            //Parse and Apply all Changes

            var type = im.ReadByte();
            switch ((MessageType)type)
            {
                case MessageType.GameState:

                    //Update GameState

                    break;
                case MessageType.ObjectUpdate:
                    var objectMessageType = (ObjectMessageType)im.ReadByte();
                    var objectID = im.ReadInt32();
                    Vector2 center;
                    switch (objectMessageType)
                    {
                        case ObjectMessageType.PlayerSpawn:

                            int clientNumberP = im.ReadInt32();
                            
                            center = XNAExtensions.ReadVector2(im);
                            CarType carType = (CarType)im.ReadByte();
                            CarFrontType carFrontType = (CarFrontType)im.ReadByte();
                            AbilityType abilityType = (AbilityType)im.ReadByte();
                            scene.RemoteAddPlayer(center, objectID, carType, carFrontType, abilityType, lobbyOnline.GetOnlinePlayer(clientNumberP));
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
                            break;
                        case ObjectMessageType.StateChange:
                            break;
                        case ObjectMessageType.UpdateHitpoints:

                            break;
                    }
                    break;
                case MessageType.ReceiveClientNumber:
                    var clientNumber = im.ReadInt32();
                    ID_Manager.Instance.SetClientNumber(clientNumber);
                    lobbyOnline.AddOnlinePlayer(Settings.Instance.PlayerName, clientNumber);
                    break;
                case MessageType.IntroduceClient:
                    int clientID = im.ReadInt32();
                    var clientName = im.ReadString();
                    lobbyOnline.AddOnlinePlayer(clientName, clientID);
                    //create new onlinePlayer for Lobby
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
