using System;
using GameServer;

namespace GameServer {
    public class ServerSend {
        private static ServerSend instance = null;
        private static readonly object padlock = new object ();

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

        #region Packets

        public void spellPurchase (int PlayerID, int shopIndex, int playerSlot, int playerGold) {
            using (Packet _packet = new Packet ((int) ServerPackets.spellPurchase)) {
                _packet.Write (shopIndex);
                _packet.Write (playerSlot);
                _packet.Write (playerGold);
                sendTCPData (PlayerID, _packet);
            }
        }
        public void itemPurchase (int PlayerID,int shopIndex, int playerSlot, int playerGold) {
            using (Packet _packet = new Packet ((int) ServerPackets.itemPurchase)) {
                _packet.Write (shopIndex);
                _packet.Write (playerSlot);
                _packet.Write (playerGold);
                sendTCPData (PlayerID, _packet);
            }
        }
        public void updateObject (Player _player) {
            using (Packet _packet = new Packet ((int) ServerPackets.updateObject)) {
                _packet.Write (_player.type);
                _packet.Write (_player.id);
                _packet.Write (_player.position);
                _packet.Write (_player.rotation);
                _packet.Write (_player.getHp ());
                SendUDPDataToAll (_packet);
            }
        }
        public void updateObject (updatable _object) {
            using (Packet _packet = new Packet ((int) ServerPackets.updateObject)) {
                _packet.Write (_object.type);
                _packet.Write (_object.id);
                _packet.Write (_object.position);
                _packet.Write (_object.rotation);
                SendUDPDataToAll (_packet);
            }
        }

        public void spawnObject (Player _object) {
            using (Packet _packet = new Packet ((int) ServerPackets.spawnObject)) {
                _packet.Write (_object.type);
                _packet.Write (_object.id);
                _packet.Write (_object.position);
                _packet.Write (_object.rotation);
                _packet.Write (_object.username);
                _packet.Write (_object.getHp ());
                SendTCPDataToAll (_packet);
            }
        }

        public void spawnObject (SpellObject _object) {
            using (Packet _packet = new Packet ((int) ServerPackets.spawnObject)) {
                _packet.Write (_object.type);
                _packet.Write (_object.id);
                _packet.Write (_object.position);
                _packet.Write (_object.rotation);
                _packet.Write ((int) _object.spellType);
                SendUDPDataToAll (_packet);
            }
        }
        public void spawnObject (Spell _object) {
            using (Packet _packet = new Packet ((int) ServerPackets.spawnObject)) {
                _packet.Write (_object.type);
                _packet.Write (_object.id);
                _packet.Write (_object.position);
                _packet.Write (_object.rotation);
                _packet.Write ((int) _object.spellType);
                SendUDPDataToAll (_packet);
            }
        }

        public void spawnObject (updatable _object) {
            using (Packet _packet = new Packet ((int) ServerPackets.spawnObject)) {
                _packet.Write (_object.type);
                _packet.Write (_object.id);
                _packet.Write (_object.position);
                _packet.Write (_object.rotation);
                SendUDPDataToAll (_packet);
            }
        }

        public void removeObject (updatable _object) {
            using (Packet _packet = new Packet ((int) ServerPackets.removeObject)) {
                _packet.Write (_object.type);
                _packet.Write (_object.id);
                SendTCPDataToAll (_packet);
            }
        }

        public void playerAnimation(int playerId,String Animation){
            using (Packet _packet = new Packet ((int) ServerPackets.playerAnimation)) {
                _packet.Write (playerId);
                _packet.Write (Animation);
                SendUDPDataToAll (_packet);
            }
        }

        public void Welcome (int _toClient, string _msg) {
            using (Packet _packet = new Packet ((int) ServerPackets.welcome)) {
                _packet.Write (_msg);
                _packet.Write (_toClient);
                sendTCPData (_toClient, _packet);
            }
        }

        #endregion
    }
}