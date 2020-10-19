using System;
using System.Net;
using System.Collections.Generic;

using Lidgren.Network;
using CarGo.Network;
using MSCommon;

namespace MasterServer
{
	public class Program
	{
		private const int maxPlayers = 4;
		
		static void Main(string[] args)
		{
			Dictionary<long, CarGoServer.ServerData> registeredHosts = new Dictionary<long, CarGoServer.ServerData>();

			NetPeerConfiguration config = new NetPeerConfiguration("masterserver");
			config.SetMessageTypeEnabled(NetIncomingMessageType.UnconnectedData, true);
			config.Port = CommonConstants.MasterServerPort;

			NetPeer peer = new NetPeer(config);
			peer.Start();

			// keep going until ESCAPE is pressed
			Console.WriteLine("Press ESC to quit");
			while (!Console.KeyAvailable || Console.ReadKey().Key != ConsoleKey.Escape)
			{
				NetIncomingMessage msg;
				while((msg = peer.ReadMessage()) != null)
				{
					switch (msg.MessageType)
					{
						case NetIncomingMessageType.UnconnectedData:
                            //
                            // We've received a message from a client or a host
                            //

                            // by design, the first byte always indicates action
                            switch ((MasterServerMessageType)msg.ReadByte())
                            {
                                case MasterServerMessageType.RegisterHost:

                                    // It's a host wanting to register its presence
                                    var id = msg.ReadInt64(); // server unique identifier
                                                              //var name = msg.ReadString();
                                    CarGoServer.ServerData serverData = new CarGoServer.ServerData();
                                    msg.ReadAllFields(serverData);
                                    IPAddress address;
                                    IPAddress.TryParse(serverData.localAddress, out address);
                                    IPEndPoint hostEndPoint = new IPEndPoint(address, serverData.serverPort);
                                    Console.WriteLine("Got registration for host " + id);
                                    registeredHosts[id] = serverData;
                                    break;

                                case MasterServerMessageType.RequestHostList:
                                    // It's a client wanting a list of registered hosts
                                    //Console.WriteLine("Sending list of " + registeredHosts.Count + " hosts to client " + msg.SenderEndPoint);
                                    foreach (var hostData in registeredHosts)
                                    {
                                        if (hostData.Value.showInServerList)
                                        {
                                            // send registered host to client
                                            //Console.WriteLine("sending " + hostData.Value.serverName);
                                            NetOutgoingMessage om = peer.CreateMessage();
                                            om.Write((byte)CarGo.Network.MessageType.ServerInList);
                                            om.Write(hostData.Key);
                                            om.WriteAllFields(hostData.Value);
                                            peer.SendUnconnectedMessage(om, msg.SenderEndPoint);
                                        }

                                    }

                                    break;
                                case MasterServerMessageType.RequestIntroduction:
                                    // It's a client wanting to connect to a specific (external) host
                                    IPEndPoint clientInternal = msg.ReadIPEndPoint();
                                    long hostId = msg.ReadInt64();
                                    string token = msg.ReadString();

                                    Console.WriteLine(msg.SenderEndPoint + " requesting introduction to " + hostId + " (token " + token + ")");

                                    // find in list
                                    IPEndPoint[] elist;
                                    CarGoServer.ServerData data;
                                    long publicAddress;
                                    IPAddress localAddress;
                                    
                                    if (registeredHosts.TryGetValue(hostId, out data))
                                    {
                                        IPAddress.TryParse(data.localAddress, out localAddress);
                                        if (long.TryParse(data.publicAddress, out publicAddress))
                                        {
                                            // found in list - introduce client and host to eachother
                                            Console.WriteLine("Sending introduction...");
                                            peer.Introduce(
                                                new IPEndPoint(localAddress, data.serverPort), // host internal
                                                new IPEndPoint(publicAddress, data.serverPort), // host external
                                                clientInternal, // client internal
                                                msg.SenderEndPoint, // client external
                                                token // request token
                                            );
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Client requested introduction to nonlisted host!");
                                    }
                                    break;
                                case MasterServerMessageType.GetHostByCode:
                                    CarGoServer.ServerData serverData1;
                                    if(registeredHosts.TryGetValue(msg.ReadInt64(),out serverData1))
                                    {
                                        NetOutgoingMessage om = peer.CreateMessage();
                                        om.Write((byte)CarGo.Network.MessageType.ReceiveServerAddress);
                                        om.WriteAllFields(serverData1);
                                        peer.SendUnconnectedMessage(om, msg.SenderEndPoint);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Code not correct");
                                    }
                                    break;
                            }
                            break;

						case NetIncomingMessageType.DebugMessage:
						case NetIncomingMessageType.VerboseDebugMessage:
						case NetIncomingMessageType.WarningMessage:
						case NetIncomingMessageType.ErrorMessage:
							// print diagnostics message
							Console.WriteLine(msg.ReadString());
							break;
                    }
                }
            }

            peer.Shutdown("shutting down");
        }
    }
}
