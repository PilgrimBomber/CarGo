using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lidgren.Network;


namespace CarGo.Network
{
    public class NetworkThread
    {
		private static NetClient s_client;
		public static List<NetIncomingMessage> incomingMessages;
		
		public NetworkThread()
        {
			NetPeerConfiguration config = new NetPeerConfiguration("chat");
			config.AutoFlushSendQueue = false;
			s_client = new NetClient(config);
			incomingMessages = new List<NetIncomingMessage>();
			s_client.RegisterReceivedCallback(new SendOrPostCallback(GotMessage),new SynchronizationContext());
			
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
						incomingMessages.Add(im);
						
						
                        break;
					default:
						break;
                }
                s_client.Recycle(im);
            }
        }
        


		//Send Message
		private void BroadCastMessage(MessageType messageType, ObjectMessageType objectMessageType)
        {

        }


    }
}
