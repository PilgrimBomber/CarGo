﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lidgren.Network;
using Lidgren.Network.Xna;

namespace CarGo.Network
{
    public class NetworkThread
    {
		private static NetClient s_client;
		
		private Thread serverThread;
		public int port=14242;
		private string host;
		public static NetworkThread Instance;
		public bool isMainClient= false;
		public bool publicServer;
		LocalUpdates localUpdates;
		private int updateCounter = 0;
		public bool serverRunning;
        
		public NetworkThread(LocalUpdates localUpdates)
        {
			this.localUpdates = localUpdates;
			NetPeerConfiguration config = new NetPeerConfiguration("GameServer");
			config.AutoFlushSendQueue = false;
			config.EnableMessageType(NetIncomingMessageType.UnconnectedData);
			s_client = new NetClient(config);
			
			//s_client.RegisterReceivedCallback(new SendOrPostCallback(GotMessage),new SynchronizationContext());
			port = 23451;
			Instance = this;
			s_client.Start();
		}

		public void Update()
        {
			if(updateCounter>1)
            {
				if (s_client.Status != NetPeerStatus.NotRunning) GotMessage();
				updateCounter = 0;
			}
			
			updateCounter++;
		}

		public void ConnectToServer(string host)
        {
			ConnectToServer(host, port);
        }
		public void ConnectToServer(string host, int port)
        {
			this.port = port;
			this.host = host;
			//make hail message contain username + ?
			NetOutgoingMessage hail = s_client.CreateMessage(Settings.Instance.PlayerName);
			s_client.Connect(host, port, hail);
			
		}
		
		public void LaunchServer(string serverName, bool registerServer, int port)
        {
            Process pr = new Process();
            ProcessStartInfo prs = new ProcessStartInfo();
#if DEBUG
            // Debug Code
			prs.WorkingDirectory =Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\CarGoServer\bin\Debug\"));
#else
			prs.WorkingDirectory = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\CarGoServer\"));
#endif
			this.port = port;
			prs.FileName = @"CarGoServer.exe";
			string args = port.ToString();
			args += " " + serverName;
			publicServer = registerServer;
			args += " " + (registerServer ? "true" : "false");
			prs.Arguments = args;
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

		public static void GotMessage()
		{
			NetIncomingMessage im;
			while ((im = s_client.ReadMessage()) != null)
			{
				// handle incoming message
				switch (im.MessageType)
				{
					case NetIncomingMessageType.DebugMessage:
					case NetIncomingMessageType.ErrorMessage:
						break;
					case NetIncomingMessageType.WarningMessage:
					case NetIncomingMessageType.VerboseDebugMessage:
						string text = im.ReadString();
						break;
					case NetIncomingMessageType.StatusChanged:
						NetConnectionStatus status = (NetConnectionStatus)im.ReadByte();
						if(status == NetConnectionStatus.Connected)
                        {
							Instance.RequestClientNumber();
							Instance.RequestServerInfo();
						}
						break;
					case NetIncomingMessageType.Data:
						if (im.Data != null)
						{
							//instance.localUpdates.incomingMessages.Add(im);
							Instance.localUpdates.ParseMessage(im);
						}
						break;
					case NetIncomingMessageType.UnconnectedData:
						Instance.localUpdates.ParseUnconnectedMessage(im);
						break;
					default:
						break;
                }
                s_client.Recycle(im);
            }
        }
        

		public void RequestServerList()
        {
			NetOutgoingMessage om = s_client.CreateMessage();
			om.Write((byte)MasterServerMessageType.RequestHostList);
			IPEndPoint masterServerEndpoint = NetUtility.Resolve(MSCommon.CommonConstants.MasterServerAddress, MSCommon.CommonConstants.MasterServerPort);
			s_client.SendUnconnectedMessage(om, masterServerEndpoint);
        }
		public void CheckServerRunning(string host, int port)
        {
			NetOutgoingMessage om = s_client.CreateMessage();
			om.Write((byte)ServerInfo.ServerMessage);
			om.Write((byte)ServerTask.CheckOnline);
			s_client.SendUnconnectedMessage(om, host,port);
		}

		//public void RequestServerAddress(long code)
		//{
		//	NetOutgoingMessage om = s_client.CreateMessage();
		//	om.Write((byte)MasterServerMessageType.GetHostByCode);
		//	om.Write(code);
		//	IPEndPoint masterServerEndpoint = NetUtility.Resolve(MSCommon.CommonConstants.MasterServerAddress, MSCommon.CommonConstants.MasterServerPort);
		//	s_client.SendUnconnectedMessage(om, masterServerEndpoint);
		//}

		public void RequestClientNumber()
        {
			NetOutgoingMessage om = s_client.CreateMessage();
			om.Write((byte)ServerInfo.ServerMessage);
			om.Write((byte)ServerTask.GetClientNumber);
			s_client.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
			s_client.FlushSendQueue();
		}
		
		public void RequestServerInfo()
        {
			NetOutgoingMessage om = s_client.CreateMessage();
			om.Write((byte)ServerInfo.ServerMessage);
			om.Write((byte)ServerTask.GetServerInformation);
			s_client.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
			s_client.FlushSendQueue();
		}

		

		//Send Message
		public void BroadCastChatMessage(string message)
        {
			NetOutgoingMessage om = s_client.CreateMessage();
			om.Write((byte)ServerInfo.Broadcast);
			om.Write((byte)MessageType.Chat);
			om.Write(Settings.Instance.PlayerName + ": " + message);
			s_client.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
			s_client.FlushSendQueue();
		}

		public void BroadCastReady()
        {
			NetOutgoingMessage om = s_client.CreateMessage();
			om.Write((byte)ServerInfo.Broadcast);
			om.Write((byte)MessageType.PlayerReady);
			om.Write(ID_Manager.Instance.ClientNumber);
			s_client.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
			s_client.FlushSendQueue();
		}

		public void BroadCastMenuInput(InputType inputType)
        {
			NetOutgoingMessage om = s_client.CreateMessage();
			om.Write((byte)ServerInfo.Broadcast);
			om.Write((byte)MessageType.MenuInput);
			om.Write(ID_Manager.Instance.ClientNumber);
			om.Write((byte)inputType);
			s_client.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
			s_client.FlushSendQueue();
		}

		public void BroadCastNewGameState(GameState newState)
        {
			NetOutgoingMessage om = s_client.CreateMessage();
			om.Write((byte)ServerInfo.Broadcast);
			om.Write((byte)MessageType.GameState);
			om.Write((byte)newState);
			s_client.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
			s_client.FlushSendQueue();
		}

        public void BroadCastEntityUpdate(ObjectMessageType objectMessageType, Entity entity)
        {
			NetOutgoingMessage om = s_client.CreateMessage();
			om.Write((byte)ServerInfo.Broadcast);
			om.Write((byte)MessageType.ObjectUpdate);
			om.Write((byte)objectMessageType);
			om.Write(entity.objectID);
            switch (objectMessageType)
            {
                case ObjectMessageType.PlayerSpawn:
					Player player = (Player)entity;
					om.Write(player.onlinePlayer.clientID);
                    XNAExtensions.Write(om, entity.Hitbox.Center);
                    
                    om.Write((byte)player.carType);
                    om.Write((byte)player.carFrontType);
                    om.Write((byte)player.abilityType);
                    break;
                case ObjectMessageType.Spawn:
                    om.Write((byte)entity.entityType);
                    XNAExtensions.Write(om, entity.Hitbox.Center);
                    break;
                case ObjectMessageType.UpdatePosition:
                    XNAExtensions.Write(om, entity.Hitbox.Center);
                    om.Write(entity.Hitbox.RotationRad);
                    XNAExtensions.Write(om, entity.Velocity);
                    break;
                case ObjectMessageType.Despawn:
					//doesnt need more information
                    break;
                case ObjectMessageType.StateChange:
                    break;
                case ObjectMessageType.UpdateHitpoints:
					om.Write(entity.hitpoints);
                    break;
            }


            s_client.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
			s_client.FlushSendQueue();
        }


    }
}
