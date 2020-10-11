using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
		public static NetworkThread Instance;
		public bool isMainClient= false;
		LocalUpdates localUpdates;
		private int updateCounter = 0;
		public bool IsServerRunning
        {
			get 
			{
				Process[] pname = Process.GetProcessesByName("CarGoServer");
				if (pname.Length != 0) return true;
				else return false;
			}
        }
		public NetworkThread(LocalUpdates localUpdates)
        {
			this.localUpdates = localUpdates;
			NetPeerConfiguration config = new NetPeerConfiguration("chat");
			config.AutoFlushSendQueue = false;
			s_client = new NetClient(config);
			
			//s_client.RegisterReceivedCallback(new SendOrPostCallback(GotMessage),new SynchronizationContext());
			port = 23451;
			Instance = this;
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
			s_client.Start();
			//make hail message contain username + ?
			NetOutgoingMessage hail = s_client.CreateMessage(Settings.Instance.PlayerName);
			s_client.Connect(host, port, hail);
			
		}
		
		public void LaunchServer()
        {
            Process pr = new Process();
            ProcessStartInfo prs = new ProcessStartInfo();
			prs.WorkingDirectory =Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\CarGoServer\bin\Debug\")); 

			prs.FileName = @"CarGoServer.exe";
			prs.Arguments = port.ToString();
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
						else
						{
							int i = 0;
						}
						
						
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
		
		public void RequestServerInfo()
        {
			NetOutgoingMessage om = s_client.CreateMessage();
			om.Write((byte)ServerInfo.ServerMessage);
			om.Write((byte)ServerTask.GetServerInformation);
			s_client.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
			s_client.FlushSendQueue();
		}


		//Send Message
		private void BroadCastMessage(MessageType messageType, ObjectMessageType objectMessageType)
        {

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
