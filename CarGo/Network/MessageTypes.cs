﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Network
{
    public enum ServerInfo
    {
        ServerMessage,
        Broadcast        
    }

    

    public enum ServerTask
    {
        GetClientNumber,
        GetServerInformation
    }
    
    public enum MessageType
    {
        GameState,
        ObjectUpdate,
        ReceiveClientNumber,
        ReceiveServerInfo,
        IntroduceClient,
        MenuInput,
        PlayerReady
    }
    public enum ObjectMessageType
    {
        Spawn,
        PlayerSpawn,
        UpdatePosition,
        Despawn,
        StateChange,
        UpdateHitpoints
    }
    public enum ObjectType
    {
        Player

    }


}
