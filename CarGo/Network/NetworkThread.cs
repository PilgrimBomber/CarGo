using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
		private Thread serverThread;
		public NetworkThread()
        {
			NetPeerConfiguration config = new NetPeerConfiguration("chat");
			config.AutoFlushSendQueue = false;
			s_client = new NetClient(config);
			incomingMessages = new List<NetIncomingMessage>();
			s_client.RegisterReceivedCallback(new SendOrPostCallback(GotMessage),new SynchronizationContext());
			
		}


		public void ConnectToServer()
        {

        }

		public void LaunchServer()
        {
            Process pr = new Process();
            ProcessStartInfo prs = new ProcessStartInfo();
			prs.WorkingDirectory =Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\CarGoServer\bin\Release\")); 

			prs.FileName = @"CarGoServer.exe";
            pr.StartInfo = prs;
			pr.Start();
            
			//Process pr = new Process();
			//ProcessStartInfo prs = new ProcessStartInfo();
			//prs.FileName = @"../../CarGoServer/bin/Release/CarGoServer.exe";
			//pr.StartInfo = prs;

			//ThreadStart ths = new ThreadStart(() => pr.Start());
			//serverThread = new Thread(ths);
			//serverThread.Start();
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
        

		public void RequestClientNumber()
        {
			NetOutgoingMessage om = s_client.CreateMessage();
			om.Write((byte)ServerInfo.ServerMessage);
			om.Write((byte)ServerTask.GetClientNumber);
			s_client.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
			s_client.FlushSendQueue();
		}
		//public static void Send(string text)
		//{
		//	NetOutgoingMessage om = s_client.CreateMessage(text);
		//	s_client.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
		//	s_client.FlushSendQueue();
		//}

		//Send Message
		private void BroadCastMessage(MessageType messageType, ObjectMessageType objectMessageType)
        {

        }


    }
}
