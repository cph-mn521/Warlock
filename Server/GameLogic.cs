    using System.Collections.Generic;
    using System.Numerics;
    using System.Text;
    using System;

    namespace GameServer {
        public class GameLogic {

            #region flags
            public bool round = false;
            private bool endGoofTime = false;
            private bool init = false;

            #endregion
            
            #region Constructor and Constructor params
            DateTime gameStart;

            public GameLogic(){
                gameStart = DateTime.Now;
            }
            #endregion
            
            private DateTime time = DateTime.Now;
            public float mapSize = 25;
            public int deadPlayers = 0;
            public int players = 0;
            public void Update () {
                if (init) {
                    InitRound ();
                }

                if (round) {
                    gameRound ();
                } else {
                    goofTime ();
                }

            }

            private void gameRound () {
                foreach (Client _client in Server.clients.Values) {
                    if (_client.player != null) {
                        _client.player.Update ();
                    }
                }
                foreach (Spell _spell in Spell.AllSpells) {
                    _spell.update ();
                }
                #region Cleanup
                foreach (updatable _object in Server.cleanUp) {
                    ServerSend.Instance.removeObject(_object);

                }
                Server.cleanUp = new List<updatable>();
                #endregion

                if (players == deadPlayers + 1) {
                    Console.WriteLine ("time to restart round");
                    for (int i = 1; i <= ServerHandle.playersInGame; i++) {
                         ServerSend.Instance.removeObject(Server.clients[i].player);
                        Wait(DateTime.Now,2000);
                    }
                    init = true;
                    round = false;
                }

                ThreadManager.UpdateMain ();
            }

            private void goofTime () {
                foreach (Spell _spell in Spell.AllSpells) {
                    _spell.update ();
                }

                foreach (Client _client in Server.clients.Values) {
                    if (_client.player != null) {
                        _client.player.Update ();
                    }
                }
                
                #region Cleanup
                foreach (updatable _object in Server.cleanUp) {
                    ServerSend.Instance.removeObject(_object);

                }
                Server.cleanUp = new List<updatable>();
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
                        ServerSend.Instance.removeObject (Server.clients[i].player);
                        Wait(DateTime.Now,2000);                        
                    }
                }

                ThreadManager.UpdateMain ();
            }

            #region helpfunctions
            private void InitRound () {
                Console.WriteLine ("initialising round");
                for (int i = 1; i <= ServerHandle.playersInGame; i++) {
                    Server.clients[i].player.position = new Vector3 (0, 0, 0);
                    Server.clients[i].player.removed = false;
                    Server.clients[i].player.resetHp ();
                    deadPlayers = 0;
                   ServerSend.Instance.spawnObject(Server.clients[i].player);
                }
                init = false;
                
            }
            private void Wait(DateTime time,int wait){
                TimeSpan TimeElapsed = DateTime.Now - time;
                while (TimeElapsed.TotalMilliseconds<wait)
                {
                     TimeElapsed = DateTime.Now - time;
                }
            }
            #endregion

        }
    }