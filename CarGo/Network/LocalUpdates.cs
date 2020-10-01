using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using Lidgren.Network.Xna;


namespace CarGo.Network
{
    public class LocalUpdates
    {
        Scene scene;
        StateMachine stateMachine;
        public LocalUpdates(Scene scene)
        {
            this.scene = scene;
            this.stateMachine = StateMachine.Instance;
        }


        public void Update()
        {
            for (int i = 0; i < NetworkThread.incomingMessages.Count; i++)
            {
                ParseMessage(NetworkThread.incomingMessages[i]);
            }
            NetworkThread.incomingMessages.Clear();
        }


        private void ParseMessage(NetIncomingMessage im)
        {
            //Parse and Apply all Changes

            var type = im.ReadByte();
            switch ((MessageType)type)
            {
                case MessageType.GameState:

					//Update GameState

                    break;
                case MessageType.ObjectUpdate:
					var objectID = im.ReadInt32();
					switch ((ObjectMessageType)im.ReadByte())
					{
						case ObjectMessageType.Spawn:
                            var objectType = im.ReadByte();
                            switch((ObjectType)objectType)
                            {
                                case ObjectType.Player:

                                    scene.addPlayer(false,im.ReadVector2(),(CarType)im.ReadByte(),(CarFrontType)im.ReadByte(),(AbilityType)im.ReadByte(),(int)objectID);
                                    break;
                            }
							break;
                                        

						case ObjectMessageType.Despawn:
							break;
						case ObjectMessageType.UpdatePosition:
							break;
						case ObjectMessageType.UpdateRotation:
							break;
						case ObjectMessageType.StateChange:
							break;
					}
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
