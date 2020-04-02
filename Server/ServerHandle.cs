    using System.Collections.Generic;
    using System.Text;
    using System;
    using System.Numerics;

    namespace GameServer {
        public class ServerHandle {

            public static int playersInGame=0;
            public static void welcomReceived (int _fromClient, Packet _packet) {
                int _clientIdCheck = _packet.ReadInt ();
                string _username = _packet.ReadString ();
                Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected succefully and is now player: {_fromClient}");
                playersInGame = _fromClient;
                if(_fromClient != _clientIdCheck){
                    Console.WriteLine($"Player \"{_username}\" (ID:{_fromClient}) has assumed wrong client ID ({_clientIdCheck})");
                }
                Server.clients[_fromClient].SendIntoGame(_username);
            }
            public static void playerMovement(int _fromClient, Packet _packet){
                Vector3 _inputs = _packet.ReadVector3();
                Quaternion _rotation = _packet.ReadQuaternion();
                Server.clients[_fromClient].player.SetInput(_inputs,_rotation);
            }

            public static void playerCast(int _fromClient, Packet _packet){ // Race Conditions.
                Player p = Server.clients[_fromClient].player;
                int slot = _packet.ReadInt();
                DateTime _nextLoop = DateTime.Now;
                if (p.spells[slot] != null){ //Spilleren har spell i det slot
                    TimeSpan tmElapsed = DateTime.Now - p.LastCast[slot];
                    if (tmElapsed.TotalMilliseconds  >= p.cooldowns[slot]){ //Spell ikke på Cooldown 
                        int id = Spell.spellCount;                       
                        Spell _spell = new Spell(id,p.spellRank[slot],_fromClient,p.spells[slot],p.position,p.rotation); //find en måde at opbevare de her spells. Hvor henne giver mening?
                        Spell.AllSpells.Add(_spell);
                        Spell.spellCount =Spell.spellCount+1;
                        p.LastCast[slot] = DateTime.Now;
                        ServerSend.SpawnSpell(_spell);
                        
                    }
                }

                
            }

        }

    }