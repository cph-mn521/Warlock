    using System.Collections.Generic;
    using System.Numerics;
    using System.Text;
    using System;
    using GameServer;

namespace GameServerServer
{
    public class broadcast
    {
        
        private List<Packet> _broadcast;

        public void addToBroadcast(Packet _packet){
            _broadcast.Add(_packet);
        }
        public void send(){

        }
    }
}