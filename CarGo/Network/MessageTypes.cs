using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Network
{
    public enum MessageType
    {
        Spawn,
        UpdatePosition,
        UpdateRotation,
        Despawn,
        StateChange
    }
    public enum ObjectType
    {
        Player,

    }


}
