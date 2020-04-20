    using System.Collections.Generic;
    using System.Numerics;
    using System.Text;
    using System;
    using GameServer;

    namespace GameServerServer {
        public class broadcast {

            private List<Packet> _broadcast;

            public void addToBroadcast (Packet _packet) {
                _broadcast.Add (_packet);
            }
            public Packet ToPacket () {
                Packet _packet = new Packet ((int) ServerPackets.serverBroadcast);
                _packet.Write (_broadcast.Count);
                foreach (Packet packet in _broadcast) {
                    _packet.Write (packet.ToArray ());
                }
                return _packet;
            }
            public void clearBroadcast(){
                _broadcast = new List<Packet> ();
            }
        }
    }
    