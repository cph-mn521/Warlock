    using System.Collections.Generic;
    using System.Numerics;
    using System.Text;
    using System;

    namespace GameServer {
        public class GameLogic {

            #region flags
            public static bool round = false;
            private static bool endGoofTime = false;
            private static bool init = false;

            #endregion
            private static DateTime time = DateTime.Now;
            public static float mapSize = 25;
            public static int deadPlayers = 0;
            public static int players = 0;
            public static void Update () {
                if (init) {
                    InitRound ();
                }

                if (round) {
                    gameRound ();
                } else {
                    goofTime ();
                }

            }

            private static void gameRound () {
                foreach (Client _client in Server.clients.Values) {
                    if (_client.player != null) {
                        _client.player.Update ();
                    }
                }
                foreach (Spell _spell in Spell.AllSpells) {
                    _spell.update ();
                }
                #region Cleanup
                foreach (Spell _spell in Spell.spellsToRemove) {
                    ServerSend.removeSpell (_spell);
                    Spell.AllSpells.Remove (_spell);
                }

                foreach (Player _player in Player.playersToDespawn) {
                    ServerSend.DespawnPlayer (_player);

                }

                Spell.spellsToRemove = new List<Spell> ();
                Player.playersToDespawn = new List<Player> ();
                #endregion

                if (players == deadPlayers + 1) {
                    Console.WriteLine ("time to restart round");
                    for (int i = 1; i <= ServerHandle.playersInGame; i++) {
                        ServerSend.DespawnPlayer (Server.clients[i].player);
                        Wait(DateTime.Now,2000);
                    }
                    init = true;
                    round = false;
                }

                ThreadManager.UpdateMain ();
            }

            private static void goofTime () {
                foreach (Client _client in Server.clients.Values) {
                    if (_client.player != null) {
                        _client.player.Update ();
                    }
                }
                foreach (Spell _spell in Spell.AllSpells) {
                    _spell.update ();
                }
                #region Cleanup
                foreach (Spell _spell in Spell.spellsToRemove) {
                    ServerSend.removeSpell (_spell);
                    Spell.AllSpells.Remove (_spell);
                }
                Spell.spellsToRemove = new List<Spell> ();
                #endregion
                if (players > 1) {

                    if (!endGoofTime) {
                        Console.WriteLine ("more than 2 players, restarting in 10 sec");
                        time = DateTime.Now;
                    }
                    endGoofTime = true;

                }
                TimeSpan TimeElapsed = DateTime.Now - time;
                if (endGoofTime && TimeElapsed.TotalSeconds > 10) {
                    Console.WriteLine ("ending goof round");
                    round = true;
                    init = true;
                    endGoofTime = false;
                    for (int i = 1; i <= ServerHandle.playersInGame; i++) {
                        ServerSend.DespawnPlayer (Server.clients[i].player);
                        Wait(DateTime.Now,2000);                        
                    }
                }

                ThreadManager.UpdateMain ();
            }

            #region helpfunctions
            private static void InitRound () {
                Console.WriteLine ("initialising round");
                for (int i = 1; i <= ServerHandle.playersInGame; i++) {
                    Server.clients[i].player.position = new Vector3 (0, 0, 0);
                    Server.clients[i].player.removed = false;
                    Server.clients[i].player.resetHp ();
                    deadPlayers = 0;
                    ServerSend.SpawnPlayer (Server.clients[i].player);
                }
                init = false;
                
            }
            private static void Wait(DateTime time,int wait){
                TimeSpan TimeElapsed = DateTime.Now - time;
                while (TimeElapsed.TotalMilliseconds<wait)
                {
                     TimeElapsed = DateTime.Now - time;
                }
            }
            #endregion

        }
    }