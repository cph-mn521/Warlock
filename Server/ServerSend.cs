using System;
using GameServerServer;

namespace GameServer {
    public class ServerSend {
        private static ServerSend instance = null;
        private static readonly object padlock = new object ();
        private broadcast _broadcast;
        public ServerSend () {
            _broadcast = new broadcast();
        }
        public static ServerSend Instance {
            get {
                lock (padlock) {
                    if (instance == null) {
                        instance = new ServerSend ();
                    }
                    return instance;
                }
            }
        }

        

        #region TCP
        private static void sendTCPData (int _toClient, Packet _packet) {
            _packet.WriteLength ();
            Server.clients[_toClient].tcp.SendData (_packet);
        }

        private static void SendTCPDataToAll (Packet _packet) {
            _packet.WriteLength ();
            for (int i = 1; i < Server.MaxPlayers; i++) {
                Server.clients[i].tcp.SendData (_packet);
            }
        }
        private static void SendTCPDataToAll (int _exceptClient, Packet _packet) {
            _packet.WriteLength ();
            for (int i = 1; i < Server.MaxPlayers; i++) {
                if (i != _exceptClient) {
                    Server.clients[i].tcp.SendData (_packet);
                }
            }
        }
        #endregion

        #region UDP
        private static void SendUDPData (int _toClient, Packet _packet) {
            _packet.WriteLength ();
            Server.clients[_toClient].udp.SendData (_packet);
        }

        private static void SendUDPDataToAll (Packet _packet) {
            _packet.WriteLength ();
            for (int i = 1; i < Server.MaxPlayers; i++) {
                Server.clients[i].udp.SendData (_packet);
            }
        }
        private static void SendUDPDataToAll (int _exceptClient, Packet _packet) {
            _packet.WriteLength ();
            for (int i = 1; i < Server.MaxPlayers; i++) {
                if (i != _exceptClient) {
                    Server.clients[i].udp.SendData (_packet);
                }
            }
        }

        #endregion

        public void sendBroadcast(){
            using (Packet _packet = _broadcast.ToPacket()) {
                SendUDPDataToAll (_packet);
                _broadcast.clearBroadcast();
            }
        }


        #region Packets
        public void Welcome (int _toClient, string _msg) {
            using (Packet _packet = new Packet ((int) ServerPackets.welcome)) {
                _packet.Write (_msg);
                _packet.Write (_toClient);
                sendTCPData (_toClient, _packet);
            }
        }

        public void SpawnPlayer (int _toClient, Player _player) {
            using (Packet _packet = new Packet ((int) ServerPackets.spawnPlayer)) {
                _packet.Write (_player.id);
                _packet.Write (_player.username);
                _packet.Write (_player.getHp ());
                _packet.Write (_player.position);
                _packet.Write (_player.rotation);
                _broadcast.addToBroadcast (_packet);
                sendTCPData (_toClient, _packet);
            }
        }

        public void SpawnPlayer (Player _player) {
            using (Packet _packet = new Packet ((int) ServerPackets.spawnPlayer)) {
                _packet.Write (_player.id);
                _packet.Write (_player.username);
                _packet.Write (_player.getHp ());
                _packet.Write (_player.position);
                _packet.Write (_player.rotation);
                _broadcast.addToBroadcast (_packet);
                SendTCPDataToAll (_packet);
            }
        }

        public void PlayerPosition (Player _player) {
            using (Packet _packet = new Packet ((int) ServerPackets.playerPosition)) {
                _packet.Write (_player.id);
                _packet.Write (_player.position);
                _broadcast.addToBroadcast(_packet);
                SendUDPDataToAll (_packet);
            }
        }

        public void PlayerRotation (Player _player) {
            using (Packet _packet = new Packet ((int) ServerPackets.playerRotation)) {
                _packet.Write (_player.id);
                _packet.Write (_player.rotation);
                SendUDPDataToAll (_player.id, _packet);
            }
        }
        public void DespawnPlayer (Player _player) {
            using (Packet _packet = new Packet ((int) ServerPackets.playerDespawn)) {
                _packet.Write (_player.id);
                SendUDPDataToAll (_packet);
            }
        }
        public void PlayerHp (Player _player) {
            using (Packet _packet = new Packet ((int) ServerPackets.playerHp)) {
                _packet.Write (_player.id);
                _packet.Write (_player.getHp ());
                SendUDPDataToAll (_packet);
            }
        }

        #region  Spells
        public void SpawnSpell (Spell _spell) {
            using (Packet _packet = new Packet ((int) ServerPackets.spawnSpell)) {
                _packet.Write (_spell.id);
                _packet.Write ((int) _spell.type);
                _packet.Write (_spell.position);
                _packet.Write (_spell.rotation);
                SendTCPDataToAll (_packet); //Måske skal det her være UDP istedet for, men tænker det er vigtigt at disse inputs faktisk bliver håndteret.
            }
        }

        public void SpellUpdate (Spell _spell) {
            using (Packet _packet = new Packet ((int) ServerPackets.SpellUpdate)) {
                _packet.Write (_spell.id);
                _packet.Write (_spell.position);
                _packet.Write (_spell.rotation);
                SendUDPDataToAll (_packet);
            }
        }

        internal void removeSpell (Spell _spell) {
            using (Packet _packet = new Packet ((int) ServerPackets.removeSpell)) {
                _packet.Write (_spell.id);
                SendUDPDataToAll (_packet);
            }
        }
        #endregion

        #endregion
    }
}