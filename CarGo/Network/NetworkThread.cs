using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lidgren.Network;


namespace CarGo.Network
{
    class NetworkThread
    {
		private static NetClient s_client;


		NetworkThread()
        {
			NetPeerConfiguration config = new NetPeerConfiguration("chat");
			config.AutoFlushSendQueue = false;
			s_client = new NetClient(config);

			s_client.RegisterReceivedCallback(new SendOrPostCallback(GotMessage));
		}






		public static void GotMessage(object peer)
		{
			NetIncomingMessage im;
			while ((im = s_client.ReadMessage()) != null)
			{
				// handle incoming message
				switch (im.MessageType)
				{
					case NetIncomingMessageType.DebugMessage:
					case NetIncomingMessageType.ErrorMessage:
					case NetIncomingMessageType.WarningMessage:
					case NetIncomingMessageType.VerboseDebugMessage:
						string text = im.ReadString();
						break;
					case NetIncomingMessageType.StatusChanged:
						NetConnectionStatus status = (NetConnectionStatus)im.ReadByte();
						break;
					case NetIncomingMessageType.Data:
                        switch ((MessageType)im.ReadByte())
                        {
                            case MessageType.Spawn:
                                break;


							case MessageType.Despawn:
                                break;
                            case MessageType.UpdatePosition:
                                break;
                            case MessageType.UpdateRotation:
                                break;
                            case MessageType.StateChange:
                                break;
                        }
                        break;
					default:
						break;
                }
                s_client.Recycle(im);
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
    }
}
