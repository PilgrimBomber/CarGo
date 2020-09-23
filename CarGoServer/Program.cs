using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lidgren.Network;
using Lidgren.Network.Xna;
using Microsoft.Xna.Framework;

namespace CarGoServer
{
	

	class Program
    {
        private static NetServer s_server;

        
		

        static void Main(string[] args)
        {
            NetPeerConfiguration config = new NetPeerConfiguration("chat");
            config.MaximumConnections = 100;
            config.Port = 14242;
            s_server = new NetServer(config);
			s_server.Start();
			s_server.RegisterReceivedCallback(new SendOrPostCallback(CheckForMessages), new SynchronizationContext());

			Console.WriteLine("Server started on Port " + config.Port);

            while (true)
            {
				//CheckForMessages();
				NetBuffer message = new NetBuffer();
				XNAExtensions.Write(message, new Vector3(1, 2, 3));

				//Send messages

                Thread.Sleep(1);
            }


        }


        private static void CheckForMessages(object peer)
		{
			NetIncomingMessage im;
			while ((im = s_server.ReadMessage()) != null)
			{
				// handle incoming message
				switch (im.MessageType)
				{
					case NetIncomingMessageType.DebugMessage:
					case NetIncomingMessageType.ErrorMessage:
					case NetIncomingMessageType.WarningMessage:
					case NetIncomingMessageType.VerboseDebugMessage:
						string text = im.ReadString();
						Console.WriteLine(text);
						break;
                    case NetIncomingMessageType.StatusChanged:
						NetConnectionStatus status = (NetConnectionStatus)im.ReadByte();

						string reason = im.ReadString();
						Console.WriteLine(NetUtility.ToHexString(im.SenderConnection.RemoteUniqueIdentifier) + " " + status + ": " + reason);
						
						if (status == NetConnectionStatus.Connected)
						Console.WriteLine("Remote hail: " + im.SenderConnection.RemoteHailMessage.ReadString());

						//UpdateConnectionsList();
						break;
					case NetIncomingMessageType.Data:
						// incoming chat message from a client
						string chat = im.ReadString();

						Console.WriteLine("Broadcasting '" + chat + "'");

						// broadcast this to all connections, except sender
						List<NetConnection> all = s_server.Connections; // get copy
						all.Remove(im.SenderConnection);

						if (all.Count > 0)
						{
							NetOutgoingMessage om = s_server.CreateMessage();
							om.Write(NetUtility.ToHexString(im.SenderConnection.RemoteUniqueIdentifier) + " said: " + chat);
							XNAExtensions.Write(om, new Vector3(1, 2, 3));
							s_server.SendMessage(om, all, NetDeliveryMethod.ReliableOrdered, 0);
						}
						break;
					default:
						Console.WriteLine("Unhandled type: " + im.MessageType + " " + im.LengthBytes + " bytes " + im.DeliveryMethod + "|" + im.SequenceChannel);
						break;
				}
				s_server.Recycle(im);
			}
		}

    }
}
