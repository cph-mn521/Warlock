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

    public class Spell {
        public static List<Spell> AllSpells = new List<Spell> ();

        public static List<Spell> spellsToRemove = new List<Spell> ();

        public static int spellCount = 1;
        public delegate void SpellBehavior (Spell _spell);

        //TODO:: Initialize!
        public static Dictionary<int, SpellBehavior> SpellHandlers;

        public int id, rank, ownerId;
        public Vector3 position;
        public Quaternion rotation;
        public SpellType type;
        public Vector3 target;

        public DateTime spawnTime;

        public Spell (int _id, int _rank, int _ownerId, SpellType _type, Vector3 _position, Quaternion _rotation) {
            id = _id;
            rank = _rank;
            ownerId = _ownerId;
            type = _type;
            position = _position;
            position.Y = 1;
            rotation = _rotation;
            spawnTime = DateTime.Now;
        }
        public Spell (int _id, int _rank, int _ownerId, SpellType _type, Vector3 _position, Quaternion _rotation, Vector3 _target) {
            id = _id;
            rank = _rank;
            ownerId = _ownerId;
            type = _type;
            position = _position;
            position.Y = 1;
            rotation = _rotation;
            target = _target;
            spawnTime = DateTime.Now;
        }

        public void update () {
            SpellHandlers[(int) this.type] (this);
        }

        private static Vector3 forward (Quaternion _rotation) {
            //forward vector:
            float x = 2 * (_rotation.X * _rotation.Z + _rotation.W * _rotation.Y);
            float z = 1 - 2 * (_rotation.X * _rotation.X + _rotation.Y * _rotation.Y);
            return new Vector3 (x, 0, z);
        }

        public static void InitializeSpells () {
            SpellHandlers = new Dictionary<int, SpellBehavior> () {
                {
                (int) SpellType.Fireball, FireballBehavior
                }
            };
        }

        #region SpellBehavior
        public static void FireballBehavior (Spell _spell) {
            float FireballSpeed = 1;
            bool cleanupFlag = true;
            Vector3 _forward = forward (_spell.rotation);
            _spell.position += _forward * FireballSpeed;
            ServerSend.SpellUpdate (_spell);
            TimeSpan tmElapsed = DateTime.Now - _spell.spawnTime;
            List<Client> items = new List<Client> ();
            items.AddRange (Server.clients.Values);
            for (int i = 1; i <= ServerHandle.playersInGame; i++) {
                try {
                    Vector3 playerPos = Server.clients[i].player.position;
                    //TODO fix diz bug nul pointer.
                    if (playerPos != null) {
                        Vector3 distance = playerPos - _spell.position;
                        distance.Y = 0;
                        if (distance.Length () <= 2f && _spell.ownerId != Server.clients[i].player.id) {
                            Console.WriteLine(distance * (1f + 0.2f * _spell.rank));
                            Server.clients[i].player.addVelocity (distance * (1f + 0.2f * _spell.rank));
                             spellsToRemove.Add (_spell);
                             cleanupFlag = false;
                        }
                    }

                } catch (System.Exception ex) {

                    Console.WriteLine ($"Caught exception in spell behavior, Exception msg: {ex.Message}");
                }

            }
            if (tmElapsed.TotalMilliseconds > 4000 && cleanupFlag) {
                spellsToRemove.Add (_spell);

            }
        }
        public static void FireballLigthning (Spell _spell) {

        }
        public static void FireballTeleport (Spell _spell) {

        }
        #endregion

    }
}