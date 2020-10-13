using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lidgren.Network;
using Lidgren.Network.Xna;
using Microsoft.Xna.Framework;
using MSCommon;

namespace CarGoServer
{
	

	class Program
    {
        private static NetServer s_server;

		private static Dictionary<int, NetConnection> clients;
		private static Dictionary<int, string> clientNames;
		
		//This is the Server
		private static ServerData serverData;

		private static bool registerOnline;
		private static bool gameRunning;
		static void Main(string[] args)
        {

			IPEndPoint masterServerEndpoint = NetUtility.Resolve(CommonConstants.MasterServerAddress, CommonConstants.MasterServerPort);
			NetPeerConfiguration config = new NetPeerConfiguration("GameServer");
            config.MaximumConnections = 4;
			serverData.serverPort = 14242;
            config.Port = 14242;
			if (args.Length >= 1)
			{
				int port;
				if (Int32.TryParse(args[0], out port))
				{
					serverData.serverPort = port;
					config.Port = port;
				}				
					
			}
			serverData.serverName = "CarGo Server";
			if (args.Length >= 2)
			{
				serverData.serverName = args[1];
			}
			if(args.Length >= 3)
            {
				if (args[2] == "true") registerOnline = true;
				else registerOnline = false;
            }
			registerOnline = true;

			serverData.publicAddress = GetIPAddress();

			s_server = new NetServer(config);
			s_server.Start();
			s_server.RegisterReceivedCallback(new SendOrPostCallback(CheckForMessages), new SynchronizationContext());
			clients = new Dictionary<int, NetConnection>();
			clientNames = new Dictionary<int, string>();
			Console.WriteLine("Server started on Port " + config.Port);

			var lastRegistered = -60.0f;

			while ((Console.KeyAvailable == false || Console.ReadKey().Key != ConsoleKey.Escape)&& !gameRunning )
			{
				// (re-)register periodically with master server
				if (NetTime.Now > lastRegistered + 60 && registerOnline)
				{
					// register with master server
					NetOutgoingMessage regMsg = s_server.CreateMessage();
					regMsg.Write((byte)MasterServerMessageType.RegisterHost);
					IPAddress mask;
					serverData.localAddress = NetUtility.GetMyAddress(out mask);
					regMsg.Write(s_server.UniqueIdentifier);
					regMsg.WriteAllFields(serverData);
					//regMsg.Write(new IPEndPoint(adr, 14242));
					Console.WriteLine("Sending registration to master server");
					s_server.SendUnconnectedMessage(regMsg, masterServerEndpoint);
					lastRegistered = (float)NetTime.Now;
				}
			}

        }


        private static void CheckForMessages(object peer)
		{
			NetIncomingMessage im;
			while ((im = s_server.ReadMessage()) != null)
			{
				NetOutgoingMessage om;
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
                        {
							string hail = im.SenderConnection.RemoteHailMessage.ReadString();
							for (int i = 1; i < 5; i++)
                            {
								if(clients.ContainsKey(i))
                                {
									continue;
                                }
								clients.Add(i, im.SenderConnection);
								serverData.numClients++;
                                //send existing client names
                                foreach (int key in clientNames.Keys)
                                {
									om = s_server.CreateMessage();
									om.Write((byte)1);
									om.Write((byte)CarGo.Network.MessageType.IntroduceClient);
									om.Write(key);
									om.Write(clientNames[key]);
									s_server.SendMessage(om, im.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
									
                                }
								clientNames.Add(i,hail);
								
								//introduce new client to others
								List<NetConnection> all = s_server.Connections; // get copy
								all.Remove(im.SenderConnection);
								if (all.Count > 0)
								{
									om = s_server.CreateMessage();
									om.Write((byte)1);
									om.Write((byte)CarGo.Network.MessageType.IntroduceClient);
									om.Write(i);
									om.Write(hail);
									s_server.SendMessage(om, all, NetDeliveryMethod.ReliableOrdered, 0);
								}
								break;
							}
							

						}
						

						//UpdateConnectionsList();
						break;
					case NetIncomingMessageType.Data:
						// incoming chat message from a client
						var messageType = im.ReadByte();
						

						switch ((CarGo.Network.ServerInfo)messageType)
                        {
                            case CarGo.Network.ServerInfo.ServerMessage:
                                //handle task
                                var task = im.ReadByte();
                                switch ((CarGo.Network.ServerTask)task)
                                {
                                    case CarGo.Network.ServerTask.GetClientNumber:
                                        foreach (int key in clients.Keys)
                                        {
                                            NetConnection value;
                                            clients.TryGetValue(key, out value);
                                            if (value == im.SenderConnection)
                                            {

                                                om = s_server.CreateMessage();
												om.Write((byte)19);
												om.Write((byte)CarGo.Network.MessageType.ReceiveClientNumber);
                                                om.Write(key);
                                                s_server.SendMessage(om, im.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
                                                break;
                                            }
                                        }


                                        break;
                                    case CarGo.Network.ServerTask.GetServerInformation:
										om = s_server.CreateMessage();
										om.Write((byte)19);
										om.Write((byte)CarGo.Network.MessageType.ReceiveServerInfo);
										//om.WriteAllFields(serverData);
										om.Write(serverData.serverName);
										
										om.Write(serverData.publicAddress);
										s_server.SendMessage(om, im.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
										break;
                                    default:
                                        //fehler
                                        break;
                                }
                                break;
                            case CarGo.Network.ServerInfo.Broadcast:
                                
								if((CarGo.Network.MessageType)im.ReadByte()== CarGo.Network.MessageType.GameState)
                                {
									if((CarGo.GameState)im.ReadByte()== CarGo.GameState.MenuModificationSelection)
                                    {
										gameRunning = true;
                                    }
                                }
								//broadcast this to all connections, except sender
                                List<NetConnection> all = s_server.Connections; // get copy
                                all.Remove(im.SenderConnection);
                                if (all.Count > 0)
                                {
                                    om = s_server.CreateMessage();
                                    om.Write(im);
                                    s_server.SendMessage(om, all, NetDeliveryMethod.ReliableOrdered, 0);
                                }
                                break;
                            default:
                                //Fehler
                                break;
                        }


                        break;
					default:
						Console.WriteLine("Unhandled type: " + im.MessageType + " " + im.LengthBytes + " bytes " + im.DeliveryMethod + "|" + im.SequenceChannel);
						break;
                }
                s_server.Recycle(im);
            }
        }


		static string GetIPAddress()
		{
			String address = "";
			WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
			using (WebResponse response = request.GetResponse())
			using (StreamReader stream = new StreamReader(response.GetResponseStream()))
			{
				address = stream.ReadToEnd();
			}

			int first = address.IndexOf("Address: ") + 9;
			int last = address.LastIndexOf("</body>");
			address = address.Substring(first, last - first);

			return address;
		}

	}
}
