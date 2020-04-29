    using System.Collections.Generic;
    using System.Numerics;
    using System.Text;
    using System;

    namespace GameServer {
        public class GameLogic {

            #region flags
            public bool round = false;
            public bool endOfRound = false;
            private bool endGoofTime = false;
            private bool init = false;

            public static List<SpellObject> spells2 = new List<SpellObject> ();
            #endregion

            #region Constructor and Constructor params
            DateTime gameStart;

            public GameLogic () {
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
                GameLoop ();

                if (players <= deadPlayers + 1 && !endOfRound ) {
                    Console.WriteLine ("ending the round in 10 sec.");
                    
                    time = DateTime.Now;
                    
                    endOfRound= true;
                }
                TimeSpan TimeElapsed = DateTime.Now - time;
                if (endOfRound && TimeElapsed.TotalSeconds > 10){
                    round = false;
                    endOfRound = false;
                    endRound();
                    

                }

                ThreadManager.UpdateMain ();
            }

            private void goofTime () {

                GameLoop ();
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
                    
                    endGoofTime = false;
                    endRound();
                    Wait (DateTime.Now, 2000);

                }

                ThreadManager.UpdateMain ();
            }

            private void GameLoop () {
                foreach (SpellObject _spell in GameLogic.spells2) {
                    _spell.update ();

                }
                foreach (Client _client in Server.clients.Values) {
                    if (_client.player != null) {
                        _client.player.Update ();
                    }
                }
                #region Cleanup
                foreach (updatable _object in Server.cleanUp) {
                    ServerSend.Instance.removeObject (_object);
                    if (_object is SpellObject) {
                        spells2.Remove (_object as SpellObject);
                    }

                }
                Server.cleanUp = new List<updatable> ();
                #endregion
            }

            #region helpfunctions

            private void endRound () {
                Console.WriteLine ("ending the round.");
                for (int i = 1; i <= ServerHandle.playersInGame; i++) {
                    ServerSend.Instance.removeObject (Server.clients[i].player);
                    init = true;
                    Wait (DateTime.Now, 2000);
                }
            }
            private void InitRound () {
                Console.WriteLine ("initialising round");
                for (int i = 1; i <= ServerHandle.playersInGame; i++) {
                    Server.clients[i].player.position = new Vector3 (0, 0, 0);
                    Server.clients[i].player.removed = false;
                    Server.clients[i].player.resetHp ();
                    deadPlayers = 0;
                    ServerSend.Instance.spawnObject (Server.clients[i].player);
                    init = false;
                    Wait (DateTime.Now, 2000);
                }
                init = false;

            }
            private void Wait (DateTime time, int wait) {
                TimeSpan TimeElapsed = DateTime.Now - time;
                while (TimeElapsed.TotalMilliseconds < wait) {
                    TimeElapsed = DateTime.Now - time;
                }
            }
            #endregion

        }
    }