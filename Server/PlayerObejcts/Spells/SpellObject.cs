using System;
using System.Numerics;
namespace GameServer {
    public abstract class SpellObject : SpellInterface, updatable {
        private int MyRank;
        public int rank { get { return MyRank; } set { MyRank = value; } }

        private int MyOwner;
        public int owner { get { return MyOwner; } set { MyOwner = value; } }
        private SpellType MySpellType;
        public SpellType spellType { get { return MySpellType; } set { MySpellType = value; } }

        public int type { get { return 2; } }

        private Vector3 MyTarget;
        public Vector3 target { get { return MyTarget; } set { MyTarget = value; } }

        private int MyId;
        public int id { get { return MyId; } set { MyId = value; } }

        private Vector3 MyPosition;
        public Vector3 position { get { return MyPosition; } set { MyPosition = value; } }

        private Vector3 MyOfset = new Vector3 (0, 1, 0);
        public Vector3 offset { get { return MyOfset; } set { MyOfset = value; } }

        private float MyOffsetScalar = 0f;
        public float OffsetScalar { get { return MyOffsetScalar; } set { MyOffsetScalar = value; } }

        private Quaternion MyRotation;
        public Quaternion rotation { get { return MyRotation; } set { MyRotation = value; } }

        private float MyCastTime;
        public float CastTime { get { return MyCastTime; } set { MyCastTime = value; } }

        private float MyCooldown;
        public float Cooldown { get { return MyCooldown; } set { MyCooldown = value; } }

        private DateTime MyLastCast;
        public DateTime LastCast { get { return MyLastCast; } set { MyLastCast = value; } }

        private DateTime MySpawnTime;
        public DateTime SpawnTime { get { return MySpawnTime; } set { MySpawnTime = value; } }

        private String MyAnimation = "Cast1";
        public String Animation { get { return MyAnimation; } set { MyAnimation = value; } }

        public SpellObject (int _owner) {
            owner = _owner;
            rank = 1;
        }
        public SpellObject toObject () {
            SpellObject clone = (SpellObject) this.MemberwiseClone ();
            clone.owner = owner;
            clone.rank = rank;
            clone.spellType = spellType;
            clone.OffsetScalar = OffsetScalar;
            try {
                clone.rotation = Server.clients[owner].player.rotation;
                clone.position = Server.clients[owner].player.position + this.offset;
            }catch{
                clone.rotation = new Quaternion();
                clone.position = new Vector3();
            }
            clone.position += clone.forward () * OffsetScalar;
            clone.SpawnTime = DateTime.Now;
            return clone;
        }
        public Vector3 forward () {
            //forward vector:
            float x = 2 * (rotation.X * rotation.Z + rotation.W * rotation.Y);
            float z = 1 - 2 * (rotation.X * rotation.X + rotation.Y * rotation.Y);
            return new Vector3 (x, 0, z);
        }
        public Vector3 normalize (Vector3 vector) {
            Vector3 o = vector / vector.Length ();
            return o;
        }
        public abstract void update ();
    }
}