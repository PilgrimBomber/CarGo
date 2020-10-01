using System;
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
        GetID
    }
    
    public enum MessageType
    {
        GameState,
        ObjectUpdate
    }
    public enum ObjectMessageType
    {
        Spawn,
        UpdatePosition,
        UpdateRotation,
        Despawn,
        StateChange
    }
    public enum ObjectType
    {
        Player

    }


}
