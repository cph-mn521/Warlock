using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer {

    public enum SpellType {
        Fireball = 1,
        Ligthning,
        Teleport
    }

    public class Spell : updatable {
        #region Interfaces
        private int MyId;
        public int id {
            get { return MyId; }
            set { MyId = value; }
        }

        private Vector3 MyPosition;
        public Vector3 position {
            get { return MyPosition; }
            set { MyPosition = value; }
        }
        private Quaternion MyRotation;
        public Quaternion rotation {
            get { return MyRotation; }
            set { MyRotation = value; }
        }
        public int type {
            get { return 2; }
        }
        #endregion

        public static List<Spell> AllSpells = new List<Spell> ();

        public static int spellCount = 1;
        public delegate void SpellBehavior (Spell _spell);

        //TODO:: Initialize!
        public static Dictionary<int, SpellBehavior> SpellHandlers;

        public int rank, ownerId;

        public Vector3 target;

        public SpellType spellType;

        public DateTime spawnTime;

        public Spell (int _id, int _rank, int _ownerId, SpellType _type, Vector3 _position, Quaternion _rotation) {
            id = _id;
            rank = _rank;
            ownerId = _ownerId;
            spellType = _type;
            position = _position;
            MyPosition.Y = 1;
            rotation = _rotation;
            spawnTime = DateTime.Now;
        }
        public Spell (int _id, int _rank, int _ownerId, SpellType _type, Vector3 _position, Quaternion _rotation, Vector3 _target) {
            id = _id;
            rank = _rank;
            ownerId = _ownerId;
            spellType = _type;
            position = _position;
            MyPosition.Y = 1;
            rotation = _rotation;
            target = _target;
            spawnTime = DateTime.Now;
        }

        public void update () {
            SpellHandlers[(int) this.spellType] (this);
        }

        private static Vector3 forward (Quaternion _rotation) {
            //forward vector:
            float x = 2 * (_rotation.X * _rotation.Z + _rotation.W * _rotation.Y);
            float z = 1 - 2 * (_rotation.X * _rotation.X + _rotation.Y * _rotation.Y);
            return new Vector3 (x, 0, z);
        }
        private static Vector3 noramlize (Vector3 vector) {
            Vector3 o = vector / vector.Length ();
            return o;
        }

        public static void InitializeSpells () {
            SpellHandlers = new Dictionary<int, SpellBehavior> () {
                {
                (int) SpellType.Fireball, Fireball
                }, {
                (int) SpellType.Teleport,
                Teleport
                }
            };
        }

        #region SpellBehavior
        public static void Fireball (Spell _spell) {
            float FireballSpeed = 1;
            bool cleanupFlag = false;
            Vector3 _forward = forward (_spell.rotation);

            _spell.position += _forward * FireballSpeed;

            for (int i = 1; i <= ServerHandle.playersInGame; i++) {
                try {
                    Vector3 playerPos = Server.clients[i].player.position;
                    Player _player = Server.clients[i].player;
                    //TODO fix diz bug nul pointer.
                    if (playerPos != null) {
                        Vector3 distance = playerPos - _spell.position;
                        distance.Y = 0;
                        if (distance.Length () <= 2f && _spell.ownerId != Server.clients[i].player.id) {
                            Vector3 normDistance = noramlize (distance);
                            _player.addVelocity (normDistance * (.8f + 0.2f * _spell.rank));
                            _player.dmg (5);
                            cleanupFlag = true;
                        }
                    }

                } catch (System.Exception ex) {

                    Console.WriteLine ($"Caught exception in spell behavior, Exception msg: {ex.Message}");
                }

            }
            TimeSpan tmElapsed = DateTime.Now - _spell.spawnTime;
            if (tmElapsed.TotalMilliseconds > 4000 || cleanupFlag) {
                Server.cleanUp.Add (_spell);
            } else {
                ServerSend.Instance.updateObject (_spell);
            }
        }
        public static void Ligthning (Spell _spell) {

        }
        public static void Teleport (Spell _spell) {
            Player _player = Server.clients[_spell.ownerId].player;
            Vector3 distance = _spell.target - _player.position;
            int distancePrRank = 8;
            if (distance.Length () > distancePrRank * _spell.rank) {
                distance = distance * (distancePrRank * _spell.rank / distance.Length ());
            }
            Console.WriteLine (_spell.target);
            distance.Y = 0;
            _player.position = _player.position + distance;
            Server.cleanUp.Add (_spell);

        }
        #endregion

    }
}