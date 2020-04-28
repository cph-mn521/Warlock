    using System.Collections.Generic;
    using System.Numerics;
    using System.Text;
    using System;

    namespace GameServer {
        public class ServerHandle {
            private static Shop shop = new Shop();
            public static int playersInGame = 0;
            public static void welcomReceived (int _fromClient, Packet _packet) {
                int _clientIdCheck = _packet.ReadInt ();
                string _username = _packet.ReadString ();
                Console.WriteLine ($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected succefully and is now player: {_fromClient}");
                playersInGame = _fromClient;
                if (_fromClient != _clientIdCheck) {
                    Console.WriteLine ($"Player \"{_username}\" (ID:{_fromClient}) has assumed wrong client ID ({_clientIdCheck})");
                }
                Server.clients[_fromClient].SendIntoGame (_username);
            }
            public static void playerMovement (int _fromClient, Packet _packet) {
                Vector3 _inputs = _packet.ReadVector3 ();
                Quaternion _rotation = _packet.ReadQuaternion ();
                Server.clients[_fromClient].player.SetInput (_inputs, _rotation);
            }

            public static void playerCast2 (int _fromClient, Packet _packet) {
                
                Player player = Server.clients[_fromClient].player;
                int slot = _packet.ReadInt ();
                Vector3 _target = _packet.ReadVector3 ();
                Quaternion _rotation = _packet.ReadQuaternion ();
                if (player.removed || slot >= player.spellBook.size() ) {
                    return;
                }
                player.spellBook.CastSpell(slot);
                player.status.Target = _target;
            }

            public static void requestBuySpell(int _fromClient, Packet _packet){
                Player _player = Server.clients[_fromClient].player;
                int item = _packet.ReadInt();
                Console.WriteLine($"request to buy spell at pos {item}");
                
                shop.BuySpell(_player,item);

                
            }

            public static void requestBuyItem(int _fromClient,Packet _packet){
                Player _player = Server.clients[_fromClient].player;
                int item = _packet.ReadInt();
                Console.WriteLine($"request to buy item at pos {item}");
                shop.buyItem(_player,item);
            }

            public static void playerCast (int _fromClient, Packet _packet) { // Race Conditions.
                Console.WriteLine("Not dis one seniorer");
                Player p = Server.clients[_fromClient].player;
                if (p.removed) {
                    return;
                }
                int slot = _packet.ReadInt ();
                Vector3 _target = _packet.ReadVector3 ();
                Quaternion _rotation = _packet.ReadQuaternion ();
                DateTime _nextLoop = DateTime.Now;
                if (p.spells[slot] != null) { //Spilleren har spell i det slot
                    TimeSpan tmElapsed = DateTime.Now - p.LastCast[slot];
                    if (tmElapsed.TotalMilliseconds >= p.cooldowns[slot]) { //Spell ikke på Cooldown 
                        int id = Spell.spellCount;
                        Spell _spell = new Spell (id, p.spellRank[slot], _fromClient, 
                        p.spells[slot], p.position, p.rotation, _target); //find en måde at opbevare de her spells. Hvor henne giver mening?
                        Spell.AllSpells.Add (_spell);
                        Spell.spellCount = Spell.spellCount + 1;
                        p.LastCast[slot] = DateTime.Now;
                        ServerSend.Instance.spawnObject (_spell);

                    }
                }

            }

        }

    }